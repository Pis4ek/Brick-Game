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
using UniRx;
using UnityEngine.VFX;

public class PlayModeEntryPoint : MonoBehaviour
{
    [SerializeField] GameObject _interfaceGameObject;
    [SerializeField] Text _initTimeText;

    private GameStateHolder _gameStateHolder = new GameStateHolder();
    private PlayModeConfig playModeConfig;

    private async void Start()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        Init();
        stopWatch.Stop();

        _initTimeText.text = $"Init time: {stopWatch.ElapsedMilliseconds}";

        if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
        {
            await loadingScreen.DisactivateScreen();
        }

        _gameStateHolder.state.Value = GameStateType.Playing;
        _gameStateHolder.SendStartGame();
    }

    private void Init()
    {
        var blockContainer = AddObject("BlockMapContainer");

        #region DATA
        var timerData = new TimerData();
        var brickData = new BrickShape();
        var blockMapData = new BlockMapData();
        var brickSpawnerData = new BrickSpawnerData(blockMapData.MapSize);
        var scoreData = new ScoreData();
        var levelData = new LevelData();
        #endregion

        #region LOGIC
        var timer = AddObject("Timer").AddComponent<Timer>().Init(timerData, _gameStateHolder);
        var gameMap = new BlockMap();
        var converter = new CoordinateConverter(blockMapData.CellSize, blockMapData.WorldStartMap);
        var scoreCounter = new ScoreCounter(scoreData, gameMap, timerData, playModeConfig);
        var levelCounter = new LevelCounter(levelData, scoreData, timerData, playModeConfig);
        var brick = new Brick(gameMap);
        var fallingTimer = AddObject("FallingTimer").AddComponent<FallingTimeCounter>()
            .Init(brick, levelData, _gameStateHolder);
        var brickSpawner = new BrickSpawner(brickSpawnerData, brick, _gameStateHolder, _gameStateHolder, brick);
        var brickSpawningHolder = new BrickSpawningHolder(brickSpawner, brickSpawnerData);
        var leaderboard = new Leaderboard(true);
        var gameResultCalculator = new GameResultCalculator(_gameStateHolder, timerData, scoreData, leaderboard);
        #endregion

        #region VIEW
        var brickView = gameObject.GetComponentInChildren<BrickView>().Init(brick, converter);
        brickView.gameObject.AddComponent<BrickFallingView>().Init(converter, brick);

        var blockMapView = new BlockMapView(gameMap, converter, blockContainer.transform);

        var pauseInput = _interfaceGameObject.GetComponentInChildren<PauseInput>();

        var worldSpaceUINode = _interfaceGameObject.GetComponentInChildren<GamePlayUINode>()
            .Init(brickSpawningHolder, brickSpawnerData, scoreData, timerData, levelData, gameResultCalculator);
        var screenSpaceUINode = _interfaceGameObject.GetComponentInChildren<InputUINode>().Init(brick, timerData, _gameStateHolder, fallingTimer);

        var uiController = new UIController(_gameStateHolder, screenSpaceUINode, worldSpaceUINode);
        #endregion
    }

    private GameObject AddObject(string name)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }
}
