using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public void SetSliderValue()
    {
        text.text = slider.value.ToString();
    }

    public int GetSliderValue()
    {
        return (int)slider.value;
    }
}
