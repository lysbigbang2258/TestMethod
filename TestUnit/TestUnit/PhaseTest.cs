// 201901211:32 PM

using System;
using System.Collections.Generic;
using System.Linq;

namespace TestUnit {
    public class PhaseTest {
        float[] floats1 = new float[16];
        float[] floats2 = new float[16];
        void Init() {
            for(int i = 0; i < 16; i++) {
                floats1[i] = i * 1.0f;
                floats2[i] = i + 1.0f;
            }
        }

        public PhaseTest() {
            Init();
        }

       public void CalPahse() {
           var res1 =new float[16];
           var res2 =new float[16];
           var list = new List<float>();
           for(int i = 0; i < 16; i++) {
               res1[i] =  (float) Math.Cos(i * Math.PI / 8) * 4;
               res2[i] =  (float) Math.Sin(i * Math.PI / 8) * 4;
               
           }
           var t1 = res2.Average();
           var t2 = res1.Average();
           var tmp =t1 / t2;

           list.Add((float)Math.Atan(tmp));
           Console.WriteLine(1);
        }
    }
}
