using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace PlayMode.View
{
    public class PauseInput : MonoBehaviour
    {
        public event Action OnValueChangedEvent;

        [SerializeField] Image _buttonIcon;
        [SerializeField] Sprite _playIcon;
        [SerializeField] Sprite _pauseIcon;

        public bool IsPaused { get; private set; }

        private Button _pauseButton;

        public PauseInput Init()
        {
            _pauseButton = GetComponent<Button>();
            _pauseButton.onClick.AddListener(Pause);

            

            return this;
        }

        private void Pause()
        {
            IsPaused = !IsPaused;
            _buttonIcon.sprite =  IsPaused ? _playIcon :_pauseIcon;
            OnValueChangedEvent?.Invoke();
        }

        private async void LoadSprites()
        {
            var playIconHandle = Addressables.LoadAssetAsync<Sprite>("UISheet{UISheet_12}");
            var pauseIconHandle = Addressables.LoadAssetAsync<Sprite>("UISheet{UISheet_8}");

            await Task.WhenAll(playIconHandle.Task, pauseIconHandle.Task);

            _playIcon = playIconHandle.Result;
            _pauseIcon = pauseIconHandle.Result;

            Addressables.Release(playIconHandle);
            Addressables.Release(pauseIconHandle);
        }
    }
}