using LeaderboardComponents;
using PlayMode;
using PlayMode.Bricks;
using PlayMode.GameResultCalculation;
using PlayMode.Map;
using PlayMode.Score;
using PlayMode.View;
using Services;
using Services.Timer;
using System;
using UnityEngine;

public class PlayModeEntryPoint : MonoBehaviour, IGameStateEvents
{
    public event Action OnGameStartedEvent;
    public event Action OnGameEndedEvent;

    [SerializeField] GameObject _interfaceGameObject;

    private LocalServices _localServices;


    private void Awake()
    {
        _localServices = new LocalServices();

        Init();

        _localServices.GetService<BrickSpawner>().OnBrickCanNotSpawnEvent += delegate ()
        {
            OnGameEndedEvent?.Invoke();
        };

        OnGameStartedEvent?.Invoke();
    }

    private void Init()
    {
        var timer = AddObject("Timer").AddComponent<Timer>().Init(this);
        _localServices.Add(timer);

        var gameMap = new BlockMap(AddObject("BlockMapContainer").transform);
        _localServices.Add(gameMap);

        var converter = new CoordinateConverter(gameMap.CellSize, gameMap.WorldStartMap);

        var scoreCounter = new ScoreCounter(gameMap, timer);
        _localServices.Add(scoreCounter);

        var brickGO = AddObject("Brick");
        var brickData = new BrickData(brickGO.transform);
        var brick = new Brick(gameMap, brickData);
        _localServices.Add(brick);

        var brickSpawner = brickGO.AddComponent<BrickSpawner>().Init(gameMap, brick, this);
        _localServices.Add(brickSpawner);

        var leaderboard = new Leaderboard(true);
        _localServices.Add(leaderboard);

        var gameResultCalculator = new GameResultCalculator(this, timer, scoreCounter, leaderboard);
        _localServices.Add(gameResultCalculator);


        //---------------VIEW---------------


        var brickView = brickGO.AddComponent<BrickView>().Init(brick, converter, brickData);

        var brickSaverInput = _interfaceGameObject.GetComponentInChildren<BrickSaverInput>()
            .Init(_localServices.GetService<BrickSpawner>());

        var brickInput = _interfaceGameObject.GetComponentInChildren<BrickInput>()
            .Init(brick, timer);

        var brickConfigWidget = _interfaceGameObject.GetComponentInChildren<BrickConfigWidget>()
            .Init(_localServices.GetService<BrickSpawner>());

        var scoreView = _interfaceGameObject.GetComponentInChildren<ScoreView>()
            .Init(_localServices.GetService<ScoreCounter>());

        var gameResultView = _interfaceGameObject.GetComponentInChildren<GameResultView>(true)
            .Init(_localServices.GetService<GameResultCalculator>());
    }

    private GameObject AddObject(string name)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }
}
