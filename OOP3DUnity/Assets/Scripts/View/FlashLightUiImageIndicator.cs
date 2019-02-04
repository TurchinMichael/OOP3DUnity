using UnityEngine;
using UnityEngine.UI;

public class FlashLightUiImageIndicator : MonoBehaviour // b.Вывод на экран заряда батареи
{
    private Image _image;
    
    void Start()
    {
        _image = GetComponent<Image>();
    }
    
    public float ImageIndicator
    {
        set => _image.fillAmount = value;
    }

    public void SetActive(bool value)
    {
        _image.gameObject.SetActive(value);
    }
}
