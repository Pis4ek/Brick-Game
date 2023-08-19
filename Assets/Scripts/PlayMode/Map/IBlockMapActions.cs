using System;

namespace PlayMode.Map
{
    public interface IBlockMapActions
    {
        public event Action<int> OnBlocksAddedEvent;
        public event Action<int> OnLinesDestroyedEvent;
    }
}
