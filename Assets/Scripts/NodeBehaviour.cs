using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            collision.transform.localPosition = RandomNodePosition();
        }
    }

    private Vector3 RandomNodePosition()
    {
        return new Vector3(Random.Range(-850f, 850f), Random.Range(-400f, 400f));
    }

}
