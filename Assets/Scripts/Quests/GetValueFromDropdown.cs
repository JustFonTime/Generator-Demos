using TMPro;
using UnityEngine;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private int selectedOptionIndex;
    private string selectedOptionName;

    public void SetDropdownValue()
    {
        selectedOptionIndex = dropdown.value;
        selectedOptionName = dropdown.options[selectedOptionIndex].text;

        //print($"User selected '{selectedOptionName.ToUpper()}' from the {gameObject.name} list");

    }

    /// <summary>
    /// Returns the value of the current dropdown, if no value was selected it will return the from the 0th index
    /// </summary>
    public string GetDropdownValue()
    {
        if (selectedOptionIndex == 0)
        {
            selectedOptionName = dropdown.options[0].text;
        }

        return selectedOptionName;
    }


}
