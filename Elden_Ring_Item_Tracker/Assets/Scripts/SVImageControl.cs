using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image _pickerImage;
    private RawImage _svImage;
    private ColorPickerControl _cc;
    private RectTransform _rectTransform, _pickerTransform;

    private void Awake()
    {
        _svImage = GetComponent<RawImage>();
        _cc = FindObjectOfType<ColorPickerControl>();
        _rectTransform = GetComponent<RectTransform>();

        _pickerTransform = _pickerImage.GetComponent<RectTransform>();
        _pickerTransform.position = new Vector2(-(_rectTransform.sizeDelta.x * 0.5f), -(_rectTransform.sizeDelta.y * 0.5f));
    }

    private void UpdateColor(PointerEventData eventData)
    {
        Vector3 pos = _rectTransform.InverseTransformDirection(eventData.position);

        float deltaX = _rectTransform.sizeDelta.x * 0.5f;
        float deltaY = _rectTransform.sizeDelta.y * 0.5f;

        if(pos.x < -deltaX)
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

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        float xNorm = x / _rectTransform.sizeDelta.x;
        float yNorm = y / _rectTransform.sizeDelta.y;

        _pickerTransform.localPosition = pos;
        Color newColor = (Color.HSVToRGB(0, 0, 1 - yNorm));
        _pickerImage.color = new Color(newColor.r, newColor.g, newColor.b, 0.6f);

        _cc.SetSV(xNorm, yNorm);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }
}
