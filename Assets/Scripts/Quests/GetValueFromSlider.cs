using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private enum LengthState
    {
        TINY,
        SHORT,
        MEDIUM,
        LONG,
        HUGE
    }

    private LengthState length;  

    public void SetSliderValue()
    {
        if (slider.value <= 20)
        {
            length = LengthState.TINY;
        }
        else if (slider.value > 20 && slider.value <= 40)
        {
            length = LengthState.SHORT;
        }
        else if (slider.value > 40 && slider.value <= 60)
        {
            length = LengthState.MEDIUM;
        }
        else if (slider.value > 60 && slider.value <= 80)
        { 
            length = LengthState.LONG;
        }
        else if(slider.value > 80)
        {
            length = LengthState.HUGE;
        }

        text.text = length.ToString();

        //print($"User selected a Quest Length of: {length}");
    }

    public string GetSliderValue()
    {
        return length.ToString();
    }


}
