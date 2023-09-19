using Services.LoadingScreen;
using UnityEngine;
using System.Collections;

namespace MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] Transform _interfaceObject;

        private async void Start()
        {
            #region DATA
            #endregion

            #region LOGIC
            var startGameMenuController = new StartGameMenuController();
            #endregion

            #region VIEW
            var mainManuView = _interfaceObject.GetComponentInChildren<MainManuView>(true).Init();
            var startGameView = _interfaceObject.GetComponentInChildren<StartGameMenuView>(true).Init(startGameMenuController);
            var settingsView = _interfaceObject.GetComponentInChildren<SettingsMenuView>(true).Init();
            var scoreTableView = _interfaceObject.GetComponentInChildren<ScoreTableMenu>(true).Init();

            var mediator = new MainManuScreenMediator(mainManuView, startGameView, settingsView, scoreTableView);

            mainManuView.Mediator = mediator;
            startGameView.Mediator = mediator;
            settingsView.Mediator = mediator;
            scoreTableView.Mediator = mediator;
            #endregion

            if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
            {
                await loadingScreen.DisactivateScreen();
            }
        }
    }
}