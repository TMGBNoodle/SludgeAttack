using System;
using System.Xml.Schema;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 maxSpeed = new Vector3(10, 10, 0);
    public Vector3 defaultSpeed = new Vector3(5, 5, 0);
    public float adaptSpeed = 0.01f;
    public float decaySpeed = (float)(1 * Math.Pow(10, -3));

    public float dashMult = 5;

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
        float xDif = goalSpeedX - currentSpeed.x;
        float yDif = goalSpeedY - currentSpeed.y;
        float xSpeed;
        float ySpeed;
        if (Math.Abs(xDif) < 0.1f) {
            xSpeed = goalSpeedX;
        } else if (goalSpeedX != 0){
            if (Math.Sign(goalSpeedX) == Math.Sign(currentSpeed.x)) {
                if (Math.Abs(goalSpeedX) > Math.Abs(currentSpeed.x)) {
                    xSpeed = currentSpeed.x + (Math.Sign(xDif) * decaySpeed * Time.deltaTime);
                } else {
                    xSpeed = currentSpeed.x + (Math.Sign(xDif) * adaptSpeed * Time.deltaTime);
                }
            } else {
                xSpeed = currentSpeed.x + (Math.Sign(xDif) * adaptSpeed * Time.deltaTime);
            }
        } else {
            xSpeed = currentSpeed.x + (Math.Sign(xDif) * decaySpeed * Time.deltaTime);
        }
        if (Math.Abs(yDif) < 0.1f) {
            ySpeed = goalSpeedY;
        } else if (goalSpeedY != 0){
            if (Math.Sign(goalSpeedY) == Math.Sign(currentSpeed.y)) {
                if (Math.Abs(goalSpeedY) > Math.Abs(currentSpeed.y)) {
                    ySpeed = currentSpeed.y + (Math.Sign(yDif) * decaySpeed * Time.deltaTime);
                } else {
                    ySpeed = currentSpeed.y + (Math.Sign(yDif) * adaptSpeed * Time.deltaTime);
                }
            } else {
                ySpeed = currentSpeed.y + (Math.Sign(yDif) * adaptSpeed * Time.deltaTime);
            }
        } else {
            ySpeed = currentSpeed.y + (Math.Sign(yDif) * decaySpeed * Time.deltaTime);
        }
        currentSpeed = new Vector3(xSpeed, ySpeed, 0);
        if (Input.GetKeyDown(KeyCode.Space)) {
            currentSpeed = new Vector3(goalSpeedX * dashMult, goalSpeedY *dashMult, 0);
            //currentSpeed = new Vector3(Math.Max(Math.Min(goalSpeedX + currentSpeed.x, defaultSpeed.x), defaultSpeed.x * -1) * dashMult, Math.Max(Math.Min(goalSpeedY + currentSpeed.y, defaultSpeed.y), defaultSpeed.y * -1) * dashMult, 0);
        }
        thisBody.MovePosition(gameObject.transform.position + new Vector3(currentSpeed.x * Time.deltaTime, currentSpeed.y * Time.deltaTime, 0));
    }
}
