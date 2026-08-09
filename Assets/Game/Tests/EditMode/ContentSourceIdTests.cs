using NUnit.Framework;
using Varynth.Core.Common;

namespace Varynth.Tests.EditMode
{
    public class ContentSourceIdTests
    {
        [TestCase("core")]
        [TestCase("test")]
        [TestCase("author.modname")]
        [TestCase("mygreatmod")]
        public void Parse_ValidId_Succeeds(string raw)
        {
            var id = ContentSourceId.Parse(raw);
            Assert.AreEqual(raw, id.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("Core")]
        [TestCase("author..modname")]
        [TestCase(".author")]
        [TestCase("author.")]
        [TestCase("author modname")]
        [TestCase("author#modname")]
        public void Parse_InvalidId_Throws(string raw)
        {
            Assert.Throws<ContentIdFormatException>(() => ContentSourceId.Parse(raw));
        }

        [Test]
        public void TryParse_ValidId_ReturnsTrue()
        {
            var success = ContentSourceId.TryParse("core", out var id);
            Assert.IsTrue(success);
            Assert.AreEqual("core", id.ToString());
        }

        [Test]
        public void TryParse_InvalidId_ReturnsFalseAndDefault()
        {
            var success = ContentSourceId.TryParse("Invalid Id", out var id);
            Assert.IsFalse(success);
            Assert.IsTrue(id.IsDefault);
        }

        [Test]
        public void Equality_SameValue_AreEqual()
        {
            var a = ContentSourceId.Parse("author.modname");
            var b = ContentSourceId.Parse("author.modname");
            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void Equality_DifferentValue_AreNotEqual()
        {
            var a = ContentSourceId.Parse("core");
            var b = ContentSourceId.Parse("test");
            Assert.AreNotEqual(a, b);
            Assert.IsTrue(a != b);
        }
    }
}
