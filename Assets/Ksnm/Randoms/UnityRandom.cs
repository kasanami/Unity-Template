/*
 Copyright (c) 2014-2017 Takahiro Kasanami
 
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
namespace Ksnm.Randoms
{
    /// <summary>
    /// UnityEngine.RandomをSystem.Randomを継承して実装したクラス
    /// 
    /// System.Randomを引数で受け取っている関数などに使用する。
    /// 
    /// UnityEngine.Randomのseedはstaticなので、生成時にseedを上書きしています。
    /// </summary>
    class UnityRandom : System.Random
    {
        UnityEngine.Random.State state;

        /// <summary>
        /// 時間に応じて決定される既定のシード値を使用し、新しいインスタンスを初期化します。
        /// </summary>
        public UnityRandom() : this((int)System.DateTime.Now.Ticks) { }

        /// <summary>
        /// 指定した値を使用して 新しいインスタンスを初期化します。
        /// </summary>
        public UnityRandom(int seed)
        {
            state = UnityEngine.Random.state;
        }

        /// <summary>
        /// 0 以上で System.Int32.MaxValue より小さい乱数を返します。
        /// </summary>
        /// <returns>0 以上で System.Int32.MaxValue より小さい 32 ビット符号付整数。</returns>
        public override int Next()
        {
            return Next(int.MaxValue);
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上の乱数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。maxValue は 0 以上にする必要があります。</param>
        /// <returns>0 以上で maxValue 未満の 32 ビット符号付き整数。
        /// つまり、通常は戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、0 が返されます。
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">maxValue が 0 未満です。</exception>
        public override int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new System.ArgumentOutOfRangeException();
            UnityEngine.Random.state = state;
            var v = UnityEngine.Random.Range(0, maxValue);
            state = UnityEngine.Random.state;
            return v;
        }

        /// <summary>
        /// 0.0 と 1.0 の間の乱数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
#if false
            // テスト
            // Next()とNext()の間で、UnityEngine.Randomを使用しても、seedを保持しているので結果に影響なし。
            var value = UnityEngine.Random.value;
#endif
            return Next() / (double)int.MaxValue;
        }
    }
}