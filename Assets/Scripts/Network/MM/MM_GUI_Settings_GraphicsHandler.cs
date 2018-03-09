using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_GUI_Settings_GraphicsHandler : ISettingsPanelHandler
{
    //Overall Quality
    private Slider overallQualitySlider;
    private Text overallQualitySliderText;
    private int qualityLevel;
    private float lastOverallSliderValue;
    private bool fullscreen;
    private int lastScreenResolutionSelected;

    //Resolution Container

    public string GetPanelName() { return "Graphics"; }

    public MM_GUI_Settings_GraphicsHandler()
    {
        overallQualitySlider = GameObject.Find("GUI_SettingsGraphics_Slider").GetComponent<Slider>();
        overallQualitySliderText = GameObject.Find("GUI_SettingsGraphics_SliderText").GetComponent<Text>();
        overallQualitySlider.onValueChanged.AddListener(delegate { UpdateEventSystem(); });
        GameObject.Find("GUI_SettingsPanel_Graphics_Apply").GetComponent<Button>().onClick.AddListener(delegate { ApplyChanges(); });
        var resolutionDropDown = GameObject.Find("GUI_SettingsGraphics_ResolutionDropdown").GetComponent<Dropdown>();
        SetupResolutions(resolutionDropDown);
        resolutionDropDown.onValueChanged.AddListener(delegate { ResolutionChanged(resolutionDropDown.value); });
        var fullscreenToggle = GameObject.Find("GUI_SettingsPanelToggle_FullScreen").GetComponent<Toggle>();
        SetupFullScreenToggle(fullscreenToggle);
        fullscreenToggle.onValueChanged.AddListener(delegate { FullscreenToggle(fullscreenToggle); });
    }

    private void SetupFullScreenToggle(Toggle fullscreenToggle)
    {
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    private void FullscreenToggle(Toggle fullscreenToggle)
    {
        fullscreen = fullscreenToggle.isOn;
        ResolutionChanged(lastScreenResolutionSelected);
    }

    private void SetupResolutions(Dropdown resolutionDropDown)
    {
        Resolution[] resolutions = Screen.resolutions;
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (var res in resolutions)
        {
            string text = res.width + " x " + res.height;
            options.Add(new Dropdown.OptionData(text));
        }

        resolutionDropDown.options = options;

        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].Equals(Screen.currentResolution)) {
                resolutionDropDown.value = i;
                break;
            }
        }
    }

    private void ResolutionChanged(int value)
    {
        lastScreenResolutionSelected = value;
        Screen.SetResolution(Screen.resolutions[value].width, Screen.resolutions[value].height, fullscreen);
    }

    private void UpdateEventSystem()
    {
        var val = overallQualitySlider.value;
        if (val == lastOverallSliderValue)
            return;
        var levelsCount = QualitySettings.names.Length;

        val *= levelsCount - 1;
        qualityLevel = Mathf.RoundToInt(val);
        var percentage = ((float)qualityLevel) / (levelsCount-1);
        lastOverallSliderValue = percentage;
        overallQualitySlider.value = percentage;
        overallQualitySliderText.text = QualitySettings.names[qualityLevel];
    }

    void ApplyChanges()
    {
        QualitySettings.SetQualityLevel(qualityLevel, true);
    }
}