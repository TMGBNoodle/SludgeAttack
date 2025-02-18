using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SludgeBehavior : MonoBehaviour
{
    private Rigidbody2D thisbody;

    public GameObject cleanParticles;

    public SludgeManager manager;
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

    public void clean() {
        if (manager != null) {
            manager.updateSludge(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
