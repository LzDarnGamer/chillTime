using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Object
{
    public Card[,] GameBoard { get; private set; }
    public int col { get; private set; }
    public int row { get; private set; }
    private int[] cards { get; set; }
    public Board(int col, int row) {
        if (col * row % 3 != 0) throw new System.Exception();
        this.col = col;
        this.row = row;
        GameBoard = new Card[col, row];
        Initialize();
    }
    public Board(int col, int row, int load) {
        this.col = col;
        this.row = row;
        GameBoard = new Card[col, row];
    }
    private void Initialize() {
        InitialCards();
        //Shuffle();
        int c = 0;
        for (int i = 0; i < GameBoard.GetLength(0); i++) {
            for (int j = 0; j < GameBoard.GetLength(1); j++) {
                GameBoard[i, j] = new Card(cards[c], ResourceLoader.icons[cards[c]], i, j);
                c++;
            }
        }
        
    }

    private void Shuffle() {
        for (int i = 0; i < cards.Length; i++) {
            int idx = Random.Range(i, cards.Length);
            int c = cards[i];
            cards[i] = cards[idx];
            cards[idx] = c;
        }
    }
    

    private void InitialCards() {
        int size = col * row / 3;
        int[] tempcards = new int[size];

        for (int i = 0; i < tempcards.Length; i++) {
            int r = Random.Range(1, ResourceLoader.Names.Length - 1);
            tempcards[i] = r;
            for (int j = 0; j < i; j++) {
                if (tempcards[i] == tempcards[j]) {
                    i--;
                    break;
                }
            }
        }

        cards = new int[tempcards.Length * 3];
        for (int i = 0; i < tempcards.Length; i++) {
            cards[i*3] = tempcards[i];
            cards[i*3+1] = tempcards[i];
            cards[i*3+2] = tempcards[i];

        }
    }

}
