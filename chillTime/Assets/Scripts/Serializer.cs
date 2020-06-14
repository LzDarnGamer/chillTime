using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class Serializer
{
    private static char SEPARTOR = '*';


    //Biggest spaghetti ever
    public static void WriteToFile(Player player) {
        SerializedPlayer sp = new SerializedPlayer(player);
        if (!PlayerPrefs.HasKey("Data")) {
            string json = JsonUtility.ToJson(sp);
            PlayerPrefs.SetString("Data", json);
            PlayerPrefs.Save();
        } else { 
            string jsons = PlayerPrefs.GetString("Data");

            List<SerializedPlayer> list = new List<SerializedPlayer>();
            string[] strings = jsons.Split(SEPARTOR);
            foreach (var item in strings) {
                list.Add(JsonUtility.FromJson<SerializedPlayer>(item));
            }

            foreach (var item in list) {
                if (item.Playername.Equals(player.Playername) && !item.hasEndedGame) {
                    list.Insert(list.IndexOf(item), sp);
                    list.Remove(item);
                    StringBuilder s = new StringBuilder();

                    SerializedPlayer[] sl = list.ToArray<SerializedPlayer>();
                    for(int i = 0; i < sl.Length; i++) {
                        string temp = JsonUtility.ToJson(sl[i]);
                        if(i != sl.Length-1) s.Append(temp + SEPARTOR);
                        else s.Append(temp);
                    }
                    PlayerPrefs.SetString("Data", s.ToString());
                    PlayerPrefs.Save();
                    return;
                }
            }

            string json = JsonUtility.ToJson(sp);
            string joined = jsons + SEPARTOR + json;

            PlayerPrefs.SetString("Data", joined);
            PlayerPrefs.Save();
        }
    }
    
    public static Player ReadFromFile(string playername) {
        if (PlayerPrefs.HasKey("Data")) {
            List<SerializedPlayer> list = new List<SerializedPlayer>();
            string jsons = PlayerPrefs.GetString("Data");
            string[] strings = jsons.Split(SEPARTOR);
            foreach (var item in strings) {
                list.Add(JsonUtility.FromJson<SerializedPlayer>(item));
            }
            
            foreach (var item in list) {

                if (item.Playername.Equals(playername) && !item.hasEndedGame) {
                    return SerializedPlayer.DeSerialize(item);
                }
            }
        }
        return new Player(playername, new Board(Game.COL, Game.ROW));
    }

}
