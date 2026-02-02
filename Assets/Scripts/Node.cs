using System.Collections.Generic;
using System.Linq;
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
        SetQuestType();
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
        
    }

    private void SetQuestType()
    {
        influence = EventBus.Instance.GetComponent<GetValueFromDropdown>().GetDropdownValue().ToUpper();

        TypeRandomizer();
    }

    private void TypeRandomizer()
    {
        List<string> availableQuestTypes = new();

        // add the relative quest types

        if (influence == "Adventure".ToUpper())
        {

        }
        else if (influence == "MMO")
        {

        }
        else if (influence == "RPG")
        {

        }
        else if (influence == "Strategy".ToUpper())
        {

        }
    }

}
