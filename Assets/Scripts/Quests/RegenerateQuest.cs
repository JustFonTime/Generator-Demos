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
    private InputAction openMenu;

    private List<GameObject> nodeList = new();

    [Header("Edges")]
    [SerializeField] private LineRenderer edgePrefab;
    public List<GameObject> edgeList;

    private void Start()
    {
        eventBusRef = EventBus.Instance;
        regenerate = InputSystem.actions.FindAction("Generate");
        openMenu = InputSystem.actions.FindAction("OpenMenu");

        eventBusRef.OnNodeOverlapped += HandleOverlap;
    }

    private void Update()
    {
        if (targetCanvas.enabled == false && regenerate.WasReleasedThisFrame())
        {
            eventBusRef.RegenRequest();
            Regenerate();
        }
        
        if(openMenu.WasReleasedThisFrame())
        {
            ClearAll();
            targetCanvas.enabled = true;
        }
    }

    public void Regenerate()
    {
        ClearAll();

        targetCanvas.enabled = false;
        print($"Regenerating quest in the style of {eventBusRef.questInfluence.GetDropdownValue().ToUpper()} with a length of {eventBusRef.questLength.GetSliderValue()}.");

        var nodeCount = eventBusRef.questLength.GetSliderValue();

        // Instansiate our Nodes (plus one to account for exclusive Range)
        for (int i = 0; i < nodeCount + 1; i++)
        {
            Vector3 position = RandomNodePosition();


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
