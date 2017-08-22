using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {

    public BattleController _battle;
    public InterfaceAnimator _animator { get; private set; }

    public GameObject _canvas { get; private set; }
    public MenuButtonInterface _menuButton { get; private set; }
    public MainLayoutInterface _mainMenu { get; private set; }

    public PlayerPlaceInterface _charPlayerPlace { get; private set; }
    public EnemyPlaceInterface _charEnemyPlace { get; private set; }
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

        //建立動畫管理
        _animator = new InterfaceAnimator(this);

        // 先把基本畫布建立，才能繪製各原件
        createCanvasLayout();

        // 需要先繪製基本版型
        _mainMenu = new MainLayoutInterface(this);
        _menuButton = new MenuButtonInterface(this);

        _actionButton = new ActionButtonInterface(this);
        _towerStatus = new TowerStatusInterface(this);
        _towerStatusEnemy = new TowerStatusEnemyInterface(this);
        _attrPoints = new AttrPointsInterface(this);
        _attrPointsEnemy = new AttrPointsEnemyInterface(this);
        _skillMenu = new SkillMenuInterface(this);
        
        _teamPlayerStatus = new TeamPlayerStatusInterface(this, _battle._playerManager);
        _teamEnemyStatus = new TeamEnemyStatusInterface(this, battle._enemyManager);

        _diceBox = new DiceBoxInterface(this);
        _dicePlay = new DicePlayInterface(this);
        _attrDecision = new AttrDecisionInterface(this);

        _charPlayerPlace = new PlayerPlaceInterface(this);
        _charEnemyPlace = new EnemyPlaceInterface(this);
        _turnStatus = new TurnStatus(this);
    }
    public void create() {
        _mainMenu.create();
        _menuButton.create();
        _skillMenu.create();

        _charPlayerPlace.create();
        _charEnemyPlace.create();

        _teamPlayerStatus.create();
        _teamEnemyStatus.create();

        _diceBox.create();
        _turnStatus.create();
        _attrDecision.create();

        _attrPoints.create();
        _towerStatus.create();
        _attrPointsEnemy.create();
        _towerStatusEnemy.create();
    }

    public void initial() {
        _mainMenu.initial();
        _menuButton.initial();

        _charPlayerPlace.initial();
        _charEnemyPlace.initial();

        _towerStatus.initial();
        _attrPoints.initial();
        _attrPointsEnemy.initial();
        _towerStatusEnemy.initial();

        _diceBox.initial();
        _turnStatus.initial();
        _attrDecision.initial();

        setTeamPlayer();
        setTeamEnemy();
        _teamPlayerStatus.initial();
        _teamEnemyStatus.initial();
    }

    public void update() {
        _animator.update();

        _menuButton.update();
        _teamPlayerStatus.update();
        _teamEnemyStatus.update();
        _actionButton.update();
        _diceBox.update();
        _dicePlay.update();
        _attrDecision.update();
        _towerStatus.update();
        _attrPoints.update();
        _towerStatusEnemy.update();
        _attrPointsEnemy.update();
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
    public List<DiceFace> getDiceThrowResult() { return _dicePlay._result; }

    public void changeAttrDecision() { _attrDecision.showFaces(); }

    public void startWaitDicesAnimate() { _dicePlay.setUpdateMode((int)DicePlayInterface.UpdateMode.waitStop); }
    public void removeDices3D() { _dicePlay.removeDices(); }

    public void changeTowerStatus(AttrTower tower) { _towerStatus.changeTowerStatus(tower); }
    public void setTowerStatus(AttrTower[] towers) { _towerStatus.setTowerStatus(towers); }
    public void setAttrNums(int[] attrNums) { _attrPoints.setAttrNums(attrNums); }
    public void addAttrNum(int attr, int num) { _attrPoints.addAttrNum(attr, num); }
    public void changeTowerStatusEnemy(AttrTower tower) { _towerStatusEnemy.changeTowerStatus(tower); }
    public void setTowerStatusEnemy(AttrTower[] towers) { _towerStatusEnemy.setTowerStatus(towers); }
    public void setAttrNumsEnemy(int[] attrNums) { _attrPointsEnemy.setAttrNums(attrNums); }

    // 取得下階段可使用骰子
    public List<Dice> getDicesGroundUseStack() {
        return _battle._playerManager._groundDices._dicesUnused.GetRange(0, _battle._playerManager._groundDices._useStack);
    }
    public List<Dice> getDicesTeamUseStack() {
        return _battle._playerManager._teamDices._dicesUnused.GetRange(0, _battle._playerManager._teamDices._useStack);
    }
    public List<Dice> getDicesPersonUseStack() {
        return _battle._playerManager._personDices._dicesUnused.GetRange(0, _battle._playerManager._personDices._useStack);
    }

    // 基本Layout物件
    public void createCanvasLayout() {
        _canvas = GameObject.Find("Canvas");
        if (_canvas == null) { _canvas = CanvasFactory.createCanvas(); }
    }
    public GameObject getImageMainBattleField() { return _mainMenu._mainBattleField; }
    public GameObject getImageLeftBattleField() { return _mainMenu._leftBattleField; }
    public GameObject getImageMiddleBattleField() { return _mainMenu._middleBattleField; }
    public GameObject getImageRightBattleField() { return _mainMenu._rightBattleField; }
    public GameObject getImageDecisionField() { return _mainMenu._decisionField; }
    public GameObject getImageTopBar() { return _mainMenu._topBar; }
    public GameObject getImageBottomBar() { return _mainMenu._bottomBar; }
    public GameObject getImageMainMenu() { return _mainMenu._mainMenu; }
    public GameObject getImageLeftMenu() { return _mainMenu._leftMenu; }
    public GameObject getImageMiddleMenu() { return _mainMenu._middleMenu; }
    public GameObject getImageRightMenu() { return _mainMenu._rightMenu; }
    public GameObject getImageEnemyTableMask() { return _mainMenu._enemyTableMask; }
    public GameObject getImageActionBase() { return _mainMenu._actionBase; }
    public GameObject getImageLeftStatus() { return _mainMenu._leftStatus; }
    public GameObject getImageRightStatus() { return _mainMenu._rightStatus; }
    public GameObject getImageDiceBox() { return _diceBox._diceBox;  }
    public GameObject getImageTurnStatus() { return _turnStatus._turnStatus; }
    public GameObject getImageTurnInfo() { return _turnStatus._turnInfo; }
    public GameObject getImageMovTurn() { return _turnStatus._movTurn; }
    public GameObject getImageAtkTurn() { return _turnStatus._atkTurn; }
    public GameObject getImageDefTurn() { return _turnStatus._defTurn; }
    public GameObject getImagePlayerAtkTurn() { return _turnStatus._playerAtk; }
    public GameObject getImagePlayerDefTurn() { return _turnStatus._playerDef; }
    public GameObject getImageEnemyAtkTurn() { return _turnStatus._enemyAtk; }
    public GameObject getImageEnemyDefTurn() { return _turnStatus._enemyDef; }
    public GameObject getImageAttrDecisionBack() { return _attrDecision._attrBack; }
    public GameObject getImageBaseDecisionBack() { return _attrDecision._baseBack; }

    // 各段落動畫
    public void animate_PrepareShiftIn() { _animator.prepareShiftIn(); }
    public void animate_StartMovTurn() { _animator.StartMovTurn(); }
    public void animate_CollectFaceDecision() { _animator.CollectFaceDecision(); }
    public void animate_AggregateAttrTower() { _animator.AggregateAttrTower(); }
    public void animate_StartPlayerAtkTurn() { _animator.StartPlayerAtkTurn(); }
    public void animate_StartPlayerDefTurn() { _animator.StartPlayerDefTurn(); }
}
 

