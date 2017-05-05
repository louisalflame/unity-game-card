using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {
    private MenuButtonInterface _menuButton = null;
    private TeamStatusInterface _teamStatus = null;
    private CharStatusInterface _charStatus = null;
    private ActionButtonInterface _actionButton = null;
    private DiceBoxInterface _diceBox = null;
    private DicePlayInterface _dicePlay = null;
    private TowerStatusInterface _towerStatus = null;
    private SkillMenuInterface _skillMenu = null;

    public InterfaceController() {
        _menuButton = new MenuButtonInterface();
        _teamStatus = new TeamStatusInterface();
        _actionButton = new ActionButtonInterface();
        _charStatus = new CharStatusInterface();
        _diceBox = new DiceBoxInterface();
        _dicePlay = new DicePlayInterface();
        _towerStatus = new TowerStatusInterface();
        _skillMenu = new SkillMenuInterface();
    }

    public void update() { }

    // 介面管理
    public void showNextButton() { _actionButton.showNextButton(); }
    public void hideNextButton() { _actionButton.hideNextButton(); }
    public void showThrowButton() { _actionButton.showThrowButton(); }
    public void hideThrowButton() { _actionButton.hideThrowButton(); }
    public void showDiceBox(List<Dice> dicesUnused) { _diceBox.showDiceBox(dicesUnused); }
    public void showDicePlay(List<Dice> dicesUsing) { _dicePlay.showDicePlay(dicesUsing); }

    public bool isAllDicesStop() { return _dicePlay.isAllDicesStop(); }
    public bool isAllDicesCollectReady() { return _dicePlay.isAllDicesCollectReady(); }
    public void getDicesValue() { _dicePlay.getDicesValue(); }
    public void startCollectDices() { _dicePlay.startCollectDices();  }
    public void collectDices3D() { _dicePlay.collectDices(); }
    public void removeDices3D() { _dicePlay.removeDices(); }
}

// 基本指令選單按鈕
public class MenuButtonInterface {
    GameObject _exit = null;
    public MenuButtonInterface() {
        _exit = MonoBehaviour.Instantiate(Resources.Load("ExitBtn")) as GameObject;
        _exit.transform.localPosition = new Vector3(-4, 4, 1);
        _exit.transform.localScale = new Vector3(1, 1, 1);
    }
}
// 行動選單按鈕
public class ActionButtonInterface {
    GameObject _next = null;
    GameObject _throw = null;
    public ActionButtonInterface() {
        _next = MonoBehaviour.Instantiate(Resources.Load("NextBtn")) as GameObject;
        _next.transform.position = new Vector3(-7, -3, 1);
        _next.transform.localScale = new Vector3(1, 1, 1);
        _next.SetActive(false);
        _throw = MonoBehaviour.Instantiate(Resources.Load("ThrowBtn")) as GameObject;
        _throw.transform.localPosition = new Vector3(-5, -3, 1);
        _throw.transform.localScale = new Vector3(1, 1, 1);
        _throw.SetActive(false);
    }
    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }
}

// 隊伍狀態顯示
public class TeamStatusInterface {
    GameObject _char1 = null;
    GameObject _char2 = null;
    GameObject _char3 = null;
    public TeamStatusInterface() {
        _char1 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char1.transform.position = new Vector3(4, -4, -1);
        _char1.transform.localScale = new Vector3(1, 1, 1);
        _char2 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char2.transform.position = new Vector3(6.5f, -4, -1);
        _char2.transform.localScale = new Vector3(1, 1, 1);
        _char3 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char3.transform.position = new Vector3(9, -4, -1);
        _char3.transform.localScale = new Vector3(1, 1, 1);
    }
}
// 角色狀態顯示
public class CharStatusInterface {
    GameObject character = null;
}
// 待使用骰子顯示
public class DiceBoxInterface {
    private List<GameObject> _dices = null;
    public DiceBoxInterface() {
        _dices = new List<GameObject>();
    }

