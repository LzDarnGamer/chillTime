using UnityEngine;

public class Connector : Object
{
    public Card card;
    public GameObject obj;

    public Connector(Card card, GameObject obj) {
        this.card = card;
        this.obj = obj;
    }
}
    