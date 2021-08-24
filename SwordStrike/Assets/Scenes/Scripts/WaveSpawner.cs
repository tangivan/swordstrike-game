using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GenericEnemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    public GameObject boss;
    public Transform bossSpawnPoint;
    public bool isBossLevel;

    public GameObject healthBar;

    private SceneTransitions sceneTransitions;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave (int index)
    {
        currentWave = waves[index];

        for(int i = 0; i< currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }

            GenericEnemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if(i == currentWave.count -1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if(finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length==0)
        {
            finishedSpawning = false;
            if(currentWaveIndex+1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));

            }
            else
            {
                if(isBossLevel){
                    Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                    healthBar.SetActive(true);
                }
                else {
                    this.enabled = false;
                    sceneTransitions.LoadScene("Win");
                }
            }

        }

            
    }
}
