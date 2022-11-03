using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    void Start()
    {
        _scoreText.text = "You Survived {levels}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
