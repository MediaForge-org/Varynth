using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;
using Varynth.Data.Mods;
using Varynth.Data.Sources;

namespace Varynth.Tests.EditMode.Data
{
    public class ContentSourceTests
    {
        [Test]
        public void Construct_CoreSource_HasExpectedDefaults()
        {
            var source = new ContentSource(ContentSourceId.Parse("core"), ContentSourceType.Core, "/content/core");

            Assert.AreEqual(ContentSourceType.Core, source.Type);
            Assert.AreEqual("/content/core", source.RootPath);
            Assert.AreEqual(0, source.Priority);
            Assert.AreEqual(0, source.RequiredDependencies.Count);
            Assert.AreEqual(0, source.OptionalDependencies.Count);
            Assert.AreEqual(0, source.LoadAfter.Count);
        }

        [Test]
        public void FromModManifest_BuildsModSource_WithSplitDependencies()
        {
            var manifest = new ModManifest(
                ContentSourceId.Parse("author.modname"),
                "1.0.0",
                LocalizationKey.Parse("mod.author.modname.name"),
                new[]
                {
                    new ModDependency(ContentSourceId.Parse("someother.mod"), optional: false),
                    new ModDependency(ContentSourceId.Parse("anothermod"), optional: true)
                },
                new[] { ContentSourceId.Parse("core") });

            var source = ContentSource.FromModManifest(manifest, "/mods/author.modname", priority: 5);

            Assert.AreEqual(ContentSourceType.Mod, source.Type);
            Assert.AreEqual(manifest.Id, source.Id);
            Assert.AreEqual(5, source.Priority);
            Assert.AreEqual(1, source.RequiredDependencies.Count);
            Assert.AreEqual(ContentSourceId.Parse("someother.mod"), source.RequiredDependencies[0]);
            Assert.AreEqual(1, source.OptionalDependencies.Count);
            Assert.AreEqual(ContentSourceId.Parse("anothermod"), source.OptionalDependencies[0]);
            Assert.AreEqual(1, source.LoadAfter.Count);
            Assert.AreEqual(ContentSourceId.Parse("core"), source.LoadAfter[0]);
        }
    }
}
