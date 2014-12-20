using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace MyLinearProgramSolver.Tests
{
    [TestClass]
    public class Assignment1PivotTests
    {
        private LinearProgramDictionaryReader _reader;
        private IAnalyzeEnteringLeavingVariables _analyze;
        private IPivotDictionary _pivotor;

        private const string DictionaryFiles =
            @".\part1TestCases\unitTests\";

        private const double Tolerance = .0001;

        [TestInitialize]
        public void Init()
        {
            _reader = new LinearProgramDictionaryReader();
            _analyze = new SimpleAnalyzeEnteringLeavingVariables();
            _pivotor = new HackyPivot2();
        }

        [TestMethod]
        public void Dict1_ShouldEnterWith_4_ShouldLeaveWith_3_WithObjective_7_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict1");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(4);
            _analyze.Leaving.ShouldEqual(3);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(7.0, Tolerance);
        }

        [TestMethod]
        public void Dict2_ShouldEnterWith_2_ShouldLeaveWith_3_WithObjective_4_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict2");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(2);
            _analyze.Leaving.ShouldEqual(3);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(4.0, Tolerance);
        }

        [TestMethod]
        public void Dict3_ShouldEnterWith_1_ShouldLeaveWith_6_WithObjective_2_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict3");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(1);
            _analyze.Leaving.ShouldEqual(6);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(2.0, Tolerance);
        }

        [TestMethod]
        public void Dict4_ShouldEnterWith_1_ShouldLeaveWith_5_WithObjective_3_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict4");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(1);
            _analyze.Leaving.ShouldEqual(5);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(3.0, Tolerance);
        }

        [TestMethod]
        public void Dict5_ShouldEnterWith_3_ShouldLeaveWith_2_WithObjective_2_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict5");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(3);
            _analyze.Leaving.ShouldEqual(2);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(2.0, Tolerance);
        }

        [TestMethod]
        public void Dict6_ShouldEnterWith_1_ShouldLeaveWith_Unbounded()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict6");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(1);
            _analyze.Leaving.ShouldEqual(0);
        }

        [TestMethod]
        public void Dict7_ShouldEnterWith_5_ShouldLeaveWith_2_WithObjective_6_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict7");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(5);
            _analyze.Leaving.ShouldEqual(2);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);
            linearProgram.ObjectiveValue.ShouldEqual(6.0, Tolerance);
        }

        [TestMethod]
        public void Dict8_ShouldEnterWith_2_ShouldLeaveWith_8_WithObjective_1_5077()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict8");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(2);
            _analyze.Leaving.ShouldEqual(8);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);

            linearProgram.ObjectiveValue.ShouldEqual(1.5077, Tolerance);
        }

        [TestMethod]
        public void Dict9_ShouldEnterWith_1_ShouldLeaveWith_9_WithObjective_0_1435()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict9");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(1);
            _analyze.Leaving.ShouldEqual(9);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);

            linearProgram.ObjectiveValue.ShouldEqual(.1435, Tolerance);
        }

        [TestMethod]
        public void Dict10_ShouldEnterWith_2_ShouldLeaveWith_13_WithObjective_0_0829()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict10");
            _analyze.Analyze(linearProgram);

            _analyze.Entering.ShouldEqual(2);
            _analyze.Leaving.ShouldEqual(13);

            _pivotor.Pivot(_analyze.Entering, _analyze.Leaving, linearProgram);

            linearProgram.ObjectiveValue.ShouldEqual(.0829, Tolerance);
        }
    }
}
