using System;

namespace MyLinearProgramSolver
{
    //hacky because i only calculate the objective
    public class HackyPivot : IPivotDictionary
    {
        public void Pivot(int entering, int leaving, LinearProgram linearProgram)
        {
            var enteringPos = Array.FindIndex(linearProgram.NonBasicIndices, i => i == entering);
            var leavingPos = Array.FindIndex(linearProgram.BasicIndices, i => i == leaving);

            var a = linearProgram.ObjectiveCoefficients;
            var b = linearProgram.Coefficients[leavingPos];

            var m = a[enteringPos];
            var n = b[enteringPos];

            var objValue = a[0] + m * b[0] / -n;
            linearProgram.ObjectiveCoefficients[0] = objValue;
        }
    }
}