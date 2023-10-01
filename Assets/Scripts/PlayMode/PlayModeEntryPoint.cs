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
    [SerializeField] VisualEffect _visualEffectDowning;
    [SerializeField] VisualEffect _visualEffectDestroing;

    private GameStateHolder _gameStateHolder = new GameStateHolder();
    private CompositeDisposable disposables;

    private async void Start()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        Init();
        stopWatch.Stop();

        _initTimeText.text = $"Init time: {stopWatch.ElapsedMilliseconds}";

        /*        if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
                {
                    await loadingScreen.DisactivateScreen();
                }*/

        _gameStateHolder.state.Value = GameStateType.Playing;
        _gameStateHolder.SendStartGame();
    }

    private void Init()
    {
        var brickGO = AddObject("Brick");
        var blockContainer = AddObject("BlockMapContainer");

        #region DATA
        var timerData = new TimerData();
        var brickData = new BrickData();
        var blockMapData = new BlockMapData(blockContainer.transform, _visualEffectDestroing);
        var brickSpawnerData = new BrickSpawnerData(blockMapData.MapSize);
        var scoreData = new ScoreData();
        var levelData = new LevelData();
        #endregion

        #region LOGIC
        var timer = AddObject("Timer").AddComponent<Timer>().Init(timerData, levelData, _gameStateHolder);
        var gameMap = new BlockMap();
        var converter = new CoordinateConverter(blockMapData.CellSize, blockMapData.WorldStartMap);
        var scoreCounter = new ScoreCounter(scoreData, gameMap, timerData);
        var levelCounter = new LevelCounter(levelData, scoreData, timerData);
        var brick = new Brick(gameMap, brickData);
        var brickSpawner = new BrickSpawner(brickSpawnerData, brick, brickData, _gameStateHolder, _gameStateHolder);
        var brickSpawningHolder = new BrickSpawningHolder(brickSpawner, brickSpawnerData);
        var leaderboard = new Leaderboard(true);
        var gameResultCalculator = new GameResultCalculator(_gameStateHolder, timerData, scoreData, leaderboard);
        #endregion

        #region VIEW
        brickGO.AddComponent<BrickView>().Init(brick, converter, brickData, _visualEffectDowning);

        var brickView = new BlockMapView(gameMap, converter, blockContainer.transform, _visualEffectDestroing);

        var pauseInput = _interfaceGameObject.GetComponentInChildren<PauseInput>();

        var worldSpaceUINode = _interfaceGameObject.GetComponentInChildren<GamePlayUINode>()
            .Init(brickSpawningHolder, brickSpawnerData, scoreData, timerData, levelData, gameResultCalculator);
        var screenSpaceUINode = _interfaceGameObject.GetComponentInChildren<InputUINode>().Init(brick, timerData, _gameStateHolder);

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
