using DG.Tweening;
using UnityEngine;

namespace PlayMode.Map
{
    public class BlockObject : MonoBehaviour
    {
        public MeshRenderer Renderer { get; private set; }

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
        }

        public void MoveBlock(Vector3 coordinatesInWorld, float movingTime = 0.15f)
        {
            transform.DOMove(coordinatesInWorld, movingTime);
        }

        public void SetColor(Color color)
        {
            Renderer.material.color = color;
        }
    }
}