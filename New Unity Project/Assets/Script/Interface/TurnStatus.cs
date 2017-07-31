using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatus {
    public InterfaceController _interface;

    public GameObject _turnStatus { get; private set; }
    public GameObject _movTurn { get; private set; }
    public GameObject _atkTurn { get; private set; }
    public GameObject _defTurn { get; private set; }
    public GameObject _turnInfo { get; private set; }

    private GameObject _playerAtk = null;
    private GameObject _playerDef = null;
    private GameObject _enemyAtk = null;
    private GameObject _enemyDef = null;
    
    public TurnStatus(InterfaceController inter) {
        _interface = inter;
    }
    public void create() {
        create_BattleScene_TurnStatus(_interface.getImageMiddleBattleField());
    }
    public void initial() {
        _turnStatus.SetActive(true);
        _turnStatus.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 330f);
        AnimateWork.setAlpha(_turnStatus.transform, 0);
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
    

    // Turn Status   *************
    public void create_BattleScene_TurnStatus(GameObject parent) {
        _turnStatus = CanvasFactory.createEmptyRect(parent, "TurnStatus");
        CanvasFactory.setRectTransformAnchor(_turnStatus, new Vector2(0f, 0.45f), new Vector2(1f, 0.85f), Vector2.zero, Vector2.zero);

        _movTurn = CanvasFactory.createImage(_turnStatus, "MovTurn");
        CanvasFactory.setImageSprite(_movTurn, "Sprite/TurnIcon/movTurn");
        CanvasFactory.setRectTransformPosition(_movTurn, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(240f, 240f));

        _atkTurn = CanvasFactory.createEmptyRect(_turnStatus, "AtkTurn");
        CanvasFactory.setWholeRect(_atkTurn);

        _defTurn = CanvasFactory.createEmptyRect(_turnStatus, "DefTurn");
        CanvasFactory.setWholeRect(_defTurn);

        _playerAtk = CanvasFactory.createImage(_atkTurn, "Player");
        CanvasFactory.setImageSprite(_playerAtk, "Sprite/TurnIcon/playerAtkTurn");
        CanvasFactory.setRectTransformPosition(_playerAtk, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        CanvasFactory.setRectPivot(_playerAtk, new Vector2(1f, 0.5f));
        GameObject playerAtkTxt = CanvasFactory.createText(_playerAtk, "txt", "攻擊\n階段");
        CanvasFactory.setZeroPosition(playerAtkTxt, new Vector2(0.65f, 0.45f));
        CanvasFactory.setTextScaleSize(playerAtkTxt, 0.1f, 200);
        CanvasFactory.setTextColor(playerAtkTxt, Color.black);

        _enemyDef = CanvasFactory.createImage(_atkTurn, "Enemy");
        CanvasFactory.setImageSprite(_enemyDef, "Sprite/TurnIcon/enemyDefTurn");
        CanvasFactory.setRectTransformPosition(_enemyDef, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        CanvasFactory.setRectPivot(_enemyDef, new Vector2(0f, 0.5f));
        GameObject enemyDefTxt = CanvasFactory.createText(_enemyDef, "txt", "防禦\n階段");
        CanvasFactory.setZeroPosition(enemyDefTxt, new Vector2(0.35f, 0.45f));
        CanvasFactory.setTextScaleSize(enemyDefTxt, 0.1f, 200);
        CanvasFactory.setTextColor(enemyDefTxt, Color.black);

        _playerDef = CanvasFactory.createImage(_defTurn, "Player");
        CanvasFactory.setImageSprite(_playerDef, "Sprite/TurnIcon/playerDefTurn");
        CanvasFactory.setRectTransformPosition(_playerDef, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        CanvasFactory.setRectPivot(_playerDef, new Vector2(1f, 0.5f));
        GameObject playerDefTxt = CanvasFactory.createText(_playerDef, "txt", "防禦\n階段");
        CanvasFactory.setZeroPosition(playerDefTxt, new Vector2(0.65f, 0.45f));
        CanvasFactory.setTextScaleSize(playerDefTxt, 0.1f, 200);
        CanvasFactory.setTextColor(playerDefTxt, Color.black);

        _enemyAtk = CanvasFactory.createImage(_defTurn, "Enemy");
        CanvasFactory.setImageSprite(_enemyAtk, "Sprite/TurnIcon/enemyAtkTurn");
        CanvasFactory.setRectTransformPosition(_enemyAtk, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        CanvasFactory.setRectPivot(_enemyAtk, new Vector2(0f, 0.5f));
        GameObject enemyAtkTxt = CanvasFactory.createText(_enemyAtk, "txt", "攻擊\n階段");
        CanvasFactory.setZeroPosition(enemyAtkTxt, new Vector2(0.35f, 0.45f));
        CanvasFactory.setTextScaleSize(enemyAtkTxt, 0.1f, 200);
        CanvasFactory.setTextColor(enemyAtkTxt, Color.black);

        _turnInfo = CanvasFactory.createText(_turnStatus, "TurnInfo", "第 1 回合");
        CanvasFactory.setZeroPosition(_turnInfo, new Vector2(0.5f, 0.68f));
        CanvasFactory.setTextScaleSize(_turnInfo, 0.1f, 300);
        CanvasFactory.setTextColor(_turnInfo, Color.black);

        _turnStatus.SetActive(false);
    }
}
