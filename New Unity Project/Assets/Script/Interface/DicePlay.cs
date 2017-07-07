using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 擲骰顯示
public class DicePlayInterface {
    public InterfaceController _interface;
    public GameObject _plane { get; private set; }
    public List<Dice> _dices { get; private set; }
    public List<DiceFace> _result { get; private set; }
    private List<GameObject> _dices3D = null;
    private List<Vector3> _targets = null;
    private List<Vector3> _angles = null;

    // 初始設定地板斜度
    public DicePlayInterface(InterfaceController inter) {
        _interface = inter;
        _plane = MonoBehaviour.Instantiate(Resources.Load("Plane")) as GameObject;
        _plane.transform.localPosition = Position.getVector3(Position.planePosition);
        _plane.transform.localRotation = Quaternion.Euler(Position.getVector3(Position.planeRotation));
        _plane.transform.localScale = Position.getVector3(Position.planeScale);
        _plane.SetActive(false);
    }

    // 擲骰和回收動畫
    public enum UpdateMode { none=1, waitStop, waitCollect };
    private UpdateMode _mode = UpdateMode.none;
    public void update() { 
        switch(_mode){
            case UpdateMode.none: 
                break;
            case UpdateMode.waitStop:
                if (isAllDicesStop()) {
                    _mode = UpdateMode.waitCollect;
                    startCollectDices();
                }
                break;
            case UpdateMode.waitCollect:
                if (isAllDicesCollectReady()) {
                    _mode = UpdateMode.none;
                    _interface._battle.nextTurn();
                }
                else collectDices();
                break;
        }
    }
    public void setUpdateMode(int mode) {
        switch(mode){
            case (int)UpdateMode.none        : _mode = UpdateMode.none; break;
            case (int)UpdateMode.waitStop    : _mode = UpdateMode.waitStop; break;
            case (int)UpdateMode.waitCollect : _mode = UpdateMode.waitCollect; break;
        }
    }

    // 顯示地板,依順序產生骰子
    public void showDicePlay() {
        // 將地板物理顯示
        _plane.SetActive(true);
        _dices = new List<Dice>();
        _dices3D = new List<GameObject>();

        for( int i = 0; i < _interface._battle._playerManager._groundDices._dicesUsing.Count; i++ ) {
            Dice d = _interface._battle._playerManager._groundDices._dicesUsing[i];
            GameObject dice = DiceFactory.createDice3D(d);
            _dices.Add(d);
            _dices3D.Add(dice);
        }
        for( int i = 0; i < _interface._battle._playerManager._teamDices._dicesUsing.Count; i++ ) {
            Dice d = _interface._battle._playerManager._teamDices._dicesUsing[i];
            GameObject dice = DiceFactory.createDice3D(d);
            _dices.Add(d);
            _dices3D.Add(dice);
        }
        for( int i = 0; i < _interface._battle._playerManager._personDices._dicesUsing.Count; i++ ) {
            Dice d = _interface._battle._playerManager._personDices._dicesUsing[i];
            GameObject dice = DiceFactory.createDice3D(d);
            _dices.Add(d);
            _dices3D.Add(dice);
        }

        for (int i = 0; i < _dices3D.Count; i++) {
            //設定左下>右上的初始位置
            _dices3D[i].transform.localPosition = Position.getThrowDicePosition(i);
            //設定初始隨機角度
            _dices3D[i].transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            //設定初始旋轉方向和隨機力度
            _dices3D[i].GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 100, Random.value * 100, Random.value * 100));
            _dices3D[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }
    //檢查骰子是否停止
    public bool isAllDicesStop() {
        foreach (GameObject d in _dices3D) {
            if (d.GetComponent<Rigidbody>().velocity.magnitude > 0) return false;
        }
        return true;
    }
    //檢查骰子是否歸位
    public bool isAllDicesCollectReady() {
        for (int i = 0; i < _dices.Count && i < _targets.Count; i++) {
            if (Vector3.Distance(_dices3D[i].transform.localPosition, _targets[i]) > 0.01f) return false;
            if (Quaternion.Angle(_dices3D[i].transform.localRotation, Quaternion.Euler(_angles[i])) > 1) return false;
        }
        return true; 
    }
    //擲骰完畢後，確認骰面並將骰子歸位
    public void startCollectDices() {
        _plane.SetActive(false);
        _targets = new List<Vector3>();
        _angles = new List<Vector3>();
        _result = new List<DiceFace>();
        for (int i = 0; i < _dices3D.Count; i++) {
            //先解除骰子的重力
            _dices3D[i].GetComponent<Rigidbody>().useGravity = false;
            //設定排序位置
            _targets.Add(Position.getDiceCollectPosition(i, _dices3D.Count));
            //設定轉向 UP:5 FORWARD:2 RIGHT:3 DOWN:6 BACK:1 LEFT:4
            if ( Vector3.Dot(_dices3D[i].transform.up, _plane.transform.up) >= 0.9f ) { 
                _angles.Add( new Vector3(-90, 0, 0));
                _result.Add(_dices[i].getFace(5));
            } else if ( Vector3.Dot(- _dices3D[i].transform.up, _plane.transform.up) >= 0.9f ) {
                _angles.Add(new Vector3(90, 0, 0));
                _result.Add(_dices[i].getFace(6));
            } else if (Vector3.Dot(_dices3D[i].transform.forward, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(180, 0, 0));
                _result.Add(_dices[i].getFace(2));
            } else if (Vector3.Dot(_dices3D[i].transform.forward, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, 0, 0));
                _result.Add(_dices[i].getFace(1));
            } else if (Vector3.Dot(_dices3D[i].transform.right, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, 90, 0));
                _result.Add(_dices[i].getFace(3));
            } else if (Vector3.Dot(- _dices3D[i].transform.right, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, -90, 0));
                _result.Add(_dices[i].getFace(4));
            } else { // 例外處理，直接當作朝上
                _angles.Add( new Vector3(-90, 0, 0));
                _result.Add(_dices[i].getFace(5));
            }
        }  
    }
    //update中將骰子歸位
    public void collectDices() {
        Vector3 up = _plane.transform.up;
        for (int i = 0; i < _dices3D.Count && i < _targets.Count; i++) {
            GameObject d = _dices3D[i];
            d.transform.localPosition = 
                Vector3.MoveTowards(d.transform.localPosition, _targets[i], Time.deltaTime * ConstNum.DiceCollectSpeed);
            d.transform.localRotation = 
                Quaternion.RotateTowards(d.transform.localRotation, Quaternion.Euler(_angles[i]), Time.deltaTime*ConstNum.DiceRotateSpeed);
        }  
    }

    //擲完骰後將3D骰子消除
    public void removeDices() {
        foreach (GameObject dice in _dices3D) {
            MonoBehaviour.Destroy(dice);
        }
    }
}