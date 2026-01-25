using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;

public class RegnerateQuest : MonoBehaviour
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
            print($"User Requested a Regeneration with same Parameters.");
            EventBus.Instance.RegenRequest();
            Regenerate();
        }
    }

    // Determine how many Nodes need to be generated
    public void Regenerate()
    {
        ClearOldNodes();

        // Hide the PromptCanvas
        targetCanvas.enabled = false;
        print($"Regenerating quest in the style of {questInfluence.GetDropdownValue().ToUpper()} with a length of {questLength.GetSliderValue()}.");

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
                nodeCount = Random.Range(1, 3);
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

        // Instansiate our Nodes (plus one to account for exclusive Range)
        for (int i = 0; i < nodeCount + 1; i++)
        {
            Vector3 position = RandomNodePosition();

            print($"new node at {position}, ID {i}");

            var node = Instantiate(nodePrefab, outputContainer.transform);
            node.name = $"Node_{i}";
            nodeList.Add(node);

            node.transform.localPosition = position;
        }
    }

    // Check all nodes if there is any overlap, if so generate a new position
    private void CheckNodeOverlap()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = 0; j < nodeList.Count; j++)
            {
                if (nodeList[i] != null || nodeList[j] != null)
                {
                    if (nodeList[i].transform.localPosition == nodeList[j].transform.localPosition)
                    {
                        nodeList[i].transform.localPosition = RandomNodePosition();
                    }
                }
            }
        }

    }

    private Vector3 RandomNodePosition()
    {
        return new Vector3(Random.Range(-850f, 850f), Random.Range(-400f, 400f));
    }

    // Remove all previously instantiated Nodes
    private void ClearOldNodes()
    {
        if (nodeList.Count > 0)
        {
            foreach (var node in nodeList)
            {
                if (node != null)
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