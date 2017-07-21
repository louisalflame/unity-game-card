using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TeamPlayerStatusInterface {
    public InterfaceController _interface { get; private set; }
    public TeamManager _teamManager { get; private set; }
    
    public CharPlayerStatusInterface[] _charStatuses { get; private set; }
    public GameObject _teamStatus { get; private set; }

    public TeamPlayerStatusInterface(InterfaceController inter, TeamManager teamManager) {
        _interface = inter;
        _teamManager = teamManager;
        _teamStatus = CanvasFactory.create_BattleScene_PlayerTeamStatus(_interface.getImageLeftStatus());
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharPlayerStatusInterface[ charMs.Length ];
        for (int i = 0; i < charMs.Length; i++) {
            GameObject charBar = _teamStatus.transform.GetChild(i).gameObject;
            string label = StringCoder.getChangeCharString(i);
            _charStatuses[i] = new CharPlayerStatusInterface(_interface, charBar, charMs[i], label);
            _charStatuses[i].setCharInfo();
            _charStatuses[i].setActive( i==0 );
        }
        hideTeamRearrangeButton();
    }
    public void update() { }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        GameObject nextActive = _charStatuses[select]._char;
        GameObject preActive = null;
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            if (status._active) { 
                preActive = status._char;
                status.setActive(false);
                break; 
            }
        }
        _charStatuses[select].setActive(true);
        GameObject activeParent = preActive.transform.parent.gameObject;
        preActive.transform.SetParent(nextActive.transform.parent);
        nextActive.transform.SetParent(activeParent.transform);
    }

    // 顯示更換角色按鈕
    public void showTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            if (status._active) { status.hideStatus(); }
            else if (_teamManager.isCharSafe(status._charM)) { 
                status.showStatus(); 
                status.showChangeButton(); 
            } else { status.hideStatus();  }
        }
    }
    // 隱藏更換角色按鈕
    public void hideTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            status.hideChangeButton();
            if (status._active) { status.showStatus(); } 
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
    public InterfaceController _interface { get; private set; }
    public TeamManager _teamManager { get; private set; }

    public CharEnemyStatusInterface[] _charStatuses { get; private set; }
    public GameObject _teamStatus { get; private set; }

    public TeamEnemyStatusInterface(InterfaceController inter, TeamManager teamManager) {
        _interface = inter;
        _teamManager = teamManager;
        _teamStatus = CanvasFactory.create_BattleScene_EnemyTeamStatus(_interface.getImageRightStatus());
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharEnemyStatusInterface[charMs.Length];
        for (int i = 0; i < charMs.Length; i++) {
            GameObject charBar = _teamStatus.transform.GetChild(i).gameObject;
            _charStatuses[i] = new CharEnemyStatusInterface(_interface, charBar, charMs[i], "enemyChar-none");
            _charStatuses[i].setCharInfo();
            _charStatuses[i].setActive( i==0 );
        }
        placeCharStatusPos(); 
    }
    public void update() { }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        GameObject nextActive = _charStatuses[select]._char;
        GameObject preActive = null;
        foreach (CharEnemyStatusInterface status in _charStatuses) {
            if (status._active) { 
                preActive = status._char;
                status.setActive(false);
                break; 
            }
        }
        _charStatuses[select].setActive(true);
        GameObject activeParent = preActive.transform.parent.gameObject;
        preActive.transform.SetParent(nextActive.transform.parent);
        nextActive.transform.SetParent(activeParent.transform);
    }
    
    public void placeCharStatusPos() {
        foreach (CharEnemyStatusInterface status in _charStatuses) { 
            if (status._active) { status.showStatus(); } 
            else { status.hideStatus(); }
        }
    }
    
    // 更新角色狀態
    public void updateCharStatusInfo() {
    }
}



// 角色狀態顯示
public class CharStatusInterface{
    public InterfaceController _interface;
    public bool _active { get; protected set; }
    public CharManager _charM { get; protected set; }
    public GameObject _char { get; protected set; }

    public void setCharInfo() {
        _char.transform.Find("txtName").GetComponent<Text>().text = _charM._name;
        _char.transform.Find("Info/numHP").GetComponent<Text>().text = _charM._hp.ToString();
        _char.transform.Find("Info/numATK").GetComponent<Text>().text = _charM._atk.ToString();
        _char.transform.Find("Info/numDEF").GetComponent<Text>().text = _charM._def.ToString();
    }
    public void setActive(bool active) { _active = active; }
}

// 我方角色狀態顯示
public class CharPlayerStatusInterface : CharStatusInterface {
    public CharPlayerStatusInterface(InterfaceController inter, GameObject parent, CharManager charM, string label) { 
        _interface = inter;
        _charM = charM;

        _char = CanvasFactory.create_PlayerCharStatus_Unit(parent, label); 
    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() { 
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( 125f, 0f );
    }
    public void hideStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
    }
    public void showChangeButton() {
        _char.GetComponent<Button>().interactable = true;
    }
    public void hideChangeButton() { 
        _char.GetComponent<Button>().interactable = false;
    }

    public void update() { }
}

// 敵方角色狀態顯示
public class CharEnemyStatusInterface : CharStatusInterface {

    public CharEnemyStatusInterface(InterfaceController inter, GameObject parent, CharManager charM, string label) {
        _interface = inter; 
        _charM = charM;

        _char = CanvasFactory.create_EnemyCharStatus_Unit(parent, label);
        _char.GetComponent<Button>().interactable = false;
    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( -125f, 0f );
    }
    public void hideStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( 0f, 0f );
    }

    public void update() { }
}