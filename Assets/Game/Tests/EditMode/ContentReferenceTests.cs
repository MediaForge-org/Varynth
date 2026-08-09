using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Registry;

namespace Varynth.Tests.EditMode
{
    public class ContentReferenceTests
    {
        [Test]
        public void TryResolve_ValidReference_ResolvesToRegisteredDefinition()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var definition = new TestDefinition("good.meridia.coffee", "good.coffee.name");
            registry.Register(definition);

            var reference = ContentReference<TestDefinition>.To(definition.Id);

            Assert.IsTrue(reference.TryResolve(registry, out var resolved));
            Assert.AreSame(definition, resolved);
        }

        [Test]
        public void TryResolve_MissingId_FailsToResolve()
        {
            var registry = new ContentRegistry<TestDefinition>();

            var reference = ContentReference<TestDefinition>.To(ContentId.Parse("good.meridia.coffee"));

            Assert.IsFalse(reference.TryResolve(registry, out var resolved));
            Assert.IsNull(resolved);
        }

        [Test]
        public void TryResolve_WrongDefinitionType_FailsToResolve()
        {
            var teaRegistry = new ContentRegistry<TestDefinition>();
            teaRegistry.Register(new TestDefinition("good.meridia.coffee", "good.coffee.name"));

            var otherRegistry = new ContentRegistry<OtherTestDefinition>();
            otherRegistry.Register(new OtherTestDefinition("good.meridia.tea", "good.tea.name"));

            // A reference typed for OtherTestDefinition pointing at an id that only
            // exists in the TestDefinition registry must not resolve.
            var reference = ContentReference<OtherTestDefinition>.To(ContentId.Parse("good.meridia.coffee"));

            Assert.IsFalse(reference.TryResolve(otherRegistry, out var resolved));
            Assert.IsNull(resolved);
        }
    }
}
