using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {
    public BattleController _battle;
    private MenuButtonInterface _menuButton = null;
    private TeamStatusInterface _teamStatus = null;
    private CharStatusInterface _charStatus = null;
    private ActionButtonInterface _actionButton = null;
    private DiceBoxInterface _diceBox = null;
    private DicePlayInterface _dicePlay = null;
    private AttrDecisionInterface _attrDecision = null;
    private TowerStatusInterface _towerStatus = null;
    private AttrPointsInterface _attrPoints = null;
    private SkillMenuInterface _skillMenu = null;

    public InterfaceController(BattleController battle) {
        _battle = battle;
        _menuButton = new MenuButtonInterface(this);
        _teamStatus = new TeamStatusInterface(this);
        _actionButton = new ActionButtonInterface(this);
        _charStatus = new CharStatusInterface(this);
        _diceBox = new DiceBoxInterface(this);
        _dicePlay = new DicePlayInterface(this);
        _attrDecision = new AttrDecisionInterface(this);
        _towerStatus = new TowerStatusInterface(this);
        _attrPoints = new AttrPointsInterface(this);
        _skillMenu = new SkillMenuInterface(this);
    }

    public void update() {
        _menuButton.update();
        _teamStatus.update();
        _charStatus.update();
        _actionButton.update();
        _diceBox.update();
        _dicePlay.update();
        _attrDecision.update();
        _towerStatus.update();
        _attrPoints.update();
        _skillMenu.update();
    }

    // 介面管理
    public void showNextButton() { _actionButton.showNextButton(); }
    public void hideNextButton() { _actionButton.hideNextButton(); }
    public void showThrowButton() { _actionButton.showThrowButton(); }
    public void hideThrowButton() { _actionButton.hideThrowButton(); }
    public void showMoveActionButton() { _actionButton.showMoveActionButton(); }
    public void hideMoveActionButton() { _actionButton.hideMoveActionButton(); }

    public void showTeamRearrangeButton() { _teamStatus.showTeamRearrangeButton(); }
    public void hideTeamRearrangeButton() { _teamStatus.hideTeamRearrangeButton();  }

    public void showDiceBox(List<Dice> dicesUnused) { _diceBox.showDiceBox(dicesUnused); }
    public void showDicePlay(List<Dice> dicesUsing) { _dicePlay.showDicePlay(dicesUsing); }

    public void setAttrDecision(AttrDecisionManager attrDecisionManager) { _attrDecision.setAttrDecision(attrDecisionManager); }
    public void showAttrDecision() { _attrDecision.showFaces(); }

    public void startWaitDicesAnimate() { _dicePlay.setUpdateMode((int)DicePlayInterface.UpdateMode.waitStop); }
    public List<int> getDicesResult() { return _dicePlay.getDicesResult(); }
    public void removeDices3D() { _dicePlay.removeDices(); }
    public void removeFaceDecision() { _attrDecision.clear(); }

    public void setTowerStatus(AttrTower[] towers) { _towerStatus.setTowerStatus(towers); }
    public void setAttrNums(int[] attrNums) { _attrPoints.setAttrNums(attrNums); }
}

// 基本指令選單按鈕
public class MenuButtonInterface {
    public InterfaceController _interface;
    private GameObject _exit = null;
    public MenuButtonInterface(InterfaceController inter) {
        _interface = inter; 

        _exit = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _exit.transform.localPosition = new Vector3(-4, 4, 1);
        _exit.transform.localScale = new Vector3(1, 1, 1);
        _exit.transform.Find("text").GetComponent<TextMesh>().text = "EXIT";
        _exit.GetComponent<Button>().ButtonID = "exit";
    }
    public void update() { }
}

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
        _getFirst.transform.Find("text").GetComponent<TextMesh>().text = "Get First";
        _getFirst.GetComponent<Button>().ButtonID = "get_first";
        _getFirst.SetActive(false);

        _exchange = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _exchange.transform.localPosition = new Vector3(-3.5f, -2, 1);
        _exchange.transform.localScale = new Vector3(1, 1, 1);
        _exchange.transform.Find("text").GetComponent<TextMesh>().text = "Exchange";
        _exchange.GetComponent<Button>().ButtonID = "exchange";
        _exchange.SetActive(false);

        _standby = MonoBehaviour.Instantiate(Resources.Load("SingleButton")) as GameObject;
        _standby.transform.localPosition = new Vector3(-1.5f, -2, 1);
        _standby.transform.localScale = new Vector3(1, 1, 1);
        _standby.transform.Find("text").GetComponent<TextMesh>().text = "StandBy";
        _standby.GetComponent<Button>().ButtonID = "standby";
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

