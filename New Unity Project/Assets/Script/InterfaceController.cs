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