using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Object {
    public string Playername { get; private set; }
    public int Highscore { get; set; }
    public Board board { get; private set; }
    public int TimeSeconds{ get; set; }
    public int Moves{ get; set; }
    public bool hasEndedGame { get; set; }

    public Player(string Playername, Board board) {
        this.Playername = Playername;
        this.board = board;
        hasEndedGame = false;
    }


    public void SetHighScore(int score) {
        if (score > Highscore) Highscore = score;
    }

}
