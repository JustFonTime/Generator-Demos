using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class AssignQuestData : MonoBehaviour
{
    public QuestGenre questGenre;
    [SerializeField] string selectedQuestGenre;

    [SerializeField] List<QuestGenre> questGenres = new();

    [SerializeField] List<QuestType> selectedQuestType;

    private void Start()
    {
        EventBus.Instance.OnNodeCreated += GenerateData;
        EventBus.Instance.OnRegenerateRequest += ClearData;
    }

    public void GenerateData()
    {
        List<GameObject> nodePoints = GetComponent<GenerateQuest>().nodeList;
        DetermineGenre();
        DetermineWeight();

        if (nodePoints.Count > 0)
        {
            foreach (var node in nodePoints)
            {
                if (node != null)
                {
                    node.GetComponent<Node>().influence = selectedQuestGenre;
                }
            }
        }
    }
   
    public void DetermineGenre()
    {
        selectedQuestGenre = GetComponent<GetValueFromDropdown>().GetDropdownValue();

        if (questGenres.Count > 0)
        {
            foreach(var genre in questGenres)
            {
                if (genre != null)
                {
                    var compareString = genre.name.Substring(genre.name.IndexOf("_") + 1);

                    if (selectedQuestGenre == compareString)
                    {
                        questGenre = genre;
                        selectedQuestGenre = compareString;
                        break;
                    }
                }
            }
        }
    }


    public void DetermineWeight()
    {
        float totalWeight = 0;
        QuestType tempQuestType = null;

        foreach (var type in questGenre.questTypes)
        {
            totalWeight += 1f / type.weight;
        }

        float randomWeight = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var type in questGenre.questTypes)
        {
            cumulativeWeight += 1f / type.weight;

            if (randomWeight <= cumulativeWeight)
            {
                tempQuestType = type;
                break;
            }
        }

        selectedQuestType.Add(tempQuestType);

    }


    public void ClearData()
    {
        selectedQuestType.Clear();
    }
}
