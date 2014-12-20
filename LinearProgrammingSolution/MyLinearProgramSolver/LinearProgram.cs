namespace MyLinearProgramSolver
{
    public class LinearProgram
    {
        //the number of decision variables
        public int M { get; set; }

        //the number of constraints
        public int N { get; set; }

        //which variables are basic
        public int[] BasicIndices { get; set; }
        //which variables are non basic
        public int[] NonBasicIndices { get; set; }

        //the coefficent values
        public double[][] Coefficients { get; set; }
        //the object coefficient values
        public double[] ObjectiveCoefficients { get; set; }

        public double ObjectiveValue
        {
            get
            {
                if (ObjectiveCoefficients != null && ObjectiveCoefficients.Length > 0)
                    return ObjectiveCoefficients[0];

                return 0;
            }
        }
    }
}