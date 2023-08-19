using System;

namespace PlayMode.BrickSpawnerElements
{
    public interface ISpawnerData
    {
        public event Action OnNewPredicationSettedEvent;

        public BrickConfig NextBrick { get; }
        public BrickConfig PostNextBrick { get; }
        public BrickConfig CurrentBrick { get; }
        public BrickConfig HeldBrick { get; }
    }
}