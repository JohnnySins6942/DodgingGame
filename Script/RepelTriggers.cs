using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelTriggers : MonoBehaviour
{
    private MovementScript movement;

    private void Start()
    {
        movement = FindObjectOfType<MovementScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            movement.isDead = true;
            movement.gameObject.SetActive(false);
        }
    }
}
