/*
The zlib License

Copyright (c) 2015 Takahiro Kasanami

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
