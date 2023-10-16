using Services.Timer;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace PlayMode.Bricks
{
    public class BrickInput : MonoBehaviour
    {
        [SerializeField] Button _lMoveButton;
        [SerializeField] Button _dMoveButton;
        [SerializeField] Button _rMoveButton;
        [SerializeField] Button _rotationButton;
        [SerializeField] Button _fullDownButton;

        private IControllableBrick _brick;

        public BrickInput Init(IControllableBrick brick, IReadOnlyTimerData timerData, FallingTimeCounter fallingTimeCounter)
        {
            _brick = brick;

            //timerData.OnFallingTimeTickedEvent += delegate() { _brick.DownMove(); };
            fallingTimeCounter.OnFallingStepTicked += delegate () { _brick.DownMove(); };

            _lMoveButton.onClick.AddListener(() => _brick.LeftMove());
            _dMoveButton.onClick.AddListener(() => _brick.DownMove());
            _rMoveButton.onClick.AddListener(() => _brick.RightMove());
            _rotationButton.onClick.AddListener(() => _brick.Rotate());
            _fullDownButton.onClick.AddListener(() => _brick.FullDownMove());

            return this;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _brick.DownMove();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _brick.LeftMove();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _brick.RightMove();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                _brick.Rotate();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                _brick.FullDownMove();
            }
        }

        public void ShowButtons()
        {
            _lMoveButton.Activate();
            _dMoveButton.Activate();
            _rMoveButton.Activate();
            _rotationButton.Activate();
            _fullDownButton.Activate();
        }

        public void HideButtons()
        {
            _lMoveButton.Disactivate();
            _dMoveButton.Disactivate();
            _rMoveButton.Disactivate();
            _rotationButton.Disactivate();
            _fullDownButton.Disactivate();
        }
    }
}