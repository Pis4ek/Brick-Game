using LeaderboardComponents;
using PlayMode;
using PlayMode.Bricks;
using PlayMode.BrickSpawnerElements;
using PlayMode.GameResultCalculation;
using PlayMode.Level;
using PlayMode.Map;
using PlayMode.Score;
using PlayMode.View;
using Services.LoadingScreen;
using Services.Timer;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayModeEntryPoint : MonoBehaviour, IGameStateEvents
{
    public event Action OnValueChangedEvent;
    public event Action OnGameStartedEvent;
    public event Action OnGameEndedEvent;
    public event Action OnGamePausedEvent;
    public event Action OnGameUnpausedEvent;

    [SerializeField] GameObject _interfaceGameObject;
    [SerializeField] Text _initTimeText;

    public GameState State {
        get { return _state; } 
        private set 
        {
            _state = value;
            OnValueChangedEvent?.Invoke();
        } 
    }

    private GameState _state = GameState.Uninitialized;

    private async void Awake()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        Init();
        stopWatch.Stop();
        UnityEngine.Debug.Log($"Init time: {stopWatch.ElapsedMilliseconds}");
        _initTimeText.text = $"Init time: {stopWatch.ElapsedMilliseconds}";

        if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
        {
            await loadingScreen.DisactivateScreen();
        }

        State = GameState.Playing;
        OnGameStartedEvent?.Invoke();
    }

    private void Init()
    {
        var brickGO = AddObject("Brick");

        #region DATA
        var timerData = new TimerData();
        var brickData = new BrickData();
        var blockMapData = new BlockMapData(AddObject("BlockMapContainer").transform);
        var brickSpawnerData = new BrickSpawnerData(blockMapData.MapSize);
        var scoreData = new ScoreData();
        var levelData = new LevelData();
        #endregion

        #region LOGIC
        var timer = AddObject("Timer").AddComponent<Timer>().Init(timerData, levelData, this);
        var gameMap = new BlockMap(blockMapData);
        var converter = new CoordinateConverter(blockMapData.CellSize, blockMapData.WorldStartMap);
        var scoreCounter = new ScoreCounter(scoreData, gameMap, timerData);
        var levelCounter = new LevelCounter(levelData, scoreData);
        var brick = new Brick(gameMap, brickData);
        var brickSpawner = new BrickSpawner(brickSpawnerData, brick, brickData, this);
        brickSpawner.OnBrickCanNotSpawnEvent += delegate ()
        {
            State = GameState.Ended;
            OnGameEndedEvent?.Invoke();
        };
        var brickSpawningHolder = new BrickSpawningHolder(brickSpawner, brickSpawnerData);
        var leaderboard = new Leaderboard(true);
        var gameResultCalculator = new GameResultCalculator(this, timerData, scoreData, leaderboard);
        #endregion

        #region VIEW
        brickGO.AddComponent<BrickView>().Init(brick, converter, brickData);

        var pauseInput = _interfaceGameObject.GetComponentInChildren<PauseInput>();
        pauseInput.OnValueChangedEvent += delegate ()
        {
            if (pauseInput.IsPaused)
            {
                State = GameState.Paused;
                OnGamePausedEvent?.Invoke();
            }
            else
            {
                State = GameState.Playing;
                OnGameUnpausedEvent?.Invoke();
            }
        };

        var worldSpaceUINode = _interfaceGameObject.GetComponentInChildren<WorldSpaceUINode>()
            .Init(brickSpawningHolder, brickSpawnerData, scoreData, timerData, levelData, gameResultCalculator);
        var screenSpaceUINode = _interfaceGameObject.GetComponentInChildren<ScreenSpaceUINode>().Init(brick, timerData);

        var uiController = new UIController(this, screenSpaceUINode, worldSpaceUINode);
        #endregion
    }

    private GameObject AddObject(string name)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }
}
