using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SludgeBehavior : MonoBehaviour
{
    private Rigidbody2D thisbody;
    Vector3 dir;

    void Start()
    {
        thisbody = gameObject.GetComponent<Rigidbody2D>();
        dir = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
    }
    void Update()
    {
        thisbody.MovePosition(gameObject.transform.position + (dir * Time.deltaTime));
    }
}
