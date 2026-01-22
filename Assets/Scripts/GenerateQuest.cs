using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateQuest : MonoBehaviour
{
    [SerializeField] private GetValueFromDropdown questInfluence;
    [SerializeField] private GetValueFromSlider questLength;
    

    public void Generate()
    {
        print($"Generating quest in the style of {questInfluence.GetDropdownValue().ToUpper()} with a length of {questLength.GetSliderValue()}.");
    }

}


//using Newtonsoft.Json.Linq;
//using UnityEngine;

//public class CharacterClass
//{
//    public int sprite;
//    public string health;
//    public string mana;
//    public string mana_regeneration;
//    public string spellpower;
//    public string speed;

//    public CharacterClass(JObject json)
//    {
//        sprite = (int)json["sprite"];
//        health = json["health"]?.ToString();
//        mana = json["mana"]?.ToString();
//        mana_regeneration = json["mana_regeneration"]?.ToString();
//        spellpower = json["spellpower"]?.ToString();
//        speed = json["speed"]?.ToString();
//    }
//}