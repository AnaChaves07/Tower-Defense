using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawn : MonoBehaviour // Gerencia o spawn dos inimigos
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributs")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f; 

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftSpawn;
    private float eps;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    void Start()//Inicia o controle de ondas.
    {
       StartCoroutine(StartWave());
       // SpawnEnemy();
    }
    void Update()//Controla a lógica de spawn de inimigos.
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        while (timeSinceLastSpawn >= (1f / eps) && enemiesLeftSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftSpawn--;
       //   enemiesAlive++;
            timeSinceLastSpawn -= (1f / eps);
        }

        if(enemiesAlive == 0 && enemiesLeftSpawn == 0)
        {
            EndWave();
        }
        
    }
    private void EnemyDestroyed()//Atualiza o número de inimigos vivos.
    {
        enemiesAlive--; 
    }
    private IEnumerator StartWave()//Controla o início de uma nova onda.
    {
        yield return new WaitForSeconds(timeBetweenWaves);
         isSpawning = true;
        enemiesLeftSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();

    }
    private void EndWave()// Finaliza a onda atual e inicia a próxima.
    {
        isSpawning = false;
      
        StartCoroutine(StartWave());
        currentWave++;
    }

    private void SpawnEnemy()//Instancia um novo inimigo.
    {

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        enemiesAlive++;
    
    }
    private int EnemiesPerWave()//Calcula o número de inimigos por onda.
    {
        return Mathf.RoundToInt(baseEnemies*Mathf.Pow(currentWave, difficultScalingFactor));
    }

   private int EnemiesPerSecond() //Calcula a taxa de spawn de inimigos.
    {
     return (int)Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultScalingFactor), 0f, enemiesPerSecondCap);
    }
}
