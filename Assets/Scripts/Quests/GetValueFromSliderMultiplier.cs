using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromSliderMultiplier : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private float val = 0f;

    public void SetSliderValueMultiplier()
    {
        if (slider.value <= 15)
        {
            text.text = "1.0x";
            val = 1.0f;
        }
        else if (slider.value > 15 && slider.value <= 30)
        {
            text.text = "1.2x";
            val = 1.2f;
        }
        else if (slider.value > 30 && slider.value <= 45)
        {
            text.text = "1.4x";
            val = 1.4f;
        }
        else if (slider.value > 45 && slider.value <= 60)
        {
            text.text = "1.6x";
            val = 1.6f;
        }
        else if (slider.value > 60 && slider.value <= 75)
        {
            text.text = "1.8x";
            val = 1.8f;
        }
        else if (slider.value > 75)
        {
            text.text = "2.0x";
            val = 2.0f;
        }
    }

    public float GetSliderValueMult()
    {
        return val;
    }
}