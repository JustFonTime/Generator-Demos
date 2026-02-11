using UnityEngine;

public class Node : MonoBehaviour
{
    public string influence = "";
    public string questType = "";
    public string questDescription = "";
    public string questReward = "";

    public string optionalQuestType = "";
    public string optionalQuestDescription = "";

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
