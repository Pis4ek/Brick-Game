using UnityEngine;

namespace UnityEngine
{
    public class CameraScaler
    {
        public CameraScaler()
        {
            float ratio = (float)Screen.height / (float)Screen.width;
            Debug.Log(ratio);
        }
    }
}
