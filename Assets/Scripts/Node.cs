using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class Node : MonoBehaviour
{
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
        questType = EventBus.Instance.GetComponent<GetValueFromDropdown>().GetDropdownValue().ToUpper();

    }

    // detect what the quest influence is
    // read the JSON and find matching influence section, this determines available quest types
    // set the randomized quest type
    private void TypeRandomizer()
    {

        // add the relative quest types

        if (questType == "Adventure".ToUpper())
        {

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
