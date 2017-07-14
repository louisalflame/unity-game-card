using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TeamPlayerStatusInterface {
    public InterfaceController _interface;
    public CharPlayerStatusInterface[] _charStatuses { get; private set; }

    public TeamPlayerStatusInterface(InterfaceController inter) {
        _interface = inter;
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharPlayerStatusInterface[ charMs.Length ];
        for (int i = 0; i < charMs.Length; i++) {
            _charStatuses[i] = new CharPlayerStatusInterface(_interface, i, charMs[i]);
            _charStatuses[i].setCharInfo();
        }
        placeCharStatusPos();
    }
    public void update() { }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        CharPlayerStatusInterface tmp = _charStatuses[0];
        for (int i = 0; i < _charStatuses.Length; i++) {
            if (_charStatuses[i]._id == select) {
                _charStatuses[0] = _charStatuses[i];
                _charStatuses[0].setPos(0);
                _charStatuses[i] = tmp;
                _charStatuses[i].setPos(i);
                Debug.Log(_charStatuses[0]._charM._name + " <=> " + _charStatuses[i]._charM._name);
                break;
            }
        }
    }

    // 顯示更換角色按鈕
    public void showTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            if (_interface._battle._playerManager.isCharActive(status._charM)) { status.hideStatus(); } 
            else if (_interface._battle._playerManager.isCharSafe(status._charM)) { status.showChangeButton(); }
            else { status.hideStatus();  }
        }
    }
    // 隱藏更換角色按鈕
    public void hideTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            if (_interface._battle._playerManager.isCharActive(status._charM)) { status.showStatus(); } 
            else { status.hideStatus(); }
        }
    }
    // 基本角色狀態位置
    public void placeCharStatusPos() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            if (_interface._battle._playerManager.isCharActive(status._charM)) { status.showStatus(); } 
            else { status.hideStatus(); }
        }
    }

    // 更新角色狀態
    public void updateCharStatusInfo() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].setCharInfo();
        }
    }
}


public class TeamEnemyStatusInterface {
    public InterfaceController _interface;
    public CharEnemyStatusInterface[] _charStatuses { get; private set; }

    public TeamEnemyStatusInterface(InterfaceController inter) {
        _interface = inter;
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharEnemyStatusInterface[charMs.Length];
        for (int i = 0; i < _interface._battle._enemyManager._characters.Length; i++) {
            _charStatuses[i] = new CharEnemyStatusInterface(_interface, i, charMs[i]);
            _charStatuses[i].setCharInfo();
        }
        placeCharStatusPos();
    }
    public void update() { }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        CharEnemyStatusInterface tmp = _charStatuses[0];
        for (int i = 0; i < _charStatuses.Length; i++) {
            if (_charStatuses[i]._id == select) {
                _charStatuses[0] = _charStatuses[i];
                _charStatuses[0].setPos(0);
                _charStatuses[i] = tmp;
                _charStatuses[i].setPos(i);
                Debug.Log(_charStatuses[0]._charM._name + " <=> " + _charStatuses[i]._charM._name);
                break;
            }
        }
    }
    
    public void placeCharStatusPos() {
        foreach (CharEnemyStatusInterface status in _charStatuses) {
            if (_interface._battle._enemyManager.isCharActive(status._charM)) { status.showStatus(); } 
            else { status.hideStatus(); }
        }
    }
    
    // 更新角色狀態
    public void updateCharStatusInfo() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].setCharInfo();
        }
    }
}



// 角色狀態顯示
public class CharStatusInterface{
    public InterfaceController _interface;
    public int _pos { get; protected set; }
    public int _id { get; protected set; }
    public CharManager _charM { get; protected set; }
    public GameObject _char { get; protected set; }

    public void setCharInfo() {
        _char.transform.Find("charName").GetComponent<TextMesh>().text = _charM._name;
        _char.transform.Find("charBarInfo").transform.Find("numHP").GetComponent<TextMesh>().text = _charM._hp.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numATK").GetComponent<TextMesh>().text = _charM._atk.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numDEF").GetComponent<TextMesh>().text = _charM._def.ToString();
    }
    public void setPos(int pos) { _pos = pos; }
}

// 我方角色狀態顯示
public class CharPlayerStatusInterface : CharStatusInterface {
    public CharPlayerStatusInterface(InterfaceController inter, int i, CharManager charM) { 
        _interface = inter;
        _pos = i; _id = i;
        _charM = charM;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharPlayerBar") as GameObject);
        _char.GetComponent<ButtonEvent>().ButtonID = StringCoder.getChangeCharString(_id);
        _char.GetComponent<BoxCollider2D>().enabled = false;

    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() {
        _char.transform.localPosition = Position.getCharPlayerShowStatusPosition(_pos);
        _char.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void showChangeButton() {
        _char.transform.localPosition = Position.getCharPlayerShowStatusPosition(_pos);
        _char.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void hideStatus() {
        _char.transform.localPosition = Position.getCharPlayerHideStatusPosition(_pos);
        _char.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void update() { }
}

// 敵方角色狀態顯示
public class CharEnemyStatusInterface : CharStatusInterface {

    public CharEnemyStatusInterface(InterfaceController inter, int i, CharManager charM) {
        _interface = inter;
        _pos = i; _id = i;
        _charM = charM;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharEnemyBar") as GameObject);
    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() {
        _char.transform.localPosition = Position.getCharEnemyShowStatusPosition(_pos);
    }
    public void hideStatus() {
        _char.transform.localPosition = Position.getCharEnemyHideStatusPosition(_pos);
    }

    public void update() { }
}