using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerControllerSettings playerSettings;

    private bool canMove = false;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove && InputManager.IsClickDownAnything)
        {
            //first move
            canMove = !canMove;
        }
    }

}