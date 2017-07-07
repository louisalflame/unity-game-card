using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 隊伍狀態顯示
public class TeamStatusInterface {
    public InterfaceController _interface;
    public CharPlayerStatusInterface[] _playerChars { get; private set; }
    public CharEnemyStatusInterface[] _enemyChars { get; private set; }

    public TeamStatusInterface(InterfaceController inter) {
        _interface = inter;
    }

    public void setPlayerCharacters() {
        _playerChars = new CharPlayerStatusInterface[ _interface._battle._playerManager._characters.Length ];
        for (int i = 0; i < _interface._battle._playerManager._characters.Length; i++) {
            _playerChars[i] = new CharPlayerStatusInterface(_interface, i, _interface._battle._playerManager._characters[i]);
            _playerChars[i].setCharInfo();
        }
    }

    public void setEnemyCharacters() {
        _enemyChars = new CharEnemyStatusInterface[ _interface._battle._enemyManager._characters.Length];
        for (int i = 0; i < _interface._battle._enemyManager._characters.Length; i++) {
            _enemyChars[i] = new CharEnemyStatusInterface(_interface, i, _interface._battle._enemyManager._characters[i]);
            _enemyChars[i].setCharInfo();
        }
    }

    public void update() { }

    public void changeActiveCharStatus(int select) {
        CharPlayerStatusInterface tmp = _playerChars[0];
        for (int i = 0; i < _playerChars.Length; i++) {
            if (_playerChars[i]._id == select) {
                _playerChars[0] = _playerChars[i];
                _playerChars[0].setPos(0);
                _playerChars[i] = tmp;
                _playerChars[i].setPos(i);
                Debug.Log(_playerChars[0]._charM._name + " <=> " + _playerChars[i]._charM._name);
                break;
            }
        }
    }
    public void showTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _playerChars) {
            if (_interface._battle._playerManager.isCharActive(status._charM)) {
                status.hideStatus();
            } else {
                if (_interface._battle._playerManager.isCharSafe(status._charM)) {
                    status.showChangeButton();
                }
            }
        }
    }
    public void hideTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _playerChars) {
            if (_interface._battle._playerManager.isCharActive(status._charM)) {
                status.showStatus();
            } else {
                status.hideStatus();
            }
        }
    }
}

// 角色狀態顯示
public class CharPlayerStatusInterface {
    public InterfaceController _interface;
    public int _pos { get; private set; }
    public int _id { get; private set; }
    public CharManager _charM { get; private set; }
    public GameObject _char { get; private set; }

    public CharPlayerStatusInterface(InterfaceController inter, int i, CharManager charM) { 
        _interface = inter;
        _pos = i; _id = i;
        _charM = charM;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharPlayerBar") as GameObject);
        if (_interface._battle._playerManager.isCharActive(_charM)) {
            _char.transform.localPosition = Position.getCharPlayerShowStatusPosition(_pos);
        } else {
            _char.transform.localPosition = Position.getCharPlayerHideStatusPosition(_pos);
        }
        _char.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(_id);
        _char.GetComponent<BoxCollider2D>().enabled = false;

    }

    public void setCharInfo() {
        _char.transform.Find("charName").GetComponent<TextMesh>().text = _charM._name;
        _char.transform.Find("charBarInfo").transform.Find("numHP").GetComponent<TextMesh>().text = _charM._hp.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numATK").GetComponent<TextMesh>().text = _charM._atk.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numDEF").GetComponent<TextMesh>().text = _charM._def.ToString();
    }
    public void setPos(int pos) { _pos = pos; }

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
public class CharEnemyStatusInterface {
    public InterfaceController _interface;
    public int _pos { get; private set; }
    public int _id { get; private set; }
    public CharManager _charM { get; private set; }
    public GameObject _char { get; private set; }

    public CharEnemyStatusInterface(InterfaceController inter, int i, CharManager charM) {
        _interface = inter;
        _pos = i; _id = i;
        _charM = charM;

        _char = MonoBehaviour.Instantiate(Resources.Load("CharEnemyBar") as GameObject);
        if (_interface._battle._enemyManager.isCharActive(_charM)) {
            _char.transform.localPosition = Position.getCharEnemyShowStatusPosition(_pos);
        } else {
            _char.transform.localPosition = Position.getCharEnemyHideStatusPosition(_pos);
        }
    }

    public void setCharInfo() {
        _char.transform.Find("charName").GetComponent<TextMesh>().text = _charM._name;
        _char.transform.Find("charBarInfo").transform.Find("numHP").GetComponent<TextMesh>().text = _charM._hp.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numATK").GetComponent<TextMesh>().text = _charM._atk.ToString();
        _char.transform.Find("charBarInfo").transform.Find("numDEF").GetComponent<TextMesh>().text = _charM._def.ToString();
    }

    public void update() { }
}