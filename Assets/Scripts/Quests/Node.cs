using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class Node : MonoBehaviour
{
    public string influence = "";
    public string questType = "";
    public string questDescription = "";

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

}
