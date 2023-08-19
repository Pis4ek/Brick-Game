using UnityEngine;
using UnityEngine.UI;

namespace PlayMode.View
{
    public class BrickVisualizationImageWidget : MonoBehaviour, IBrickVisualizationWidget
    {
        [SerializeField] Color _baseColor = new Color(0.25f, 0.25f, 0.25f);
        [SerializeField] bool _visualiseDeactivatedElements = true;

        private RawImage[,] _images;

        public BrickVisualizationImageWidget Init()
        {
            InitializeImages();

            return this;
        }

        public void SetConfig(BrickConfig brickConfig)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (brickConfig.shape[y, x])
                    {
                        _images[y, x].transform.Activate();
                        _images[y, x].color = brickConfig.Color;
                    }
                    else
                    {
                        SetDeactivatedBlock(_images[y, x]);
                    }
                    
                }
            }
        }

        private void SetDeactivatedBlock(RawImage image)
        {
            if (_visualiseDeactivatedElements)
            {
                image.color = _baseColor;
            }
            else
            {
                image.transform.Disactivate();
            }
        }

        private void InitializeImages()
        {
            _images = new RawImage[4, 4];

            for (int y = 0; y < 4; y++)
            {
                var line = transform.GetChild(y);

                for (int x = 0; x < 4; x++)
                {
                    if (line.GetChild(x).TryGetComponent<RawImage>(out var image))
                    {
                        _images[y, x] = image;
                    }
                    else
                    {
                        _images[y, x] = line.GetChild(x).gameObject.AddComponent<RawImage>();
                    }
                    _images[y, x].color = _baseColor;
                }
            }
        }
    }
}