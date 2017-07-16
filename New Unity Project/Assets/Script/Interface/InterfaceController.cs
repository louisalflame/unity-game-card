using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {
    public GameObject _canvas { get; private set; }
    public GameObject _panel { get; private set; }

    public BattleController _battle;
    public MenuButtonInterface _menuButton { get; private set; }
    public TurnStatus _turnStatus { get; private set; }

    public ActionButtonInterface _actionButton { get; private set; }

    public TowerStatusInterface _towerStatus { get; private set; }
    public TowerStatusEnemyInterface _towerStatusEnemy { get; private set; }
    public AttrPointsInterface _attrPoints { get; private set; }
    public AttrPointsEnemyInterface _attrPointsEnemy { get; private set; }

    public SkillMenuInterface _skillMenu { get; private set; }
    public TeamPlayerStatusInterface _teamPlayerStatus { get; private set; }
    public TeamEnemyStatusInterface _teamEnemyStatus { get; private set; }

    public DiceBoxInterface _diceBox { get; private set; }
    public DicePlayInterface _dicePlay { get; private set; }
    public AttrDecisionInterface _attrDecision { get; private set; }

    public InterfaceController(BattleController battle) {
        _battle = battle;

        _canvas = CanvasFactory.createCanvas();
        _panel = CanvasFactory.createBasicRatioPanel(_canvas);

        _menuButton = new MenuButtonInterface(this);
        _turnStatus = new TurnStatus(this);

        _actionButton = new ActionButtonInterface(this);

        _towerStatus = new TowerStatusInterface(this);
        _towerStatusEnemy = new TowerStatusEnemyInterface(this);
        _attrPoints = new AttrPointsInterface(this);
        _attrPointsEnemy = new AttrPointsEnemyInterface(this);

        _skillMenu = new SkillMenuInterface(this);
        _teamPlayerStatus = new TeamPlayerStatusInterface(this);
        _teamEnemyStatus = new TeamEnemyStatusInterface(this);

        _diceBox = new DiceBoxInterface(this);
        _dicePlay = new DicePlayInterface(this);
        _attrDecision = new AttrDecisionInterface(this);
    }

    public void update() {
        _menuButton.update();
        _teamPlayerStatus.update();
        _teamEnemyStatus.update();
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

    public void setTeamPlayer() { _teamPlayerStatus.setCharacters( _battle._playerManager._characters ); }
    public void setTeamEnemy() { _teamEnemyStatus.setCharacters(_battle._enemyManager._characters ); }

    public void showMoveTurn() { _turnStatus.showMoveTurn(); }
    public void showPlayerAtkTurn() { _turnStatus.showPlayerAtkTurn(); }
    public void showPlayerDefTurn() { _turnStatus.showPlayerDefTurn(); }

    public void resetActionButtons(CharManager character) { _actionButton.resetActionButtons(character); }
    public void showMoveActionButton() { _actionButton.showMoveActionButton(); }
    public void hideMoveActionButton() { _actionButton.hideMoveActionButton(); }
    public void showAttackActionButton() { _actionButton.showAttackActionButton(); }
    public void hideAttackActionButton() { _actionButton.hideAttackActionButton(); }
    public void showDefenseActionButton() { _actionButton.showDefenseActionButton(); }
    public void hideDefenseActionButton() { _actionButton.hideDefenseActionButton(); }

    public void changePlayerActiveCharTo(int n) { _teamPlayerStatus.changeActiveCharStatus(n); }
    public void changeEnemyActiveCharTo(int n) { _teamEnemyStatus.changeActiveCharStatus(n); }
    public void showTeamRearrangeButton() { _teamPlayerStatus.showTeamRearrangeButton(); }
    public void hideTeamRearrangeButton() { _teamPlayerStatus.hideTeamRearrangeButton(); }
    public void placeEnemyTeamStatus() { _teamEnemyStatus.placeCharStatusPos(); }
    
    public void checkDiceBox(int type) { _diceBox.checkDiceBox(type); }
    public void showDicePlay() { _dicePlay.showDicePlay(); }

    public void showAttrDecision() { _attrDecision.showFaces(); }

    public void startWaitDicesAnimate() { _dicePlay.setUpdateMode((int)DicePlayInterface.UpdateMode.waitStop); }
    public void removeDices3D() { _dicePlay.removeDices(); }
    public void hideFaceDecision() { _attrDecision.hideFaceDecision(); }

    public void setTowerStatus(AttrTower[] towers) { _towerStatus.setTowerStatus(towers); }
    public void setAttrNums(int[] attrNums) { _attrPoints.setAttrNums(attrNums); }
    public void setTowerStatusEnemy(AttrTower[] towers) { _towerStatusEnemy.setTowerStatus(towers); }
    public void setAttrNumsEnemy(int[] attrNums) { _attrPointsEnemy.setAttrNums(attrNums); }
    
}

// 基本指令選單按鈕
public class MenuButtonInterface {
    public InterfaceController _interface;
    public GameObject _topBar { get; private set; }
    public GameObject _bottomBar { get; private set; }
    public GameObject _mainBase { get; private set; }
    public GameObject _actionBase { get; private set; }

    public GameObject _exit { get; private set; }
    public GameObject _next { get; private set; }
    public GameObject _throw { get; private set; }

    public MenuButtonInterface(InterfaceController inter) {
        _interface = inter;

        _topBar = CanvasFactory.create_BattleScene_TopBar(_interface._panel);
        _mainBase = CanvasFactory.create_BattleScene_MainButtonBase(_interface._panel);
        _actionBase = CanvasFactory.create_BattleScene_ActionButtonBase(_mainBase);
        _bottomBar = CanvasFactory.create_BattleScene_BottomBar(_interface._panel);

        _exit = CanvasFactory.create_BattleScene_ExitBtn(_topBar);
        _next = CanvasFactory.create_BattleScene_NextBtn(_mainBase);
        _throw = CanvasFactory.create_BattleScene_ThrowBtn(_mainBase);

        hideNextButton();
        hideThrowButton();

        /*
        _topBar = MonoBehaviour.Instantiate(Resources.Load("TopBar")) as GameObject;
        _bottomBar = MonoBehaviour.Instantiate(Resources.Load("BottomBar")) as GameObject;
        _mainButtonBase = MonoBehaviour.Instantiate(Resources.Load("MainButtonBack")) as GameObject;
        _mainButtonBase.transform.localPosition = Position.getVector3(Position.mainBtnBase);

        _exit = MonoBehaviour.Instantiate(Resources.Load("SystemButton")) as GameObject;
        _exit.transform.parent = _topBar.transform;
        _exit.transform.localPosition = Position.getVector3( Position.systemBtn );
        NameCoder.setButtonLabel_ID(_exit, NameCoder.ExitButton);

        _next = MonoBehaviour.Instantiate(Resources.Load("MainButton")) as GameObject;
        _next.transform.parent = _mainButtonBase.transform;
        _next.transform.localPosition = Position.getVector3(Position.mainBtn);
        NameCoder.setButtonLabel_ID(_next, NameCoder.NextButton);
        _next.SetActive(false);

        _throw = MonoBehaviour.Instantiate(Resources.Load("MainButton")) as GameObject;
        _throw.transform.parent = _mainButtonBase.transform;
        _throw.transform.localPosition = Position.getVector3(Position.mainBtn);
        NameCoder.setButtonLabel_ID(_throw, NameCoder.ThrowButton);
        _throw.SetActive(false);*/


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
            _skillButtons[i].transform.parent = _interface._menuButton._mainBase.transform;
            _skillButtons[i].transform.localPosition = Position.getVector3(Position.skillBtn[i]);
        }
    }
    public void update() { }
}