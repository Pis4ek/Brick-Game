using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuView : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _scoreTableButton;
    [SerializeField] Button _settingsButton;
    [SerializeField] Button _brickEditorButton;
    [SerializeField] Button _quitButton;

    public IMediator Mediator;

    public MainManuView Init()
    {
        _startButton.onClick.AddListener(OnStartGameButtonClicked);
        _scoreTableButton.onClick.AddListener(OnScoreTableButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        _quitButton.onClick.AddListener(OnQuitButtonClicked);

        return this;
    }

    private void OnStartGameButtonClicked()
    {
        Mediator.Notify(this, "OpenStartGameMenu");
    }

    private void OnScoreTableButtonClicked()
    {
        Mediator.Notify(this, "OpenScoreMenu");
    }

    private void OnSettingsButtonClicked()
    {
        Mediator.Notify(this, "OpenSettingsMenu");
    }

    private void OnEditorButtonClicked()
    {

    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
