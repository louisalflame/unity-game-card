using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStatus {
    public InterfaceController _interface;

    private GameObject _turnInfo = null;

    public TurnStatus(InterfaceController inter) {
        _interface = inter;

        _turnInfo = MonoBehaviour.Instantiate(Resources.Load("TurnInfo") as GameObject);
        _turnInfo.transform.localPosition = Position.getVector3( Position.turnStatusPosition );
        _turnInfo.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _turnInfo.transform.Find("turnBack").GetComponent<SpriteRenderer>().sortingLayerID;
        _turnInfo.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _turnInfo.transform.Find("turnBack").GetComponent<SpriteRenderer>().sortingOrder + 1;
        _turnInfo.transform.Find("turnBack").gameObject.SetActive(false);

        GameObject playerTurn = _turnInfo.transform.Find("playerTurn").gameObject;
        playerTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = playerTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        playerTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = playerTurn.GetComponent<SpriteRenderer>().sortingOrder + 1;
        playerTurn.SetActive(false);

        GameObject enemyTurn = _turnInfo.transform.Find("enemyTurn").gameObject;
        enemyTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = enemyTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        enemyTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = enemyTurn.GetComponent<SpriteRenderer>().sortingOrder + 1;
        enemyTurn.SetActive(false);

    }

    public void showMoveTurn() {
        Debug.Log("show mov turn");
        _turnInfo.transform.Find("text").GetComponent<TextMesh>().text = NameCoder.getTurnString(_interface._battle._turnManager._turnNum);
        _turnInfo.transform.Find("turnBack").gameObject.SetActive(true);
        _turnInfo.transform.Find("playerTurn").gameObject.SetActive(false);
        _turnInfo.transform.Find("enemyTurn").gameObject.SetActive(false);
    }
    public void showPlayerAtkTurn() {
        Debug.Log("show atk turn");
        _turnInfo.transform.Find("turnBack").gameObject.SetActive(false);
        _turnInfo.transform.Find("playerTurn").gameObject.SetActive(true);
        _turnInfo.transform.Find("enemyTurn").gameObject.SetActive(true);
    }
    public void showPlayerDefTurn() {
        Debug.Log("show def turn");
        _turnInfo.transform.Find("turnBack").gameObject.SetActive(false);
        _turnInfo.transform.Find("playerTurn").gameObject.SetActive(true);
        _turnInfo.transform.Find("enemyTurn").gameObject.SetActive(true);
    }
}
