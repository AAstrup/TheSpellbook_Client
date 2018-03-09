using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopWrapper : MonoBehaviour {
    public static UIShopWrapper instance;
    public List<UIShopDefinition> shopUIElements;
    public List<ShopDefinition> shopItems;
    public GameObject UIContainer;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < shopUIElements.Count; i++)
        {
            if (i < shopItems.Count)
            {
                shopUIElements[i].gameObject.SetActive(true);
                shopUIElements[i].UpdateUI(shopItems[i]);
            }
            else
                shopUIElements[i].gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        UIContainer.SetActive(false);
    }
}
