using PlayMode.BrickSpawnerElements;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickHolderInput : MonoBehaviour
    {
        [SerializeField] Button _saveButton;

        private BrickSpawningHolder _spawningHolder;

        public BrickHolderInput Init(BrickSpawningHolder spawningHolder)
        {
            _spawningHolder = spawningHolder;

            _saveButton.onClick.AddListener(spawningHolder.HoldCurrentBrick);

            return this;
        }
    }
}
