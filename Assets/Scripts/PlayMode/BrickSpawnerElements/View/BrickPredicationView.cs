﻿using PlayMode.BrickSpawnerElements;
using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickPredicationView : MonoBehaviour
    {
        [SerializeField] BrickVisualizationImageWidget _nextBrickWidget;
        [SerializeField] BrickVisualizationImageWidget _postNextBrickWidget;
        [SerializeField] Button _saveButton;

        private ISpawnerData _data;

        public BrickPredicationView Init(ISpawnerData data)
        {
            _data = data;
            _data.OnNewPredicationSettedEvent += UpdateWidgets;

            _nextBrickWidget.Init();
            _postNextBrickWidget.Init();

            return this;
        }

        private void UpdateWidgets()
        {
            _nextBrickWidget.SetConfig(_data.NextBrick);
            _postNextBrickWidget.SetConfig(_data.PostNextBrick);
        }
    }
}