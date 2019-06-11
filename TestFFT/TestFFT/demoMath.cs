// 2019012510:06 AM

using System;
using MathNet.Numerics.IntegralTransforms;

namespace TestFFT {
    public class DemoMath {
        public void Test() {
            double[] data = new double[1026];
            for (int i = 0; i < data.Length; i++) {
                data[i] = (i+1) * 1.0f;
            }
            Fourier.ForwardReal(data, 1024);
            Console.WriteLine(data);
        } 
    }
}
