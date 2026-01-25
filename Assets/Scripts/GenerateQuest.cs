using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class GenerateQuest : MonoBehaviour
{
    [SerializeField] private GetValueFromDropdown questInfluence;
    [SerializeField] private GetValueFromSlider questLength;

    [SerializeField] private Canvas targetCanvas;

    [SerializeField] private Canvas outputContainer;
    [SerializeField] private GameObject nodePrefab;

    private InputAction regenerate;

    private List<GameObject> nodeList = new();

    private void Start()
    {
        regenerate = InputSystem.actions.FindAction("Generate");
    }

    private void Update()
    {
        if (regenerate.WasReleasedThisFrame())
        {
            print("User Requested a Regeneration with same Parameters");
            Generate();
        }
    }

    public void Generate()
    {
        ClearOldNodes();

        // Hide the PromptCanvas
        targetCanvas.enabled = false;
        print($"Generating quest in the style of {questInfluence.GetDropdownValue().ToUpper()} with a length of {questLength.GetSliderValue()}.");

        // Instantiate 
        /*
         * TINY = 1 to 3
         * SHORT = 3 to 5
         * MEDIUM = 6 to 9
         * LONG = 10 to 12
         * HUGE = 15 to 20
        */
        var nodeCount = 0;

        switch (questLength.GetSliderValue())
        {
            case "TINY":
                nodeCount = Random.Range(1,3);
                break;
            case "SHORT":
                nodeCount = Random.Range(3, 5);
                break;
            case "MEDIUM":
                nodeCount = Random.Range(6, 9);
                break;
            case "LONG":
                nodeCount = Random.Range(10, 12);
                break;
            case "HUGE":
                nodeCount = Random.Range(15, 20);
                break;
        }

        // plus one to account for exclusive Range
        for (int i = 0; i < nodeCount + 1; i++)
        {
            Vector3 position = new (Random.Range(-850f, 850f), Random.Range(-400f, 400f));

            print($"new node at {position}");

            var node = Instantiate(nodePrefab, outputContainer.transform, true);
            nodeList.Add(node);

            node.transform.position = position;
        }

    }

    public void ClearOldNodes()
    {
        if (nodeList.Count > 0)
        {
            foreach (var node in nodeList)
            {
                if(node != null)
                {
                    Destroy(node);
                }
            }
        }

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