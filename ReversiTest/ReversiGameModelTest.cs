using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reversi.Model;
using Reversi.Persistence;
using System.Threading.Tasks;

namespace ReversiTest
{
    [TestClass]
    public class ReversiGameModelTest
    {
        private readonly Int32[] _supportedGameTableSizesArray = new Int32[] { 10, 20, 30 };
        private readonly Int32 _tableSizeDefaultSetting = 10;

        private ReversiFileDataAccess _dataAccess;

        private ReversiGameModel _model;

        [TestMethod]
        [ExpectedException(typeof(ReversiModelException))]
        public void ReversiGameModelNewGameInitializeOddTest()
        {
            Int32[] wrongGameTableSizesArray = new Int32[] { 10, 15, 20 };

            _dataAccess = new ReversiFileDataAccess(wrongGameTableSizesArray);
            _model = new ReversiGameModel(_dataAccess, _tableSizeDefaultSetting);
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiModelException))]
        public void ReversiGameModelNewGameInitializeTooSmallTest()
        {
            Int32[] wrongGameTableSizesArray = new Int32[] { 10, 2, 20 };

            _dataAccess = new ReversiFileDataAccess(wrongGameTableSizesArray);
            _model = new ReversiGameModel(_dataAccess, _tableSizeDefaultSetting);
        }

        [TestInitialize]
        public void Initialize()
        {
            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);

            _model = new ReversiGameModel(_dataAccess, _tableSizeDefaultSetting);

            _model.UpdateTable += new EventHandler<ReversiUpdateTableEventArgs>(model_UpdateTable);
            _model.SetGameEnded += new EventHandler<ReversiSetGameEndedEventArgs>(model_SetGameEnded);

        } 

        [TestMethod]
        public void ReversiGameModelNewGameSizeTest()
        {
            Assert.AreEqual(10, _model.TableSizeSetting);
            Assert.AreEqual(0, _model.ActiveTableSize);

            _model.TableSizeSetting = 20;

            Assert.AreEqual(20, _model.TableSizeSetting);
            Assert.AreEqual(0, _model.ActiveTableSize);

            _model.TableSizeSetting = 15;

            Assert.AreEqual(20, _model.TableSizeSetting);
            Assert.AreEqual(0, _model.ActiveTableSize);

            _model.NewGame();

            Assert.AreEqual(20, _model.TableSizeSetting);
            Assert.AreEqual(20, _model.ActiveTableSize);

            _model.TableSizeSetting = 10;

            Assert.AreEqual(10, _model.TableSizeSetting);
            Assert.AreEqual(20, _model.ActiveTableSize);

            _model.TableSizeSetting = 15;

            Assert.AreEqual(10, _model.TableSizeSetting);
            Assert.AreEqual(20, _model.ActiveTableSize);
        }

        [TestMethod]
        public async Task ReversiGameModelNewGameLoadTestOk0Step()
        {
            await _model.LoadGame("../../Resources/ok 0 step.reversi");
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ReversiGameModelNewGameLoadEmptyFileTest()
        {
            // Zero or one line file.
            await _model.LoadGame("../../Resources/empty file.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ReversiGameModelNewGameLoadLessPutDownThenPutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/less put down then put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPlayer2TimePutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/no player 2 time and put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPlayersTimePutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/no players time and put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/no put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongOddPutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/wrong odd put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayer1TimeTest()
        {
            await _model.LoadGame("../../Resources/wrong player 1 time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayer2TimeTest()
        {
            await _model.LoadGame("../../Resources/wrong player 2 time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayersTimeTest()
        {
            await _model.LoadGame("../../Resources/wrong players time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1Instead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step -1 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1Instead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step -1 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1InsteadPassTest()
        {
            await _model.LoadGame("../../Resources/wrong step -1 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep0Instead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 0 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep0Instead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 0 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1Instead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 1 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1Instead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 1 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1InsteadPassTest()
        {
            await _model.LoadGame("../../Resources/wrong step 1 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep3Instead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 3 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep3InsteadPassTest()
        {
            await _model.LoadGame("../../Resources/wrong step 3 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep5Instead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 5 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep5Instead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 5 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep6Instead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step 6 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep6InsteadPassTest()
        {
            await _model.LoadGame("../../Resources/wrong step 6 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepPassInstead3Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step pass instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepPassInstead6Or4Test()
        {
            await _model.LoadGame("../../Resources/wrong step pass instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeTableSizeTest()
        {
            await _model.LoadGame("../../Resources/wrong table size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeTooBigPutDownSizeTest()
        {
            await _model.LoadGame("../../Resources/wrong too big put down size.reversi");
        }

        private void model_UpdateTable(Object sender, ReversiUpdateTableEventArgs e)
        {
            
        }

        private void model_SetGameEnded(Object sender, ReversiSetGameEndedEventArgs e)
        {

        }

    }
}
