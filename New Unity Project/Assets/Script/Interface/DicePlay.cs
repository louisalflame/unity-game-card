using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 擲骰顯示
public class DicePlayInterface {
    public InterfaceController _interface;
    private GameObject _plane = null; 
    private List<GameObject> _dices = null;
    private List<Vector3> _targets = null;
    private List<Vector3> _angles = null;
    private List<int> _result = null;

    // 初始設定地板斜度
    public DicePlayInterface(InterfaceController inter) {
        _interface = inter;
        _plane = MonoBehaviour.Instantiate(Resources.Load("Plane")) as GameObject;
        _plane.transform.localPosition = new Vector3(0, 0, -10);
        _plane.transform.localRotation = Quaternion.Euler(new Vector3(-40, 0, 0));
        _plane.transform.localScale = new Vector3(1000, 1000, 1000);
        _plane.SetActive(false);
    }

    // 擲骰和回收動畫
    public enum UpdateMode { none=1, waitStop, waitCollect };
    private int _mode = (int)UpdateMode.none;
    public void update() { 
        switch(_mode){
            case (int)UpdateMode.none: 
                break;
            case (int)UpdateMode.waitStop:
                if (isAllDicesStop()) {
                    _mode = (int)UpdateMode.waitCollect;
                    startCollectDices();
                }
                break;
            case (int)UpdateMode.waitCollect:
                if (isAllDicesCollectReady()) {
                    _mode = (int)UpdateMode.none;
                    _interface._battle.nextTurn();
                }
                else collectDices();
                break;
        }
    }
    public void setUpdateMode(int mode) { _mode = mode; }

    // 顯示地板,依順序產生骰子
    public void showDicePlay(List<Dice> diceUsing) {
        _plane.SetActive(true);
        _dices = new List<GameObject>();
        for (int n = 0; n < diceUsing.Count; n++) {
            Dice d = diceUsing[n];
            GameObject dice = DiceFactory.createDice3D(d);
            //設定左下>右上的初始位置
            dice.transform.localPosition = 
                new Vector3(-4 + ((n % 3) * 1.2f + (n / 3) * 1.2f), 5 + ((n / 3) * 1.2f), -18 + ((n / 3) * 2f));
            //設定初始隨機角度
            dice.transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            //設定初始旋轉方向和隨機力度
            dice.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 100, Random.value * 100, Random.value * 100));
            dice.GetComponent<Rigidbody>().useGravity = true;
            _dices.Add(dice);
        }
    }
    //檢查骰子是否停止
    public bool isAllDicesStop() {
        foreach (GameObject d in _dices) {
            if (d.GetComponent<Rigidbody>().velocity.magnitude > 0) return false;
        }
        return true;
    }
    //檢查骰子是否歸位
    public bool isAllDicesCollectReady() {
        for (int i = 0; i < _dices.Count && i < _targets.Count; i++) {
            if (Vector3.Distance(_dices[i].transform.localPosition, _targets[i]) > 0.01f) return false;
            if (Quaternion.Angle(_dices[i].transform.localRotation, Quaternion.Euler(_angles[i]) ) > 1) return false;
        }
        return true; 
    }
    //擲骰完畢後，確認骰面並將骰子歸位
    public void startCollectDices() {
        _plane.SetActive(false);
        _targets = new List<Vector3>();
        _angles = new List<Vector3>();
        _result = new List<int>();
        for (int i = 0; i < _dices.Count; i++) {
            GameObject dice = _dices[i];
            //先解除骰子的重力
            dice.GetComponent<Rigidbody>().useGravity = false;
            //設定排序位置
            _targets.Add(new Vector3(-5 + 1.2f * i, 0.5f, -1));
            //設定轉向
            if (Vector3.Dot(dice.transform.up, _plane.transform.up) >= 0.9f) { 
                _angles.Add( new Vector3(-90, 0, 0));
                _result.Add(5);
            } else if (Vector3.Dot(-dice.transform.up, _plane.transform.up) >= 0.9f) { 
                _angles.Add( new Vector3(90, 0, 0)); 
                _result.Add(6);
            } else if (Vector3.Dot(dice.transform.forward, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(180, 0, 0));
                _result.Add(2);
            } else if (Vector3.Dot(-dice.transform.forward, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, 0, 0));
                _result.Add(1);
            } else if (Vector3.Dot(dice.transform.right, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, 90, 0));
                _result.Add(3);
            } else if (Vector3.Dot(-dice.transform.right, _plane.transform.up) >= 0.9f) {
                _angles.Add(new Vector3(0, -90, 0));
                _result.Add(4);
            } else { _angles.Add(dice.transform.localRotation.eulerAngles); }
        }  
    }
    //update中將骰子歸位
    public void collectDices() {
        Vector3 up = _plane.transform.up;
        for (int i = 0; i < _dices.Count && i < _targets.Count; i++) {
            GameObject d = _dices[i];
            d.transform.localPosition = Vector3.MoveTowards(d.transform.localPosition, _targets[i], Time.deltaTime * ConstNum.DiceCollectSpeed);
            d.transform.localRotation = Quaternion.RotateTowards(d.transform.localRotation, Quaternion.Euler(_angles[i]), Time.deltaTime*ConstNum.DiceRotateSpeed);
        }  
    }
    public List<int> getDicesResult() { return _result; }
    //擲完骰後將3D骰子消除
    public void removeDices() {
        foreach (GameObject dice in _dices) {
            MonoBehaviour.Destroy(dice);
        }
    }
}