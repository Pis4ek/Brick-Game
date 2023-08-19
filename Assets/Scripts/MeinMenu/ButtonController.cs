using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonController : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;

    private void Awake()
    {

    }
}
