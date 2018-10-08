using System.Collections;
using System.Collections.Generic;

namespace Ksnm.ExtensionClasses.UnityEngine
{
    using System = global::System;
    using Base = global::UnityEngine;
    /// <summary>
    /// UnityEngine.Randomを拡張したクラス
    /// UnityEngine.Randomは静的メソッドだけなので、拡張メソッドは使えない
    /// </summary>
    public class Random
    {
        #region 拡張
        /// <summary>
        /// ランダムな色を返します
        /// </summary>
        public static Base.Color color
        {
            get
            {
                return new Base.Color(value, value, value, value);
            }
        }
        /// <summary>
        /// 一辺が 2 の立方体の内部のランダムな点を返します
        /// </summary>
        public static Base.Vector3 insideUnitCube
        {
            get
            {
                return new Base.Vector3(Range(-1.0f, 1.0f), Range(-1.0f, 1.0f), Range(-1.0f, 1.0f));
            }
        }
        /// <summary>
        /// Enumの値をランダムに取得
        /// </summary>
        public static T Enum<T>()
        {
            var values = System.Enum.GetValues(typeof(T)) as T[];
            var index = Range(0, values.Length);
            return values[index];
        }

        #endregion 拡張

        #region 継承出来ないのでラップ

        static Base.Vector2 insideUnitCircle { get { return Base.Random.insideUnitCircle; } }
        static Base.Vector3 insideUnitSphere { get { return Base.Random.insideUnitSphere; } }
        static Base.Vector3 onUnitSphere { get { return Base.Random.onUnitSphere; } }
        static Base.Quaternion rotation { get { return Base.Random.rotation; } }
        static Base.Quaternion rotationUniform { get { return Base.Random.rotationUniform; } }
        static Base.Random.State state { get { return Base.Random.state; } set { Base.Random.state = value; } }
        static float value { get { return Base.Random.value; } }
        public static float Range(float min, float max) { return Base.Random.Range(min, max); }
        public static int Range(int min, int max) { return Base.Random.Range(min, max); }

        #endregion 継承出来ないのでラップ
    }
}
