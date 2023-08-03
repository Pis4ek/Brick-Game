using Services.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.Bricks
{
    public class BrickInput : MonoBehaviour
    {
        [SerializeField] Button _lMoveButton;
        [SerializeField] Button _ldMoveButton;
        [SerializeField] Button _dMoveButton;
        [SerializeField] Button _rdMoveButton;
        [SerializeField] Button _rMoveButton;
        [SerializeField] Button _rotationButton;
        [SerializeField] Button _fullDownButton;

        private IControllableBrick _brick;
        private Timer _timer;

        public BrickInput Init(IControllableBrick brick, Timer timer)
        {
            _brick = brick;
            _timer = timer;
            timer.OnSecondTickedEvent += MoveDown;

            _lMoveButton.onClick.AddListener(delegate () { _brick.Move(Vector2Int.left); });
            _ldMoveButton.onClick.AddListener(delegate () { _brick.Move(new Vector2Int(-1, -1)); });
            _dMoveButton.onClick.AddListener(delegate () { _brick.Move(Vector2Int.down); });
            _rdMoveButton.onClick.AddListener(delegate () { _brick.Move(new Vector2Int(1, -1)); });
            _rMoveButton.onClick.AddListener(delegate () { _brick.Move(Vector2Int.right); });

            _rotationButton.onClick.AddListener(delegate () { _brick.Rotate(); });
            _fullDownButton.onClick.AddListener(delegate ()
            {
                while (true)
                {
                    if (_brick.Move(Vector2Int.down) == false)
                    {
                        break;
                    }
                }

            });

            return this;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _brick.Move(Vector2Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _brick.Move(Vector2Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _brick.Move(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _brick.Move(Vector2Int.right);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                _brick.Rotate();
            }
        }

        private void MoveDown()
        {
            _brick.Move(Vector2Int.down);
        }
    }
}