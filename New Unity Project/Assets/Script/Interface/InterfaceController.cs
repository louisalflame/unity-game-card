using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InterfaceController {
    public GameObject _canvas { get; private set; }
    //public GameObject _panel { get; private set; }

    public BattleController _battle;
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

        createCanvasLayout();

        _mainMenu = new MainLayoutInterface(this); 
        _menuButton = new MenuButtonInterface(this);

        _charPlayerPlace = new PlayerPlaceInterface(this);
        _charEnemyPlace = new EnemyPlaceInterface(this);
        _turnStatus = new TurnStatus(this);

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

    // 基本Layout物件
    public void createCanvasLayout() {
        _canvas = CanvasFactory.createCanvas();
        //_panel = CanvasFactory.createBasicRatioPanel(_canvas);
    }
    public GameObject getImageMainBattleField() { return _mainMenu._mainBattleField; }
    public GameObject getImageLeftBattleField() { return _mainMenu._leftBattleField; }
    public GameObject getImageMiddleBattleField() { return _mainMenu._middleBattleField; }
    public GameObject getImageRightBattleField() { return _mainMenu._rightBattleField; }
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
    
}

// 基本指令選單按鈕
public class MenuButtonInterface {
    public InterfaceController _interface;

    public GameObject _exit { get; private set; }
    public GameObject _next { get; private set; }
    public GameObject _throw { get; private set; }

    public MenuButtonInterface(InterfaceController inter) {
        _interface = inter;

        createExitButton();
        createNextButton();
        createThrowButton();

        hideNextButton();
        hideThrowButton();
    }
    public void update() { }

    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }

    private void createExitButton() {
        _exit = CanvasFactory.createButton(_interface.getImageTopBar(), "ExitBtn", NameCoder.getLabel(NameCoder.ExitButton));
        GameObject txt = CanvasFactory.createText(_exit, "txt", NameCoder.getText(NameCoder.ExitButton));

        CanvasFactory.setRectTransformPosition(_exit, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(150f, 0f), new Vector2(120f, 40f));
        CanvasFactory.setPointerImageNORMAL_DOWN_UP_ENTER_EXIT(
            _exit, "Sprite/Button/buttonNor", 
            "Sprite/Button/buttonPressed", "Sprite/Button/buttonNor", "Sprite/Button/buttonOver", "Sprite/Button/buttonNor"
        );

        CanvasFactory.setTextScaleSize(txt, 0.1f, 300);
        CanvasFactory.setTextColor(txt, Color.black);
    }
    private void createNextButton() {
        _next = CanvasFactory.createButton(_interface.getImageMiddleMenu(), "NextBtn", NameCoder.getLabel(NameCoder.NextButton));
        GameObject txt = CanvasFactory.createText(_next, "txt", NameCoder.getText(NameCoder.NextButton));

        float width = Mathf.Min(_interface.getImageMiddleMenu().GetComponent<RectTransform>().rect.width, 100f);
        CanvasFactory.setRectTransformPosition(_next, new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.4f), Vector2.zero, new Vector2(width, width));
        CanvasFactory.setPointerImageNORMAL_DOWN_UP_ENTER_EXIT(
            _next, "Sprite/Button/okNor",
            "Sprite/Button/okPressed", "Sprite/Button/okNor", "Sprite/Button/okOver", "Sprite/Button/okNor"
        );

        CanvasFactory.setTextScaleSize(txt, 0.1f, 300);
        CanvasFactory.setTextColor(txt, Color.black);
    }
    private void createThrowButton() {
        _throw = CanvasFactory.createButton(_interface.getImageMiddleMenu(), "ThrowBtn", NameCoder.getLabel(NameCoder.ThrowButton));
        GameObject txt = CanvasFactory.createText(_throw, "txt", NameCoder.getText(NameCoder.ThrowButton));

        float width = Mathf.Min(_interface.getImageMiddleMenu().GetComponent<RectTransform>().rect.width, 100f);
        CanvasFactory.setRectTransformPosition(_throw, new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.4f), Vector2.zero, new Vector2(width, width));
        CanvasFactory.setPointerImageNORMAL_DOWN_UP_ENTER_EXIT(
            _throw, "Sprite/Button/okNor",
            "Sprite/Button/okPressed", "Sprite/Button/okNor", "Sprite/Button/okOver", "Sprite/Button/okNor"
        );

        CanvasFactory.setTextScaleSize(txt, 0.1f, 300);
        CanvasFactory.setTextColor(txt, Color.black);
    }
}

 

// 技能選單顯示
public class SkillMenuInterface {
    public InterfaceController _interface;
    public GameObject _skillTable { get; private set; }
    public GameObject[] _skillButtons { get; private set; }

    public SkillMenuInterface(InterfaceController inter) {
        _interface = inter;

        Dictionary<string, GameObject[]> dict = CanvasFactory.create_BattleScene_PlayerSkillTable(_interface.getImageLeftMenu());
        if (dict.ContainsKey("SkillTable")) _skillTable = dict["SkillTable"][0];
        if (dict.ContainsKey("SkillButtons")) _skillButtons = dict["SkillButtons"]; 
    }
    public void update() { }
}