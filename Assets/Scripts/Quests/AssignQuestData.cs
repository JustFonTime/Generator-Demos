using System.Collections.Generic;
using UnityEngine;

public class AssignQuestData : MonoBehaviour
{
    private QuestGenre questGenre;
    private string questGenreAsString;

    [SerializeField] List<QuestGenre> questGenres = new();

    [SerializeField] List<QuestType> selectedQuestTypes;

    private void Start()
    {
        EventBus.Instance.OnAllNodesCreated += AssignData;
        EventBus.Instance.OnRegenerateRequest += ClearData;
    }

    private void AssignData()
    {

        List<GameObject> nodePoints = GetComponent<GenerateQuest>().nodeList;
        DetermineGenre();
        

        if (nodePoints.Count > 0)
        {
            SelectQuestType(nodePoints.Count);

            for (int i = 0; i < nodePoints.Count; i++)
            {
                var node = nodePoints[i].GetComponent<Node>();

                node.influence = questGenreAsString;

                QuestType currType = selectedQuestTypes[0];
                selectedQuestTypes.RemoveAt(0);

                node.questType = currType.name.Substring(currType.name.LastIndexOf("_") + 1);

                node.questDescription = currType.possibleDescriptions[Random.Range(0, currType.possibleDescriptions.Count)];

                node.questReward = $"{ 100 + (100 * (i + EventBus.Instance.questReward.GetSliderValueMult())) }x Coins";

                if(EventBus.Instance.hasQuestBranching.GetToggleValue())
                {
                    if(Random.Range(0, 100) % 2 == 0)
                    {
                        SelectQuestType(1);
                        QuestType currOptionalType = selectedQuestTypes[0];
                        selectedQuestTypes.RemoveAt(0);

                        node.optionalQuestType = "Optional Quest: " + currOptionalType.name.Substring(currOptionalType.name.LastIndexOf("_") + 1);

                        node.optionalQuestDescription = currOptionalType.possibleDescriptions[Random.Range(0, currOptionalType.possibleDescriptions.Count)];
                    }
                }
                else
                {
                    node.optionalQuestType = string.Empty;
                    node.optionalQuestDescription = string.Empty;
                }
            }
        }
    }

    public void DetermineGenre()
    {
        questGenreAsString = GetComponent<GetValueFromDropdown>().GetDropdownValue();

        if (questGenres.Count > 0)
        {
            foreach(var genre in questGenres)
            {
                if (genre != null)
                {
                    var compareString = genre.name.Substring(genre.name.IndexOf("_") + 1);

                    if (questGenreAsString == compareString)
                    {
                        questGenre = genre;
                        questGenreAsString = compareString;
                        break;
                    }
                }
            }
        }
    }


    public void SelectQuestType(int iterationCount)
    {
        for(int i = 0; i < iterationCount; i++)
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

            selectedQuestTypes.Add(tempQuestType);
        }

    }

    public void ClearData()
    {
        selectedQuestTypes.Clear();
    }
}
