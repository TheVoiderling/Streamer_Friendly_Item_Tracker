using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SVColorPicker : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Color output;
    [SerializeField] private Image _pickerImage;
    [SerializeField] private RawImage _svReference;
    private ColorPickerControl _cc;
    [SerializeField] private RectTransform _rectTransform, _pickerTransform;
    [SerializeField] private Image _changeThisColor;
    [SerializeField] private TMP_InputField _hexField;

    private void Start()
    {
        _cc = FindObjectOfType<ColorPickerControl>();
        _rectTransform = GetComponent<RectTransform>();
        _pickerTransform = _pickerImage.GetComponent<RectTransform>();
        output = LoadColor();
        _changeThisColor.color = output;
        UpdateHexcodeText();
    }

    public void UpdateViaHexcode()
    {
        Color tempColor;
        ColorUtility.TryParseHtmlString("#" + _hexField.text, out tempColor);
        output = tempColor;
        _changeThisColor.color = output;
        SaveColor(output);
    }

    private void UpdateHexcodeText()
    {
        _hexField.text = ColorUtility.ToHtmlStringRGB(output);
    }

    public void SaveColor(Color input)
    {
        PlayerPrefs.SetFloat("Red", input.r);
        PlayerPrefs.SetFloat("Green", input.g);
        PlayerPrefs.SetFloat("Blue", input.b);
        UpdateHexcodeText();
    }

    public Color LoadColor()
    {
        Color loadedColor = new Color(PlayerPrefs.GetFloat("Red", 0.5f), PlayerPrefs.GetFloat("Green", 0.5f), PlayerPrefs.GetFloat("Blue", 0.5f));
        return loadedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.TryGetComponent<RawImage>(out RawImage target))
        {
            if(target == _svReference)
            {
                output = Pick(Camera.main.WorldToScreenPoint(eventData.position));
                _changeThisColor.color = output;
                SaveColor(output);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<RawImage>(out RawImage target))
        {
            if (target == _svReference)
            {
                output = Pick(Camera.main.WorldToScreenPoint(eventData.position));
                _changeThisColor.color = output;
                SaveColor(output);
            }
        }
    }

    public void UpdateHue()
    {
        Vector2 point = _pickerTransform.localPosition;
        point += _svReference.rectTransform.sizeDelta / 2;
        Texture2D t = (Texture2D)GetComponent<RawImage>().texture;
        Vector2Int m_point = new Vector2Int((int)((t.width * point.x) / _svReference.rectTransform.sizeDelta.x), (int)((t.height * point.y) / _svReference.rectTransform.sizeDelta.y));
        output = t.GetPixel(m_point.x, m_point.y);
        _changeThisColor.color = output;
        SaveColor(output);
    }


    Color Pick(Vector2 screenPoint)
    {
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_svReference.rectTransform, screenPoint, Camera.main, out point);
        Vector2 pos = point;
        float deltaX = _svReference.rectTransform.sizeDelta.x * 0.5f;
        float deltaY = _svReference.rectTransform.sizeDelta.y * 0.5f;

        if (pos.x < -deltaX)
        {
            pos.x = -deltaX;
        }
        if (pos.x > deltaX)
        {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY)
        {
            pos.y = -deltaY;
        }
        if (pos.y > deltaY)
        {
            pos.y = deltaY;
        }
        _pickerTransform.localPosition = pos;
        point += _svReference.rectTransform.sizeDelta / 2;
        Texture2D t = (Texture2D)GetComponent<RawImage>().texture;
        Vector2Int m_point = new Vector2Int((int)((t.width * point.x) / _svReference.rectTransform.sizeDelta.x),(int)((t.height * point.y) / _svReference.rectTransform.sizeDelta.y));
        return t.GetPixel(m_point.x, m_point.y);
    }
}
