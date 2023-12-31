﻿using PlayMode.BrickSpawnerElements;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickHolderView : MonoBehaviour
    {
        [SerializeField] BrickVisualizationImageWidget _widget;
        [SerializeField] Text _title;
        [SerializeField] Button _saveButton;


        private BrickSpawningHolder _holder;

        public BrickHolderView Init(BrickSpawningHolder holder)
        {
            _holder = holder;
            _widget.Init();
            _widget.gameObject.SetActive(false);



            _saveButton.onClick.AddListener(holder.HoldCurrentBrick);

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