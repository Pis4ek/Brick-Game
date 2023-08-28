using Services;
using Services.LoadingScreen;
using Services.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootScene
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] LoadingScreen _loadingScreen;

        void Start()
        {
            var services = GloabalServices.Instance;

            ConfigService configService = new ConfigService();
            configService.LoadConfigsForScene(SceneManager.GetActiveScene().name);
            services.Add(configService);

            JsonStorageService jsonStorageService = new JsonStorageService();
            services.Add(jsonStorageService);

            BinaryStorageService binaryStorageService = new BinaryStorageService();
            services.Add(binaryStorageService);

            DontDestroyOnLoad(_loadingScreen.transform.parent.parent);
            services.Add(_loadingScreen);

            SceneManager.LoadScene("MainMenu");
        }
    }
}
