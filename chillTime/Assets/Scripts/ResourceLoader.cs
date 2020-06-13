using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoader : MonoBehaviour
{
    public static Sprite card_back { get; private set; }
    public static Sprite card_glow { get; private set; }
    public static Sprite card_mask { get; private set; }

    public static int[] Names { get; private set; }
    public static Dictionary<int, Sprite> icons { get; private set; }


    public void Awake() {
        card_back = Resources.Load<Sprite>("Images/card_back");
        card_glow = Resources.Load<Sprite>("Images/card_glow");
        card_mask = Resources.Load<Sprite>("Images/card_mask");
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/unit_cards");
        
        Names = new int[sprites.Length];
        icons = new Dictionary<int, Sprite>();

        for (int i = 0; i < sprites.Length; i++) {
            Names[i] = i;
            icons.Add(i, sprites[i]);
        }
    }
}
