using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Ksnm
{
    /// <summary>
    /// 
    /// http://wonderpla.net/blog/engineer/Unity5_Search_MissingAssets/
    /// を参考に作成
    /// </summary>
    public class MissingListWindow : EditorWindow
    {
        private static string[] extensions = { ".prefab", ".mat", ".controller", ".cs", ".shader", ".mask", ".asset" };

        private static List<MissingItem> missingList = new List<MissingItem>();
        private Vector2 scrollPos;

        /// <summary>
        /// Missingがあるアセットを検索してそのリストを表示する
        /// </summary>
        [MenuItem("Window/Missing List")]
        private static void ShowMissingList()
        {
            // Missingがあるアセットを検索
            Search();

            // ウィンドウを表示
            var window = GetWindow<MissingListWindow>();
            window.minSize = new Vector2(900, 300);
        }

        /// <summary>
        /// Missingがあるアセットを検索
        /// </summary>
        private static void Search()
        {
            // 全てのアセットのファイルパスを取得
            var allPaths = AssetDatabase.GetAllAssetPaths();
            int length = allPaths.Length;

            for (int i = 0; i < length; i++)
            {
                // プログレスバーを表示
                EditorUtility.DisplayProgressBar("Search Missing", string.Format("{0}/{1}", i + 1, length), (float)i / length);

                // Missing状態のプロパティを検索
                if (extensions.Contains(Path.GetExtension(allPaths[i])))
                {
                    SearchMissing(allPaths[i]);
                }
            }

            // プログレスバーを消す
            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// 指定アセットにMissingのプロパティがあれば、それをmissingListに追加する
        /// </summary>
        /// <param name="path">Path.</param>
        private static void SearchMissing(string path)
        {
            // 指定パスのアセットを全て取得
            IEnumerable<UnityEngine.Object> assets = AssetDatabase.LoadAllAssetsAtPath(path);

            // 各アセットについて、Missingのプロパティがあるかチェック
            foreach (var obj in assets)
            {
                if (obj == null)
                {
                    continue;
                }
                if (obj.name == "Deprecated EditorExtensionImpl")
                {
                    continue;
                }

                // SerializedObjectを通してアセットのプロパティを取得する
                var sobj = new SerializedObject(obj);
                var property = sobj.GetIterator();

                while (property.Next(true))
                {
                    // プロパティの種類がオブジェクト（アセット）への参照で、
                    // その参照がnullなのにもかかわらず、参照先インスタンスIDが0でないものはMissing状態！
                    if (property.propertyType == SerializedPropertyType.ObjectReference &&
                        property.objectReferenceValue == null &&
                        property.objectReferenceInstanceIDValue != 0)
                    {
                        // Missing状態のプロパティリストに追加する
                        missingList.Add(new MissingItem()
                        {
                            Object = obj,
                            Path = path,
                            Property = property
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Missingのリストを表示
        /// </summary>
        private void OnGUI()
        {
            // 列見出し
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Asset", GUILayout.Width(200));
            EditorGUILayout.LabelField("Property", GUILayout.Width(200));
            EditorGUILayout.LabelField("Path");
            EditorGUILayout.EndHorizontal();

            // リスト表示
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            foreach (var data in missingList)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(data.Object, data.Object.GetType(), true, GUILayout.Width(200));
                EditorGUILayout.TextField(data.Property.name, GUILayout.Width(200));
                EditorGUILayout.TextField(data.Path);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        public class MissingItem
        {
            /// <summary>
            /// アセットのObject
            /// </summary>
            public UnityEngine.Object Object { get; set; }
            /// <summary>
            /// アセットのパス
            /// </summary>
            public string Path { get; set; }
            /// <summary>
            /// プロパティ
            /// </summary>
            public SerializedProperty Property { get; set; }
        }
    }
}
