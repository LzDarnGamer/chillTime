using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static int ROW = 3;
    public static int COL = 6;

    [Header("Login Page")]
    [SerializeField] Text inputtext;
    [Header("Panels")]
    [SerializeField] GameObject GamePage;
    [SerializeField] GameObject LoginPage;
    [Header("Player Info")]
    [SerializeField] Text playerName;
    [SerializeField] Text ntries;
    [SerializeField] Text ntime;
    [Header("UI")]
    [SerializeField] GameObject card;
    [SerializeField] GameObject slots;
    [SerializeField] GameObject doneSlots;
    [SerializeField] GameObject blackCard;
    [SerializeField] GameObject finish;



    private Board board;
    private Player player;
    private Connector[] flippedCards;
    

    private bool Wait;
    private int amountClicked, MaxTrios;

    private void Start() {
        Wait = false;
        flippedCards = new Connector[3];
    }


    public void Login() {
        player = Serializer.ReadFromFile(inputtext.text);
        Initialize();
    }

    private void Initialize() {
        board = player.board;
        playerName.text = player.Playername;
        ntries.text = player.Moves.ToString();
        MaxTrios = player.board.col * player.board.row / 3;
        Populate();
        InvokeRepeating("Timer", 0, 1.0f);
    }

    private void Timer() {
        player.TimeSeconds++;
        int seconds = player.TimeSeconds % 60;
        double minute = Math.Floor(player.TimeSeconds / 60f);
        
        ntime.text = minute.ToString("00") + ":" + seconds.ToString("00");
    }


    public void Populate() {
        int counter = 0;
        for (int i = 0; i < MaxTrios; i++) {
            GameObject obj = Instantiate(blackCard, doneSlots.transform);
        }
        for (int i = 0; i < board.GameBoard.GetLength(0); i++) {
            for (int j = 0; j < board.GameBoard.GetLength(1); j++) {
                Debug.Log(board.GameBoard[i, j].IsDone);
                counter++;
                GameObject obj = Instantiate(card, slots.transform);
                int x = i;
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => { OnClick(obj, x, y); });
                if (board.GameBoard[i, j].IsDone) {
                    obj.GetComponent<Image>().sprite = board.GameBoard[i, j].Icon;
                    obj.GetComponent<Button>().interactable = false;
                    if (counter % 3 == 0) {
                        FillDoneSlot(board.GameBoard[i, j].Icon);
                    }
                }

            }
        }



    }


    public void dePopulate() {
        foreach (Transform child in slots.transform) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in doneSlots.transform) {
            Destroy(child.gameObject);
        }
    }

    public void OnClick(GameObject obj, int x, int y) {
        if (!Wait) {
            Card card = board.GameBoard[x, y];
            
            obj.GetComponent<Image>().sprite = card.Icon;
            obj.GetComponent<Button>().interactable = false;

            flippedCards[amountClicked++] = new Connector(card, obj);
            if (amountClicked == 3) {
                StartCoroutine(WaitToFlip());
            }
        }
    }


    private IEnumerator WaitToFlip() {
        Wait = true;
        ntries.text = (++player.Moves).ToString();
        if (CheckIfMatch()) {
            TurnDoneCards();
            FillDoneSlot(flippedCards[0].card.Icon);
            clearFlippedCards();
            amountClicked = 0;
            if (GameEnded()) {
                CancelInvoke();
                player.hasEndedGame = true;
                player.Highscore = CalculateHighScore();
                Serializer.WriteToFile(player);
                dePopulate();
                finish.SetActive(true);
            }
            yield return new WaitForSeconds(0.1f);
        } else {
            yield return new WaitForSeconds(0.5f);
            TurnFlippedCards();
            clearFlippedCards();
            amountClicked = 0;
        }
        Wait = false;
    }

    private bool GameEnded() {
        for (int i = 0; i < board.GameBoard.GetLength(0); i++) {
            for (int j = 0; j < board.GameBoard.GetLength(1); j++) {
                if (!board.GameBoard[i, j].IsDone) {
                    return false;
                }
            }
        }
        return true;
    }
    private void TurnDoneCards() {
        for (int i = 0; i < flippedCards.Length; i++) {
            board.GameBoard[flippedCards[i].card.x, flippedCards[i].card.y].IsDone = true;
        }
    }

    private void FillDoneSlot(Sprite icon) {
        for (int i = 0; i < doneSlots.transform.childCount; i++) {
            if(doneSlots.transform.GetChild(i).GetComponent<Image>().sprite == null) {
                doneSlots.transform.GetChild(i).GetComponent<Image>().sprite = icon;
                break;
            }
        }
    }

    private void TurnFlippedCards() {
        for (int i = 0; i < flippedCards.Length; i++) {
            board.GameBoard[flippedCards[i].card.x, flippedCards[i].card.y].Flipped = false;
            flippedCards[i].card.Flipped = false;
            flippedCards[i].obj.GetComponent<Image>().sprite = ResourceLoader.card_back;
            flippedCards[i].obj.GetComponent<Button>().interactable = true;
        }
    }

    private bool CheckIfMatch() {
        return flippedCards[0].card.CardID == flippedCards[1].card.CardID
            && flippedCards[0].card.CardID == flippedCards[2].card.CardID
            && flippedCards[1].card.CardID == flippedCards[2].card.CardID;
    }
    private void clearFlippedCards() {
        Array.Clear(flippedCards, 0, flippedCards.Length);
    }


    private int CalculateHighScore() {
        return (player.Moves * 5) + player.TimeSeconds;
    }


    public void OnApplicationQuit() {
        CancelInvoke();
        player.hasEndedGame = false;
        Serializer.WriteToFile(player);
        dePopulate();
    }
}
