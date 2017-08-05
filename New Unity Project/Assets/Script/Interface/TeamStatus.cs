using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
 
public class TeamPlayerStatusInterface {
    public InterfaceController _interface { get; private set; }
    public TeamManager _teamManager { get; private set; }
    
    public CharPlayerStatusInterface[] _charStatuses { get; private set; }
    public CharPlayerStatusInterface _activeStatus { get; private set; }
    public GameObject _teamStatus { get; private set; }

    public TeamPlayerStatusInterface(InterfaceController inter, TeamManager teamManager) {
        _interface = inter;
        _teamManager = teamManager;
    }
    public void create(){
        create_BattleScene_PlayerTeamStatus(_interface.getImageLeftStatus());
    }
    public void initial() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].initial();
        }
    }
    public AnimateWork[] getShiftInAnimates() {
        AnimateWork[] works = new AnimateWork[_charStatuses.Length];
        for (int i = 0; i < _charStatuses.Length; i++) {
            works[i] = new AnimateMoveTo(_charStatuses[i]._char.GetComponent<RectTransform>(), Vector2.zero, 30);
        }
        return works;
    }
    public AnimateWork getActiveShiftAnimate() {
        return new AnimateMoveTo(_activeStatus._char.GetComponent<RectTransform>(), new Vector2(125f, 0f), 25);
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharPlayerStatusInterface[ charMs.Length ];
        for (int i = 0; i < charMs.Length; i++) {
            GameObject charBar = _teamStatus.transform.GetChild(i).gameObject;
            string label = StringCoder.getChangeCharString(i);
            _charStatuses[i] = new CharPlayerStatusInterface(_interface, charBar, charMs[i], label);
            _charStatuses[i].create();
            _charStatuses[i].setCharInfo();
            if (i == 0) {
                _activeStatus = _charStatuses[i];
                _charStatuses[i].setActive(true);
            }else{ _charStatuses[i].setActive(false); }
        }
        hideTeamRearrangeButton();
    }
    public void update() { 
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].update();
        }
    }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        GameObject nextActive = _charStatuses[select]._char;
        GameObject preActive = _activeStatus._char;
        _activeStatus.setActive(false);
        _charStatuses[select].setActive(true);
        _activeStatus = _charStatuses[select];
        GameObject activeParent = preActive.transform.parent.gameObject;
        preActive.transform.SetParent(nextActive.transform.parent);
        nextActive.transform.SetParent(activeParent.transform);
    }

    // 顯示更換角色按鈕
    public void showTeamRearrangeButton() {
        foreach (CharPlayerStatusInterface status in _charStatuses) {
            status.closeCheckMode();
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
            if (status._active) {
                status.showStatus();
                status.closeCheckMode();
            } else {
                status.hideStatus();
                status.openCheckMode();
            }
        }
    } 

    // 更新角色狀態
    public void updateCharStatusInfo() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].setCharInfo();
        }
    }
    
    public void create_BattleScene_PlayerTeamStatus(GameObject parent) {
        _teamStatus = CanvasFactory.createEmptyRect(parent, "TeamStatus");
        CanvasFactory.setRectTransformAnchor(_teamStatus, new Vector2(0f, 0.4f), new Vector2(1f, 0.79f), Vector2.zero, Vector2.zero);

        GameObject char1Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus1");
        CanvasFactory.setRectTransformAnchor(char1Obj, new Vector2(0f, 0.75f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        GameObject char2Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus2");
        CanvasFactory.setRectTransformAnchor(char2Obj, new Vector2(0f, 0.5f), new Vector2(1f, 0.75f), Vector2.zero, Vector2.zero);
        GameObject char3Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus3");
        CanvasFactory.setRectTransformAnchor(char3Obj, new Vector2(0f, 0.25f), new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        GameObject char4Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus4");
        CanvasFactory.setRectTransformAnchor(char4Obj, new Vector2(0f, 0f), new Vector2(1f, 0.25f), Vector2.zero, Vector2.zero);
    }
}


public class TeamEnemyStatusInterface {
    public InterfaceController _interface { get; private set; }
    public TeamManager _teamManager { get; private set; }

    public CharEnemyStatusInterface[] _charStatuses { get; private set; }
    public CharEnemyStatusInterface _activeStatus { get; private set; }
    public GameObject _teamStatus { get; private set; }

    public TeamEnemyStatusInterface(InterfaceController inter, TeamManager teamManager) {
        _interface = inter;
        _teamManager = teamManager;
    }
    public void create() {
        create_BattleScene_EnemyTeamStatus(_interface.getImageRightStatus());
    }
    public void initial() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].initial();
        }
    }
    public AnimateWork[] getShiftInAnimates() {
        AnimateWork[] works = new AnimateWork[_charStatuses.Length];
        for (int i = 0; i < _charStatuses.Length; i++) {
            works[i] = new AnimateMoveTo(_charStatuses[i]._char.GetComponent<RectTransform>(), Vector2.zero, 30);
        }
        return works;
    }
    public AnimateWork getActiveShiftAnimate() {
        return new AnimateMoveTo(_activeStatus._char.GetComponent<RectTransform>(), new Vector2(-125f, 0f), 25);
    }

    public void setCharacters( CharManager[] charMs ) {
        _charStatuses = new CharEnemyStatusInterface[charMs.Length];
        for (int i = 0; i < charMs.Length; i++) {
            GameObject charBar = _teamStatus.transform.GetChild(i).gameObject;
            _charStatuses[i] = new CharEnemyStatusInterface(_interface, charBar, charMs[i], "enemyChar-none");
            _charStatuses[i].create();
            _charStatuses[i].setCharInfo();
            if (i == 0) {
                _activeStatus = _charStatuses[i];
                _charStatuses[i].setActive(true);
            }else{ _charStatuses[i].setActive(false); }
        }
        placeCharStatusPos(); 
    }
    public void update() { 
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].update();
        }
    }

    // 更換角色
    public void changeActiveCharStatus(int select) {
        GameObject nextActive = _charStatuses[select]._char;
        GameObject preActive = _activeStatus._char;
        _activeStatus.setActive(false);
        _charStatuses[select].setActive(true);
        _activeStatus = _charStatuses[select];
        GameObject activeParent = preActive.transform.parent.gameObject;
        preActive.transform.SetParent(nextActive.transform.parent);
        nextActive.transform.SetParent(activeParent.transform);
    }
    
    public void placeCharStatusPos() {
        foreach (CharEnemyStatusInterface status in _charStatuses) { 
            if (status._active) { 
                status.showStatus();
                status.closeCheckMode();
            } 
            else { 
                status.hideStatus();
                status.openCheckMode();
            }
        }
    }
    
    // 更新角色狀態
    public void updateCharStatusInfo() {
        for (int i = 0; i < _charStatuses.Length; i++) {
            _charStatuses[i].setCharInfo();
        }
    }
    
    public void create_BattleScene_EnemyTeamStatus(GameObject parent) {
        _teamStatus = CanvasFactory.createEmptyRect(parent, "TeamStatus");
        CanvasFactory.setRectTransformAnchor(_teamStatus, new Vector2(0f, 0.5f), new Vector2(1f, 0.89f), Vector2.zero, Vector2.zero);

        GameObject char1Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus1");
        CanvasFactory.setRectTransformAnchor(char1Obj, new Vector2(0f, 0f), new Vector2(1f, 0.25f), Vector2.zero, Vector2.zero);
        GameObject char2Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus2");
        CanvasFactory.setRectTransformAnchor(char2Obj, new Vector2(0f, 0.25f), new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        GameObject char3Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus3");
        CanvasFactory.setRectTransformAnchor(char3Obj, new Vector2(0f, 0.5f), new Vector2(1f, 0.75f), Vector2.zero, Vector2.zero);
        GameObject char4Obj = CanvasFactory.createEmptyRect(_teamStatus, "CharStatus4");
        CanvasFactory.setRectTransformAnchor(char4Obj, new Vector2(0f, 0.75f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero); 
    }
}



// 角色狀態顯示
public class CharStatusInterface{
    public InterfaceController _interface;
    public bool _active { get; protected set; }
    public CharManager _charM { get; protected set; }
    public GameObject _char { get; protected set; }
    public GameObject _name { get; protected set; }
    public GameObject _info { get; protected set; }
    public GameObject _hp { get; protected set; }
    public GameObject _atk { get; protected set; }
    public GameObject _def { get; protected set; }
    public GameObject _hpBar { get; protected set; }
    public GameObject _hpBarBase { get; protected set; }

    protected float _openingSpeed = 8f;
    protected float _closingSpeed = 10f;

    public void setCharInfo() {
        CanvasFactory.setTextString(_name, _charM._name);
        CanvasFactory.setTextString(_hp, _charM._hp.ToString());
        CanvasFactory.setTextString(_atk, _charM._atk.ToString());
        CanvasFactory.setTextString(_def, _charM._def.ToString());
        setHpBar((float)_charM._hp / (float)_charM._character._hp);
    }
    public void setActive(bool active) { _active = active; }

    protected static Color _empty = new Color32(255, 0, 0, 255);
    protected static Color _middle = new Color32(255, 255, 0, 255);
    protected static Color _full = new Color32(0, 255, 0, 255);
    public void setHpBar(float n) {
        if (n > 0.5f) { CanvasFactory.setImageMaterialColor(_hpBar, "_Color", Color.Lerp(_middle, _full, 2 * n - 1)); }
        else { CanvasFactory.setImageMaterialColor(_hpBar, "_Color", Color.Lerp(_empty, _middle, 2 * n )); }
        CanvasFactory.setImageMaterialFloat(_hpBar, "_Progress", n);
    }
}

// 我方角色狀態顯示
public class CharPlayerStatusInterface : CharStatusInterface {
    GameObject _parent;
    string _label;
    private enum Mode { none = 1, ready, opening, show, closing }
    private Mode _mode = Mode.none;

    public CharPlayerStatusInterface(InterfaceController inter, GameObject parent, CharManager charM, string label) { 
        _interface = inter;
        _charM = charM;
        _parent = parent; _label = label;
    }
    public void create() {
        create_PlayerCharStatus_Unit(_parent, _label); 
    }
    public void initial() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f, 0f);
        _char.SetActive(true);
    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() { 
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( 125f, 0f );
    }
    public void hideStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
    }
    public void showChangeButton() {
        CanvasFactory.setButtonInteractable(_char, true);
    }
    public void hideChangeButton() {
        CanvasFactory.setButtonInteractable(_char, false);
    }
    public void closeCheckMode() { _mode = Mode.none; }
    public void openCheckMode() { _mode = Mode.ready; }

    public void update() {
        switch (_mode) {
            case Mode.none: break;
            case Mode.ready: break;
            case Mode.opening:
                if (_char.GetComponent<RectTransform>().anchoredPosition.x < 125f) {
                    float movingX = _char.GetComponent<RectTransform>().anchoredPosition.x;
                    movingX = Mathf.Min( 125f, movingX + _openingSpeed);
                    _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(movingX, 0f);
                }else { _mode = Mode.show; }
                break;
            case Mode.show: break;
            case Mode.closing:
                if (_char.GetComponent<RectTransform>().anchoredPosition.x > 0f) {
                    float movingX = _char.GetComponent<RectTransform>().anchoredPosition.x;
                    movingX = Mathf.Max( 0f, movingX - _closingSpeed);
                    _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(movingX, 0f);
                }else { _mode = Mode.ready; }
                break;
        }
    }
    
    public void create_PlayerCharStatus_Unit(GameObject parent, string label) {
        _char = CanvasFactory.createButton(parent, "CharBar", label);
        CanvasFactory.setImageSprite(_char, "Sprite/CharBar/charBarBack");
        CanvasFactory.setRectTransformPosition(_char, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(250f, 50f));

        CanvasFactory.addPointerEnterCallback(_char, (e) => {
            if (_mode != Mode.none) _mode = Mode.opening; } );
        CanvasFactory.addPointerExitCallback(_char, (e) => {
            if (_mode != Mode.none) _mode = Mode.closing; } );

        _info = CanvasFactory.createImage(_char, "Info");
        CanvasFactory.setImageSprite(_info, "Sprite/CharBar/charBarInfo");
        CanvasFactory.setRectTransformPosition(_info, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(100f, 50f));
        CanvasFactory.setRectPivot(_info, new Vector2(0f, 0.5f));
        CanvasFactory.create_CharStatus_Unit_Info(_info);
        _hp = CanvasFactory.create_CharStatus_Unit_NumHp(_info);
        _atk = CanvasFactory.create_CharStatus_Unit_NumATK(_info);
        _def = CanvasFactory.create_CharStatus_Unit_NumDEF(_info);

        _hpBarBase = CanvasFactory.createImage(_char, "HpBarBase");
        CanvasFactory.setImageSprite(_hpBarBase, "Sprite/CharBar/charBarHpBase");
        CanvasFactory.setRectTransformPosition(_hpBarBase, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(46f, 50f));
        CanvasFactory.setRectPivot(_hpBarBase, new Vector2(1f, 0.5f));

        _hpBar = CanvasFactory.createImage(_hpBarBase, "HpBar");
        CanvasFactory.setImageSprite(_hpBar, "Sprite/CharBar/charBarHp");
        CanvasFactory.setImageMaterial(_hpBar, "Shader/MaterialMask");
        CanvasFactory.setWholeRect(_hpBar);
        
        _name = CanvasFactory.createText(_char, "txtName", "name");
        CanvasFactory.setRectTransformAnchor(_name, new Vector2(0.8f, 0f), new Vector2(0.8f, 0f), Vector2.zero, Vector2.zero);
        CanvasFactory.setTextScaleSize(_name, 0.1f, 150);
        CanvasFactory.setTextColor(_name, Color.white);
        CanvasFactory.setTextAnchor(_name, TextAnchor.LowerRight);

        _char.SetActive(false);
    }
}

