using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadLiderboard : MonoBehaviour
{
    public static List<List<string>> Leaderboard = new List<List<string>>();

    public void Start()
    {
        Leaderboard = DatabaseCommunicator.LoadLeaderboard();

        for (int i = 0; i < Leaderboard.Count; i++)
        {
            GameObject score = Instantiate(Resources.Load<GameObject>("Prefabs/LoadLeaderboard"), transform);

            score.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{Leaderboard[i][0]}";
            score.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = $"{i + 1}";
            score.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{Leaderboard[i][1]}";
        }
    }
}
