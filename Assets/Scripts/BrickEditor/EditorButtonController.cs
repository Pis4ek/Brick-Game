using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorButtonController : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;

    [SerializeField] private BrickEditor _brickEditor;
    private void Awake()
    {
        _buttons[0].onClick.AddListener(_brickEditor.OnSaveButtonClicked);
    }

}
