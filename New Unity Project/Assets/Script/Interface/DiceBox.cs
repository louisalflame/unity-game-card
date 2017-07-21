using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 待使用骰子顯示
public class DiceBoxInterface {
    public InterfaceController _interface;
    public GameObject _diceBox { get; private set; }
    public GameObject _dicesGround { get; private set; }
    public GameObject _dicesTeam { get; private set; }
    public GameObject _dicesPerson { get; private set; }
    public List<GameObject> _dices { get; private set; }

    private DiceBoxMode _mode;
    public void update() { }

    public DiceBoxInterface(InterfaceController inter) {
        _interface = inter;
        _mode = new DiceBoxNormalMode(this);

        Dictionary<string, GameObject> dict = CanvasFactory.create_BattleScene_DiceBox(_interface.getImageLeftStatus());
        _diceBox = dict["DiceBox"];
        _dicesGround = dict["DicesGround"];
        _dicesTeam = dict["DicesTeam"];
        _dicesPerson = dict["DicesPerson"]; 

        _dices = new List<GameObject>();
    }

    public void clear() {
        foreach (GameObject d in _dices) {
            MonoBehaviour.Destroy(d);
        }
    } 

    public void checkDiceBox(int type) {
        clear();

        _mode = _mode.getNextMode( _mode.getID(), type );
        _mode.setModePosition();
        _mode.showDiceBox( _dices );
    }

    public void shiftButtonPosition(float groundX, float teamX, float personX) {
        _dicesGround.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(groundX, _dicesGround.GetComponent<RectTransform>().anchoredPosition.y);
        _dicesTeam.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(teamX, _dicesTeam.GetComponent<RectTransform>().anchoredPosition.y);
        _dicesPerson.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(personX, _dicesPerson.GetComponent<RectTransform>().anchoredPosition.y);
    } 
    public void shiftTextPosition(float txtX) {
        _dicesGround.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition =
            new Vector2(txtX, _dicesGround.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition.y);
        _dicesTeam.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition =
            new Vector2(txtX, _dicesTeam.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition.y);
        _dicesPerson.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition =
            new Vector2(txtX, _dicesPerson.transform.Find("txt").GetComponent<RectTransform>().anchoredPosition.y);
    }
}

public abstract class DiceBoxMode { 
    public DiceBoxInterface _diceBox { get; protected set; }
    public DiceBoxMode getNextMode(int nowID, int nextID) {
        if (nowID == nextID) return new DiceBoxNormalMode(_diceBox);
        else if (nextID == DiceBoxGroundMode.id)    return new DiceBoxGroundMode(_diceBox);
        else if (nextID == DiceBoxTeamMode.id)      return new DiceBoxTeamMode(_diceBox);
        else if (nextID == DiceBoxPersonMode.id)    return new DiceBoxPersonMode(_diceBox);
        else return this;  
    }
    public abstract int getID();
    public abstract void setModePosition();
    public abstract void showDiceBox( List<GameObject> dices );
}

public class DiceBoxNormalMode : DiceBoxMode {
    public const int id = 0;
    public DiceBoxNormalMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override void setModePosition() {
        _diceBox.shiftButtonPosition(0f, 100f, 200f);
        _diceBox.shiftTextPosition(0f);
    }
    public override void showDiceBox(List<GameObject> diceObjs) { }
}
public class DiceBoxGroundMode : DiceBoxMode {
    public const int id = 1;
    public DiceBoxGroundMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override void setModePosition() {
        _diceBox.shiftButtonPosition(70f, 100f, 130f);
        _diceBox.shiftTextPosition(-60f);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface._battle._playerManager._groundDices._dicesUnused.GetRange(
            0, _diceBox._interface._battle._playerManager._groundDices._useStack);
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesGround.transform.Find("Layout").gameObject, "icon_"+i);
            imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(dices[i].getIconImage());
            imageObj.GetComponent<Image>().SetNativeSize();
            diceObjs.Add(imageObj);
        }
    }
}
public class DiceBoxTeamMode : DiceBoxMode { 
    public const int id = 2;
    public DiceBoxTeamMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override void setModePosition() {
        _diceBox.shiftButtonPosition(-60f, 100f, 130f);
        _diceBox.shiftTextPosition(-60f);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface._battle._playerManager._teamDices._dicesUnused.GetRange(
            0, _diceBox._interface._battle._playerManager._teamDices._useStack);
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesTeam.transform.Find("Layout").gameObject, "icon_"+i);
            imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(dices[i].getIconImage());
            imageObj.GetComponent<Image>().SetNativeSize();
            diceObjs.Add(imageObj);
        }
    }
}
public class DiceBoxPersonMode : DiceBoxMode {
    public const int id = 3;
    public DiceBoxPersonMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override void setModePosition() {
        _diceBox.shiftButtonPosition(-60f, -30f, 130f);
        _diceBox.shiftTextPosition(-60f);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface._battle._playerManager._personDices._dicesUnused.GetRange(
            0, _diceBox._interface._battle._playerManager._personDices._useStack);
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesPerson.transform.Find("Layout").gameObject, "icon_"+i);
            imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(dices[i].getIconImage());
            imageObj.GetComponent<Image>().SetNativeSize();
            diceObjs.Add(imageObj);
        }
    }
}