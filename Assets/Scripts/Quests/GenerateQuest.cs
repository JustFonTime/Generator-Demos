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
    private InputAction regenerate;
    private InputAction openMenu;

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
        regenerate = InputSystem.actions.FindAction("Generate");
        openMenu = InputSystem.actions.FindAction("OpenMenu");

        EventBus.Instance.OnNodeOverlapped += HandleOverlap;
    }

    private void Update()
    {
        if (targetCanvas.enabled == false && regenerate.WasReleasedThisFrame())
        {
            ClearAll();
            Generate();
        }

        if (openMenu.WasReleasedThisFrame())
        {
            ClearAll();
            targetCanvas.enabled = true;
        }
    }

    // Determine how many Nodes need to be generated
    public void Generate()
    {
        EventBus.Instance.RegenerateRequest();
        ClearAll();

        
        print($"Generating quest in the style of {eventBusRef.questInfluence.GetDropdownValue().ToUpper()} with a length of {eventBusRef.questLength.GetSliderValue()}.");

        var nodeCount = eventBusRef.questLength.GetSliderValue();

        // Instansiate our Nodes
        for (int i = 0; i < nodeCount; i++)
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
            EventBus.Instance.NodeCreated();

            node.transform.position = position;
        }

        CreateEdges();

        targetCanvas.enabled = false;
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