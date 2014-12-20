using System.Linq;

namespace MyLinearProgramSolver
{
    public class LinearProgramSolverWeek3 : ILinearProgramSolver
    {
        private readonly IAnalyzeEnteringLeavingVariables _analyze;
        private readonly IPivotDictionary _pivotor;
        private readonly IMapPrimalToDual _mapPrimalToDual;
        private readonly IMapDualToPrimal _mapDualToPrimal;
        private readonly IRestoreObjective _restoreObjective;

        public LinearProgramSolverWeek3(IAnalyzeEnteringLeavingVariables analyze, IPivotDictionary pivotor, IMapPrimalToDual mapPrimalToDual, IMapDualToPrimal mapDualToPrimal, IRestoreObjective restoreObjective)
        {
            _analyze = analyze;
            _pivotor = pivotor;
            _mapPrimalToDual = mapPrimalToDual;
            _mapDualToPrimal = mapDualToPrimal;
            _restoreObjective = restoreObjective;
        }

        public LinearProgamSolution Solve(LinearProgram linearProgram)
        {
            //check if needs initialization
            bool needsInit = linearProgram.Coefficients.Any(c => c[0] < 0);

            if (needsInit)
            {
                //solve dual
                var dual = _mapPrimalToDual.Map(linearProgram);
                var dualSol = SolveSingle(dual);

                //todo: check if feasbile
                if (dualSol.SolutionType != LinearProgramSolutionType.Solved) return new LinearProgamSolution(0, 0, LinearProgramSolutionType.Infeasible);

                //map back to primal
                var primal = _mapDualToPrimal.Map(dual);

                //restore objective
                _restoreObjective.Restore(primal, dual.OriginalObjectiveCoefficients, dual.OriginalNonBasicIndices);

                //solve
                var primalSol = SolveSingle(primal);
                return primalSol;
            }
            else
            {
                return SolveSingle(linearProgram);
            }
        }

        private LinearProgamSolution SolveSingle(LinearProgram linearProgram)
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