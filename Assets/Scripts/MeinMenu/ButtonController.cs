using System.Collections;
using System.Collections.Generic;
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

    private void StartGame()
    {
        SceneManager.LoadScene("PlayMode");
    }

    private void Quit()
    {
        Application.Quit();
    }
}
