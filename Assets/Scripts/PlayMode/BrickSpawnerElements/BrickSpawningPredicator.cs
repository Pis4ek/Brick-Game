using UnityEngine;

namespace PlayMode.BrickSpawnerElements
{
    public class BrickSpawningPredicator
    {
        private BrickSpawnerData _data;

        public BrickSpawningPredicator(BrickSpawnerData brickSpawnerData)
        {
            _data = brickSpawnerData;

            _data.NextBrickIndex = _data.ConfigLoader.GetRandomIndex();
            _data.PostNextBrickIndex = _data.ConfigLoader.GetRandomIndex();
        }

        public void SetNextBrickIndex()
        {
            _data.CurrentBrickIndex = _data.NextBrickIndex;
            _data.NextBrickIndex = _data.PostNextBrickIndex;
            _data.PostNextBrickIndex = _data.ConfigLoader.GetRandomIndex();
            _data.SendPredicationEvent();
        }
    }
}