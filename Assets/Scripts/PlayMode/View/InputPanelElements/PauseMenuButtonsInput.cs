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

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OpenMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}