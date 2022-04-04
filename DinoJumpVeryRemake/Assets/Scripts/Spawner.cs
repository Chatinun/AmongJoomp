using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacle;
    [SerializeField] private GameObject[] item;
    private Transform spawnPoint;
    private float time;
    private float itemTime;
    
    public float spawnerTimer = 2f;
    private float obstacleSpawnMostWait;
    private float obstacleSpawnLeastWait;
    private float obstacleRandSpawnTime;
    
    public float itemSpawnerTimer = 15f;
    private float itemSpawnMostWait;
    private float itemSpawnLeastWait;
    private float itemRandSpawnTime;

    public static Spawner instance;
    
    private void Awake()
    {
        instance = this;
        spawnPoint = GetComponent<Transform>();
    }

    private void Start()
    {
        StartCoroutine(obstacleWaitSpawner());
        StartCoroutine(itemWaitSpawner());
    }

    private void Update()
    {
        obstacleSpawnLeastWait = spawnerTimer;
        obstacleSpawnMostWait = spawnerTimer + 0.6f;

        itemSpawnLeastWait = itemSpawnerTimer;
        itemSpawnMostWait = itemSpawnerTimer + 0.6f;

        obstacleRandSpawnTime = Random.Range(obstacleSpawnLeastWait, obstacleSpawnMostWait);
        itemRandSpawnTime = Random.Range(itemSpawnLeastWait, itemSpawnMostWait);

        /*
        if (!PlayerMovement.instance.isDead)
        {
            time += Time.deltaTime;
            itemTime += Time.deltaTime;
            float randSpawnerTimer = Random.Range(spawnerTimer, spawnerTimer - 0.2f);
            if (time > randSpawnerTimer)
            {
                time = 0;
                Instantiate(obstacle[Random.Range(0, 2)], spawnPoint.position, Quaternion.identity);
            }
            if(itemTime > itemSpawnerTimer && !PowerUp.instance.isPowerup)
            {
                itemTime = 0;
                Instantiate(item[Random.Range(0, item.Length)], new Vector3(spawnPoint.position.x,spawnPoint.position.y + Random.Range(2,3), spawnPoint.position.z), Quaternion.identity);
            }
        }
        */

    }

    IEnumerator obstacleWaitSpawner()
    {
        yield return new WaitForSeconds(obstacleRandSpawnTime);

        while (!PlayerMovement.instance.isDead)
        {
            Instantiate(obstacle[Random.Range(0, obstacle.Length)], spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(obstacleRandSpawnTime);
        }
    }

    IEnumerator itemWaitSpawner()
    {
        yield return new WaitForSeconds(itemRandSpawnTime);

        while (!PlayerMovement.instance.isDead)
        {
            yield return new WaitForSeconds(itemRandSpawnTime);
            Instantiate(item[Random.Range(0, item.Length)], new Vector3(spawnPoint.position.x, spawnPoint.position.y + Random.Range(2, 3), spawnPoint.position.z), Quaternion.identity);
        }
    }
}
