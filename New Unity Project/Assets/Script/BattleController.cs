using UnityEngine;
using System.Collections;

public class BattleController {

    private TurnManager turnManager;
    private InterfaceController interfaceController;

    GameObject cube;

    public BattleController() {
        turnManager = new TurnManager( this, new PrepareTurn(this) );
        interfaceController = new InterfaceController();

        cube = DiceFactory.createDice();
    }

    public void CountResultAndNextTurn(){
        turnManager.nextTurn();
    }
	
    //debug dice
    public void rotateDices() {
        cube.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 20, Random.value * 20, Random.value * 20));
    }
    public void stopDices() {
        cube.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Debug.Log(cube.GetComponent<Rigidbody>().angularVelocity);
    }
}