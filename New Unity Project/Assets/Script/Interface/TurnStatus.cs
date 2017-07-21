using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatus {
    public InterfaceController _interface;

    private GameObject _turnStatus = null;
    private GameObject _turnInfo = null;
    private GameObject _movTurn = null;
    private GameObject _atkTurn = null;
    private GameObject _defTurn = null;

    private GameObject _playerAtkTurn = null;
    private GameObject _playerDefTurn = null;
    private GameObject _enemyAtkTurn = null;
    private GameObject _enemyDefTurn = null;
    
    public TurnStatus(InterfaceController inter) {
        _interface = inter;

        Dictionary<string, GameObject> dict = CanvasFactory.create_BattleScene_TurnStatus(_interface.getImageMiddleBattleField());
        _turnStatus = dict["TurnStatus"];
        _movTurn = dict["MovTurn"];
        _atkTurn = dict["AtkTurn"];
        _defTurn = dict["DefTurn"];
        _turnInfo = dict["TurnInfo"];

        _playerAtkTurn = _atkTurn.transform.Find("Player").gameObject;
        _playerDefTurn = _defTurn.transform.Find("Player").gameObject;
        _enemyAtkTurn = _atkTurn.transform.Find("Enemy").gameObject;
        _enemyDefTurn = _defTurn.transform.Find("Enemy").gameObject;

    }

    public void showMoveTurn() {
        _turnInfo.GetComponent<Text>().text = NameCoder.getTurnString(_interface._battle._turnManager._turnNum);

        _movTurn.transform.SetSiblingIndex(_turnInfo.transform.GetSiblingIndex() - 1);
    }
    public void showPlayerAtkTurn() {
        _atkTurn.transform.SetSiblingIndex(_turnInfo.transform.GetSiblingIndex() - 1);
    }
    public void showPlayerDefTurn() {
        _defTurn.transform.SetSiblingIndex(_turnInfo.transform.GetSiblingIndex() - 1);
    }
}
