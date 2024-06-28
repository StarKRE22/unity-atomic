using NUnit.Framework;

namespace Atomic.Contexts
{
    [TestFixture]
    public sealed class ContextLifecycleTests
    {
        private Context context;

        [SetUp]
        public void SetUp()
        {
            this.context = new Context("Test");
        }

        [Test]
        public void InitializeContext()
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
        public void EnableContext()
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
            this.context.Initialize();
            this.context.Enable();

            //Assert
            Assert.IsTrue(this.context.IsEnabled());
            Assert.IsTrue(wasEvent.value);
            Assert.IsTrue(enableSystem.enabled);
        }

        [Test]
        public void EnableContextFromOffFailed()
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
        public void DisableContext()
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
        public void DestroyContext()
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
        public void UpdateContext()
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
        public void UpdateContextFromOffFailed()
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
    }
}