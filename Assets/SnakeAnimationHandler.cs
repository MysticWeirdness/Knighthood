using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimationHandler : MonoBehaviour
{
    [SerializeField] private SnakeManager snakeManager;

    public void DetectAnimationEvent()
    {
        snakeManager.HurtPlayer();
    }
}
