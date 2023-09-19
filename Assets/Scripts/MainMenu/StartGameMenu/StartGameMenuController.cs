using Services.LoadingScreen;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartGameMenuController
    {
        public async void StartGame()
        {

            if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
            {
                await loadingScreen.ActivateScreen();
            }

            SceneManager.LoadScene("PlayMode");
        }
    }
}