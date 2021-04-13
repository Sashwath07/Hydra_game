using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BlueprintTest
{
    [Test]
    public void BlueprintTestSimplePasses()
    {
        // Use the Assert class to test conditions
        var blueprint = new TurretBlueprint();

        blueprint.cost = 100;

        Assert.AreEqual(blueprint.cost / 2, blueprint.GetSellAmount());
    }
}
