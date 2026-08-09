using System;
using System.Collections.Generic;
using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Core.Registry;

namespace Varynth.Tests.EditMode
{
    internal sealed class TestDefinition : IContentDefinition
    {
        public ContentId Id { get; }
        public LocalizationKey NameKey { get; }

        public TestDefinition(string id, string nameKey)
        {
            Id = ContentId.Parse(id);
            NameKey = LocalizationKey.Parse(nameKey);
        }
    }

    public class ContentRegistryTests
    {
        [Test]
        public void Register_ThenTryGet_ReturnsSameInstance()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var definition = new TestDefinition("good.meridia.coffee", "good.coffee.name");

            registry.Register(definition);

            Assert.IsTrue(registry.TryGet(definition.Id, out var found));
            Assert.AreSame(definition, found);
        }

        [Test]
        public void Register_ThenGet_ReturnsSameInstance()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var definition = new TestDefinition("good.meridia.coffee", "good.coffee.name");

            registry.Register(definition);

            Assert.AreSame(definition, registry.Get(definition.Id));
        }

        [Test]
        public void TryGet_UnknownId_ReturnsFalse()
        {
            var registry = new ContentRegistry<TestDefinition>();

            var success = registry.TryGet(ContentId.Parse("good.meridia.coffee"), out var found);

            Assert.IsFalse(success);
            Assert.IsNull(found);
        }

        [Test]
        public void Get_UnknownId_Throws()
        {
            var registry = new ContentRegistry<TestDefinition>();

            Assert.Throws<KeyNotFoundException>(
                () => registry.Get(ContentId.Parse("good.meridia.coffee")));
        }

        [Test]
        public void Register_DuplicateId_ThrowsAndKeepsOriginalUntouched()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var original = new TestDefinition("good.meridia.coffee", "good.coffee.name");
            var duplicate = new TestDefinition("good.meridia.coffee", "good.coffee.duplicate.name");

            registry.Register(original);

            Assert.Throws<DuplicateContentIdException>(() => registry.Register(duplicate));

            Assert.IsTrue(registry.TryGet(original.Id, out var found));
            Assert.AreSame(original, found);
            Assert.AreEqual(1, registry.Count);
        }

        [Test]
        public void Register_Null_Throws()
        {
            var registry = new ContentRegistry<TestDefinition>();

            Assert.Throws<ArgumentNullException>(() => registry.Register(null));
        }

        [Test]
        public void Register_MultipleEntries_AllRetrievable()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var a = new TestDefinition("good.meridia.coffee", "good.coffee.name");
            var b = new TestDefinition("good.meridia.tea", "good.tea.name");
            var c = new TestDefinition("good.meridia.spice", "good.spice.name");

            registry.Register(a);
            registry.Register(b);
            registry.Register(c);

            Assert.AreEqual(3, registry.Count);
            Assert.IsTrue(registry.TryGet(a.Id, out _));
            Assert.IsTrue(registry.TryGet(b.Id, out _));
            Assert.IsTrue(registry.TryGet(c.Id, out _));
        }

        [Test]
        public void All_ReflectsRegisteredEntries_AsReadOnlyCollection()
        {
            var registry = new ContentRegistry<TestDefinition>();
            var definition = new TestDefinition("good.meridia.coffee", "good.coffee.name");
            registry.Register(definition);

            IReadOnlyCollection<TestDefinition> all = registry.All;

            Assert.AreEqual(1, all.Count);
            Assert.IsTrue(new List<TestDefinition>(all).Contains(definition));
        }
    }
}