    public void showDiceBox(List<Dice> diceUnused) {
        for (int i = 0; i < diceUnused.Count; i++) {
            GameObject dice = DiceFactory.createDice2D();
            dice.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( diceUnused[i].getIconImage() );
            dice.transform.localPosition = new Vector3(-9,-4+i*1.5f,-10);
            dice.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            _dices.Add( dice );
        }
    }
}
// 擲骰顯示
public class DicePlayInterface {
    private GameObject _plane = null; 
    private List<GameObject> _dices = null;
    private List<Vector3> _targets = null;
    private List<Vector3> _angles = null;
    // 初始設定地板斜度
    public DicePlayInterface() {
        _plane = MonoBehaviour.Instantiate(Resources.Load("Plane")) as GameObject;
        _plane.transform.localPosition = new Vector3(0, 0, -10);
        _plane.transform.localRotation = Quaternion.Euler(new Vector3(-40, 0, 0));
        _plane.transform.localScale = new Vector3(10, 10, 10);
        _plane.SetActive(false);
    }
    // 顯示地板,依順序產生骰子
    public void showDicePlay(List<Dice> diceUsing) {
        _plane.SetActive(true);
        _dices = new List<GameObject>();
        for (int n = 0; n < diceUsing.Count; n++) {
            Dice d = diceUsing[n];
            GameObject dice = DiceFactory.createDice3D(d);
            //設定左下>右上的初始位置
            dice.transform.localPosition = new Vector3(-4 + (float)((n % 3) * 1.5 + (n / 3) * 1.5), 5 + (float)((n / 3) * 1.5), -15 + (float)((n / 3) * 3));
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
            if (Quaternion.Angle(_dices[i].transform.localRotation, Quaternion.Euler(_angles[i]) ) > 1) {
                Debug.Log(i + ":" + Quaternion.Angle(_dices[i].transform.localRotation, Quaternion.Euler(_angles[i])) ); return false;
            }
        }
        return true; 
    }
    //確認骰子骰面
    public void getDicesValue() {
        Vector3 up = _plane.transform.up;
        foreach (GameObject dice in _dices) {
            if (Vector3.Dot(dice.transform.up, up) >= 0.9f) { Debug.Log(5); }
            if (Vector3.Dot(-dice.transform.up, up) >= 0.9f) { Debug.Log(6); }
            if (Vector3.Dot(dice.transform.forward, up) >= 0.9f) { Debug.Log(2); }
            if (Vector3.Dot(-dice.transform.forward, up) >= 0.9f) { Debug.Log(1); }
            if (Vector3.Dot(dice.transform.right, up) >= 0.9f) { Debug.Log(3); }
            if (Vector3.Dot(-dice.transform.right, up) >= 0.9f) { Debug.Log(4); }
        }
    }
    //擲骰完畢後，確認骰面並將骰子歸位
    public void startCollectDices() {
        _plane.SetActive(false);
        _targets = new List<Vector3>();
        _angles = new List<Vector3>();
        for (int i = 0; i < _dices.Count; i++) {
            GameObject dice = _dices[i];
            //先解除骰子的重力
            dice.GetComponent<Rigidbody>().useGravity = false;
            //設定排序位置
            _targets.Add(new Vector3(-4 + 1.2f * i, -1, -10));
            //設定轉向
            if (Vector3.Dot(dice.transform.up, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(-90, 0, 0)); }
            else if (Vector3.Dot(-dice.transform.up, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(90, 0, 0)); }
            else if (Vector3.Dot(dice.transform.forward, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(180, 0, 0)); }
            else if (Vector3.Dot(-dice.transform.forward, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(0, 0, 0)); }
            else if (Vector3.Dot(dice.transform.right, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(0, 90, 0)); }
            else if (Vector3.Dot(-dice.transform.right, _plane.transform.up) >= 0.9f) { _angles.Add( new Vector3(0, -90, 0)); }
            else { _angles.Add(dice.transform.localRotation.eulerAngles); }
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
    //擲完骰後將3D骰子消除
    public void removeDices() {
        foreach (GameObject dice in _dices) {
            MonoBehaviour.Destroy(dice);
        }
    }

}
// 儲存塔狀態顯示
public class TowerStatusInterface { }
// 技能選單顯示
public class SkillMenuInterface {
    private GameObject _skillBtn1 = null;
    private GameObject _skillBtn2 = null;
    private GameObject _skillBtn3 = null;
    private GameObject _skillBtn4 = null;
    public SkillMenuInterface() {
        _skillBtn1 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn1.transform.localPosition = new Vector3(-7, -4, 1);
        _skillBtn1.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn2 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn2.transform.localPosition = new Vector3(-5, -4, 1);
        _skillBtn2.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn3 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn3.transform.localPosition = new Vector3(-3, -4, 1);
        _skillBtn3.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn4 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn4.transform.localPosition = new Vector3(-1, -4, 1);
        _skillBtn4.transform.localScale = new Vector3(1, 1, 1);
    }
}