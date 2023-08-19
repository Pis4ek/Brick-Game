using System.Collections;
using UnityEngine;

namespace PlayMode.View
{
    public class BrickVisualizationGOWidget : MonoBehaviour, IBrickVisualizationWidget
    {
        [SerializeField] Color _baseColor = new Color(0.25f, 0.25f, 0.25f);
        [SerializeField] bool _visualiseDeactivatedElements = true;

        [SerializeField] Material _baseMat;

        private MeshRenderer[,] __objects;

        public BrickVisualizationGOWidget Init()
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
                        __objects[y, x].transform.Activate();
                        __objects[y, x].material.color = brickConfig.Color;
                    }
                    else
                    {
                        SetDeactivatedBlock(__objects[y, x]);
                    }
                }
            }
        }

        private void SetDeactivatedBlock(MeshRenderer obj)
        {
            if (_visualiseDeactivatedElements)
            {
                obj.material = _baseMat;
            }
            else
            {
                obj.transform.Disactivate();
            }
        }

        private void InitializeImages()
        {
            __objects = new MeshRenderer[4, 4];

            for (int y = 0; y < 4; y++)
            {
                var line = transform.GetChild(y);

                for (int x = 0; x < 4; x++)
                {
                    if (line.GetChild(x).TryGetComponent<MeshRenderer>(out var image))
                    {
                        __objects[y, x] = image;
                    }
                    else
                    {
                        __objects[y, x] = line.GetChild(x).gameObject.AddComponent<MeshRenderer>();
                    }
                    __objects[y, x].material.color = _baseColor;
                }
            }
        }
    }
}