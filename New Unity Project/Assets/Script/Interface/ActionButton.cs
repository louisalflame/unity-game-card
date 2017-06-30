using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 行動選單按鈕
public class ActionButtonInterface { 
    public InterfaceController _interface;

    public GameObject[] _movActions { get; private set; }
    public GameObject[] _atkActions { get; private set; }
    public GameObject[] _defActions { get; private set; }

    public ActionButtonInterface(InterfaceController inter) {
        _interface = inter;

        _movActions = new GameObject[] { };
        _atkActions = new GameObject[] { };
        _defActions = new GameObject[] { };
    }

    public void update() { }

    public void setActionButtonLabel_ID(GameObject btn, string[] Label_ID) {
        btn.transform.Find("actUp/text").GetComponent<TextMesh>().text = Label_ID[1];
        btn.GetComponent<Button>().ButtonID = Label_ID[0];
    }

    public void setMoveActionButton(CharManager character) {
        _movActions = new GameObject[ character._movActions.Length ];
        for (int i = 0; i < _movActions.Length; i++) {
            GameObject actBtn = MonoBehaviour.Instantiate(Resources.Load("ActionMov")) as GameObject;
            actBtn.transform.parent = _interface._menuButton._mainButtonBack.transform.Find("mainBack").transform;
            setActionButtonLabel_ID(actBtn, character._movActions[i].getLabel_ID());
            actBtn.SetActive(false);

            _movActions[i] = actBtn;
            _movActions[i].transform.localPosition = Position.getActionButtonPosition(i);
        } 
    }
    public void showMoveActionButton() {
        foreach (GameObject btn in _movActions) { btn.SetActive(true); }
    }
    public void hideMoveActionButton() {
        foreach (GameObject btn in _movActions) { btn.SetActive(false); }
    }

    public void setAttackActionButton(CharManager character) { 
        _atkActions = new GameObject[ character._atkActions.Length ];
        for (int i = 0; i < _atkActions.Length; i++) {
            GameObject actBtn = MonoBehaviour.Instantiate(Resources.Load("ActionAtk")) as GameObject;
            actBtn.transform.parent = _interface._menuButton._mainButtonBack.transform.Find("mainBack").transform;
            setActionButtonLabel_ID(actBtn, character._atkActions[i].getLabel_ID());
            actBtn.SetActive(false);

            _atkActions[i] = actBtn;
            _atkActions[i].transform.localPosition = Position.getActionButtonPosition(i);
        }
    }
    public void showAttackActionButton() {
        foreach (GameObject btn in _atkActions) { btn.SetActive(true); }
    }
    public void hideAttackActionButton() {
        foreach (GameObject btn in _atkActions) { btn.SetActive(false); }
    }

    public void setDefenseActionButton(CharManager character) { 
        _defActions = new GameObject[ character._defActions.Length ];
        for (int i = 0; i < _defActions.Length; i++) {
            GameObject actBtn = MonoBehaviour.Instantiate(Resources.Load("ActionDef")) as GameObject;
            actBtn.transform.parent = _interface._menuButton._mainButtonBack.transform.Find("mainBack").transform;
            setActionButtonLabel_ID(actBtn, character._defActions[i].getLabel_ID());
            actBtn.SetActive(false);

            _defActions[i] = actBtn;
            _defActions[i].transform.localPosition = Position.getActionButtonPosition(i);
        }
    }
    public void showDefenseActionButton() { 
        foreach (GameObject btn in _defActions) { btn.SetActive(true); }
    }
    public void hideDefenseActionButton() { 
        foreach (GameObject btn in _defActions) { btn.SetActive(false); }
    }

    public void cleanActionButtons() {
        foreach (GameObject btn in _movActions) { MonoBehaviour.Destroy(btn); }
        foreach (GameObject btn in _atkActions) { MonoBehaviour.Destroy(btn); }
        foreach (GameObject btn in _defActions) { MonoBehaviour.Destroy(btn); }
    }
    public void resetActionButtons(CharManager character) {
        cleanActionButtons();
        setMoveActionButton(character);
        setAttackActionButton(character);
        setDefenseActionButton(character);
    }
}