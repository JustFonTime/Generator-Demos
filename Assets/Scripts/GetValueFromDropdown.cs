using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public void GetDropdownValue()
    {
        int selectedOptionIndex = dropdown.value;
        string selectedOptionName = dropdown.options[selectedOptionIndex].text;

        print($"User selected '{selectedOptionName.ToUpper()}' from the {gameObject.name} list");

    }

}
