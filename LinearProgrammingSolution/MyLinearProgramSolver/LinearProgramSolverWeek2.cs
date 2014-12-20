namespace MyLinearProgramSolver
{
    public class LinearProgramSolverWeek2 : ILinearProgramSolver
    {
        private readonly IAnalyzeEnteringLeavingVariables _analyze;
        private readonly IPivotDictionary _pivotor;
        public LinearProgramSolverWeek2(IAnalyzeEnteringLeavingVariables analyze, IPivotDictionary pivotor)
        {
            _analyze = analyze;
            _pivotor = pivotor;
        }

        public LinearProgamSolution Solve(LinearProgram linearProgram)
        {
            var pivotCount = 0;

            while (true)
            {
                //do this until no entering, or stop if unbounded
                _analyze.Analyze(linearProgram);
                
                //no entering, must be solved
                if (_analyze.Entering == 0) break;
                
                //if unbounded
                if (_analyze.Leaving == 0) return new LinearProgamSolution(0, 0, LinearProgramSolutionType.Unbounded);

                //if here, we can pivot
                ++pivotCount;

                _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            }

            return new LinearProgamSolution(linearProgram.ObjectiveValue, pivotCount, LinearProgramSolutionType.Solved);
        }
    }
}