// 敵方角色狀態顯示
public class CharEnemyStatusInterface : CharStatusInterface {
    GameObject _parent;
    string _label;
    private enum Mode { none = 1, ready, opening, show, closing }
    private Mode _mode = Mode.none;

    public CharEnemyStatusInterface(InterfaceController inter, GameObject parent, CharManager charM, string label) {
        _interface = inter;
        _charM = charM;
        _parent = parent; _label = label;
    }
    public void create() {
        create_EnemyCharStatus_Unit(_parent, _label);
    }
    public void initial() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, 0f);
        _char.SetActive(true); 
    }
    
    // 顯示戰鬥中/待機/可更換
    public void showStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( -125f, 0f );
    }
    public void hideStatus() {
        _char.GetComponent<RectTransform>().anchoredPosition = new Vector2( 0f, 0f );
    }
    public void closeCheckMode() { _mode = Mode.none; }
    public void openCheckMode() { _mode = Mode.ready; }

    public void update() {
        switch (_mode) {
            case Mode.none: break;
            case Mode.ready: break;
            case Mode.opening:
                if (_char.GetComponent<RectTransform>().anchoredPosition.x > -125f) {
                    float movingX = _char.GetComponent<RectTransform>().anchoredPosition.x;
                    movingX = Mathf.Max( -125f, movingX - _openingSpeed);
                    _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(movingX, 0f);
                }else { _mode = Mode.show; }
                break;
            case Mode.show: break;
            case Mode.closing:
                if (_char.GetComponent<RectTransform>().anchoredPosition.x < 0f) {
                    float movingX = _char.GetComponent<RectTransform>().anchoredPosition.x;
                    movingX = Mathf.Min( 0f, movingX + _closingSpeed);
                    _char.GetComponent<RectTransform>().anchoredPosition = new Vector2(movingX, 0f);
                }else { _mode = Mode.ready; }
                break;
        } 
    }

    public void create_EnemyCharStatus_Unit(GameObject parent, string label) {
        _char = CanvasFactory.createButton(parent, "CharBar", label);
        CanvasFactory.setImageSprite(_char, "Sprite/CharBar/charBarBackEnemy");
        CanvasFactory.setRectTransformPosition(_char, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(250f, 50f));

        CanvasFactory.setButtonInteractable(_char, false);
        CanvasFactory.addPointerEnterCallback(_char, (e) => {
            if (_mode != Mode.none) _mode = Mode.opening; } );
        CanvasFactory.addPointerExitCallback(_char, (e) => {
            if (_mode != Mode.none) _mode = Mode.closing; } );

        _info = CanvasFactory.createImage(_char, "Info");
        CanvasFactory.setImageSprite(_info, "Sprite/CharBar/charBarInfo");
        CanvasFactory.setRectTransformPosition(_info, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(100f, 50f));
        CanvasFactory.setRectPivot(_info, new Vector2(1f, 0.5f));
        CanvasFactory.create_CharStatus_Unit_Info(_info);
        _hp = CanvasFactory.create_CharStatus_Unit_NumHp(_info);
        _atk = CanvasFactory.create_CharStatus_Unit_NumATK(_info);
        _def = CanvasFactory.create_CharStatus_Unit_NumDEF(_info);

        _hpBarBase = CanvasFactory.createImage(_char, "HpBarBase");
        CanvasFactory.setImageSprite(_hpBarBase, "Sprite/CharBar/charBarHpBase");
        CanvasFactory.setRectTransformPosition(_hpBarBase, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(46f, 50f));
        CanvasFactory.setRectPivot(_hpBarBase, new Vector2(0f, 0.5f));

        _hpBar = CanvasFactory.createImage(_hpBarBase, "HpBar");
        CanvasFactory.setImageSprite(_hpBar, "Sprite/CharBar/charBarHp");
        CanvasFactory.setImageMaterial(_hpBar, "Shader/MaterialMask");
        CanvasFactory.setWholeRect(_hpBar);

        _name = CanvasFactory.createText(_char, "txtName", "name");
        CanvasFactory.setRectTransformAnchor(_name, new Vector2(0.1f, 0f), new Vector2(0.1f, 0f), Vector2.zero, Vector2.zero);
        CanvasFactory.setTextScaleSize(_name, 0.1f, 150);
        CanvasFactory.setTextColor(_name, Color.white);
        CanvasFactory.setTextAnchor(_name, TextAnchor.LowerLeft);

        _char.SetActive(false);
    }
}