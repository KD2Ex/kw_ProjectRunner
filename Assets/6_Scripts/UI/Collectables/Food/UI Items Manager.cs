using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemsManager : MonoBehaviour
{
    [SerializeField] private GInventory AllItems;
    private List<Image> images = new();
    [SerializeField] private GInventory playerInventory;

    private void Awake()
    {
        foreach (var item in AllItems.Items)
        {
            var instance = Instantiate(item.UIObject, transform);
            images.Add(instance.GetComponent<Image>());
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < AllItems.Items.Count; i++)
        {
            var alpha = playerInventory.Items.Contains(AllItems.Items[i]) ? 1f : .3f;
            images[i].color = GetColor(images[i].color, alpha);
        }
    }

    private Color GetColor(Color originalColor, float alpha)
    {
        return new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
