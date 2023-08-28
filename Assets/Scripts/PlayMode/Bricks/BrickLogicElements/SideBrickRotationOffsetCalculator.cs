using System.Collections;
using UnityEngine;

namespace PlayMode.Bricks
{
    public class SideBrickRotationOffsetCalculator
    {
        public int Offset => -_offset;

        private OffsetState _curentState;
        private int _offset;

        private enum OffsetState
        {
            NotInitialized,
            Left,
            Right
        }

        public void ConsiderBlock(int blockPosition)
        {
            if (_curentState == OffsetState.NotInitialized)
            {
                _offset = blockPosition;
                if (blockPosition > 0)
                {
                    _curentState = OffsetState.Left;
                }
                else if (blockPosition < 0)
                {
                    _curentState = OffsetState.Right;
                }
            } 
            else if (_curentState == OffsetState.Left)
            {
                if (blockPosition < Offset)
                    _offset = blockPosition;
            }  
            else if (_curentState == OffsetState.Right)
            {
                if (blockPosition > Offset)
                    _offset = blockPosition;
            }  
        }

        public void Reset()
        {
            _offset = 0;
            _curentState = OffsetState.NotInitialized;
        }
    }
}