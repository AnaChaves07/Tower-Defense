using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributs")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    void Start()
    {
       StartCoroutine(StartWave()); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftSpawn == 0)
        {
            EndWave();
        }
        
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--; 
    }

    private IEnumerator StartWave()
    {
      yield return new WaitForSeconds(timeBetweenWaves);
    isSpawning = true;
        enemiesLeftSpawn = EnemiesPerWave();

    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        StartCoroutine(StartWave());
        currentWave++;
    }

    private void SpawnEnemy()
    {
     GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies*Mathf.Pow(currentWave, difficultScalingFactor));
    }
}
