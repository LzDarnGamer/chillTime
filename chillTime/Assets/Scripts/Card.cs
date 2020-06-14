using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Object {
  
    public int CardID { get; private set; }
    public Sprite Icon { get; private set; }
    public bool Flipped { get; set; }
    public bool IsDone { get; set; }
    public int x { get; private set; }
    public int y { get; private set; }
    public Card(int CardID, Sprite Icon, int x, int y) {
        this.CardID = CardID;
        this.Icon = Icon;
        this.x = x;
        this.y = y;
    }

}
