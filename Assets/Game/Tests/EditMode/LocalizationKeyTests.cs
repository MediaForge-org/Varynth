using NUnit.Framework;
using Varynth.Core.Common;
using Varynth.Core.Definitions;

namespace Varynth.Tests.EditMode
{
    public class LocalizationKeyTests
    {
        [TestCase("quest.story.occidentia.001.title")]
        [TestCase("ui.population.title")]
        [TestCase("good.coffee.name")]
        [TestCase("dialogue.story.occidentia.001.open.helena.001")]
        public void Parse_ValidKey_Succeeds(string raw)
        {
            var key = LocalizationKey.Parse(raw);
            Assert.AreEqual(raw, key.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("UI.Population.Title")]
        [TestCase("ui..title")]
        [TestCase(".ui.title")]
        [TestCase("ui.title.")]
        [TestCase("singleSegment")]
        public void Parse_InvalidKey_Throws(string raw)
        {
            Assert.Throws<ContentIdFormatException>(() => LocalizationKey.Parse(raw));
        }

        [Test]
        public void TryParse_ValidKey_ReturnsTrue()
        {
            var success = LocalizationKey.TryParse("ui.population.title", out var key);

            Assert.IsTrue(success);
            Assert.AreEqual("ui.population.title", key.ToString());
        }

        [Test]
        public void TryParse_InvalidKey_ReturnsFalseAndDefault()
        {
            var success = LocalizationKey.TryParse("Invalid Key", out var key);

            Assert.IsFalse(success);
            Assert.IsTrue(key.IsDefault);
        }

        [Test]
        public void Equality_SameValue_AreEqual()
        {
            var a = LocalizationKey.Parse("ui.population.title");
            var b = LocalizationKey.Parse("ui.population.title");

            Assert.AreEqual(a, b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValue_AreNotEqual()
        {
            var a = LocalizationKey.Parse("ui.population.title");
            var b = LocalizationKey.Parse("ui.population.subtitle");

            Assert.AreNotEqual(a, b);
        }
    }
}
