using System.Linq;

namespace MyLinearProgramSolver
{
    //hacky becuase i should use LA to convert to dual
    public class HackyMapPrimalToDual : IMapPrimalToDual
    {
        //[Line 1] m n
        //[Line 2] B1 B2 ... Bm [the list of basic indices m integers]
        //[Line 3] N1 N2 ... Nn [the list of non-basic indices n integers]
        //[Line 4] b1 .. bm (m floating point numbers)
        //[Line 5] a11 ... a1n (first row coefficients. See dictionary notation above.)
        //....
        //[Line m+4] am1 ... amn (mth row coefficients. See dictionary notation above.)
        //[Line m+5] z0 c1 .. cn (objective coefficients (n+1 floating point numbers)) 

        public DualLinearProgram Map(LinearProgram primal)
        {
            var dual = new DualLinearProgram();
            dual.M = primal.N;
            dual.N = primal.M;
            dual.OriginalObjectiveCoefficients = primal.ObjectiveCoefficients;
            dual.OriginalNonBasicIndices = primal.NonBasicIndices;

            dual.BasicIndices = new int[primal.NonBasicIndices.Length];
            dual.NonBasicIndices = new int[primal.BasicIndices.Length];

            dual.Coefficients = new double[primal.Coefficients[0].Length][];

            dual.ObjectiveCoefficients = new double[primal.BasicIndices.Length];
            
            //map primal coefficients to dual coefficients
            var a = primal.Coefficients.Length;
            for (var i = 0; i < dual.Coefficients.Length; i++)
            {
                dual.Coefficients[i] = new double[a];//empty row for 1-based indexes
                if (i == 0) continue;

                for (var k = 1; k < primal.Coefficients.Length; k++)
                {
                    dual.Coefficients[i][k] = -primal.Coefficients[k][i];
                }
            }

            //map 1 to dual coefficients b-value
            for (var i = 1; i < primal.ObjectiveCoefficients.Length; i++)
            {
                dual.Coefficients[i][0] = 1;
            }

            //map primal b values to dual objective coefficients
            for (var i = 1; i < primal.Coefficients.Length; i++)
            {
                dual.ObjectiveCoefficients[i] = -primal.Coefficients[i][0];
            }

            //set basic / non basic indices
            var y = 0;
            for (var i = 1; i < dual.BasicIndices.Length; i++)
            {
                dual.BasicIndices[i] = ++y;
            }
            for (var i = 1; i < dual.NonBasicIndices.Length; i++)
            {
                dual.NonBasicIndices[i] = ++y;
            }

            //set complements
            dual.Complements = new int[2][];
            dual.Complements[0] = primal.NonBasicIndices.Skip(1).Concat(primal.BasicIndices.Skip(1)).ToArray();
            dual.Complements[1] = dual.NonBasicIndices.Skip(1).Concat(dual.BasicIndices.Skip(1)).ToArray();

            return dual;
        }
    }
}