// 技能選單顯示
public class SkillMenuInterface {
    public InterfaceController _interface;
    public GameObject _skillTable { get; private set; }
    public GameObject[] _skillButtons { get; private set; }

    public SkillMenuInterface(InterfaceController inter) { _interface = inter; }

    public void create() {
        create_BattleScene_PlayerSkillTable( _interface.getImageLeftMenu() );
    }

    public void update() { }

    // Menu Skill Table *************
    public void create_BattleScene_PlayerSkillTable(GameObject parent) {
        _skillTable = CanvasFactory.createEmptyRect(parent, "PlayerSkill");
        CanvasFactory.setRectTransformAnchor(_skillTable, new Vector2(0.1f, 0.75f), new Vector2(0.95f, 0.95f), Vector2.zero, Vector2.zero);

        GameObject skill1 = create_SkillBtn_Unit(_skillTable);
        CanvasFactory.setRectTransformAnchor(skill1, new Vector2(0.01f, 0f), new Vector2(0.24f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill2 = create_SkillBtn_Unit(_skillTable);
        CanvasFactory.setRectTransformAnchor(skill2, new Vector2(0.26f, 0f), new Vector2(0.49f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill3 = create_SkillBtn_Unit(_skillTable);
        CanvasFactory.setRectTransformAnchor(skill3, new Vector2(0.51f, 0f), new Vector2(0.74f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill4 = create_SkillBtn_Unit(_skillTable);
        CanvasFactory.setRectTransformAnchor(skill4, new Vector2(0.76f, 0f), new Vector2(0.99f, 1f), Vector2.zero, Vector2.zero);

        _skillButtons = new GameObject[] { skill1, skill2, skill3, skill4 };
    }
    public GameObject create_SkillBtn_Unit(GameObject parent) {
        GameObject skillBtn = CanvasFactory.createButton(parent, "SkillBtn", "skill_LABEL");
        GameObject txt = CanvasFactory.createText(skillBtn, "txt", "skill_ID");

        CanvasFactory.setPointerImageNORMAL_DOWN_UP_ENTER_EXIT(
            skillBtn, "Sprite/Button/skillNor",
            "Sprite/Button/skillPressed", "Sprite/Button/skillNor", "Sprite/Button/skillOver", "Sprite/Button/skillNor"
        );

        CanvasFactory.setTextScaleSize(txt, 0.1f, 200);
        CanvasFactory.setTextColor(txt, Color.black);

        return skillBtn;
    }
}