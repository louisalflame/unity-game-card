using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController {

    private TurnManager turnManager;
    private InterfaceController interfaceController;

    List<GameObject> dices;

    public BattleController() {
        turnManager = new TurnManager( this, new PrepareTurn(this) );
        interfaceController = new InterfaceController();
    }

    public void CountResultAndNextTurn(){
        turnManager.nextTurn();
    }
	
    //debug dice
    public void createDice() {
        dices = DiceFactory.createDices(6);
    }
    public void checkDices() {
        Vector3 up = GameObject.Find("Plane").transform.up;
        foreach (GameObject dice in dices) {
            if( Vector3.Dot(dice.transform.up, up) >= 0.9f){ Debug.Log(5); }
            if (Vector3.Dot(-dice.transform.up, up) >= 0.9f) { Debug.Log(6); }
            if (Vector3.Dot(dice.transform.forward, up) >= 0.9f) { Debug.Log(2); }
            if (Vector3.Dot(-dice.transform.forward, up) >= 0.9f) { Debug.Log(1); }
            if (Vector3.Dot(dice.transform.right, up) >= 0.9f) { Debug.Log(3); }
            if (Vector3.Dot(-dice.transform.right, up) >= 0.9f) { Debug.Log(4); }
        }
    }
    public void collectDices() {
        Vector3 up = GameObject.Find("Plane").transform.up;
        for (int i = 0; i < dices.Count; i++) {
            GameObject dice = dices[i];
            if (Vector3.Dot(dice.transform.up, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0)); }
            else if (Vector3.Dot(-dice.transform.up, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); }
            else if (Vector3.Dot(dice.transform.forward, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0)); }
            else if (Vector3.Dot(-dice.transform.forward, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); }
            else if (Vector3.Dot(dice.transform.right, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); }
            else if (Vector3.Dot(-dice.transform.right, up) >= 0.9f) { dice.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0)); }
            dice.GetComponent<Rigidbody>().useGravity = false;
            dice.transform.localPosition = new Vector3(-4 + 2 * (i % 5), 0 - 2 * (i / 5), -20);
        }
    }
    public void removeDices() { 
        foreach(GameObject dice in dices){
            MonoBehaviour.Destroy(dice);
        }
    }
}