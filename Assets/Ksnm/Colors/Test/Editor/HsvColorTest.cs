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
using UnityEngine;
using NUnit.Framework;

namespace Ksnm.Colors
{
    [TestFixture]
    public class HsvColorTest
    {
        [Test]
        [TestCase(0.0f, 0.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 0.5f)]
        [TestCase(0.0f, 0.5f, 0.0f)]
        [TestCase(0.0f, 0.5f, 0.5f)]
        [TestCase(0.5f, 0.0f, 0.0f)]
        [TestCase(0.5f, 0.0f, 0.5f)]
        [TestCase(0.5f, 0.5f, 0.0f)]
        [TestCase(0.5f, 0.5f, 0.5f)]
        [TestCase(0.0f, 0.0f, 1.0f)]
        [TestCase(0.0f, 1.0f, 0.0f)]
        [TestCase(0.0f, 1.0f, 1.0f)]
        [TestCase(1.0f, 0.0f, 0.0f)]
        [TestCase(1.0f, 0.0f, 1.0f)]
        [TestCase(1.0f, 1.0f, 0.0f)]
        [TestCase(1.0f, 1.0f, 1.0f)]
        public void Rgb(float r, float g, float b)
        {
            var rgb = new Color(r, g, b);
            var hsv = HsvColor.FromRgb(rgb);
            var rgb2 = HsvColor.ToRgb(hsv);
            Assert.AreEqual(rgb, rgb2);
            //Debug.Log(rgb.ToString() + "→" + hsv.ToString());
        }

        [Test]
        [TestCase("black")]
        [TestCase("blue")]
        [TestCase("cyan")]
        [TestCase("gray")]
        [TestCase("green")]
        [TestCase("grey")]
        [TestCase("magenta")]
        [TestCase("red")]
        [TestCase("white")]
        // UnityEngine.Color.yellow の値が微妙なので、どうしても誤差が発生する。
        public void StaticColor(string name)
        {
            if (name == "black") Assert.AreEqual(HsvColor.FromRgb(Color.black), HsvColor.black);
            if (name == "blue") Assert.AreEqual(HsvColor.FromRgb(Color.blue), HsvColor.blue);
            if (name == "cyan") Assert.AreEqual(HsvColor.FromRgb(Color.cyan), HsvColor.cyan);
            if (name == "gray") Assert.AreEqual(HsvColor.FromRgb(Color.gray), HsvColor.gray);
            if (name == "green") Assert.AreEqual(HsvColor.FromRgb(Color.green), HsvColor.green);
            if (name == "grey") Assert.AreEqual(HsvColor.FromRgb(Color.grey), HsvColor.grey);
            if (name == "magenta") Assert.AreEqual(HsvColor.FromRgb(Color.magenta), HsvColor.magenta);
            if (name == "red") Assert.AreEqual(HsvColor.FromRgb(Color.red), HsvColor.red);
            if (name == "white") Assert.AreEqual(HsvColor.FromRgb(Color.white), HsvColor.white);
            if (name == "yellow") Assert.AreEqual(HsvColor.FromRgb(Color.yellow), HsvColor.yellow);
        }
    }
}