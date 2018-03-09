using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_GUI_SettingsHandler
{
    GameObject settingPanel;
    List<GameObject> panels;
    List<Image> panelButtonsSpriteRenderer;
    private GameObject lastSelected;

    public MM_GUI_SettingsHandler()
    {
        settingPanel = GameObject.Find("GUI_Settings");
        GameObject.Find("GUI_Settings_CloseButton").GetComponent<Button>().onClick.AddListener(delegate { HidePanel(); });
        GameObject.Find("BarButtonSettings").GetComponent<Button>().onClick.AddListener(delegate { ShowPanel(); });

        SetupPanelButtons();

        HidePanel();

        EnablePanel(panels[0].name);
    }

    public void Update()
    {
        if (!settingPanel.activeSelf)
            return;
        if(lastSelected == EventSystemReference.instance.EventSystem.currentSelectedGameObject.gameObject)
            return;
        lastSelected = EventSystemReference.instance.EventSystem.currentSelectedGameObject.gameObject;
    }

    private void SetupPanelButtons()
    {
        List<ISettingsPanelHandler> handlers = new List<ISettingsPanelHandler>();
        handlers.Add(new MM_GUI_Settings_GraphicsHandler());
        handlers.Add(new MM_GUI_Settings_TestBuildHandler());
        panels = new List<GameObject>();
        panelButtonsSpriteRenderer = new List<Image>();

        foreach (var item in handlers)
        {
            string name = item.GetPanelName();
            var panel = GameObject.Find("GUI_SettingsPanel_" + name);
            panels.Add(panel);
            var but = GameObject.Find("GUI_SettingsButton_" + name).GetComponent<Button>();
            panelButtonsSpriteRenderer.Add(but.gameObject.GetComponent<Image>());
            but.onClick.AddListener(
                delegate
                {
                    DisableAllPanels();
                    EnablePanel(panel.name);
                });
            but.interactable = true;
        }
        DisableAllPanels();
    }

    private void EnablePanel(string panelName)
    {
        foreach (var item in panels)
        {
            if (item.name == panelName) {
                item.SetActive(true);
                return;
            }
        }
    }

    private void DisableAllPanels()
    {
        foreach (var item in panels)
        {
            item.SetActive(false);
        }
    }

    private void HidePanel()
    {
        settingPanel.SetActive(false);
    }

    private void ShowPanel()
    {
        settingPanel.SetActive(true);
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(panelButtonsSpriteRenderer[0].gameObject);
    }
}