namespace MyLinearProgramSolver
{
    //hacky becuase i should use LA to convert to primal
    public class HackyMapDualToPrimal : IMapDualToPrimal
    {
        //[Line 1] m n
        //[Line 2] B1 B2 ... Bm [the list of basic indices m integers]
        //[Line 3] N1 N2 ... Nn [the list of non-basic indices n integers]
        //[Line 4] b1 .. bm (m floating point numbers)
        //[Line 5] a11 ... a1n (first row coefficients. See dictionary notation above.)
        //....
        //[Line m+4] am1 ... amn (mth row coefficients. See dictionary notation above.)
        //[Line m+5] z0 c1 .. cn (objective coefficients (n+1 floating point numbers)) 

        public LinearProgram Map(DualLinearProgram dual)
        {
            var primal = new LinearProgram();
            primal.M = dual.N;
            primal.N = dual.M;

            primal.BasicIndices = new int[dual.NonBasicIndices.Length];
            primal.NonBasicIndices = new int[dual.BasicIndices.Length];

            //set basic / non basic indices
            dual.NonBasicIndices.CopyTo(primal.BasicIndices, 0);
            dual.BasicIndices.CopyTo(primal.NonBasicIndices, 0);

            //set coefficients
            primal.Coefficients = new double[dual.Coefficients[0].Length][];

            primal.ObjectiveCoefficients = new double[dual.BasicIndices.Length];

            //map dual coefficients to primal coefficients
            var a = dual.Coefficients.Length;
            for (var i = 0; i < primal.Coefficients.Length; i++)
            {
                primal.Coefficients[i] = new double[a];//empty row for 1-based indexes
                if (i == 0) continue;

                for (var k = 1; k < dual.Coefficients.Length; k++)
                {
                    primal.Coefficients[i][k] = -dual.Coefficients[k][i];
                }
            }

            //map dual objective coefficients to primal b-values
            for (var i = 1; i < dual.ObjectiveCoefficients.Length; i++)
            {
                primal.Coefficients[i][0] = -dual.ObjectiveCoefficients[i];
            }

            //map dual b-values to primal objective coefficients
            for (var i = 1; i < dual.Coefficients.Length; i++)
            {
                primal.ObjectiveCoefficients[i] = -dual.Coefficients[i][0];
            }
            primal.ObjectiveCoefficients[0] = -dual.ObjectiveCoefficients[0];

            return primal;
        }
    }
}