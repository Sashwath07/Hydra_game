using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public float speed = 10f;
    public int startHealth = 100;
    public float health;
    public int value = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private Transform target;
    private int waypointIndex = 0;

    void Start(){
        target = Waypoints.points[0];
        health = startHealth;
    }

    public void TakeDamage(int amount){
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0){
            Die();
        }
    }

    void Die(){
        PlayerStats.Money += value;
        Debug.Log("Money increased by $50");
        PlayerStats.GameScore += value * (QuizHandler.Score + 1);
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
        Debug.Log("Enemy destroyed");
    }

    void Update(){
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint(){
        if (waypointIndex >= Waypoints.points.Length - 1){
            EndOfPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    void EndOfPath(){
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        Debug.Log("Enemy destroyed, 1 life lost");
    }
}
