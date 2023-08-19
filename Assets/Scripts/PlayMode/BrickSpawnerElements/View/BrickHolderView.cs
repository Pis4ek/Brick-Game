using PlayMode.BrickSpawnerElements;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickHolderView : MonoBehaviour
    {
        [SerializeField] BrickVisualizationGOWidget _widget;
        [SerializeField] Text _title;


        private BrickSpawningHolder _holder;

        public BrickHolderView Init(BrickSpawningHolder holder)
        {
            _holder = holder;
            _widget.Init();
            _widget.gameObject.SetActive(false);

            _holder.OnBrickHeldEvent += UpdateWidget;

            return this;
        }

        private void UpdateWidget()
        {
            _widget.gameObject.SetActive(true);
            _title.gameObject.SetActive(false);
            _widget.SetConfig(_holder.Data.HeldBrick);
        }
    }
}