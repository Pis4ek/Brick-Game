using Services.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _quitButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(Quit);
    }

    private async void StartGame()
    {
        if(GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
        {
            await loadingScreen.ActivateScreen();
        }

        SceneManager.LoadScene("PlayMode");
    }

    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
