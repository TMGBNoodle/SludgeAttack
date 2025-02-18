using System;
using UnityEngine;

public class MopControl : MonoBehaviour
{
    public float followRad = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mainPos = PlayerMovement.Instance.transform.position;
        Vector3 unProcessed = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = new Vector3(unProcessed.x, unProcessed.y, 0);
        transform.position = mainPos + (mousePos-mainPos).normalized * followRad; 

        Vector3 dir = mousePos - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision with mop");
        print(collision.gameObject.tag);
        if(collision.gameObject.tag == "Cleanable") {
            SludgeBehavior behavior = collision.gameObject.GetComponent<SludgeBehavior>();
            behavior.clean();
        }
    }
}
