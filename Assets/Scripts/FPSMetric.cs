using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class FPSMetric : MonoBehaviour
    {
        [SerializeField] int _fpsLimit = 60;


        private Text _fpsText;

        private float updateRate = 4.0f;

        private float frameCount;
        private float dt;
        private float fps;

        private void Start()
        {
            _fpsText = GetComponent<Text>();
            frameCount = 0;
            dt = 0;
            fps = 0;

            Application.targetFrameRate = 60;

            InvokeRepeating("UpdateFPS", 1.0f, 1.0f / updateRate);
        }

        private void UpdateFPS()
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt = 0;
            _fpsText.text = $"FPS: {(int)fps:d2}";
            Application.targetFrameRate = _fpsLimit;
        }

        private void Update()
        {
            frameCount++;
            dt += Time.deltaTime;
        }
    }
}