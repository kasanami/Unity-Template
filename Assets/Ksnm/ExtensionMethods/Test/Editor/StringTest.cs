/*
The zlib License

Copyright (c) 2014-2015 Takahiro Kasanami

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

3. This notice may not be removed or altered from any source distribution.
*/
#if Ksnm_EnableUnitTest
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
        [Test]
        [TestCase("abcd<efgh>ijkl<mnop>qrstuvwxyz")]
        public void SubString(string source)
        {
            var result = source.SubString("<", ">", false, false);
            Assert.AreEqual(result, "efgh");
            result = source.SubString("<", ">", true, false);
            Assert.AreEqual(result, "<efgh");
            result = source.SubString("<", ">", false, true);
            Assert.AreEqual(result, "efgh>");
            result = source.SubString("<", ">", true, true);
            Assert.AreEqual(result, "<efgh>");
        }
    }
}
#endif