using NUnit.Framework;

namespace Atomic.Contexts
{
    [TestFixture]
    public sealed class ContextTests
    {
        private Context context;

        [SetUp]
        public void SetUp()
        {
            this.context = new Context("TestContext");
        }

        #region Lifecycle

        [Test]
        public void Initialize()
        {
            //Arrange
            var wasEvent = new Reference<bool>();
            var initSystem = new InitSystemStub();

            this.context.AddSystem(initSystem);
            this.context.OnStateChanged += state =>
            {
                if (state == ContextState.INITIALIZED) wasEvent.value = true;
            };

            //Act
            this.context.Initialize();

            //Assert
            Assert.IsTrue(this.context.IsIntialized());
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(initSystem.initialized);
        }

        [Test]
        public void Enable()
        {
            //Arrange
            var wasEvent = new Reference<bool>();
            var systemStub = new CommonSystemStub();

            this.context.AddSystem(systemStub);
            this.context.OnStateChanged += state =>
            {
                if (state == ContextState.ENABLED) wasEvent.value = true;
            };

            //Act
            this.context.Initialize();
            this.context.Enable();

            //Assert
            Assert.IsTrue(this.context.IsEnabled());
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(systemStub.initialized);
            Assert.IsTrue(systemStub.enabled);
        }

        [Test]
        public void Disable()
        {
            //Arrange
            var wasEvent = new Reference<bool>();
            var disableSystem = new DisableSystemStub();

            this.context.AddSystem(disableSystem);
            this.context.OnStateChanged += state =>
            {
                if (state == ContextState.DISABLED) wasEvent.value = true;
            };

            //Act
            this.context.Initialize();
            this.context.Enable();
            this.context.Disable();

            //Assert
            Assert.IsTrue(disableSystem.disabled);
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(this.context.IsDisabled());
        }


        [Test]
        public void Destroy()
        {
            //Arrange
            var wasEvent = new Reference<bool>();
            var systemStub = new DestroySystemStub();

            this.context.AddSystem(systemStub);
            this.context.OnStateChanged += state =>
            {
                if (state == ContextState.DISABLED) wasEvent.value = true;
            };

            //Act
            this.context.Initialize();
            this.context.Enable();
            this.context.Disable();
            this.context.Destroy();

            //Assert
            Assert.IsTrue(systemStub.destroyed);
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(this.context.IsDestroyed());
        }

        [Test]
        public void Update()
        {
            //Arrange
            var updateSystem = new UpdateSystemStub();
            this.context.AddSystem(updateSystem);

            //Act
            this.context.Initialize();
            this.context.Enable();
            
            this.context.Update(deltaTime: 0);
            this.context.FixedUpdate(deltaTime: 0);
            this.context.LateUpdate(deltaTime: 0);

            //Assert
            Assert.IsTrue(updateSystem.updated);
            Assert.IsTrue(updateSystem.fixedUpdated);
            Assert.IsTrue(updateSystem.lateUpdated);
        }

        [Test]
        public void WhenEnableContextFromOffStateThenFailed()
        {
            //Arrange
            var wasEvent = new Reference<bool>();
            var enableSystem = new EnableSystemStub();

            this.context.AddSystem(enableSystem);
            this.context.OnStateChanged += state =>
            {
                if (state == ContextState.ENABLED) wasEvent.value = true;
            };

            //Act
            this.context.Enable();

            //Assert
            Assert.IsTrue(this.context.IsOff());
            Assert.IsFalse(this.context.IsEnabled());
            Assert.IsFalse(wasEvent.value);
            Assert.IsFalse(enableSystem.enabled);
        }
        
        [Test]
        public void WhenUpdateContextFromOffStateThenFailed()
        {
            //Arrange
            var updateSystem = new UpdateSystemStub();
            this.context.AddSystem(updateSystem);

            //Act
            this.context.Update(deltaTime: 0);
            this.context.FixedUpdate(deltaTime: 0);
            this.context.LateUpdate(deltaTime: 0);

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
            var wasEvent = new Reference<(int, object)>();
            this.context.OnDataAdded += (key, value) => wasEvent.value = (key, value);
            
            //Act:
            bool success = this.context.AddData(1, "Apple");

            //Assert: 
            Assert.IsTrue(success);
            Assert.IsTrue(this.context.HasData(1));
            Assert.AreEqual(1, wasEvent.value.Item1);
            Assert.AreEqual("Apple", wasEvent.value.Item2);
        }

        [Test]
        public void GetData()
        {
            //Arrange:
            this.context.AddData(1, "Apple");
            
            //Act:
            string data = this.context.GetData<string>(1);

            //Assert: 
            Assert.AreEqual("Apple", data);
        }

        [Test]
        public void GetNullData()
        {
            //Act
            string data = this.context.GetData<string>(1);
            
            //Assert
            Assert.IsNull(data);
        }

        [Test]
        public void WhenAddDataThatAlreadyExistsThenFailed()
        {
            //Arrange:
            this.context.AddData(1, "Apple");
            
            var wasEvent = new Reference<bool>();
            this.context.OnDataAdded += (_, _) => wasEvent.value = true;
            
            //Act:
            bool success = this.context.AddData(1, "Apple");

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
            var wasEvent = new Reference<ISystem>();
            var systemStub = new SystemStub();
            this.context.OnSystemAdded += system => wasEvent.value = system;
            
            //Act:
            bool success = this.context.AddSystem(systemStub);

            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(this.context.HasSystem(systemStub));
            Assert.IsTrue(this.context.HasSystem<SystemStub>());

            Assert.AreEqual(systemStub, wasEvent.value);
            Assert.AreEqual(systemStub, this.context.GetSystem<SystemStub>());
        }

        [Test]
        public void RemoveSystem()
        {
            //Arrange:
            var wasEvent = new Reference<ISystem>();
            var systemStub = new SystemStub();
            this.context.AddSystem(systemStub);
            this.context.OnSystemRemoved += system => wasEvent.value = system;
            
            //Act:
            bool removed = this.context.DelSystem<SystemStub>();
            
            //Assert:
            Assert.IsTrue(removed);
            Assert.IsFalse(this.context.HasSystem<SystemStub>());
            Assert.AreEqual(systemStub, wasEvent.value);
        }
        
        [Test]
        public void WhenAddAndRemoveSystemOnEnableContextThenSystemWillListenCallbacks()
        {
            //Arrange:
            this.context.Initialize();
            this.context.Enable();

            //Act:
            var systemStub = new CommonSystemStub();
            this.context.AddSystem(systemStub);
            
            this.context.Update(deltaTime: 0);
            this.context.FixedUpdate(deltaTime: 0);
            this.context.LateUpdate(deltaTime: 0);

            this.context.DelSystem<CommonSystemStub>();
            
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
    }
}