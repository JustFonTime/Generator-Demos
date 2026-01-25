using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class GenerateQuest : MonoBehaviour
{
    [SerializeField] private GetValueFromDropdown questInfluence;
    [SerializeField] private GetValueFromSlider questLength;

    [SerializeField] private Canvas targetCanvas;

    [SerializeField] private Canvas outputContainer;
    [SerializeField] private GameObject nodePrefab;

    public List<GameObject> nodeList = new();

    private void Start()
    {
        EventBus.Instance.OnRegenRequest += ClearOldNodes;
    }

    // Determine how many Nodes need to be generated
    public void Generate()
    {
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
                if(node != null)
                {
                    Destroy(node);
                }
            }
        }
    }

}