using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour
{
    [SerializeField] private Toggle _toggleAll, _toggleRunes, _toggleNone;
    [SerializeField] private Toggle[] _toggles;
    [SerializeField] private GameObject[] _labels;
    [SerializeField] private int _toggleGroupIndex;

    private void Start()
    {
        ToggleAllLabels();
    }

    public void ToggleAllLabels()
    {
        int index = 0;
        if (_toggleNone.isOn)
        {
            foreach (Toggle item in _toggles)
            {
                _labels[index].SetActive(false);
                index++;
            }
        }
        else if (_toggleRunes.isOn)
        {
            foreach (Toggle item in _toggles)
            {
                if (!item.isOn)
                {
                    if(index < _toggleGroupIndex)
                    {
                        _labels[index].SetActive(true);
                    }
                    else
                    {
                        _labels[index].SetActive(false);
                    }
                }
                index++;
            }
        }
        else
        {
            foreach (Toggle item in _toggles)
            {
                if (!item.isOn)
                {
                    _labels[index].SetActive(true);
                }
                index++;
            }
        }
    }

    public void ToggleSingle(GameObject label)
    {
        if (_toggleNone.isOn)
        {
            return;
        }
        if (_toggleRunes.isOn)
        {
            for(int i = 0; i < _toggleGroupIndex; i++)
            {
                if(label == _labels[i])
                {
                    label.SetActive(!label.activeSelf);
                }
            }
        }
        else
        {
            label.SetActive(!label.activeSelf);
        }
    }

    public void ResetAllToggles()
    {
        foreach (Toggle item in _toggles)
        {
            item.isOn = false;
        }
    }
}
