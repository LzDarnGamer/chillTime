using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
public class Serializer : Object
{
    private static string PATH = "Resources/PlayerData/";

    private StreamReader sr;
    private StreamWriter sw;
    private string localPath;
    private string playername;
    public Serializer(string playername) {
        this.playername = playername;
        localPath = PATH + playername + ".txt";
    }

    public void WriteToFile(Player player, bool hasEnded) {
        string playerName = player.Playername;
        int highscore = player.Highscore;
        Card[,] board = player.board.GameBoard;
        sw = new StreamWriter(localPath, false);
        sw.WriteLine(hasEnded);
        sw.WriteLine(playerName);
        sw.WriteLine(highscore);
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                s.Append(board[i, j]);
            }
        }
    }

    public Player ReadFromFile() {
        if (File.Exists(localPath)) {
            sr = File.OpenText(localPath);
            string s = sr.ReadLine();
            if (s.Equals("true")) {
                string playerName = sr.ReadLine();
                string highscore = sr.ReadLine();
                return new Player(playerName, int.Parse(highscore), new Board(Game.COL, Game.ROW));
            } else {
                string playerName = sr.ReadLine();
                string highscore = sr.ReadLine();
                string board = sr.ReadLine();
                return new Player(playerName, int.Parse(highscore), new Board(board));
            }
        }
        return new Player(playername, 0, new Board(Game.COL, Game.ROW));
    }

}
