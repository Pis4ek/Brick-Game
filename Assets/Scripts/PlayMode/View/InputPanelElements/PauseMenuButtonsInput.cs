using Services.LoadingScreen;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class PauseMenuButtonsInput : MonoBehaviour
    {
        [SerializeField] Button _ratingButton;
        [SerializeField] Button _restartButton;
        [SerializeField] Button _mainMenuButton;

        public PauseMenuButtonsInput Init()
        {
            _ratingButton.onClick.AddListener(OpenRating);
            _restartButton.onClick.AddListener(RestartGame);
            _mainMenuButton.onClick.AddListener(OpenMainMenu);

            return this;
        }

        private void OpenRating()
        {

        }

        private async void RestartGame()
        {
            if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
            {
                await loadingScreen.ActivateScreen();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private async void OpenMainMenu()
        {
            if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
            {
                await loadingScreen.ActivateScreen();
            }

            SceneManager.LoadScene("MainMenu");
        }
    }
}