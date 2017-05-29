using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 行動選單按鈕
public class ActionButtonInterface { 
    public InterfaceController _interface;
    private GameObject _next = null;
    private GameObject _throw = null;
    private GameObject _getFirst = null;
    private GameObject _exchange = null;
    private GameObject _standby = null;

    public ActionButtonInterface(InterfaceController inter) {
        _interface = inter;

        _next = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _next.transform.position = new Vector3(-5.5f, -1, 1);
        _next.transform.localScale = new Vector3(1, 1, 1);
        _next.transform.Find("text").GetComponent<TextMesh>().text = "Next";
        _next.GetComponent<Button>().ButtonID = "next_turn";
        _next.SetActive(false);

        _throw = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _throw.transform.localPosition = new Vector3(-3.5f, -1, 1);
        _throw.transform.localScale = new Vector3(1, 1, 1);
        _throw.transform.Find("text").GetComponent<TextMesh>().text = "Throw";
        _throw.GetComponent<Button>().ButtonID = "throw_dice";
        _throw.SetActive(false);

        _getFirst = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _getFirst.transform.localPosition = new Vector3(-5.5f, -2, 1);
        _getFirst.transform.localScale = new Vector3(1, 1, 1);
        _getFirst.transform.Find("text").GetComponent<TextMesh>().text = Move_GetFirst.text;
        _getFirst.GetComponent<Button>().ButtonID = Move_GetFirst.label;
        _getFirst.SetActive(false);

        _exchange = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _exchange.transform.localPosition = new Vector3(-3.5f, -2, 1);
        _exchange.transform.localScale = new Vector3(1, 1, 1);
        _exchange.transform.Find("text").GetComponent<TextMesh>().text = Move_Exchange.text;
        _exchange.GetComponent<Button>().ButtonID = Move_Exchange.label;
        _exchange.SetActive(false);

        _standby = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _standby.transform.localPosition = new Vector3(-1.5f, -2, 1);
        _standby.transform.localScale = new Vector3(1, 1, 1);
        _standby.transform.Find("text").GetComponent<TextMesh>().text = Move_Standby.text;
        _standby.GetComponent<Button>().ButtonID = Move_Standby.label;
        _standby.SetActive(false);
    }

    public void update() { }
    
    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }
    public void showMoveActionButton() {
        _getFirst.SetActive(true);
        _exchange.SetActive(true);
        _standby.SetActive(true);
    }
    public void hideMoveActionButton() {
        _getFirst.SetActive(false);
        _exchange.SetActive(false);
        _standby.SetActive(false);
    }
}