using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class LeaderboardCreator : MonoBehaviour
{
    [SerializeField] GameObject playerRank;
    [SerializeField] GameObject board;
    

     public void GenerateLeaderboard() {
        foreach (Transform child in board.transform) {
            Destroy(child.gameObject);
        }
        List<SerializedPlayer> list = new List<SerializedPlayer>();
        string jsons = PlayerPrefs.GetString("Data");
        string[] strings = jsons.Split('*');
        foreach (var item in strings) {
            list.Add(JsonUtility.FromJson<SerializedPlayer>(item));
        }

        list = list.OrderBy(o => o.Highscore).ToList();


        for (int i = 0; i < list.Count; i++) {
            int seconds = list[i].TimeSeconds % 60;
            double minute = Math.Floor(list[i].TimeSeconds / 60f);

            string time = minute.ToString("00") + ":" + seconds.ToString("00");

            GameObject obj = Instantiate(playerRank, board.transform);
            obj.transform.GetChild(0).GetComponent<Text>().text = i.ToString("00");
            obj.transform.GetChild(1).GetComponent<Text>().text = list[i].Playername;
            obj.transform.GetChild(2).GetComponent<Text>().text = list[i].Highscore.ToString();
            obj.transform.GetChild(3).GetComponent<Text>().text = time;
        }


    }
}
