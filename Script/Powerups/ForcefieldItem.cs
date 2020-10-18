using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldItem : MonoBehaviour
{
    public GameObject DestroyFX;
    private GameManager manager;
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            var pp = Instantiate(DestroyFX, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
            manager.Points++;
            Destroy(pp, 3);
        }
    }
}
