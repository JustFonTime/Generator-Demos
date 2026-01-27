using UnityEngine;

public class Node : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            print("Node had a overlap");
            collision.transform.position = RandomNodePosition();
        }
    }

    private Vector3 RandomNodePosition()
    {
        return new Vector3(Random.Range(-45f, 45f), Random.Range(-20f, 20f), 100f);
    }

}
