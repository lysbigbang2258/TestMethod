using System;
using System.Collections.Generic;
using System.Linq;

namespace TestUnit
{                                                 
    class Program
    {
        static void Main(string[] args) {
//            Test1();
            PhaseTest test = new PhaseTest();
            test.CalPahse();
            Console.ReadKey();
        }

        static void Test1() {
            var RcvPhaseList = new List<List<short>>();

            for(int i = 0; i < 32; i++) {
                RcvPhaseList.Add(new List<short>());
            }
            short[] data = new short[400];
            for(int i = 0; i < data.Length; i++) {
                data[i] = (short) i;
            }
            for(int i = 0; i < 32; i++) {
                RcvPhaseList[i].AddRange(data);
            }
            for(int i = 0; i < 32; i++) {
                RcvPhaseList[i].AddRange(data);
            }

            CalToPhase(RcvPhaseList);
        }

        static void CalToPhase(List<List<short>> RcvPahseList) {
            var calphase = new short[50][];
            var calcos = new double[50][];
            var calsin = new double[50][];
            var meancos = new double[50];
            var meansin = new double[50];
            var caltin = new double[32];

            for (int i = 0; i < calphase.Length; i++)
            {
                calphase[i] = new short[16];
                calcos[i] = new double[16];
                calsin[i] = new double[16];

            }

            for(int index = 0; index < RcvPahseList.Count; index++) {
                var array = RcvPahseList[index].ToArray();
                for(int i = 0; i < array.Length; i++) {
                    var frams = i / 16;
                    var num = i % 16;
                    calphase[frams][num] = array[i];
                }
                for(int i = 0; i < calphase.Length; i++) {
                    for(int j = 0; j < 16; j++) {
                        calcos[i][j] = calphase[i][j] * Math.Cos(Math.PI / 8 * j);
                        calsin[i][j] = calphase[i][j] * Math.Sin(Math.PI / 8 * j);
                    }
                    meancos[i] += calcos[i].Average() / calcos[i].Length;
                    meansin[i] += calsin[i].Average() / calsin[i].Length;
                }
                var art = Math.Atan(meansin.Average() / meansin.Average() * -1);
                caltin[index] = art;
            }
            List<double> list1;
            List<double> list2;
            List<List<double>> oneLists = new List<List<double>>();
            List<List<double>> twoLists = new List<List<double>>();
            List<double[]> doublelist = new List<double[]>();
            for(int index = 0; index < caltin.Length; index++) {
                list1 = new List<double>();
                list2 = new List<double>();
                for(int i = 0; i < 16; i++) {
                    var tmp1 = caltin[index] * Math.Cos(Math.PI / 8 * i + Math.PI * 31.25 * 1000 / 180);
                    var tmp2 = caltin[index] * Math.Cos(Math.PI / 4 * i + Math.PI * 31.25 * 1000 / 180);
                    list1.Add(tmp1);
                    list2.Add(tmp2);
                }
                oneLists.Add(list1);
                twoLists.Add(list2);
            }
            for(int i = 0; i < 32; i++) {
                var tmplist = new List<double>();
                tmplist.AddRange(oneLists[i]);
                tmplist.AddRange(twoLists[i]);
                doublelist.Add(tmplist.ToArray());
            }

            var shortlist = doublelist.ConvertAll(DoublesToshorts);
            var bytelist = shortlist.ConvertAll(ShortTobytes);
        }

        static short[] DoublesToshorts(double[] input) {
            List<short> list = new List<short>();
            for(int i = 0; i < input.Length; i++) {
                short t =(short)(input[i] * Math.Pow(2, 12));
                list.Add(t);
            }
            return list.ToArray();
        }

        static byte[] ShortTobytes(short[] input) {
            List<byte> list = new List<byte>();
            for(int i = 0; i < input.Length; i++) {
                var t = BitConverter.GetBytes(input[i]);
                list.AddRange(t);
            }

            return list.ToArray();
        }
    }
}
