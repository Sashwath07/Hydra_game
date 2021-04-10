using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTest
{
//     [UnityTest]
//     public IEnumerator CheckEnemy(){
//         var enemy = new GameObject();
//         enemy.AddComponent<Enemy>();
//         enemy.AddComponent<Waypoints>();
//         enemy.GetComponent<Enemy>().TakeDamage(10);
//         yield return null;
//         float health = enemy.GetComponent<Enemy>().health;
//         Assert.AreEqual(90, health);
//     }

    [Test]
    public void CheckEnemyStartHealth(){
        var enemy = new GameObject();
        enemy.AddComponent<Enemy>();
        int startHealth = enemy.GetComponent<Enemy>().startHealth;
        Assert.AreEqual(100, startHealth);
    }
}
