using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickSaverInput : MonoBehaviour
    {
        [SerializeField] Button _saveButton;

        private BrickSpawner _brickSpawner;

        public BrickSaverInput Init(BrickSpawner brickSpawner)
        {
            _brickSpawner = brickSpawner;

            _saveButton.onClick.AddListener(_brickSpawner.SaveCurrentBrick);

            return this;
        }
    }
}
