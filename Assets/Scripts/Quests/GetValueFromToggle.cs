using UnityEngine;
using UnityEngine.UI;

public class GetValueFromToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;
    [SerializeField] private bool isToggleOn = true;

    public void SetToggleValue()
    {
        isToggleOn = !isToggleOn;
    }

    public bool GetToggleValue()
    {
        return isToggleOn;
    }
}
