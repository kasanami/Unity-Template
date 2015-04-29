using System.Collections.Generic;
using System.Linq;

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