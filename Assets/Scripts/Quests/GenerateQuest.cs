using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor;

public class GenerateQuest : MonoBehaviour
{
    private EventBus eventBusRef;

    [SerializeField] private Canvas targetCanvas;

    [SerializeField] private GameObject outputContainer;

    [Header("Nodes")]
    [SerializeField] private GameObject nodePrefab;
    public List<GameObject> nodeList = new();

    [Header("Edges")]
    [SerializeField] private LineRenderer edgePrefab;
    public List<GameObject> edgeList = new();

    private void Start()
    {
        eventBusRef = EventBus.Instance;
        EventBus.Instance.OnRegenRequest += ClearAll;
        EventBus.Instance.OnNodeOverlapped += HandleOverlap;
    }

    // Determine how many Nodes need to be generated
    public void Generate()
    {
        // Hide the PromptCanvas
        targetCanvas.enabled = false;
        print($"Generating quest in the style of {eventBusRef.questInfluence.GetDropdownValue().ToUpper()} with a length of {eventBusRef.questLength.GetSliderValue()}.");

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

        // Instansiate our Nodes (plus one to account for exclusive Range)
        for (int i = 0; i < nodeCount + 1; i++)
        {
            Vector3 position = RandomNodePosition();

            //print($"new node at {position}, ID {i}");

            var node = Instantiate(nodePrefab, outputContainer.transform);
            node.name = $"Node_{i}";

            if(i == 0)
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
                if(node != null)
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

        for(int i = 0; i < nodeList.Count; i++)
        {
            if (i + 1 >= nodeList.Count)
            {

                break;
            }

            var edge = Instantiate(edgePrefab, outputContainer.transform);
            edgeList.Add(edge.gameObject);

            edge.positionCount = 2;
            edge.SetPosition(0, nodeList[i].transform.position);
            edge.SetPosition(1, nodeList[i+1].transform.position);

            //edge.startColor = Color.pink;
            //edge.endColor = Color.green;
        }

        //print("Finished Generating Nodes");
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

    private void ClearAll()
    {
        ClearOldNodes();
        ClearEdges();
    }

    private void HandleOverlap()
    {
        ClearEdges();
        CreateEdges();
    }
}