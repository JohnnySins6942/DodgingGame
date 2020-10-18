using System.Collections;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] Spawns;

    private bool canspawn = true;

    private GameManager manager;
    public float waitTime = 2;
            
    private int SpawnThreshold2;
    private int SpawnThreshold3;
    private int SpawnThreshold4;

    public int SpawnAmount;
    public int PowerUpRequiredAmount;

    public bool isAirStrike;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        waitTime = 2;
        SpawnThreshold2 = Random.Range(25, 35);
        SpawnThreshold3 = Random.Range(75, 90);
        SpawnThreshold4 = Random.Range(150, 250);

        foreach (var item in Spawns)
        {
            var pp = item.GetComponent<SpawnObject>();
            pp.requiredAmount = Random.Range(0, 4);
            pp.SpawnAmount = 0;
        }
        SpawnAmount = 0;
        PowerUpRequiredAmount = Random.Range(10, 30);

    }
    private void LateUpdate()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        if (canspawn)
        {
            canspawn = false;
            yield return new WaitForSeconds(waitTime);
            SpawnItem();
            canspawn = true;
        }
    }
    void SpawnItem()
    {
        if (!isAirStrike)
        {
            int PossibleSpawns = 1;
            if (manager.Points >= SpawnThreshold2 & manager.Points <= SpawnThreshold3)
            {
                PossibleSpawns = 2;
            }
            else if (manager.Points >= SpawnThreshold3 && manager.Points <= SpawnThreshold4)
            {
                PossibleSpawns = 3;
            }
            else if (manager.Points >= SpawnThreshold4)
            {
                PossibleSpawns = 4;
            }
            else
            {
                PossibleSpawns = 1;
            }
            for (int i = 0; i < PossibleSpawns; i++)
            {
                var ranNum = Random.Range(0, Spawns.Length);
                if (SpawnAmount >= PowerUpRequiredAmount)
                {
                    Spawns[ranNum].GetComponent<SpawnObject>().canSpawnPowerUp = true;
                    SpawnAmount = 0;
                    PowerUpRequiredAmount = Random.Range(10, 30);
                }
                Spawns[ranNum].GetComponent<SpawnObject>().SpawnAmount++;
                Spawns[ranNum].SetActive(true);
                SpawnAmount++;
            }
        }
        if(isAirStrike)
        {
            for (int i = 0; i < 1; i++)
            {
                var ranNum = Random.Range(0, Spawns.Length);
                if (SpawnAmount >= PowerUpRequiredAmount)
                {
                    Spawns[ranNum].GetComponent<SpawnObject>().canSpawnPowerUp = true;
                    SpawnAmount = 0;
                    PowerUpRequiredAmount = Random.Range(10, 30);
                }
                Spawns[ranNum].GetComponent<SpawnObject>().SpawnAmount++;
                Spawns[ranNum].SetActive(true);
                SpawnAmount++;
            }
        }
    }
}
