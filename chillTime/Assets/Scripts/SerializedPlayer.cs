using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedPlayer
{
    public string Playername;

    public int Highscore;
    public int TimeSeconds;
    public int moves;
    public int col;
    public int row;

    public bool hasEndedGame;

    public int[] boardConfiguration;
    public int[] cardState;

    public SerializedPlayer(Player player) {
        Playername = player.Playername;
        Highscore = player.Highscore;
        TimeSeconds = player.TimeSeconds;
        hasEndedGame = player.hasEndedGame;
        col = player.board.col;
        row = player.board.row;
        moves = player.Moves;

        boardConfiguration = new int[player.board.col * player.board.row];
        cardState = new int[player.board.col * player.board.row];

        int counter = 0;
        for (int i = 0; i < player.board.GameBoard.GetLength(0); i++) {
            for (int j = 0; j < player.board.GameBoard.GetLength(1); j++) {
                boardConfiguration[counter] = player.board.GameBoard[i, j].CardID;
                if(player.board.GameBoard[i, j].IsDone) {
                    cardState[counter] = 1;
                } else {
                    cardState[counter] = 0;
                }
                counter++;
            }
        }
    }
    

    public static Player DeSerialize(SerializedPlayer serializedPlayer) {
        string playername = serializedPlayer.Playername;

        
        Board board = new Board(serializedPlayer.col, serializedPlayer.row, 0);
        int counter = 0;
        for (int i = 0; i < board.GameBoard.GetLength(0); i++) {
            for (int j = 0; j < board.GameBoard.GetLength(1); j++) {
                int id = serializedPlayer.boardConfiguration[counter];
                Card card = new Card(id, ResourceLoader.icons[id], i , j);
                if (serializedPlayer.cardState[counter] == 0) {
                    card.IsDone = false;
                } else {
                    card.IsDone = true;
                }
                board.GameBoard[i, j] = card;
                counter++;
            }
        }
        Player p = new Player(playername, board);
        p.Moves = serializedPlayer.moves;
        p.TimeSeconds = serializedPlayer.TimeSeconds;
        return p;
    }

}
