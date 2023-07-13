using Services;
using Services.Timer;
using UnityEngine;

public class PlayModeEntryPoint : MonoBehaviour
{
    private LocalServices _localServices;


    private void Awake()
    {
        _localServices = new LocalServices();

        Init();

        InitView();
    }

    private void Init()
    {
        var timer = AddObject("Timer").AddComponent<Timer>();
        _localServices.Add(timer);
    }

    private void InitView()
    {

    }

    private GameObject AddObject(string name)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        return go;
    }
}
