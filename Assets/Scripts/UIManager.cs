using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _healthText;
    [SerializeField] Text _levelText;

    public void UpdateHealth(int value)
    {
        _healthText.text = $"Health: {value}";
    }

    public void UpdateLevel(int value)
    {
        _levelText.text = $"Level: {value}";
    }
}
