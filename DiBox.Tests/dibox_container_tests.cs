using DiBox.Core;
using DiBox.Core.Exceptions;
using NUnit.Framework;

namespace DiBox.Tests
{
    [TestFixture]
    public class dibox_container_tests
    {
        private DiBoxContainer _container;

        [SetUp]
        public void Setup()
        {
            _container = new DiBoxContainer();
        }

        ITestInterface Act()
        {
            return _container.Resolve<ITestInterface>();
        }

        [Test]
        public void resolves_registered_components()
        {
            _container.Register<ITestInterface, TestImplementation>();

            var result = Act();

            Assert.That(result, Is.TypeOf<TestImplementation>());
        }

        [Test]
        public void resolves_components_via_constructor_dependency()
        {
            _container.Register<Dependency, Dependency>();
            _container.Register<ITestInterface, TestImplementationWithDependency>();

            var result = Act();

            Assert.That(result, Is.TypeOf<TestImplementationWithDependency>());
        }

        [Test]
        public void throws_exception_when_components_has_many_ctors()
        {
            _container.Register<Dependency, Dependency>();
            _container.Register<ITestInterface, TestImplementationWithDependencyAndMultipleCtors>();

            Assert.Throws<ComponentHasMultiplyConstructorsException>(() => Act());
        }
    }

    interface ITestInterface
    {
    }

    class TestImplementation : ITestInterface
    {
    }

    class Dependency
    {
    }

    class TestImplementationWithDependency : ITestInterface
    {
        public TestImplementationWithDependency(Dependency dependency)
        {
        }
    }

    class TestImplementationWithDependencyAndMultipleCtors : ITestInterface
    {

        public TestImplementationWithDependencyAndMultipleCtors()
        {
        }

        public TestImplementationWithDependencyAndMultipleCtors(Dependency dependency)
        {
        }
    }
}
