using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectileType
{
    LeftAlert,
    RightAlert,
    Homing,
    Boulders,
    Random
}
public class SpawnObject : MonoBehaviour
{
    public ProjectileType type;
    public float speed;
    public float Boulderspeed = 1f;
    public GameObject projectile;
    public GameObject Gold;
    public int requiredAmount;
    public int SpawnAmount;
    public GameManager manager;
    public MovementScript player;
    public GameObject[] PowerUps;
    public GameObject[] BoulderPowerUps;
    private bool spawnGold = false;
    public bool canSpawnPowerUp = false;
    public int HomingSpawnRequirement;
    public bool isAirStrike;
    void OnEnable()
    {
        DoAttack();
        if(SpawnAmount >= requiredAmount)
        {
            spawnGold = true;
        }
        gameObject.SetActive(false);
    }

    void DoAttack()
    {
        switch (type)
        {
            case ProjectileType.LeftAlert:
                if(canSpawnPowerUp)
                {
                    var PowerUpIndex = Random.Range(0, PowerUps.Length);
                    var POWERUP = Instantiate(PowerUps[PowerUpIndex], gameObject.transform.position, gameObject.transform.rotation);
                    var POWERUPrb = POWERUP.GetComponent<Rigidbody2D>();
                    POWERUPrb.AddForce(transform.right * speed);
                    Destroy(POWERUP, 10);
                    canSpawnPowerUp = false;
                    return;
                }
                if (!spawnGold)
                {
                    var pp = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                    var Leftrb = pp.GetComponent<Rigidbody2D>();
                    Leftrb.AddForce(transform.right * speed);
                    if (!isAirStrike)
                    {
                        Destroy(pp, 10);
                    }
                    if (!player.isDead && manager.mode != GameMode.Repel)
                        manager.AddPoints(1);
                }
                if(spawnGold)
                {
                    var pp = Instantiate(Gold, gameObject.transform.position, gameObject.transform.rotation);
                    var Leftrb = pp.GetComponent<Rigidbody2D>();
                    Leftrb.AddForce(transform.right * speed);
                    Destroy(pp, 10);
                    SpawnAmount = 0;
                    spawnGold = false;
                }
                break;
            case ProjectileType.RightAlert:
                if (canSpawnPowerUp)
                {
                    var PowerUpIndex = Random.Range(0, PowerUps.Length);
                    var POWERUP = Instantiate(PowerUps[PowerUpIndex], gameObject.transform.position, gameObject.transform.rotation);
                    var POWERUPrb = POWERUP.GetComponent<Rigidbody2D>();
                    POWERUPrb.AddForce(-transform.right * speed);
                    canSpawnPowerUp = false;
                    Destroy(POWERUP, 10);
                    return;
                }
                if (!spawnGold)
                {
                    var Rightpp = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                    var Rightrb = Rightpp.GetComponent<Rigidbody2D>();
                    Rightrb.AddForce(-transform.right * speed);
                    if (!isAirStrike)
                    {
                        Destroy(Rightpp, 10);
                    }
                    if (!player.isDead && manager.mode != GameMode.Repel)
                        manager.AddPoints(1);
                }
                if(spawnGold)
                {
                    var Rightpp = Instantiate(Gold, gameObject.transform.position, gameObject.transform.rotation);
                    var Rightrb = Rightpp.GetComponent<Rigidbody2D>();
                    Rightrb.AddForce(-transform.right * speed);
                    Destroy(Rightpp, 10);
                    SpawnAmount = 0;
                    spawnGold = false;
                }
                break;
            case ProjectileType.Boulders:
                if (canSpawnPowerUp)
                {
                    var PowerUpIndex = Random.Range(0, BoulderPowerUps.Length);
                    var POWERUP = Instantiate(BoulderPowerUps[PowerUpIndex], gameObject.transform.position, gameObject.transform.rotation);
                    var POWERUPrb = POWERUP.GetComponent<Rigidbody2D>();
                    POWERUPrb.gravityScale = Boulderspeed/2;
                    Destroy(POWERUP, 10);
                    canSpawnPowerUp = false;
                    return;
                }
                if (!spawnGold)
                {
                    var BOULDERPP = Instantiate(projectile, transform.position, transform.rotation);
                    var rb = BOULDERPP.GetComponent<Rigidbody2D>();
                    rb.gravityScale = Boulderspeed/2;
                    if (!isAirStrike)
                    {
                        Destroy(BOULDERPP, 10);
                    }
                    if (!player.isDead && manager.mode != GameMode.Repel)
                        manager.AddPoints(1);
                    return;
                }
                if(spawnGold)
                {
                    var BOULDERPP = Instantiate(Gold, transform.position, transform.rotation);
                    var rb = BOULDERPP.GetComponent<Rigidbody2D>();
                    rb.gravityScale = Boulderspeed/2;
                    Destroy(BOULDERPP, 10);
                    SpawnAmount = 0;
                    spawnGold = false;
                }
                break;
            case ProjectileType.Homing:
                if (manager.Points >= HomingSpawnRequirement)
                {
                     Instantiate(projectile, transform.position, transform.rotation);
                }
                break;
        }
    }

}
