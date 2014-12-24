/*
 Copyright (c) 2014 Takahiro Kasanami
 
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
using System;
using UnityEngine;

namespace Ksnm.Colors
{
    /// <summary>
    /// HSVの色を表現
    /// ・各要素は0.0～1.0の値
    /// </summary>
    public struct HsvColor
    {
        /// <summary>
        /// 色相(Hue)
        /// </summary>
        public float h;

        /// <summary>
        /// 彩度(Saturation・Chroma)
        /// </summary>
        public float s;

        /// <summary>
        /// 明度(Value・Lightness・Brightness)
        /// </summary>
        public float v;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="h">色相</param>
        /// <param name="s">彩度</param>
        /// <param name="v">明度</param>
        public HsvColor(float h, float s, float v)
        {
            this.h = h;
            this.s = s;
            this.v = v;
        }

        /// <summary>
        /// RGBAからHSVへ変換
        /// ・不透明度は無くなる
        /// </summary>
        public static HsvColor FromRgb(Color rgb)
        {
            float r = rgb.r;
            float g = rgb.g;
            float b = rgb.b;
            float max = System.Math.Max(r, System.Math.Max(g, b));
            float min = System.Math.Min(r, System.Math.Min(g, b));
            float h = max - min;
            if (h > 0.0f)
            {
                if (max == r)
                {
                    h = (g - b) / h;
                    if (h < 0.0f)
                    {
                        h += 6.0f;
                    }
                }
                else if (max == g)
                {
                    h = 2.0f + (b - r) / h;
                }
                else
                {
                    h = 4.0f + (r - g) / h;
                }
                h /= 6;
            }
            float s = (max - min);
            if (max != 0.0f)
                s /= max;
            float v = max;
            return new HsvColor(h, s, v);
        }

        /// <summary>
        /// HSVからRGBAへ変換
        /// ・不透明度は1になる。
        /// </summary>
        public static Color ToRgb(HsvColor hsv)
        {
            float v = hsv.v;
            float s = hsv.s;
            float r = v;
            float g = v;
            float b = v;
            if (s > 0)
            {
                float h = hsv.h * 6;
                int i = (int)h;
                float f = h - (float)i;
                switch (i)
                {
                    default:
                    case 0:
                        g *= 1 - s * (1 - f);
                        b *= 1 - s;
                        break;
                    case 1:
                        r *= 1 - s * f;
                        b *= 1 - s;
                        break;
                    case 2:
                        r *= 1 - s;
                        b *= 1 - s * (1 - f);
                        break;
                    case 3:
                        r *= 1 - s;
                        g *= 1 - s * f;
                        break;
                    case 4:
                        r *= 1 - s * (1 - f);
                        g *= 1 - s;
                        break;
                    case 5:
                        g *= 1 - s;
                        b *= 1 - s * f;
                        break;
                }
            }
            return new Color(r, g, b);
        }

        public static HsvColor black { get { return new HsvColor(0, 0, 0); } }
        public static HsvColor blue { get { return new HsvColor(4f / 6f, 1, 1); } }
        public static HsvColor cyan { get { return new HsvColor(3f / 6f, 1, 1); } }
        public static HsvColor gray { get { return new HsvColor(0, 0, 0.5f); } }
        public static HsvColor green { get { return new HsvColor(2f / 6f, 1, 1); } }
        /// <summary>
        /// English spelling for gray.
        /// </summary>
        public static HsvColor grey { get { return gray; } }
        public static HsvColor magenta { get { return new HsvColor(5f / 6f, 1, 1); } }
        public static HsvColor red { get { return new HsvColor(0, 1, 1); } }
        public static HsvColor white { get { return new HsvColor(0, 0, 1); } }
        /// <summary>
        /// UnityEngine.Color.yellow の値が微妙なので、どうしても誤差が発生する。
        /// </summary>
        public static HsvColor yellow { get { return new HsvColor(0.1533865f, 0.9843137f, 1); } }


        public override bool Equals(object other)
        {
            if (other is HsvColor == false)
                return false;
            var otherHsv = (HsvColor)other;
            if (this.h != otherHsv.h)
                return false;
            if (this.s != otherHsv.s)
                return false;
            if (this.v != otherHsv.v)
                return false;
            return true;
        }
        /// <summary>
        /// ハッシュコードを生成
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = BitConverter.ToInt32(BitConverter.GetBytes(h), 0);
            hashCode ^= BitConverter.ToInt32(BitConverter.GetBytes(s), 0) >> 1;
            hashCode ^= BitConverter.ToInt32(BitConverter.GetBytes(v), 0) >> 2;
            return hashCode;
        }
        public static HsvColor Lerp(HsvColor a, HsvColor b, float t)
        {
            var hsv = new HsvColor();
            hsv.h = Mathf.Lerp(a.h, b.h, t);
            hsv.s = Mathf.Lerp(a.s, b.s, t);
            hsv.v = Mathf.Lerp(a.v, b.v, t);
            return hsv;
        }
        /// <summary>
        /// 文字列に変換
        /// ・UnityEngine.Colorに合わせてformatは"0.000"
        /// </summary>
        public override string ToString()
        {
            return ToString("0.000");
        }
        public string ToString(string format)
        {
            return "HSV(" + h.ToString(format) + ", " + s.ToString(format) + ", " + v.ToString(format) + ")";
        }
    }
}