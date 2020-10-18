using UnityEngine;

public enum PowerupType
{
Forcefield,
timeSlow
}
public class Powerup : MonoBehaviour
{
    public PowerupType powerup;
    public GameObject DestroyFX;
    private GameManager manager;
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Rotate();
    }
    void Rotate()
    {
        transform.Rotate(0, 0, -50 * (Time.deltaTime * 3)); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (powerup)
            {
                case PowerupType.Forcefield:
                    var playerscript = collision.gameObject.GetComponent<MovementScript>();
                    playerscript.FORCEFIELD();
                    var FX = Instantiate(DestroyFX, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(gameObject);
                    Destroy(FX, 4);
                    break;
                case PowerupType.timeSlow:
                    manager.UseTimeSlow();
                    var FF = Instantiate(DestroyFX, gameObject.transform.position, gameObject.transform.rotation);
                    Destroy(gameObject);
                    Destroy(FF, 4);
                    break;
            }
        }
    }
}
