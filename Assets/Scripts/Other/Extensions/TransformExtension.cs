namespace UnityEngine
{
    public static class TransformExtension
    {
        public static void Activate(this Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        public static void Deactivate(this Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}
