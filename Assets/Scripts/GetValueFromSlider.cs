using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public void GetValue()
    {
        print($"Current slider value is: {slider.value}");
    }
}
