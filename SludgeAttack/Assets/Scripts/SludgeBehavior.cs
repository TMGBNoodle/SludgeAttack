using UnityEngine;

public class SludgeBehavior : MonoBehaviour
{


    private Rigidbody2D thisbody;

    public GameObject cleanParticles;

    public SludgeManager manager;
    Vector3 dir;

    private AudioSource audioSource;

    void Start()
    {
        thisbody = gameObject.GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        audioSource.enabled = true;

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

            

            audioSource.Play();

            Destroy(gameObject,0.3f);
        }
    }
}
