using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Ksnm
{
    /// <summary>
    /// エディタのバージョンが要求されたバージョンと違うなら、ダイアログを表示する。
    /// 
    /// 要求バージョンの指定方法：
    /// 　要求したいバージョンのエディタで本スクリプトをインポートし
    /// 　メニューから、Ksnm/EditorVersionChecker/SaveCurrentVersionをクリック。
    /// 　以降、エディタの起動時にバージョンをチェックする。
    /// </summary>
    [InitializeOnLoad]
    public class EditorVersionChecker
    {
        public static string RequiredVersion { get; private set; }
        public const string RequiredVersionFileName = "RequiredVersion.txt";
        public static string RequiredVersionFilePath
        {
            get
            {
                var thisFileFullPath = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                thisFileFullPath = thisFileFullPath.Replace('\\', '/');
                var thisDirectoryName = Path.GetDirectoryName(thisFileFullPath);
                return thisDirectoryName + "/" + RequiredVersionFileName;
            }
        }
        /// <summary>
        /// 静的コンストラクタ
        /// 
        /// エディタ起動時・コンパイル時に呼び出される。
        /// </summary>
        static EditorVersionChecker()
        {
            // 起動直後（起動してから10秒未満）だけ処理する
            if (EditorApplication.timeSinceStartup < 10)
            {
                if (File.Exists(RequiredVersionFilePath))
                {
                    RequiredVersion = File.ReadAllText(RequiredVersionFilePath, Encoding.UTF8);
                    var fileVersionInfo = FileVersionInfo.GetVersionInfo(EditorApplication.applicationPath);
                    if (fileVersionInfo.ProductVersion != RequiredVersion)
                    {
                        EditorUtility.DisplayDialog(
                            "バージョンチェッカー",
                            "現在のエディタのバージョンが\n要求されたバージョンではありません。\n\n" +
                            "現在のバージョン\t" + fileVersionInfo.ProductVersion + "\n" +
                            "要求バージョン\t" + RequiredVersion,
                            "OK");
                    }
                }
            }
        }
        /// <summary>
        /// 現在のエディタのバージョンを要求バージョンとする。
        /// </summary>
        [MenuItem("Ksnm/EditorVersionChecker/SaveCurrentVersion")]
        static void SaveCurrentVersion()
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(EditorApplication.applicationPath);
            RequiredVersion = fileVersionInfo.ProductVersion;
            File.WriteAllText(RequiredVersionFilePath, RequiredVersion, Encoding.UTF8);
            UnityEngine.Debug.Log("要求バージョン情報 " + RequiredVersionFileName + " を保存しました。");
            AssetDatabase.Refresh();
        }
    }
}
