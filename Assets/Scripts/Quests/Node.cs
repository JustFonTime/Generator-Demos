using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class Node : MonoBehaviour
{
    private string influence = "";
    public string questType = "";
    public string questDescription = "";

    private void Start()
    {
        GetInfluence();
        TypeRandomizer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            //print("Node had a overlap");
            collision.transform.position = RandomNodePosition();
            EventBus.Instance.NodeOverlap();
        }
    }

    private Vector3 RandomNodePosition()
    {
        return new Vector3(Random.Range(-90f, 90f), Random.Range(-45f, 40f), 200f);
    }

    private void GetInfluence()
    {
        influence = EventBus.Instance.GetComponent<GetValueFromDropdown>().GetDropdownValue().ToUpper();

    }

    // detect what the quest influence is
    // read the JSON and find matching influence section, this determines available quest types
    // set the randomized quest type
    private void TypeRandomizer()
    {
        List<string> typeList = new();
        if (influence == "Adventure".ToUpper())
        {
            typeList = new() {"Rescue", "Kill", "Retreive", "Investigate", "Explore", "Stealth", "Gather"};
        }
        else if (influence == "MMO")
        {
            typeList = new() {"Kill", "Gather", "Deliver", "Craft", "Trial", "Fishing", "Cooking", "Boss"};
        }
        else if (influence == "RPG")
        {
            typeList = new() {"Fetch", "Companion", "Escort", "Kill", "Investigate", "Stealth", "Interact", "Bequeath"};
        }

        if(typeList.Count != 0)
        {
            questType = typeList[Random.Range(0, typeList.Count)];
        }
        else
        {
            print("List was EMPTY");
        }
    }

}
