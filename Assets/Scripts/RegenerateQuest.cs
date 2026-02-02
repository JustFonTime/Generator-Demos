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
    private EventBus eventBusRef;
    [SerializeField] private Canvas targetCanvas;

    [SerializeField] private GameObject outputContainer;
    [SerializeField] private GameObject nodePrefab;

    private InputAction regenerate;

    private List<GameObject> nodeList = new();

    [Header("Edges")]
    [SerializeField] private LineRenderer edgePrefab;
    public List<GameObject> edgeList;

    private void Start()
    {
        eventBusRef = EventBus.Instance;
        regenerate = InputSystem.actions.FindAction("Generate");

        eventBusRef.OnNodeOverlapped += HandleOverlap;
    }

    private void Update()
    {
        if (regenerate.WasReleasedThisFrame())
        {
            //print($"User Requested a Regeneration with same Parameters.");
            eventBusRef.RegenRequest();
            Regenerate();
        }
    }

    // Determine how many Nodes need to be generated
    public void Regenerate()
    {
        ClearOldNodes();
        ClearEdges();

        // Hide the PromptCanvas
        targetCanvas.enabled = false;
        print($"Regenerating quest in the style of {eventBusRef.questInfluence.GetDropdownValue().ToUpper()} with a length of {eventBusRef.questLength.GetSliderValue()}.");

        // Instantiate 
        /*
         * TINY = 1 to 3
         * SHORT = 3 to 5
         * MEDIUM = 6 to 9
         * LONG = 10 to 12
         * HUGE = 15 to 20
        */
        var nodeCount = 0;

        switch (eventBusRef.questLength.GetSliderValue())
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

            //print($"new node at {position}, ID {i}");

            var node = Instantiate(nodePrefab, outputContainer.transform);
            node.name = $"Node_{i}";

            if (i == 0)
            {
                node.GetComponentInChildren<TextMeshPro>().text = "START";
            }
            else if (i == nodeCount)
            {
                node.GetComponentInChildren<TextMeshPro>().text = "END";
            }
            else
            {
                node.GetComponentInChildren<TextMeshPro>().text = $"Node{i}";
            }

            nodeList.Add(node);

            node.transform.position = position;
        }

        CreateEdges();
    }

    private Vector3 RandomNodePosition()
    {
        return new Vector3(Random.Range(-90f, 90f), Random.Range(-45f, 40f), 200f);
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

        nodeList.Clear();
    }

    private void CreateEdges()
    {
        edgePrefab.positionCount = 0;

        for (int i = 0; i < nodeList.Count; i++)
        {
            var edge = Instantiate(edgePrefab, outputContainer.transform);
            edgeList.Add(edge.gameObject);

            // node should only go from one to the next if possible
            if (i + 1 >= nodeList.Count)
            {
                break;
            }
            else
            {
                edge.positionCount = 2;

                edge.SetPosition(0, nodeList[i].transform.position);
                edge.SetPosition(1, nodeList[i + 1].transform.position);

            }
        }

        //print("Finished Regenerating Nodes");
    }

    private void ClearEdges()
    {
        if (edgeList.Count > 0)
        {
            foreach (var edge in edgeList)
            {
                if (edge != null)
                {
                    Destroy(edge);
                }
            }
        }

        edgeList.Clear();
    }

    private void HandleOverlap()
    {
        ClearEdges();
        CreateEdges();
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