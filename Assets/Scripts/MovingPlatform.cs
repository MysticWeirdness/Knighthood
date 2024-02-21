using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 point1;
    private Vector2 point2;
    private Vector2 newPlatformPosition;
    [SerializeField] private bool smoothMovement = false;
    private float xDistance;
    private float yDistance;
    private float movSpeed = 0.25f;
    private float elapsedTime = 0f;

    private void Awake()
    {
        point1 = new Vector2(-4f, 2f);
        point2 = new Vector2(4f, 2f);
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
        float interpolationValue = Mathf.PingPong(timeWithSpeed, 1);
        newPlatformPosition = Vector3.Lerp(point1, point2, interpolationValue);
        transform.position = newPlatformPosition;
    }

    private void SmoothMovement(float time, float speed)
    {
        float sinedValue = Mathf.Sin(time);
        float interpolationValue = (sinedValue / 2) + 0.5f;
        newPlatformPosition = Vector3.Lerp(point1, point2, interpolationValue);
        transform.position = newPlatformPosition;
    }
}
