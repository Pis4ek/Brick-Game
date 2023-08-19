using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using System;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private Camera _brickCamera;
    [SerializeField] private RenderTexture renderTexture;

    private List<BrickConfig> _items = new List<BrickConfig>();

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void OnEnable()
    {

    }

    private void OnNewButtonClicked()
    {
        Debug.Log("NewButtonClicked");
    }

    private void OnExitButtonClicked() 
    {
        Debug.Log("ExitButtonClicked");
    }

    private void OnSaveButtonClicked() 
    {
        Debug.Log("SaveButtonClicked");
        StartCoroutine(GetRenderTexture());
    }


    IEnumerator GetRenderTexture()
    {
        yield return new WaitForEndOfFrame();
        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, ReadbackCompleted);
    }

    public void ReadbackCompleted(AsyncGPUReadbackRequest request)
    {
        using (var imageBytes = request.GetData<byte>())
        {
            Texture2D ImageBuf = new Texture2D(256, 256);
            Color32[] colors = new Color32[256 * 256];

            for (int index = 0; index < colors.Length; index++)
            {
                int byteIndex = index * 4;
                Color32 color = new Color32(imageBytes[byteIndex], imageBytes[byteIndex + 1], imageBytes[byteIndex + 2], imageBytes[byteIndex + 3]);
                colors[index] = color;
            }

            float intensity = 0.15f; // Интенсивность свечения
            Color32 glowColor = Color.white; // Цвет свечения

            for (int index = 0; index < colors.Length; index++)
            {
                Color32 originalColor = colors[index];
                Color32 finalColor = Color32.Lerp(originalColor, glowColor, intensity);
                colors[index] = finalColor;
            }

            ImageBuf.SetPixels32(colors);
            ImageBuf.Apply();

            CreateBrickSave(ImageBuf);
        }
    }

    private void CreateBrickSave(Texture2D imageBuf)
    {
        //_items.Add(new BrickConfig("Save", imageBuf));
    }
}
