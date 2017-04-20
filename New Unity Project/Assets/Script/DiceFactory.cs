using UnityEngine;
using System.Collections;

public class DiceFactory  {

    public static GameObject createDice() {
        GameObject dice = MonoBehaviour.Instantiate(Resources.Load("DiceBase")) as GameObject;
        
        for (int i = 0; i < 6; i++) {
            GameObject face = dice.transform.GetChild(i).gameObject;
            face.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/"+(i+1));
        }
        return dice;
    }
}