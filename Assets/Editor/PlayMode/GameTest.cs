using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTest
{
    [UnityTest]
    public IEnumerator CheckEnemy(){
        var enemy = new GameObject();
        enemy.AddComponent<Enemy>();
        yield return null;
        enemy.GetComponent<Enemy>().TakeDamage(10);
        Assert.AreEqual(90, enemy.GetComponent<Enemy>().health);
    }
}
