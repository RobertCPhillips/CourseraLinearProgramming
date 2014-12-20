using System;
using System.Collections.Generic;

namespace MyLinearProgramSolver
{
    //hacky becuase i should be use LA to move entering / leaving
    public class HackyPivot2 : IPivotDictionary
    {
        public void Pivot(int entering, int leaving, LinearProgram linearProgram)
        {
            var enteringPos = Array.FindIndex(linearProgram.NonBasicIndices, i => i == entering);
            var leavingPos = Array.FindIndex(linearProgram.BasicIndices, i => i == leaving);

            var a = linearProgram.ObjectiveCoefficients;
            var b = linearProgram.Coefficients[leavingPos];

            //update leaving row:
            // - calculate new coefficients
            var n = b[enteringPos];
            b[enteringPos] = -1.0;
            var multiplier = 1 / -n;
            MultiplyArray(b, multiplier);

            //update other rows
            for (var i = 1; i < linearProgram.Coefficients.Length; i++)
            {
                //already updated leaving row
                if (i == leavingPos) continue;
                //the row to update
                var bb = linearProgram.Coefficients[i];
                //the coefficient in this row
                var c = bb[enteringPos];
                //reset coefficient for array addition
                bb[enteringPos] = 0.0;

                AddArray(bb, b, c);
            }

            //update objective row
            var oc = a[enteringPos];
            a[enteringPos] = 0;
            AddArray(a, b, oc);

            //update basic / non-basic indices
            linearProgram.BasicIndices[leavingPos] = entering;
            linearProgram.NonBasicIndices[enteringPos] = leaving;
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

        private static void MultiplyArray(IList<double> arrayToUpdate, double multiplier)
        {
            for (var i = 0; i < arrayToUpdate.Count; i++)
                arrayToUpdate[i] *= multiplier;
        }
    }
}