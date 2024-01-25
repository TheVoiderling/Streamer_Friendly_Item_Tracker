using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialLoad : MonoBehaviour
{
    [SerializeField] private Toggle _runeDisplay, _keyDisplay, _medalionDisplay, _itemDisplay, _multiDisplay, _aot, _allText, _runeText, _noText;
    public static InitialLoad instance;

    public const string RUNE_TAG = "runeDisplay";
    public const string KEY_TAG = "keyDisplay";
    public const string MEDALION_TAG = "medalionDisplay";
    public const string ITEM_TAG = "itemDisplay";
    public const string MULTI_TAG = "multiDisplay";
    public const string AOT_TAG = "AOT";
    public const string TEXT_TAG = "textDisplay";

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadSettings();
    }

    public void LoadSettings()
    {
        SetToggle(_runeDisplay, PlayerPrefs.GetInt(RUNE_TAG, 1));
        SetToggle(_keyDisplay, PlayerPrefs.GetInt(KEY_TAG, 1));
        SetToggle(_medalionDisplay, PlayerPrefs.GetInt(MEDALION_TAG, 1));
        SetToggle(_itemDisplay, PlayerPrefs.GetInt(ITEM_TAG, 1));
        SetToggle(_multiDisplay, PlayerPrefs.GetInt(MULTI_TAG, 1));
        SetToggleGroup(PlayerPrefs.GetInt(TEXT_TAG, 0));
        StartCoroutine("DelayedStart");
    }

    public IEnumerator DelayedStart()
    {
        yield return new WaitForEndOfFrame();

        SetToggle(_aot, PlayerPrefs.GetInt(AOT_TAG, 1));
    }

    public void SetToggle(Toggle toggle, int state)
    {
        if(state == 1)
        {
            toggle.isOn = true;
            return;
        }

        toggle.isOn = false;
    }

    public void SetToggleGroup(int state)
    {
        if(state == 0)
        {
            _allText.isOn = true;
            return;
        }

        if (state == 1)
        {
            _runeText.isOn = true;
            return;
        }

        _noText.isOn = true;
    }

    public void SaveToggleState(Toggle toggle, string tag)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(tag, 1);
        }

        PlayerPrefs.SetInt(tag, 0);
    }

    public void SaveToggleGroupState(int state)
    {
        PlayerPrefs.SetInt(TEXT_TAG, state);
    }

    #region SpecificToggleFunctions
    
    public void SaveRuneToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(RUNE_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(RUNE_TAG, 0);
    }

    public void SaveKeyToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(KEY_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(KEY_TAG, 0);
    }

    public void SaveMedalionToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(MEDALION_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(MEDALION_TAG, 0);
    }

    public void SaveItemToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(ITEM_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(ITEM_TAG, 0);
    }

    public void SaveMultiToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(MULTI_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(MULTI_TAG, 0);
    }

    public void SaveAOTToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt(AOT_TAG, 1);
            return;
        }

        PlayerPrefs.SetInt(AOT_TAG, 0);
    }

    #endregion
}
