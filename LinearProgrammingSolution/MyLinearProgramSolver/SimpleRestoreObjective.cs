using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLinearProgramSolver
{
    public class SimpleRestoreObjective : IRestoreObjective
    {
        public void Restore(LinearProgram linearProgram, double[] originalCoeficients, int[] originalIndices)
        {
            var newObjective = new double[linearProgram.ObjectiveCoefficients.Length];

            for (var i = 1; i < originalIndices.Length; i++)
            {
                var originalIndice = originalIndices[i];
                var originalCoef = originalCoeficients[i];

                if (linearProgram.NonBasicIndices.Contains(originalIndice))
                {
                    var index = Array.FindIndex(linearProgram.NonBasicIndices, n => n == originalIndice);
                    var ttmp = new double[linearProgram.ObjectiveCoefficients.Length];
                    ttmp[index] = originalCoef;
                    AddArray(newObjective, ttmp);
                }
                else
                {
                    var index = Array.FindIndex(linearProgram.BasicIndices, b => b == originalIndice);

                    var tmp = new double[linearProgram.ObjectiveCoefficients.Length];
                    linearProgram.Coefficients[index].CopyTo(tmp, 0);

                    AddArray(newObjective, tmp, originalCoef);
                }
            }

            linearProgram.ObjectiveCoefficients = newObjective;
        }

        private static void AddArray(IList<double> arrayToUpdate, IList<double> arrayOfValues, double multiplier = 1.0)
        {
            if (arrayToUpdate.Count != arrayOfValues.Count)
            {
                var msg =
                    String.Format("Adding arrays must be of same length.  Array1 Length: {0}.  Array2 Length: {1}.",
                        arrayToUpdate.Count, arrayOfValues.Count);

                throw new InvalidOperationException(msg);
            }

            for (var i = 0; i < arrayOfValues.Count; i++)
                arrayToUpdate[i] += (arrayOfValues[i] * multiplier);
        }
    }
}