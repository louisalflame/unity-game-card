using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStatus {
    public InterfaceController _interface;

    private GameObject _turnInfo = null;
    private GameObject _movTurn = null;
    private GameObject _playerAtkTurn = null;
    private GameObject _playerDefTurn = null;
    private GameObject _enemyAtkTurn = null;
    private GameObject _enemyDefTurn = null;
    
    public TurnStatus(InterfaceController inter) {
        _interface = inter;

        _turnInfo = MonoBehaviour.Instantiate(Resources.Load("TurnInfo") as GameObject);
        _turnInfo.transform.localPosition = Position.getVector3(Position.turnStatusPosition);

        _movTurn = _turnInfo.transform.Find("movTurn").gameObject;
        _movTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _movTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        _movTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 1;

        _turnInfo.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _movTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        _turnInfo.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 5;


        _playerAtkTurn = _turnInfo.transform.Find("playerAtkTurn").gameObject;
        _playerAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _playerAtkTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        _playerDefTurn = _turnInfo.transform.Find("playerDefTurn").gameObject;
        _playerDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _playerDefTurn.GetComponent<SpriteRenderer>().sortingLayerID;

        _enemyAtkTurn = _turnInfo.transform.Find("enemyAtkTurn").gameObject;
        _enemyAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _enemyAtkTurn.GetComponent<SpriteRenderer>().sortingLayerID;
        _enemyDefTurn = _turnInfo.transform.Find("enemyDefTurn").gameObject;
        _enemyDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingLayerID = _enemyDefTurn.GetComponent<SpriteRenderer>().sortingLayerID;


    }

    public void showMoveTurn() {
        Debug.Log("show mov turn");
        _turnInfo.transform.Find("text").GetComponent<TextMesh>().text = NameCoder.getTurnString(_interface._battle._turnManager._turnNum);

        _playerAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _playerAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _playerDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _playerDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _enemyAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _enemyAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _enemyDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _enemyDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;

    }
    public void showPlayerAtkTurn() {
        Debug.Log("show atk turn");

        _playerAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 2;
        _playerAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 3;
        _playerDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _playerDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _enemyAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _enemyAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _enemyDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 2;
        _enemyDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 3;
    }
    public void showPlayerDefTurn() {
        Debug.Log("show def turn");

        _playerAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _playerAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
        _playerDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 2;
        _playerDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 3;
        _enemyAtkTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 2;
        _enemyAtkTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder + 3;
        _enemyDefTurn.GetComponent<SpriteRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 2;
        _enemyDefTurn.transform.Find("text").GetComponent<MeshRenderer>().sortingOrder = _movTurn.GetComponent<SpriteRenderer>().sortingOrder - 1;
    }
}
