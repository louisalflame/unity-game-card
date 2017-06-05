using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 行動選單按鈕
public class ActionButtonInterface { 
    public InterfaceController _interface;
    private GameObject _next = null;
    private GameObject _throw = null;

    private GameObject[] _movActions = new GameObject[] { };
    private GameObject[] _atkActions = new GameObject[] { };
    private GameObject[] _defActions = new GameObject[] { };

    public ActionButtonInterface(InterfaceController inter) {
        _interface = inter;

        _next = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _next.transform.position = new Vector3(-5.5f, -1, 1);
        _next.transform.localScale = new Vector3(1, 1, 1);
        _next.transform.Find("text").GetComponent<TextMesh>().text = Name.NextButton[1];
        _next.GetComponent<Button>().ButtonID = Name.NextButton[0];
        _next.SetActive(false);

        _throw = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _throw.transform.localPosition = new Vector3(-3.5f, -1, 1);
        _throw.transform.localScale = new Vector3(1, 1, 1);
        _throw.transform.Find("text").GetComponent<TextMesh>().text = Name.ThrowButton[1];
        _throw.GetComponent<Button>().ButtonID = Name.ThrowButton[0];
        _throw.SetActive(false);
         
    }

    public void update() { }
    
    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }

    public void setMoveActionButton(CharManager character) {
        _movActions = new GameObject[ character._movActions.Length ];
        for (int i = 0; i < _movActions.Length; i++) { 
            GameObject btn = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.Find("text").GetComponent<TextMesh>().text = character._movActions[i].text;
            btn.GetComponent<Button>().ButtonID = character._movActions[i].label;
            btn.SetActive(false);
            _movActions[i] = btn;

            _movActions[i].transform.localPosition = new Vector3(-5.5f+2*i,-2,-1);
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
            GameObject btn = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.Find("text").GetComponent<TextMesh>().text = character._atkActions[i].text;
            btn.GetComponent<Button>().ButtonID = character._atkActions[i].label;
            btn.SetActive(false);
            _atkActions[i] = btn;

            _atkActions[i].transform.localPosition = new Vector3(-5.5f + 2 * i, -2, -1);
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
            GameObject btn = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.Find("text").GetComponent<TextMesh>().text = character._defActions[i].text;
            btn.GetComponent<Button>().ButtonID = character._defActions[i].label;
            btn.SetActive(false);
            _defActions[i] = btn;

            _defActions[i].transform.localPosition = new Vector3(-5.5f + 2 * i, -2, -1);
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