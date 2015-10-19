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

        private Boolean _simpleEvents = true;

        private Boolean _eventSetGameEnded;
        private Boolean _eventUpdatePlayerTime;
        private Boolean _eventUpdateTable;

        private Int32[,] _remainingSteps;
        private Int32 _maximumPossiblePutDownsSize;
        private Int32 _maximumReversedPutDownsSize;
        private Int32 _possibleGameCount;
        private Int32[] _possibleResults;
        private Int32 _currentPassingCount;
        private Int32 _maximumPassingCount;

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
        public async void ReversiGameModelBeforeNewGameTest()
        {
            _eventSetGameEnded = false;
            _eventUpdatePlayerTime = false;
            _eventUpdateTable = false;

            _model.Pass();
            _model.Pause();
            _model.Unpause();
            _model.PutDown(0,0);

            Assert.IsFalse(_eventSetGameEnded);
            Assert.IsFalse(_eventUpdatePlayerTime);
            Assert.IsFalse(_eventUpdateTable);

            _model.NewGame();

            await WaitMethod(2);

            Assert.IsTrue(_eventUpdatePlayerTime);

            _model.Pause();
            _eventUpdatePlayerTime = false;

            await WaitMethod(2);

            Assert.IsFalse(_eventUpdatePlayerTime);

            _model.Unpause();
            _eventUpdatePlayerTime = false;

            await WaitMethod(2);

            Assert.IsTrue(_eventUpdatePlayerTime);
        }

        [TestMethod]
        public async Task ReversiGameModelBeforeNewGameSaveTest()
        {
            await _model.SaveGame("");
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

            _simpleEvents = true;
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
            _simpleEvents = true;
            await _model.LoadGame("../../Resources/ok 0 step.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ReversiGameModelNewGameLoadEmptyFileTest()
        {
            // Zero or one line file.
            _simpleEvents = true;
            await _model.LoadGame("../../Resources/empty file.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task ReversiGameModelNewGameLoadLessPutDownThenPutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/less put down then put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPlayer2TimePutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/no player 2 time and put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPlayersTimePutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/no players time and put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task ReversiGameModelNewGameLoadNoPutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/no put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongOddPutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong odd put down size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayer1TimeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong player 1 time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayer2TimeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong player 2 time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongePlayersTimeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong players time.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1Instead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step -1 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1Instead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step -1 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepMinus1InsteadPassTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step -1 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep0Instead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 0 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep0Instead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 0 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1Instead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 1 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1Instead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 1 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep1InsteadPassTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 1 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep3Instead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 3 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep3InsteadPassTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 3 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep5Instead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 5 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep5Instead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 5 instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep6Instead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 6 instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStep6InsteadPassTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step 6 instead pass.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepPassInstead3Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step pass instead 3 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeStepPassInstead6Or4Test()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong step pass instead 6 or 4.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeTableSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong table size.reversi");
        }

        [TestMethod]
        [ExpectedException(typeof(ReversiDataException))]
        public async Task ReversiGameModelNewGameLoadWrongeTooBigPutDownSizeTest()
        {
            _simpleEvents =  true;
            await _model.LoadGame("../../Resources/wrong too big put down size.reversi");
        }

        [TestMethod]
        public void ReversiGameModelAllPossibleSenario()
        {
            _simpleEvents = false;

            _model.TableSizeSetting = 10;

            _remainingSteps = new Int32[(_model.TableSizeSetting * _model.TableSizeSetting * 2) + 1, (_model.TableSizeSetting * _model.TableSizeSetting * 2) + 1];

            _remainingSteps[0, 0] = 1;
            _remainingSteps[1, 0] = 8;

            _remainingSteps[1, 1] = 5;
            _remainingSteps[1, 2] = 3;
            _remainingSteps[1, 3] = 6;
            _remainingSteps[1, 4] = 4;
            _remainingSteps[1, 5] = 4;
            _remainingSteps[1, 6] = 6;
            _remainingSteps[1, 7] = 3;
            _remainingSteps[1, 8] = 5;

            _maximumPossiblePutDownsSize = 0;
            _maximumReversedPutDownsSize = 0;
            _possibleGameCount = 1;
            _maximumPassingCount = 0;
            _possibleResults = new Int32[] { 0, 0, 0 };

            _model.NewGame();
        }

        private void updateStepsArray(ReversiUpdateTableEventArgs e)
        {
            if (e.UpdatedFieldsCount != 0)
            {
                ++(_remainingSteps[0, 0]);

                if (_remainingSteps[_remainingSteps[0, 0], 0] == 0)
                {
                    if (e.IsPassingTurnOn)
                    {
                        _remainingSteps[_remainingSteps[0, 0], 0] += 2;
                        _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0] - 1] = -1;
                        _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0]] = -1;
                        

                        int possbilePutDownsSize = 0;
                        int reversedPutDownsSize = 0;
                        for (Int32 i = 0; i < e.UpdatedFieldsCount; i += 3)
                        {
                            if (e.UpdatedFieldsDatas[i + 2] != 1 && e.UpdatedFieldsDatas[i + 2] != -1)
                            {
                                possbilePutDownsSize += 3;
                            }
                            else
                            {
                                reversedPutDownsSize += 3;
                            }
                        }

                        if (_maximumPossiblePutDownsSize < possbilePutDownsSize)
                        {
                            _maximumPossiblePutDownsSize = possbilePutDownsSize;
                        }

                        if (_maximumReversedPutDownsSize < reversedPutDownsSize)
                        {
                            _maximumReversedPutDownsSize = reversedPutDownsSize;
                        }

                        ++_currentPassingCount;

                        if (_maximumPassingCount < _currentPassingCount)
                        {
                            _maximumPassingCount = _currentPassingCount;
                        }

                        _model.Pass();
                    }
                    else
                    {
                        Int32 checkFor = 3;
                        if (e.IsPlayer1TurnOn)
                        {
                            checkFor = 6;
                        }

                        int possbilePutDownsSize = 0;
                        int reversedPutDownsSize = 0;
                        for (Int32 i = 0; i < e.UpdatedFieldsCount; i += 3)
                        {
                            if (e.UpdatedFieldsDatas[i + 2] != 1 && e.UpdatedFieldsDatas[i + 2] != -1)
                            {
                                possbilePutDownsSize += 3;

                                if (e.UpdatedFieldsDatas[i + 2] == checkFor || e.UpdatedFieldsDatas[i + 2] == 4)
                                {
                                    _remainingSteps[_remainingSteps[0, 0], 0] += 2;
                                    _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0] - 1] = e.UpdatedFieldsDatas[i];
                                    _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0]] = e.UpdatedFieldsDatas[i + 1];
                                }
                            }
                            else
                            {
                                reversedPutDownsSize += 3;
                            }
                        }

                        if (_maximumPossiblePutDownsSize < possbilePutDownsSize)
                        {
                            _maximumPossiblePutDownsSize = possbilePutDownsSize;
                        }

                        if (_maximumReversedPutDownsSize < reversedPutDownsSize)
                        {
                            _maximumReversedPutDownsSize = reversedPutDownsSize;
                        }

                        if (possbilePutDownsSize != 0)
                        {
                            _model.PutDown(_remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0] - 1], _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0]]);
                        }
                        else
                        {
                            ; // last put down.
                        }
                    }
                }
                else
                {
                    if (_remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0] - 1] == -1 && _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0]] == -1)
                    {
                        _model.Pass();
                    }
                    else
                    {
                        _model.PutDown(_remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0] - 1], _remainingSteps[_remainingSteps[0, 0], _remainingSteps[_remainingSteps[0, 0], 0]]);
                    }
                }
            }
            else
            {
                _model.Unpause();
                _model.PutDown(_remainingSteps[1, _remainingSteps[1, 0] - 1], _remainingSteps[1, _remainingSteps[1, 0]]);
            }

            ++_remainingSteps[0, 0];
        }

        private void removeStep(ReversiSetGameEndedEventArgs e)
        {
            if (e.Player1Points > e.Player2Points)
            {
                _possibleResults[0] += 1;
            }
            else if (e.Player1Points < e.Player2Points)
            {
                _possibleResults[1] += 1;
            }
            else
            {
                _possibleResults[2] += 1;
            }

            while (_remainingSteps[_remainingSteps[0, 0], 0] == 0)
            {
                --(_remainingSteps[0, 0]);
                if (_remainingSteps[_remainingSteps[0, 0], 0] != 0)
                { 
                    _remainingSteps[_remainingSteps[0, 0], 0] -= 2;
                }

                if (_remainingSteps[0,0] == 0)
                {
                    break;
                }
            }

            if (_remainingSteps[0, 0] != 0)
            {
                _remainingSteps[0, 0] = 1;
                _currentPassingCount = 0;
                ++_possibleGameCount;

                _model.NewGame();

                _model.PutDown(_remainingSteps[1, _remainingSteps[1, 0] - 1], _remainingSteps[1, _remainingSteps[1, 0]]);
            }
            else
            {
                Assert.AreEqual(0, _maximumPossiblePutDownsSize);
                Assert.AreEqual(0, _maximumReversedPutDownsSize);
                Assert.AreEqual(0, _possibleGameCount);
                Assert.AreEqual(0, _maximumPassingCount);
                Assert.AreEqual(0, _possibleResults[0]);
                Assert.AreEqual(0, _possibleResults[1]);
                Assert.AreEqual(0, _possibleResults[2]);
            }
        }

        private void model_UpdateTable(Object sender, ReversiUpdateTableEventArgs e)
        {
            _eventUpdateTable = true;

            if(e == null)
            {
                int f = 0;
            }

            if (!_simpleEvents)
            {
                updateStepsArray(e);
            }
        }

        private void model_SetGameEnded(Object sender, ReversiSetGameEndedEventArgs e)
        {
            _eventSetGameEnded = true;

            if (!_simpleEvents)
            {
                removeStep(e);
            }
            
        }

        private void model_UpdatePlayerTime(Object sender, ReversiSetGameEndedEventArgs e)
        {
            _eventUpdatePlayerTime = true;
        }

        async Task WaitMethod(Int32 time)
        {
            await Task.Delay((Int32)TimeSpan.FromSeconds(time).TotalMilliseconds);
        }

    }
}
