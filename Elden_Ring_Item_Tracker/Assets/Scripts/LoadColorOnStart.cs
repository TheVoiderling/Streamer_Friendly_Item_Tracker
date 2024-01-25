using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadColorOnStart : MonoBehaviour
{
    [SerializeField] private Image _colorTarget;
    [SerializeField] private Toggle _textToggle;

    void Start()
    {
        _colorTarget.color = new Color(PlayerPrefs.GetFloat("Red", 0.5f), PlayerPrefs.GetFloat("Green", 0.5f), PlayerPrefs.GetFloat("Blue", 0.5f));
    }
}
