using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    //public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public Text waveCountdownText;
    private int waveIndex = 0;
    public static bool isWaveDone = true;

    void Update()
    {

        if (countdown <= 0f)
        {
            isWaveDone = false;
            Debug.Log("New wave!");
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        if (!isWaveDone)
        {
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];
        Debug.Log("Wave " + (waveIndex+1));
        for (int i = 0; i < wave.count.Length; i++)
        {
            for (int j = 0; j < wave.count[i]; j++)
            {
                SpawnEnemy(wave.enemy[i]);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        isWaveDone = true;
        PlayerStats.Money += 100;
        waveIndex++;

        Debug.Log("Wave " + waveIndex + " completed!");

        if (waveIndex == waves.Length)
        {
            Debug.Log("Level Completed!");
            GameManager.LevelCompleted = true;
            this.enabled = false;

        }

    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;

    }
}