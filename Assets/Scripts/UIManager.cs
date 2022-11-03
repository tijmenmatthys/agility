using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _heartPrefab;
    [SerializeField] GameObject _starPrefab;
    [SerializeField] RectTransform _container;

    private List<GameObject> _hearts = new List<GameObject>();
    private List<GameObject> _stars = new List<GameObject>();

    public void UpdateHealth(int value)
    {
        UpdateIcons(_heartPrefab, _hearts, value);
    }

    public void UpdateLevel(int value)
    {
        UpdateIcons(_starPrefab, _stars, value);
    }

    private void UpdateIcons(GameObject prefab, List<GameObject> icons, int amount)
    {
        // add icons
        for (int i = icons.Count; i < amount; i++)
        {
            GameObject icon = Instantiate(prefab, _container);
            RectTransform rect = icon.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;
            position.x = (1 + 2 * i) * position.x;
            rect.anchoredPosition = position;
            icons.Add(icon);
        }
        // remove excess icons
        for (int i = icons.Count - 1; i >= amount; i--)
        {
            Destroy(icons[i]);
            icons.RemoveAt(i);
        }
    }
}
