using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    private Vector3 mousePosition;
    private Rigidbody2D rb;
    public GameObject GoldFX;
    private Vector2 direction;
    public float moveSpeed = 100f;

    public GameObject DeathEffect;

    public GameObject ObjectDeathEffect;
    public bool isDead = false;
    private GameManager manager;

    //powerups
    public GameObject Forcefield;
    public float forcefieldTime = 12f;
    private bool canUseForcefied = true;
    private bool canDie = true;

    public bool isRepel = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButton(0))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePosition - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isRepel)
        {
            if (canDie)
            {
                if (collision.gameObject.tag == "Obstacle")
                {
                    Instantiate(DeathEffect, transform.position, transform.rotation);
                    gameObject.SetActive(false);
                    isDead = true;
                }
            }
        }
        if(isRepel)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                Instantiate(ObjectDeathEffect, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                manager.AddPoints(1);
            }
        }
            if (collision.gameObject.tag == "Gold")
            {
                collision.gameObject.SetActive(false);
                Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1);
                var FX = Instantiate(GoldFX, position, transform.rotation);
                manager.GetCoins(1);
                Destroy(FX, 2);
            }
    }
    public void FORCEFIELD()
    {
        StartCoroutine(UseForcefield());
    }
    IEnumerator UseForcefield()
    {
        if (canUseForcefied)
        {
            canDie = false;
            canUseForcefied = false;
            var renderer = Forcefield.GetComponent<SpriteRenderer>();
            Forcefield.SetActive(true);
            yield return new WaitForSeconds(forcefieldTime-2f * Time.timeScale);
            renderer.enabled = false;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = true;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = false;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = true;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = false;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = true;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = false;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            renderer.enabled = true;
            yield return new WaitForSeconds(.25f * Time.timeScale);
            Forcefield.SetActive(false);
            canUseForcefied = true;
            canDie = true;
        }
    }
}