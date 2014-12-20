using System;
using System.IO;
using System.Linq;

namespace MyLinearProgramSolver
{
    //[Line 1] m n
    //[Line 2] B1 B2 ... Bm [the list of basic indices m integers]
    //[Line 3] N1 N2 ... Nn [the list of non-basic indices n integers]
    //[Line 4] b1 .. bm (m floating point numbers)
    //[Line 5] a11 ... a1n (first row coefficients. See dictionary notation above.)
    //....
    //[Line m+4] am1 ... amn (mth row coefficients. See dictionary notation above.)
    //[Line m+5] z0 c1 .. cn (objective coefficients (n+1 floating point numbers)) 

    //3 4
    //1 3 6
    //2 4 5 7
    //1 3 0
    //0 0 -1 -2
    //1 -1 0 -1
    //-1 0 -2 0
    //1 -1  2 3 1

    public class LinearProgramDictionaryReader
    {
        private readonly static char[] Splitter = { ' ' };

        public LinearProgram Read(string fileName)
        {
            var lp = new LinearProgram();

            var lines = File.ReadAllLines(fileName);

            var sizeVars = GetInts(lines[0]);
            lp.M = sizeVars[0];
            lp.N = sizeVars[1];

            var basic = GetInts(lines[1]);
            lp.BasicIndices = new int[basic.Length + 1];
            basic.CopyTo(lp.BasicIndices, 1);

            var nonBasic = GetInts(lines[2]);
            lp.NonBasicIndices = new int[nonBasic.Length + 1];
            nonBasic.CopyTo(lp.NonBasicIndices, 1);

            var bVars = GetDoubles(lines[3]);
            lp.Coefficients = new double[bVars.Length + 1][];

            int m = 0;
            for(int i = 4; i < lines.Length; i++)
            {
                var values = GetDoubles(lines[i]);

                if (i == lines.Length - 1)
                {
                    lp.ObjectiveCoefficients = values;
                }
                else
                {
                    if (m == 0) lp.Coefficients[m] = new double[values.Length + 1];
                    ++m;
                    lp.Coefficients[m] = new double[values.Length + 1];
                    lp.Coefficients[m][0] = bVars[m-1];
                    values.CopyTo(lp.Coefficients[m], 1);
                }
            }
            
            return lp;
        }

        private static int[] GetInts(string line)
        {
            var ints = line.Split(Splitter, StringSplitOptions.RemoveEmptyEntries)
                .Select(Int32.Parse).ToArray();

            return ints;
        }

        private static double[] GetDoubles(string line)
        {
            var doubles = line.Split(Splitter, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse).ToArray();

            return doubles;
        }
    }
}