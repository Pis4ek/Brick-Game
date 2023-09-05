using Services.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _scoreTableButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _brickEditorButton;
    [SerializeField] Button _quitButton;

    [SerializeField] Transform _settingsMenu;

    private CanvasGroup _settingsCanvasGroup;
    private void Awake()
    {
        _settingsCanvasGroup = _settingsMenu.GetComponent<CanvasGroup>();

        _settingsCanvasGroup.alpha = 0;
        _settingsCanvasGroup.interactable = false;

        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(SettingsButtonClicked);
        _quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _settingsCanvasGroup.interactable = false;
            _settingsMenu.DOLocalMoveX(1000, 1f);
            _settingsCanvasGroup.DOFade(0, 1f);
        }
    }

    private void SettingsButtonClicked() 
    {
        _settingsMenu.DOLocalMoveX(0, 1f)
            .OnComplete(() => { _settingsCanvasGroup.interactable = true; });
        _settingsCanvasGroup.DOFade(1, 1f);
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
