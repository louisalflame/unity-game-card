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
        btn.GetComponent<ButtonEvent>().ButtonID = Label_ID[0];
    }

    public void setMoveActionButton(CharManager character) {
        _movActions = new GameObject[ character._movActions.Length ];
        for (int i = 0; i < _movActions.Length; i++) {
            GameObject actBtn = CanvasFactory.create_BattleScene_MoveActionBtn(
                _interface._menuButton._actionBase, character._movActions[i].getLabel_ID() );
            actBtn.SetActive(false);
            _movActions[i] = actBtn;
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
            GameObject actBtn = CanvasFactory.create_BattleScene_AttackActionBtn(
                _interface._menuButton._actionBase, character._atkActions[i].getLabel_ID());
            actBtn.SetActive(false);
            _atkActions[i] = actBtn;
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
            GameObject actBtn = CanvasFactory.create_BattleScene_DefenseActionBtn(
                _interface._menuButton._actionBase, character._defActions[i].getLabel_ID());
            actBtn.SetActive(false);
            _defActions[i] = actBtn;
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