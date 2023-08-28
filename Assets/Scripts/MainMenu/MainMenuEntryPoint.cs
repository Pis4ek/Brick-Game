using Services.LoadingScreen;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        public async void Awake()
        {
            if (GloabalServices.Instance.TryGetService<LoadingScreen>(out var loadingScreen))
            {
                await loadingScreen.DisactivateScreen();
            }
        }
    }
}