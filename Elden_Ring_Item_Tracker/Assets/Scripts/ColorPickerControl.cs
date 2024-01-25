using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPickerControl : MonoBehaviour
{
    public float currentHue, currentSat, currentVal;

    [SerializeField] private RawImage _hueImage, _satValImage;
    [SerializeField] private Slider _hueSlider;
    [SerializeField] private GameObject _colorPickerCanvas;

    private SVColorPicker _cp;
    private Texture2D _hueTexture, _svTexture, _outputTexture;

    [SerializeField] private Image _changeThisColor;

    private void Start()
    {
        _cp = FindObjectOfType<SVColorPicker>();
        CreateHueImage();
        CreateSVImage();
        UpdateSVImage();
        _colorPickerCanvas.SetActive(false);
    }

    private void CreateHueImage()
    {
        _hueTexture = new Texture2D(1, 16);
        _hueTexture.wrapMode = TextureWrapMode.Clamp;
        _hueTexture.name = "HueTexture";

        for(int i = 0; i < _hueTexture.height; i++)
        {
            _hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / _hueTexture.height, 1, 1));
        }
        _hueTexture.Apply();
        currentHue = 0;
        _hueImage.texture = _hueTexture;
    }

    private void CreateSVImage()
    {
        _svTexture = new Texture2D(16, 16);
        _svTexture.wrapMode = TextureWrapMode.Clamp;
        _svTexture.name = "SatValTexture";

        for(int y = 0; y < _svTexture.height; y++)
        {
            for (int x = 0; x < _svTexture.width; x++)
            {
                _svTexture.SetPixel(x, y, Color.HSVToRGB(currentHue,(float)x / _svTexture.width,(float)y / _svTexture.height));
            }
        }

        _svTexture.Apply();
        currentSat = 0;
        currentVal = 0;
        _satValImage.texture = _svTexture;
    }

    public void SetSV(float s, float v)
    {
        currentSat = s;
        currentVal = v;
    }

    public void UpdateSVImage()
    {
        currentHue = _hueSlider.value;

        for (int y = 0; y < _svTexture.height; y++)
        {
            for (int x = 0; x < _svTexture.width; x++)
            {
                _svTexture.SetPixel(x, y, Color.HSVToRGB(
                                        currentHue,
                                        (float)x / _svTexture.width,
                                        (float)y / _svTexture.height));
            }
        }

        _svTexture.Apply();
        _cp.UpdateHue();
    }
}
