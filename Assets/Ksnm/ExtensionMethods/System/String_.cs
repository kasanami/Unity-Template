/*
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
 
    3. This notice may not be removed or altered from any source
    distribution.
*/
using System.Collections.Generic;

#if Ksnm_Using_UniLinq
using UniLinq;
#else
using System.Linq;
#endif

namespace Ksnm.ExtensionMethods.System
{
    public static class String_
    {
        /// <summary>
        /// 半角を全角に変換する辞書
        /// </summary>
        static Dictionary<char, char> ToWideDictionary = new Dictionary<char, char>();
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static String_()
        {
            // 半角を全角に変換する辞書
            #region ToWideDictionary
            ToWideDictionary[' '] = '　';
            ToWideDictionary['!'] = '！';
            ToWideDictionary['"'] = '”';
            ToWideDictionary['#'] = '＃';
            ToWideDictionary['$'] = '＄';
            ToWideDictionary['%'] = '％';
            ToWideDictionary['&'] = '＆';
            ToWideDictionary['\''] = '’';
            ToWideDictionary['('] = '（';
            ToWideDictionary[')'] = '）';
            ToWideDictionary['*'] = '＊';
            ToWideDictionary['+'] = '＋';
            ToWideDictionary[','] = '，';
            ToWideDictionary['-'] = '－';
            ToWideDictionary['.'] = '．';
            ToWideDictionary['/'] = '／';
            for (int i = '0'; i <= '9'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('０' + (i - '0'));
            }
            ToWideDictionary[':'] = '：';
            ToWideDictionary[';'] = '；';
            ToWideDictionary['<'] = '＜';
            ToWideDictionary['='] = '＝';
            ToWideDictionary['>'] = '＞';
            ToWideDictionary['?'] = '？';
            ToWideDictionary['@'] = '＠';
            for (int i = 'A'; i <= 'Z'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('Ａ' + (i - 'A'));
            }
            ToWideDictionary['['] = '［';
            ToWideDictionary['\\'] = '￥';
            ToWideDictionary[']'] = '］';
            ToWideDictionary['^'] = '＾';
            ToWideDictionary['_'] = '＿';
            ToWideDictionary['`'] = '｀';
            for (int i = 'a'; i <= 'z'; i++)
            {
                ToWideDictionary[(char)(i)] = (char)('ａ' + (i - 'a'));
            }
            ToWideDictionary['{'] = '｛';
            ToWideDictionary['|'] = '｜';
            ToWideDictionary['}'] = '｝';
            ToWideDictionary['~'] = '\uFF5E';// 全角チルダ
            #endregion ToWideDictionary
        }
        /// <summary>
        /// カタカナに変換します。
        /// </summary>
        public static string HiraganaToKatakana(this string s)
        {
            return new string(s.Select(c => (c >= 'ぁ' && c <= 'ゖ') ? (char)(c + 'ァ' - 'ぁ') : c).ToArray());
        }
        /// <summary>
        /// ひらがなに変換します。
        /// </summary>
        public static string KatakanaToHiragana(this string s)
        {
            return new string(s.Select(c => (c >= 'ァ' && c <= 'ヶ') ? (char)(c + 'ぁ' - 'ァ') : c).ToArray());
        }
        /// <summary>
        /// 半角を全角に変換
        /// </summary>
        public static string ToWide(this string s)
        {
            return new string(s.Select(c => ToWideDictionary.ContainsKey(c) ? ToWideDictionary[c] : c).ToArray());
        }
        /// <summary>
        /// C#の識別子名に使用できる文字に変換
        /// </summary>
        public static string ToSafeCSharpIdentifier(this string s)
        {
            return new string(
                s.Select(
                c => char.IsUpper(c) == false &&
                    char.IsLetterOrDigit(c) == false &&
                    c != '_' ? '_' : c).ToArray());
        }
    }
}