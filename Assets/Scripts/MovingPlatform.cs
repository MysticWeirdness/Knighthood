using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 point1;
    private Vector2 point2;
    private Vector2 newPlatformPosition;
    private bool smoothMovement = false;
    private bool firstPoint2SecondPoint = true;
    private float sinedValue = 0f;
    private float xDistance;
    private float yDistance;
    private float movSpeed = 0.5f;
    private float elapsedTime = 0f;

    private void Awake()
    {
        point1 = new Vector2(-3f, -3f);
        point2 = new Vector2(3f, 4f);
        xDistance = Mathf.Abs(point1.x - point2.x);
        yDistance = Mathf.Abs(point1.y - point2.y);
    }

    private void Start()
    {
        transform.position = point1;
    }

    private void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (!smoothMovement)
        {
            LinearMovement(elapsedTime, movSpeed);
        }
        else if (smoothMovement)
        {
            SmoothMovement(elapsedTime, movSpeed);
        }
    }

    private void LinearMovement(float time, float speed)
    {
        float timeWithSpeed = time * speed;
        float interpolationValue = timeWithSpeed % 1f;
        if (firstPoint2SecondPoint)
        {
            newPlatformPosition = Vector3.Lerp(point1, point2, interpolationValue);
        }
        else
        {
            newPlatformPosition = Vector3.Lerp(point2, point1, interpolationValue);
        }
        if(transform.position.x == point2.x && transform.position.y == point2.y)
        {
            firstPoint2SecondPoint = false;
        }
        else if (transform.position.x == point1.x && transform.position.y == point1.y)
        {
            firstPoint2SecondPoint = true;
        }
        transform.position = newPlatformPosition;
    }

    private void SmoothMovement(float time, float speed)
    {
        sinedValue = Mathf.Sin(time);

    }
}
