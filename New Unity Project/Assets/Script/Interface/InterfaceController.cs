using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {
    public BattleController _battle;
    public MenuButtonInterface _menuButton { get; private set; }
    public TeamStatusInterface _teamStatus { get; private set; }
    public CharStatusInterface _charStatus { get; private set; }
    public ActionButtonInterface _actionButton { get; private set; }
    public DiceBoxInterface _diceBox { get; private set; }
    public DicePlayInterface _dicePlay { get; private set; }
    public AttrDecisionInterface _attrDecision { get; private set; }
    public TowerStatusInterface _towerStatus { get; private set; }
    public AttrPointsInterface _attrPoints { get; private set; }
    public SkillMenuInterface _skillMenu { get; private set; }

    public InterfaceController(BattleController battle) {
        _battle = battle;
        _menuButton = new MenuButtonInterface(this);
        _teamStatus = new TeamStatusInterface(this);
        _actionButton = new ActionButtonInterface(this);
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
        _actionButton.update();
        _diceBox.update();
        _dicePlay.update();
        _attrDecision.update();
        _towerStatus.update();
        _attrPoints.update();
        _skillMenu.update();
    }

    // 介面管理
    public void showNextButton() { _menuButton.showNextButton(); }
    public void hideNextButton() { _menuButton.hideNextButton(); }
    public void showThrowButton() { _menuButton.showThrowButton(); }
    public void hideThrowButton() { _menuButton.hideThrowButton(); } 

    public void setTeamPlayer(TeamManager player) { _teamStatus.setPlayerCharacters(player._characters); }
    public void setTeamEnemy(TeamManager enemy) { _teamStatus.setEnemyCharacters(enemy._characters);  }

    public void resetActionButtons(CharManager character) { _actionButton.resetActionButtons(character); }
    public void showMoveActionButton() { _actionButton.showMoveActionButton(); }
    public void hideMoveActionButton() { _actionButton.hideMoveActionButton(); }
    public void showAttackActionButton() { _actionButton.showAttackActionButton(); }
    public void hideAttackActionButton() { _actionButton.hideAttackActionButton(); }
    public void showDefenseActionButton() { _actionButton.showDefenseActionButton(); }
    public void hideDefenseActionButton() { _actionButton.hideDefenseActionButton(); }

    public void showTeamRearrangeButton() { _teamStatus.showTeamRearrangeButton(); }
    public void hideTeamRearrangeButton() { _teamStatus.hideTeamRearrangeButton();  }
    
    public void checkDiceBox(int type) { _diceBox.checkDiceBox(type); }
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
    public GameObject _topBar { get; private set; }
    public GameObject _bottomBar { get; private set; }
    public GameObject _mainButtonBack { get; private set; }

    public GameObject _exit { get; private set; }
    public GameObject _next { get; private set; }
    public GameObject _throw { get; private set; }

    public MenuButtonInterface(InterfaceController inter) {
        _interface = inter;

        _topBar = MonoBehaviour.Instantiate(Resources.Load("TopBar")) as GameObject;
        _bottomBar = MonoBehaviour.Instantiate(Resources.Load("BottomBar")) as GameObject;
        _mainButtonBack = MonoBehaviour.Instantiate(Resources.Load("MainButtonBack")) as GameObject;

        _exit = MonoBehaviour.Instantiate(Resources.Load("SystemButton")) as GameObject;
        _exit.transform.parent = _topBar.transform;
        _exit.transform.localPosition = Position.getVector3( Position.systemBtn );
        NameCoder.setButtonLabel_ID(_exit, NameCoder.ExitButton);

        _next = MonoBehaviour.Instantiate(Resources.Load("MainButton")) as GameObject;
        _next.transform.parent = _mainButtonBack.transform;
        _next.transform.localPosition = Position.getVector3(Position.mainBtn);
        NameCoder.setButtonLabel_ID(_next, NameCoder.NextButton);
        _next.SetActive(false);

        _throw = MonoBehaviour.Instantiate(Resources.Load("MainButton")) as GameObject;
        _throw.transform.parent = _mainButtonBack.transform;
        _throw.transform.localPosition = Position.getVector3(Position.mainBtn);
        NameCoder.setButtonLabel_ID(_throw, NameCoder.ThrowButton);
        _throw.SetActive(false);


    }
    public void update() { }

    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }
}


// 技能選單顯示
public class SkillMenuInterface {
    public InterfaceController _interface;
    public GameObject[] _skillButtons { get; private set; }

    public SkillMenuInterface(InterfaceController inter) {
        _interface = inter;

        _skillButtons = new GameObject[4] {null, null, null, null};

        for(int i = 0; i < _skillButtons.Length; i++){
            _skillButtons[i] = MonoBehaviour.Instantiate(Resources.Load("SkillButton") as GameObject);
            _skillButtons[i].transform.parent = _interface._menuButton._mainButtonBack.transform;
            _skillButtons[i].transform.localPosition = Position.getVector3(Position.skillBtn[i]);
        }
    }
    public void update() { }
}