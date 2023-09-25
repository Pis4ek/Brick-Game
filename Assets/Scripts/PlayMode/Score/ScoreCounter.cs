using PlayMode.Map;
using Services;
using Services.Timer;

namespace PlayMode.Score
{
    public class ScoreCounter : IService
    {
        private ScoreData _data;

        public ScoreCounter(ScoreData data, BlockMap blockMap, IReadOnlyTimerData timer)
        {
            _data = data;
            //timer.OnSecondTickedEvent += delegate () { _data.Score += 1; };

            blockMap.OnBlocksAddedEvent += delegate (int count) { _data.score.Value += 1 * count; };

            blockMap.OnLinesDestroyedEvent += delegate (int count) { _data.score.Value += 10 * count * count; };
        }
    }
}