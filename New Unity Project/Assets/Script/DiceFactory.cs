using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiceFactory  {

    public static GameObject createDice() {
        GameObject dice = MonoBehaviour.Instantiate(Resources.Load("DiceBase")) as GameObject;
        //設定6個骰面圖案
        // UP:5 FORWARD:2 RIGHT:3 DOWN:6 BACK:1 LEFT:4
        for (int i = 0; i < 6; i++)
        {
            GameObject face = dice.transform.GetChild(i).gameObject;
            face.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/" + (i + 1));
        }
        return dice;
    }

    public static List<GameObject> createDices(int num=1) {
        List<GameObject> dices = new List<GameObject>();
        for (int n = 0; n < num; n++) {
            GameObject dice = DiceFactory.createDice();
            //設定左下>右上的初始位置
            dice.transform.localPosition = new Vector3(-4 + (float)((n % 3) * 1.5 + (n / 3) * 1.5), 5 + (float)((n / 3) * 1.5), -15 + (float)((n / 3) * 3));
            //設定初始隨機角度
            dice.transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            //設定初始旋轉方向和隨機力度
            dice.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 100, Random.value * 100, Random.value * 100));
            dices.Add(dice);
        }
        return dices;
    }
}