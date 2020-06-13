using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object {
    public string Playername { get; private set; }
    public int Highscore { get; private set; }

    public Board board { get; private set; }
    
    public Player(string Playername, int Highscore, Board board) {
        this.Playername = Playername;
        this.Highscore = Highscore;
        this.board = board;
    }


    public void SetHighScore(int score) {
        if (score > Highscore) Highscore = score;
    }

}
