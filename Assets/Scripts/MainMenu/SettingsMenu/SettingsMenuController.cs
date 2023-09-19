using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _qualityPresetDropdown;
    [SerializeField] private TMP_Dropdown _controlsPresetDropdown;

    private void Awake()
    {
        _qualityPresetDropdown.ClearOptions();
        _qualityPresetDropdown.AddOptions(new List<string>(QualitySettings.names));
        _qualityPresetDropdown.onValueChanged.AddListener(delegate { OnQualityDropdownChange(); });
        _qualityPresetDropdown.value = QualitySettings.GetQualityLevel();
    }

    private void OnQualityDropdownChange() 
    {
        QualitySettings.SetQualityLevel(_qualityPresetDropdown.value);
    }
}
