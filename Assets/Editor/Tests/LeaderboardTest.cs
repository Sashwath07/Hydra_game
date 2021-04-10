using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class LeaderboardTest
{
    [UnityTest]
    public IEnumerator CheckOutputFields(){
        var leaderboard = new GameObject();
        leaderboard.AddComponent<LeaderboardGenerator>();
        TMP_Text firstPlace = leaderboard.GetComponent<LeaderboardGenerator>().firstPlace;
        TMP_Text secondPlace = leaderboard.GetComponent<LeaderboardGenerator>().secondPlace;
        TMP_Text thirdPlace = leaderboard.GetComponent<LeaderboardGenerator>().thirdPlace;
        TMP_Text fourthPlace = leaderboard.GetComponent<LeaderboardGenerator>().fourthPlace;
        TMP_Text fifthPlace = leaderboard.GetComponent<LeaderboardGenerator>().fifthPlace;
        TMP_Text sixthPlace = leaderboard.GetComponent<LeaderboardGenerator>().sixthPlace;
        TMP_Text seventhPlace = leaderboard.GetComponent<LeaderboardGenerator>().seventhPlace;
        TMP_Text eightPlace = leaderboard.GetComponent<LeaderboardGenerator>().eightPlace;
        TMP_Text ninthPlace = leaderboard.GetComponent<LeaderboardGenerator>().ninthPlace;
        TMP_Text tenthPlace = leaderboard.GetComponent<LeaderboardGenerator>().tenthPlace;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Assert.IsNull(firstPlace);
        Assert.IsNull(secondPlace);
        Assert.IsNull(thirdPlace);
        Assert.IsNull(fourthPlace);
        Assert.IsNull(fifthPlace);
        Assert.IsNull(sixthPlace);
        Assert.IsNull(seventhPlace);
        Assert.IsNull(eightPlace);
        Assert.IsNull(ninthPlace);
        Assert.IsNull(tenthPlace);
    }
}
