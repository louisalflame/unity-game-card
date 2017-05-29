using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 待使用骰子顯示
public class DiceBoxInterface {
    public InterfaceController _interface;
    private List<GameObject> _dices = null;
    public DiceBoxInterface(InterfaceController inter) {
        _interface = inter;
        _dices = new List<GameObject>();
    }

    public void clear() {
        foreach (GameObject d in _dices) {
            MonoBehaviour.Destroy(d);
        }
    }

    public void showDiceBox(List<Dice> diceUnused) {
        clear();
        for (int i = 0; i < diceUnused.Count; i++) {
            GameObject dice = DiceFactory.createDice2D();
            dice.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( diceUnused[i].getIconImage() );
            dice.transform.localPosition = new Vector3(-8,-4+i*1.5f,-10);
            _dices.Add( dice );
        }
    }
    public void update() { }
}