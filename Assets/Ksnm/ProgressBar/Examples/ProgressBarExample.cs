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

namespace Ksnm.Examples
{
    public class ProgressBarExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Ksnm/Examples/ProgressBarExample Test1")]
        public static void Test1()
        {
            var progressBar = new ProgressBar("Test1");
            progressBar.Begin(5, "[5]");
            for (int i = 0; i < 5; i++)
            {
                progressBar.Begin(4, "[4]");
                for (int j = 0; j < 4; j++)
                {
                    progressBar.Begin(3, "[3]");
                    for (int k = 0; k < 3; k++)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (progressBar.Update(1) == true)
                        {
                            break;
                        }
                    }
                    progressBar.End();

                    progressBar.Update(1);
                }
                progressBar.End();

                progressBar.Update(1);
            }
            progressBar.End();
        }
        [UnityEditor.MenuItem("Ksnm/Examples/ProgressBarExample Test2")]
        public static void Test2()
        {
            var progressBar = new CancelableProgressBar("Test2");
            progressBar.Begin(5, "[5]");
            for (int i = 0; i < 5; i++)
            {
                progressBar.Begin(4, "[4]");
                for (int j = 0; j < 4; j++)
                {
                    progressBar.Begin(3, "[3]");
                    for (int k = 0; k < 3; k++)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (progressBar.Update(1) == true)
                        {
                            break;
                        }
                    }
                    if (progressBar.Canceled)
                    {
                        break;
                    }
                    progressBar.End();

                    progressBar.Update(1);
                }
                if (progressBar.Canceled)
                {
                    break;
                }
                progressBar.End();

                progressBar.Update(1);
            }
            progressBar.End();
        }
#endif// UNITY_EDITOR
    }
}