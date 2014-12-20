using System;

namespace MyLinearProgramSolver
{
    public class SimpleAnalyzeEnteringLeavingVariables : IAnalyzeEnteringLeavingVariables
    {
        private int _enteringPos;

        public int Entering { get; private set; }
        public int Leaving { get; private set; }
        public void Analyze(LinearProgram linearProgram)
        {
            Entering = GetEntering(linearProgram);
            //if unbounded
            if (Entering == 0) return;

            Leaving = GetLeaving(linearProgram);
        }

        private int GetEntering(LinearProgram linearProgram)
        {
            var objectiveCoefficients = linearProgram.ObjectiveCoefficients;
            var nonbasic = linearProgram.NonBasicIndices;
            var n = linearProgram.N;

            //pick entering var (coeff > 0)
            var enteringPos = int.MaxValue;
            for (var j = 1; j <= n; j++)
            {
                if (objectiveCoefficients[j] > 0.0)
                {
                    if (enteringPos == int.MaxValue ||
                        nonbasic[j] < nonbasic[enteringPos])
                        enteringPos = j;
                }
            }

            if (enteringPos == int.MaxValue) return 0;

            _enteringPos = enteringPos;
            return nonbasic[enteringPos];
        }

        private int GetLeaving(LinearProgram linearProgram)
        {
            var coefficients = linearProgram.Coefficients;
            var m = linearProgram.M;
            var basic = linearProgram.BasicIndices;
            var enteringPos = _enteringPos;

            //pick leaving var (coeff <= 0, value within constraints)
            var leavingPos = int.MaxValue;
            var leavingValue = double.MaxValue;
            for (var i = 1; i <= m; i++)
            {
                var c = coefficients[i][enteringPos];
                if (c < 0)
                {
                    var value = coefficients[i][0] / -c;

                    if (value < leavingValue)
                    {
                        leavingValue = value;
                        leavingPos = i;
                    }
                    else if (Math.Abs(value - leavingValue) < .00001)
                    {
                        if (basic[i] < basic[leavingPos])
                            leavingPos = i;
                    }
                }
            }

            if (leavingPos == int.MaxValue) return 0;
            return basic[leavingPos];
        }
    }
}