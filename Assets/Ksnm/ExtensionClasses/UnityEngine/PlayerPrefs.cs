using System.Collections;
using System.Collections.Generic;

namespace Ksnm.ExtensionClasses.UnityEngine
{
    using Base = global::UnityEngine;
    /// <summary>
    /// 機能拡張したPlayerPrefs
    /// ・元のPlayerPrefsと置き換えても問題ない
    /// 
    /// 拡張したところ
    /// ・long,double,byte[]型に対応
    /// ・文字列を日本語に対応
    /// 
    /// 元のPlayerPrefs動作
    /// ・keyがない場合、defaultValueを返す。
    /// 　・defaultValueがない場合、""を返す。
    /// ・型が違う場合、defaultValueを返す。
    /// ・漢字などは、文字化けしたまま返され、defaultValueは返されない。
    /// </summary>
    public class PlayerPrefs
    {
        #region 継承出来ないのでラップ

        public PlayerPrefs() { }
        public static void DeleteAll() { Base.PlayerPrefs.DeleteAll(); }
        public static void DeleteKey(string key) { Base.PlayerPrefs.DeleteKey(key); }
        public static float GetFloat(string key) { return Base.PlayerPrefs.GetFloat(key); }
        public static float GetFloat(string key, float defaultValue) { return Base.PlayerPrefs.GetFloat(key, defaultValue); }
        public static int GetInt(string key) { return Base.PlayerPrefs.GetInt(key); }
        public static int GetInt(string key, int defaultValue) { return Base.PlayerPrefs.GetInt(key, defaultValue); }
        public static string GetString(string key)
        {
            return GetString(key, "");
        }
        public static string GetString(string key, string defaultValue)
        {
            try
            {
                var bytes = GetBytes(key);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static bool HasKey(string key) { return Base.PlayerPrefs.HasKey(key); }
        public static void Save() { Base.PlayerPrefs.Save(); }
        public static void SetFloat(string key, float value) { Base.PlayerPrefs.SetFloat(key, value); }
        public static void SetInt(string key, int value) { Base.PlayerPrefs.SetInt(key, value); }
        public static void SetString(string key, string value)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            SetBytes(key, bytes);
        }

        #endregion 継承出来ないのでラップ

        #region 拡張

        #region byte[]
        public static byte[] GetBytes(string key)
        {
            var value = Base.PlayerPrefs.GetString(key);
            return System.Convert.FromBase64String(value);
        }
        public static byte[] GetBytes(string key, byte[] defaultValue)
        {
            if (HasKey(key))
                return GetBytes(key);
            return defaultValue;
        }
        public static void SetBytes(string key, byte[] value)
        {
            var text = System.Convert.ToBase64String(value);
            Base.PlayerPrefs.SetString(key, text);
        }
        #endregion byte[]

        #region long
        public static long GetLong(string key)
        {
            var value = GetBytes(key);
            return System.BitConverter.ToInt64(value, 0);
        }
        public static long GetLong(string key, long defaultValue)
        {
            if (HasKey(key))
                return GetLong(key);
            return defaultValue;
        }
        public static void SetLong(string key, long value)
        {
            var text = System.BitConverter.GetBytes(value);
            SetBytes(key, text);
        }
        #endregion long

        #region double
        public static double GetDouble(string key)
        {
            var value = GetBytes(key);
            return System.BitConverter.ToDouble(value, 0);
        }
        public static double GetDouble(string key, double defaultValue)
        {
            if (HasKey(key))
                return GetDouble(key);
            return defaultValue;
        }
        public static void SetDouble(string key, double value)
        {
            var text = System.BitConverter.GetBytes(value);
            SetBytes(key, text);
        }
        #endregion double

        public static void Set(string key, int value) { SetInt(key, value); }
        public static void Set(string key, long value) { SetLong(key, value); }
        public static void Set(string key, float value) { SetFloat(key, value); }
        public static void Set(string key, double value) { SetDouble(key, value); }
        public static void Set(string key, string value) { SetString(key, value); }
        public static void Set(string key, byte[] value) { SetBytes(key, value); }

        public static int Get(string key, int defaultValue) { return GetInt(key, defaultValue); }
        public static long Get(string key, long defaultValue) { return GetLong(key, defaultValue); }
        public static float Get(string key, float defaultValue) { return GetFloat(key, defaultValue); }
        public static double Get(string key, double defaultValue) { return GetDouble(key, defaultValue); }
        public static string Get(string key, string defaultValue) { return GetString(key, defaultValue); }
        public static byte[] Get(string key, byte[] defaultValue) { return GetBytes(key, defaultValue); }

        #endregion 拡張

    }
}