// 隊伍狀態顯示
public class TeamStatusInterface {
    public InterfaceController _interface;
    GameObject _char1 = null;
    GameObject _char2 = null;
    GameObject _char3 = null;
    public TeamStatusInterface(InterfaceController inter) {
        _interface = inter;
        _char1 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char1.transform.position = new Vector3(-4.5f, -3.5f, -1);
        _char1.transform.localScale = new Vector3(2, 2, 1);
        _char2 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char2.transform.position = new Vector3(-1, -4, -1);
        _char2.transform.localScale = new Vector3(1, 1, 1);
        _char2.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(2);
        _char2.GetComponent<BoxCollider2D>().enabled = false;
        _char3 = MonoBehaviour.Instantiate(Resources.Load("Character") as GameObject);
        _char3.transform.position = new Vector3(1.5f, -4, -1);
        _char3.transform.localScale = new Vector3(1, 1, 1);
        _char3.GetComponent<Button>().ButtonID = StringCoder.getChangeCharString(3);
        _char3.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void update() { }

    public void showTeamRearrangeButton() {
        _char2.GetComponent<BoxCollider2D>().enabled = true;
        _char3.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void hideTeamRearrangeButton() {
        _char2.GetComponent<BoxCollider2D>().enabled = false;
        _char3.GetComponent<BoxCollider2D>().enabled = false;
    }
}
// 角色狀態顯示
public class CharStatusInterface {
    public InterfaceController _interface;
    GameObject character = null;
    public CharStatusInterface(InterfaceController inter) { 
        _interface = inter;
    }
    public void update() { }
}
// 待使用骰子顯示
public class DiceBoxInterface {
    public InterfaceController _interface;
    private List<GameObject> _dices = null;
    public DiceBoxInterface(InterfaceController inter) {
        _interface = inter;
        _dices = new List<GameObject>();
    }

    public void clear() {
        foreach (GameObject d in _dices) {
            MonoBehaviour.Destroy(d);
        }
    }

    public void showDiceBox(List<Dice> diceUnused) {
        clear();
        for (int i = 0; i < diceUnused.Count; i++) {
            GameObject dice = DiceFactory.createDice2D();
            dice.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( diceUnused[i].getIconImage() );
            dice.transform.localPosition = new Vector3(-8,-4+i*1.5f,-10);
            _dices.Add( dice );
        }
    }
    public void update() { }
}
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

//骰面收集分配
public class AttrDecisionInterface{
    public InterfaceController _interface;
    public AttrDecisionManager _manager;
    private List<GameObject> _facesAttr = null;
    private List<GameObject> _facesBase = null;
    public AttrDecisionInterface(InterfaceController inter){
        _interface = inter;
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();
    }
    public void setAttrDecision(AttrDecisionManager manager) { _manager = manager; }

    public void clear() {
        foreach (GameObject o in _facesAttr) { MonoBehaviour.Destroy(o); }
        foreach (GameObject o in _facesBase) { MonoBehaviour.Destroy(o); }
        _facesAttr = new List<GameObject>();
        _facesBase = new List<GameObject>();
    }
    public void showFaces() {
        clear();
        List<DiceFace> toAttr = _manager.getFacesAttr();
        for (int i = 0; i < toAttr.Count; i++) {
            GameObject sprite = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( toAttr[i].getImage() );
            sprite.GetComponent<Button>().ButtonID = StringCoder.getAttrDecisionString(i);
            sprite.transform.localPosition = new Vector3(-5 + i * 1.2f, 0.5f, -2);
            _facesAttr.Add(sprite);
        }
        List<DiceFace> toBase = _manager.getFacesBase();
        for (int i = 0; i < toBase.Count; i++) {
            GameObject sprite = MonoBehaviour.Instantiate(Resources.Load("FaceDecisionBtn") as GameObject);
            sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( toBase[i].getBaseImage() );
            sprite.GetComponent<Button>().ButtonID = StringCoder.getBaseDecisionString(i);
            sprite.transform.localPosition = new Vector3(-5 + i * 1.2f, -0.5f, -2);
            _facesBase.Add(sprite);
        }
    }

    public void update() { }
}

// 儲存塔狀態顯示
public class TowerStatusInterface {
    public InterfaceController _interface;
    private GameObject[] _towers = null;
    public TowerStatusInterface(InterfaceController inter) { 
        _interface = inter;
        _towers = new GameObject[6] { null, null, null, null, null, null };
        _towers[0] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[0].transform.localPosition = new Vector3(3, -2, 1);
        _towers[1] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[1].transform.localPosition = new Vector3(4, -2, 1);
        _towers[2] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[2].transform.localPosition = new Vector3(5, -2, 1);
        _towers[3] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[3].transform.localPosition = new Vector3(6, -2, 1);
        _towers[4] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[4].transform.localPosition = new Vector3(7, -2, 1);
        _towers[5]= MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _towers[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        _towers[5].transform.localPosition = new Vector3(8, -2, 1);
    }

    public void setTowerStatus(AttrTower[] towers) {
        for (int i = 0; i < _towers.Length && i < towers.Length; i++) {
            _towers[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( towers[i].getImage() );
        }
    }

    public void update() { }
}

//屬性點數存量顯示
public class AttrPointsInterface {
    public InterfaceController _interface;
    private GameObject[] _attrIcons = { null, null, null, null, null, null, null };
    private GameObject[] _attrNums = { null, null, null, null, null, null, null };
    public AttrPointsInterface(InterfaceController inter) {
        _interface = inter;
        _attrNums[0] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[0].transform.localPosition = new Vector3(3, -4, 1);
        _attrNums[0].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[1] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[1].transform.localPosition = new Vector3(4, -4, 1);
        _attrNums[1].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[2] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[2].transform.localPosition = new Vector3(5, -4, 1);
        _attrNums[2].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[3] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[3].transform.localPosition = new Vector3(6, -4, 1);
        _attrNums[3].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[4] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[4].transform.localPosition = new Vector3(7, -4, 1);
        _attrNums[4].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrNums[5] = MonoBehaviour.Instantiate(Resources.Load("NumBase") as GameObject);
        _attrNums[5].transform.localPosition = new Vector3(8, -4, 1);
        _attrNums[5].transform.Find("num").GetComponent<TextMesh>().text = 0.ToString();
        _attrIcons[0] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrNor");
        _attrIcons[0].transform.localPosition = new Vector3(3, -3, 1);
        _attrIcons[1] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrAtk");
        _attrIcons[1].transform.localPosition = new Vector3(4, -3, 1);
        _attrIcons[2] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrDef");
        _attrIcons[2].transform.localPosition = new Vector3(5, -3, 1);
        _attrIcons[3] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrMov");
        _attrIcons[3].transform.localPosition = new Vector3(6, -3, 1);
        _attrIcons[4] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrSpc");
        _attrIcons[4].transform.localPosition = new Vector3(7, -3, 1);
        _attrIcons[5] = MonoBehaviour.Instantiate(Resources.Load("AttrBase") as GameObject);
        _attrIcons[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/AttrIcon/AttrHeal");
        _attrIcons[5].transform.localPosition = new Vector3(8, -3, 1);
    }

    public void setAttrNums(int[] attrNums) {
        for (int i = 0; i < attrNums.Length && i < _attrNums.Length; i++) {
            _attrNums[i].transform.Find("num").GetComponent<TextMesh>().text = attrNums[i].ToString();
        }
    }
    public void setAttrNum(int attr, int num) {
        _attrNums[attr].transform.Find("num").GetComponent<TextMesh>().text = num.ToString();
    }

    public void update() { }
}

// 技能選單顯示
public class SkillMenuInterface {
    public InterfaceController _interface;
    private GameObject _skillBtn1 = null;
    private GameObject _skillBtn2 = null;
    private GameObject _skillBtn3 = null;
    private GameObject _skillBtn4 = null;
    public SkillMenuInterface(InterfaceController inter) {
        _interface = inter;
        _skillBtn1 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn1.transform.localPosition = new Vector3(7, 0, 1);
        _skillBtn1.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn2 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn2.transform.localPosition = new Vector3(7, -0.5f, 1);
        _skillBtn2.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn3 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn3.transform.localPosition = new Vector3(7, -1, 1);
        _skillBtn3.transform.localScale = new Vector3(1, 1, 1);
        _skillBtn4 = MonoBehaviour.Instantiate(Resources.Load("SkillBtn") as GameObject);
        _skillBtn4.transform.localPosition = new Vector3(7, -1.5f, 1);
        _skillBtn4.transform.localScale = new Vector3(1, 1, 1);
    }
    public void update() { }
}