using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class Connector : Object
{
    public Card card;
    public GameObject obj;

    public Connector(Card card, GameObject obj) {
        this.card = card;
        this.obj = obj;
    }
}
    