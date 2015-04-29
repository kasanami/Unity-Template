using NUnit.Framework;

namespace Ksnm.ExtensionMethods.System
{
    [TestFixture]
    public class StringTest
    {
        [Test]
        [TestCase("", "")]
        [TestCase("あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわゐゑをん", "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヰヱヲン")]
        [TestCase("123", "123")]
        [TestCase("abc", "abc")]
        public void HiraganaToKatakana(string hiragana, string katakana)
        {
            var result = hiragana.HiraganaToKatakana();
            Assert.AreEqual(result, katakana);
            // 逆もチェック
            result = katakana.KatakanaToHiragana();
            Assert.AreEqual(result, hiragana);
        }
        [Test]
        [TestCase("0123456789", "０１２３４５６７８９")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ")]
        [TestCase("abcdefghijklmnopqrstuvwxyz", "ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ")]
        [TestCase(" !\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~", "　！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝～")]
        public void ToWide(string source, string answer)
        {
            var result = source.ToWide();
            Assert.AreEqual(result, answer);
        }
    }
}
