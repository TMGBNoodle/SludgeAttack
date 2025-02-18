using System;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 defaultSpeed = new Vector3(5, 5, 0);
    public float adaptSpeed = 0.01f;

    public Vector3 currentSpeed = new Vector3(0, 0, 0);
    private Rigidbody2D thisBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float goalSpeedY = 0;
        float goalSpeedX = 0;
        if (Input.GetKey(KeyCode.W)) {
            goalSpeedY = defaultSpeed.y;
        }
        if (Input.GetKey(KeyCode.S)) {
            goalSpeedY = defaultSpeed.y * -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            goalSpeedX = defaultSpeed.x * -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            goalSpeedX = defaultSpeed.x;
        }
        float xDif = currentSpeed.x - goalSpeedX;
        float yDif = currentSpeed.y - goalSpeedY;
        float xSpeed;
        float ySpeed;
        if (Math.Abs(xDif) > 0.1f) {
            xSpeed = goalSpeedX;
        } else {
            xSpeed = currentSpeed.x + (Math.Sign(xDif) * adaptSpeed * Time.deltaTime);
        }
        if (Math.Abs(yDif) > 0.1f) {
            ySpeed = goalSpeedY;
        } else {
            ySpeed = currentSpeed.y + (Math.Sign(yDif) * adaptSpeed * Time.deltaTime);
        }
        currentSpeed = new Vector3(xSpeed, ySpeed, 0);
        gameObject.transform.position = gameObject.transform.position + new Vector3(currentSpeed.x * Time.deltaTime, currentSpeed.y * Time.deltaTime, 0);
    }
}
