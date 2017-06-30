using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 待使用骰子顯示
public class DiceBoxInterface {
    public InterfaceController _interface;
    public GameObject _diceBox { get; private set; }
    public GameObject _dicesGround { get; private set; }
    public GameObject _dicesTeam { get; private set; }
    public GameObject _dicesPerson { get; private set; }
    public List<GameObject> _dices { get; private set; }

    public enum DiceBoxMode { normal = 0, ground, team, person }
    private DiceBoxMode _mode = DiceBoxMode.normal;
    public void update() { 
    }


    public DiceBoxInterface(InterfaceController inter) {
        _interface = inter;

        _diceBox = MonoBehaviour.Instantiate(Resources.Load("DiceBox") as GameObject);
        _diceBox.transform.localPosition = Position.getVector3(Position.diceBoxPosition);

        _dicesGround = _diceBox.transform.Find("dicesGround").gameObject;
        _dicesGround.GetComponent<Button>().ButtonID = StringCoder.getDiceBoxString(1);
        _dicesGround.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _dicesGround.GetComponent<SpriteRenderer>().sortingLayerID;
        _dicesGround.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _dicesGround.GetComponent<SpriteRenderer>().sortingOrder + 1;

        _dicesTeam = _diceBox.transform.Find("dicesTeam").gameObject;
        _dicesTeam.GetComponent<Button>().ButtonID = StringCoder.getDiceBoxString(2);
        _dicesTeam.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _dicesTeam.GetComponent<SpriteRenderer>().sortingLayerID;
        _dicesTeam.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _dicesTeam.GetComponent<SpriteRenderer>().sortingOrder + 1;

        _dicesPerson = _diceBox.transform.Find("dicesPerson").gameObject;
        _dicesPerson.GetComponent<Button>().ButtonID = StringCoder.getDiceBoxString(3);
        _dicesPerson.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _dicesPerson.GetComponent<SpriteRenderer>().sortingLayerID;
        _dicesPerson.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _dicesPerson.GetComponent<SpriteRenderer>().sortingOrder + 1;

        _dices = new List<GameObject>();
    }

    public void clear() {
        foreach (GameObject d in _dices) {
            MonoBehaviour.Destroy(d);
        }
    }

    public void showDiceBox(List<Dice> diceUnused) {
        clear();
        /*
        for (int i = 0; i < diceUnused.Count; i++) {
            GameObject dice = DiceFactory.createDice2D();
            dice.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( diceUnused[i].getIconImage() );
            dice.transform.localPosition = new Vector3(-8,-4+i*1.5f,-10);
            _dices.Add( dice );
        }
        */
    }

    public void checkDiceBox(int type) {
        switch (type) {
            default: break;
            case (int)DiceBoxMode.ground :
                _mode = (_mode == DiceBoxMode.ground) ? DiceBoxMode.normal : DiceBoxMode.ground;
                break;
            case (int)DiceBoxMode.team:
                _mode = (_mode == DiceBoxMode.team) ? DiceBoxMode.normal : DiceBoxMode.team;
                break;
            case (int)DiceBoxMode.person:
                _mode = (_mode == DiceBoxMode.person) ? DiceBoxMode.normal : DiceBoxMode.person;
                break;
        }
        _dicesGround.transform.localPosition = Position.getVector3(Position.diceBoxModePosition[(int)_mode][0]);
        _dicesGround.transform.Find("text").localPosition =
            (_mode == DiceBoxMode.normal) ? Position.getVector3(Position.diceBoxLabelOriginPosition) : Position.getVector3(Position.diceBoxLabelCheckPosition);
        _dicesTeam.transform.localPosition = Position.getVector3(Position.diceBoxModePosition[(int)_mode][1]);
        _dicesTeam.transform.Find("text").localPosition =
            (_mode == DiceBoxMode.normal) ? Position.getVector3(Position.diceBoxLabelOriginPosition) : Position.getVector3(Position.diceBoxLabelCheckPosition);
        _dicesPerson.transform.localPosition = Position.getVector3(Position.diceBoxModePosition[(int)_mode][2]);
        _dicesPerson.transform.Find("text").localPosition =
            (_mode == DiceBoxMode.normal) ? Position.getVector3(Position.diceBoxLabelOriginPosition) : Position.getVector3(Position.diceBoxLabelCheckPosition);
    }
}