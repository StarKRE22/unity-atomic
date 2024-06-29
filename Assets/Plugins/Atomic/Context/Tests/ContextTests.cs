using NUnit.Framework;

namespace Atomic.Contexts
{
    [TestFixture]
    public sealed class ContextTests
    {

        #region Lifecycle

        [Test]
        public void Initialize()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var initSystem = new InitSystemStub();

            context.AddSystem(initSystem);
            context.OnStateChanged += state =>
            {
                if (state == ContextState.INITIALIZED) wasEvent.value = true;
            };

            //Act
            context.Initialize();

            //Assert
            Assert.IsTrue(context.IsIntialized());
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(initSystem.initialized);
        }

        [Test]
        public void Enable()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var systemStub = new CommonSystemStub();

            context.AddSystem(systemStub);
            context.OnStateChanged += state =>
            {
                if (state == ContextState.ENABLED) wasEvent.value = true;
            };

            //Act
            context.Initialize();
            context.Enable();

            //Assert
            Assert.IsTrue(context.IsEnabled());
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(systemStub.initialized);
            Assert.IsTrue(systemStub.enabled);
        }

        [Test]
        public void Disable()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var disableSystem = new DisableSystemStub();

            context.AddSystem(disableSystem);
            context.OnStateChanged += state =>
            {
                if (state == ContextState.DISABLED) wasEvent.value = true;
            };

            //Act
            context.Initialize();
            context.Enable();
            context.Disable();

            //Assert
            Assert.IsTrue(disableSystem.disabled);
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(context.IsDisabled());
        }


        [Test]
        public void Destroy()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var systemStub = new DestroySystemStub();

            context.AddSystem(systemStub);
            context.OnStateChanged += state =>
            {
                if (state == ContextState.DISABLED) wasEvent.value = true;
            };

            //Act
            context.Initialize();
            context.Enable();
            context.Disable();
            context.Destroy();

            //Assert
            Assert.IsTrue(systemStub.destroyed);
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(context.IsDestroyed());
        }

        [Test]
        public void Update()
        {
            //Arrange
            var context = new Context();
            var updateSystem = new UpdateSystemStub();
            context.AddSystem(updateSystem);

            //Act
            context.Initialize();
            context.Enable();

            context.Update(deltaTime: 0);
            context.FixedUpdate(deltaTime: 0);
            context.LateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(updateSystem.updated);
            Assert.IsTrue(updateSystem.fixedUpdated);
            Assert.IsTrue(updateSystem.lateUpdated);
        }

        [Test]
        public void WhenEnableContextFromOffStateThenFailed()
        {
            //Arrange
            var context = new Context();
            var wasEvent = new Reference<bool>();
            var enableSystem = new EnableSystemStub();

            context.AddSystem(enableSystem);
            context.OnStateChanged += state =>
            {
                if (state == ContextState.ENABLED) wasEvent.value = true;
            };

            //Act
            context.Enable();

            //Assert
            Assert.IsTrue(context.IsOff());
            Assert.IsFalse(context.IsEnabled());
            Assert.IsFalse(wasEvent.value);
            Assert.IsFalse(enableSystem.enabled);
        }

        [Test]
        public void WhenUpdateContextFromOffStateThenFailed()
        {
            //Arrange
            var context = new Context();
            var updateSystem = new UpdateSystemStub();
            context.AddSystem(updateSystem);

            //Act
            context.Update(deltaTime: 0);
            context.FixedUpdate(deltaTime: 0);
            context.LateUpdate(deltaTime: 0);

            //Assert
            Assert.IsFalse(updateSystem.updated);
            Assert.IsFalse(updateSystem.fixedUpdated);
            Assert.IsFalse(updateSystem.lateUpdated);
        }

        #endregion

        #region Data

        [Test]
        public void AddData()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<(int, object)>();
            context.OnDataAdded += (key, value) => wasEvent.value = (key, value);

            //Act:
            bool success = context.AddData(1, "Apple");

            //Assert: 
            Assert.IsTrue(success);
            Assert.IsTrue(context.HasData(1));
            Assert.AreEqual(1, wasEvent.value.Item1);
            Assert.AreEqual("Apple", wasEvent.value.Item2);
        }

        [Test]
        public void GetData()
        {
            //Arrange:
            var context = new Context();
            context.AddData(1, "Apple");

            //Act:
            string data = context.GetData<string>(1);

            //Assert: 
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void GetNullData()
        {
            //Arrange:
            var context = new Context();
            
            //Act
            string data = context.GetData<string>(1);

            //Assert
            Assert.IsNull(data);
        }

        [Test]
        public void WhenAddDataThatAlreadyExistsThenFailed()
        {
            //Arrange:
            var context = new Context();
            context.AddData(1, "Apple");

            var wasEvent = new Reference<bool>();
            context.OnDataAdded += (_, _) => wasEvent.value = true;

            //Act:
            bool success = context.AddData(1, "Apple");

            //Assert: 
            Assert.IsFalse(success);
            Assert.IsFalse(wasEvent.value);
        }

        #endregion

        #region Systems

        [Test]
        public void AddSystem()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<ISystem>();
            var systemStub = new SystemStub();
            context.OnSystemAdded += system => wasEvent.value = system;

            //Act:
            bool success = context.AddSystem(systemStub);

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(context.HasSystem(systemStub));
            Assert.IsTrue(context.HasSystem<SystemStub>());

            Assert.AreEqual(systemStub, wasEvent.value);
            Assert.AreEqual(systemStub, context.GetSystem<SystemStub>());
        }

        [Test]
        public void RemoveSystem()
        {
            //Arrange:
            var context = new Context();
            var wasEvent = new Reference<ISystem>();
            var systemStub = new SystemStub();
            context.AddSystem(systemStub);
            context.OnSystemRemoved += system => wasEvent.value = system;

            //Act:
            bool removed = context.DelSystem<SystemStub>();

            //Assert:
            Assert.IsTrue(removed);
            Assert.IsFalse(context.HasSystem<SystemStub>());
            Assert.AreEqual(systemStub, wasEvent.value);
        }

        [Test]
        public void WhenAddAndRemoveSystemOnEnableContextThenSystemWillListenCallbacks()
        {
            //Arrange:
            var context = new Context();
            context.Initialize();
            context.Enable();

            //Act:
            var systemStub = new CommonSystemStub();
            context.AddSystem(systemStub);

            context.Update(deltaTime: 0);
            context.FixedUpdate(deltaTime: 0);
            context.LateUpdate(deltaTime: 0);

            context.DelSystem<CommonSystemStub>();

            //Assert:
            Assert.IsTrue(systemStub.initialized);
            Assert.IsTrue(systemStub.enabled);
            Assert.IsTrue(systemStub.updated);
            Assert.IsTrue(systemStub.fixedUpdated);
            Assert.IsTrue(systemStub.lateUpdated);
            Assert.IsTrue(systemStub.disabled);
            Assert.IsTrue(systemStub.destroyed);
        }

        #endregion

        #region Parent

        [Test]
        public void SetParent()
        {
            //Arrange:
            var parent = new Context();
            var child = new Context();

            //Act:
            child.Parent = parent;

            //Assert:
            Assert.IsTrue(child.IsParent(parent));
            Assert.IsTrue(parent.IsChild(child));
        }

        [Test]
        public void ChangeParent()
        {
            //Arrange:
            var parent1 = new Context();
            var parent2 = new Context();
            var child = new Context(null, parent1);

            //Verify:
            Assert.IsTrue(child.IsParent(parent1));
            Assert.IsFalse(child.IsParent(parent2));
            Assert.IsTrue(parent1.IsChild(child));
            Assert.IsFalse(parent2.IsChild(child));

            //Act:
            child.Parent = parent2;

            //Assert:
            Assert.IsTrue(child.IsParent(parent2));
            Assert.IsTrue(parent2.IsChild(child));
            Assert.IsFalse(child.IsParent(parent1));
            Assert.IsFalse(parent1.IsChild(child));
        }


        //
        // [Test]
        // public void InitializeChildren()
        // {
        //     
        //     
        //     
        //     // //Arrange:
        //     // var parent1 = new Context();
        //     // var parent2 = new Context();
        //     // var child = new Context();
        //     //
        //     // //Act:
        //     // child.Parent = parent;
        //     //
        //     // //Assert:
        //     // Assert.IsTrue(child.IsParent(parent));
        //     // Assert.IsTrue(parent.IsChild(child));
        // }

        #endregion

        #region Extensions

        [Test]
        public void GetDataInParent()
        {
            //Arrange:
            var parent = new Context();
            parent.AddData(1, "Apple");
            
            var child = new Context(null, parent);
            
            //Act:
            string data = child.GetDataInParent<string>(1);
            
            //Assert:
            Assert.IsFalse(child.HasData(1));
            Assert.AreEqual("Apple", data);
        }
        
        [Test]
        public void GetDataInParentSelf()
        {
            //Arrange:
            var context = new Context();
            context.AddData(1, "Apple");

            //Act:
            string data = context.GetDataInParent<string>(1);
            
            //Assert:
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void GetDataInChildren()
        {
            //Arrange:
            var parent = new Context();
            
            var child = new Context(null, parent);
            child.AddData(1, "Apple");

            //Act:
            string data = parent.GetDataInChildren<string>(1);
            
            //Assert:
            Assert.IsFalse(child.HasData(1));
            Assert.AreEqual("Apple", data);
        }

        #endregion
    }
}