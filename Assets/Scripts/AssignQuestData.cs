using UnityEngine;

public class AssignQuestData : MonoBehaviour
{
    private string questType = "ADWAD";
    private string questDescription = "RAAAAAH";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Instance.OnNodeCreated += Test;
        //EventBus.Instance.OnNodeCreated += GetInfluence;
        //EventBus.Instance.OnNodeCreated += TypeRandomizer;
    }

    private string GetQuestType()
    {
        return questType;
    }

    private string GetQuestDescription()
    {
        return questDescription;
    }

    private void Test()
    {
        print("Node was birthed");
    }

    private void GetInfluence()
    {
        questType = EventBus.Instance.GetComponent<GetValueFromDropdown>().GetDropdownValue().ToUpper();

    }

    // detect what the quest influence is
    // read the JSON and find matching influence section, this determines available quest types
    // set the randomized quest type
    private void TypeRandomizer()
    {

        TextAsset json = Resources.Load<TextAsset>("quest_types");
        string jsonText = json.text;

        print(jsonText);


        // add the relative quest types

        if (questType == "Adventure".ToUpper())
        {
            Quest quest = JsonUtility.FromJson<Quest>(json.text);

            Debug.Log(quest.types);

            foreach (QuestType qt in quest.types)
            {
                Debug.Log(qt.name);
            }
        }
        else if (questType == "MMO")
        {

        }
        else if (questType == "RPG")
        {

        }
        else if (questType == "Strategy".ToUpper())
        {

        }
    }
}
