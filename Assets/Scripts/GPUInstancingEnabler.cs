using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GPUInstancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            GetComponent<MeshRenderer>().SetPropertyBlock(materialPropertyBlock);
        }
    }
}