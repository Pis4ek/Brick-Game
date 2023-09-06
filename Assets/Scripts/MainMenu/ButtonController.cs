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
    [Space(20)]
    [SerializeField] float _TransitionDelay;
    [Space(20)]
    [SerializeField] Transform _settingsMenu;
    [SerializeField] Transform _scoreTableMenu;

    private Transform _lastActiveMenu;
    private void Awake()
    {
        CanvasGroup CanvasGroup = _settingsMenu.GetComponent<CanvasGroup>();
        _settingsMenu.localPosition = new Vector3(1000, 0, 0);
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;

        CanvasGroup = _scoreTableMenu.GetComponent<CanvasGroup>();
        _scoreTableMenu.localPosition = new Vector3(1000,0,0);
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;

        _startButton.onClick.AddListener(StartGame);
        _scoreTableButton.onClick.AddListener(ScoreTableButtonClicked);
        _settingsButton.onClick.AddListener(SettingsButtonClicked);
        _quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            CanvasGroup menuCanvasGroup = _lastActiveMenu.GetComponent<CanvasGroup>();
            menuCanvasGroup.interactable = false;
            _lastActiveMenu.DOLocalMoveX(1000, _TransitionDelay);
            menuCanvasGroup.DOFade(0, _TransitionDelay);
            _lastActiveMenu = null;
        }
    }

    private void ScoreTableButtonClicked() 
    {
        MoveAndActivateMenu(_scoreTableMenu);
    }

    private void SettingsButtonClicked()
    {
        MoveAndActivateMenu(_settingsMenu);
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

    private void MoveAndActivateMenu(Transform menu)
    {
        _lastActiveMenu = menu;
        CanvasGroup menuCanvasGroup = menu.GetComponent<CanvasGroup>();
        menu.DOLocalMoveX(0, _TransitionDelay)
            .OnComplete(() => { menuCanvasGroup.interactable = true; });
        menuCanvasGroup.DOFade(1, _TransitionDelay);
    }
}
