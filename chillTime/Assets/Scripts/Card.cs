using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Object {
  
    public int CardID { get; private set; }
    public Sprite Icon { get; private set; }

    public Card(int CardID, Sprite Icon) {
        this.CardID = CardID;
        this.Icon = Icon;
    }

}
