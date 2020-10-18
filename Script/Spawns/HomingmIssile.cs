using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingmIssile : MonoBehaviour
{
    public Transform target;

    public GameObject ExplosionEffect;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb;

    private GameManager manager;
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<MovementScript>().gameObject.transform;
    }
    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();
        
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;

        if(target.gameObject.GetComponent<MovementScript>().isDead)
        {
            Instantiate(ExplosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        if (target.gameObject.GetComponent<MovementScript>().isDead == false)
        {
            manager.Points += 5;
        }
    }
}
