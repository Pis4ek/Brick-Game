using System;
using UnityEngine;

namespace PlayMode.BrickSpawnerElements
{
    public class BrickSpawnerData : ISpawnerData
    {
        public event Action OnNewPredicationSettedEvent;

        public BrickConfigLoader ConfigLoader { get; private set; }
        public Vector2Int SpawnPoint { get; private set; }
        public BrickConfig NextBrick { get { return ConfigLoader.GetConfig(NextBrickIndex); } }
        public BrickConfig PostNextBrick { get { return ConfigLoader.GetConfig(PostNextBrickIndex); } }
        public BrickConfig CurrentBrick { get { return ConfigLoader.GetConfig(CurrentBrickIndex); } }
        public BrickConfig HeldBrick { get { return ConfigLoader.GetConfig(HeldBrickIndex.Value); } }
        public int NextBrickIndex { get; set; }
        public int PostNextBrickIndex { get; set; }
        public int CurrentBrickIndex { get; set; }
        public int? HeldBrickIndex { get; set; } = null;


        public BrickSpawnerData(Vector2Int MapSize)
        {
            ConfigLoader = new BrickConfigLoader();
            SpawnPoint = new Vector2Int(MapSize.x / 2 - 2, 0);
        }

        public void SendPredicationEvent()
        {
            OnNewPredicationSettedEvent?.Invoke();
        }
    }
}
