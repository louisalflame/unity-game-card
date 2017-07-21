using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLayoutInterface {
    public InterfaceController _interface;

    public GameObject _mainBattleField { get; private set; }
    public GameObject _leftBattleField { get; private set; }
    public GameObject _middleBattleField { get; private set; }
    public GameObject _rightBattleField { get; private set; }

    public GameObject _topBar { get; private set; }
    public GameObject _bottomBar { get; private set; }
    
    public GameObject _mainMenu { get; private set; }
    public GameObject _leftMenu { get; private set; }
    public GameObject _middleMenu { get; private set; }
    public GameObject _rightMenu { get; private set; }
    public GameObject _actionBase { get; private set; }
    public GameObject _enemyTableMask { get; private set; }

    public GameObject _leftStatus { get; private set; }
    public GameObject _rightStatus { get; private set; }
    

    public MainLayoutInterface(InterfaceController inter) { 
        _interface = inter;
        
        createBattleField();

        createTopBar();
        createMainMenu();
        createBottomBar();

        createLeftStatus();
        createRightStatus();
    }

    private void createTopBar() { 
        _topBar = CanvasFactory.createImage(_interface._canvas, "TopBar");
        CanvasFactory.setImageSprite(_topBar, "Sprite/BackGround/topBar");
        CanvasFactory.setRectTransformAnchor(_topBar, new Vector2(0f, 0.9f), Vector2.one, Vector2.zero, Vector2.zero);
    }
    private void createBattleField() { 
        _mainBattleField = CanvasFactory.createEmptyRect(_interface._canvas, "BattleField");
        CanvasFactory.setWholeRect(_mainBattleField);

        _middleBattleField = CanvasFactory.createEmptyRect(_mainBattleField, "MiddleBattleField");
        CanvasFactory.setRectTransformAnchor(_middleBattleField, new Vector2(0.4f, 0f), new Vector2(0.6f, 1f), Vector2.zero, Vector2.zero);

        _leftBattleField = CanvasFactory.createEmptyRect(_mainBattleField, "LeftBattleField");
        CanvasFactory.setRectTransformAnchor(_leftBattleField, new Vector2(0f, 0f), new Vector2(0.45f, 1f), Vector2.zero, Vector2.zero);

        _rightBattleField = CanvasFactory.createEmptyRect(_mainBattleField, "RightBattleField");
        CanvasFactory.setRectTransformAnchor(_rightBattleField, new Vector2(0.55f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
    }
    private void createBottomBar() { 
        _bottomBar = CanvasFactory.createImage(_interface._canvas, "BottomBar");
        CanvasFactory.setImageSprite(_bottomBar, "Sprite/BackGround/bottomBar");
        CanvasFactory.setRectTransformAnchor(_bottomBar, Vector2.zero, new Vector2(1f, 0.03f), Vector2.zero, Vector2.zero);
    }

    private void createMainMenu() {
        _mainMenu = CanvasFactory.createImage(_interface._canvas, "MainMenu");
        CanvasFactory.setImageSprite(_mainMenu, "Sprite/BackGround/buttonBack");
        CanvasFactory.setRectTransformAnchor(_mainMenu, Vector2.zero, new Vector2(1f, 0.35f), Vector2.zero, Vector2.zero);

        _leftMenu = CanvasFactory.createEmptyRect(_mainMenu, "LeftMenu");
        CanvasFactory.setRectTransformAnchor(_leftMenu, new Vector2(0f, 0f), new Vector2(0.6f, 1f), Vector2.zero, new Vector2(-60f, 0f));
        _middleMenu = CanvasFactory.createEmptyRect(_mainMenu, "MiddleMenu");
        CanvasFactory.setRectTransformAnchor(_middleMenu, new Vector2(0.55f, 0f), new Vector2(0.65f, 1f), Vector2.zero, Vector2.zero);
        _rightMenu = CanvasFactory.createEmptyRect(_mainMenu, "RightMenu");
        CanvasFactory.setRectTransformAnchor(_rightMenu, new Vector2(0.6f, 0f), new Vector2(1f, 1f), new Vector2(60f, 0f), new Vector2(0f, 0f));

        createEnemyTableMask();
        createActionBase();
    }
    private void createLeftStatus() { 
        _leftStatus = CanvasFactory.createEmptyRect(_interface._canvas, "LeftStatus");
        CanvasFactory.setRectTransformAnchor(_leftStatus, new Vector2(0f, 0f), new Vector2(0.45f, 1f), Vector2.zero, Vector2.zero);
    }
    private void createRightStatus(){
        _rightStatus = CanvasFactory.createEmptyRect(_interface._canvas, "RightStatus");
        CanvasFactory.setRectTransformAnchor(_rightStatus, new Vector2(0.55f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
    }

    private void createEnemyTableMask() { 
        _enemyTableMask = CanvasFactory.createImage(_rightMenu, "EnemyTableMask");
        CanvasFactory.setRectTransformAnchor(_enemyTableMask, new Vector2(0f, 0f), new Vector2(1f, 1.4f), Vector2.zero, Vector2.zero);
        _enemyTableMask.AddComponent<Mask>().showMaskGraphic = false;
    }
    private void createActionBase() {
        _actionBase = CanvasFactory.createImage(_leftMenu, "ActionBase");
        CanvasFactory.setImageSprite(_actionBase, "Sprite/BackGround/mainBack");
        CanvasFactory.setRectTransformAnchor(_actionBase, new Vector2(0.1f, 0.15f), new Vector2(0.95f, 0.7f), Vector2.zero, Vector2.zero);

        Mask mask = _actionBase.AddComponent<Mask>();
        mask.showMaskGraphic = true;

        HorizontalLayoutGroup layout = _actionBase.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.UpperLeft;
        layout.childControlHeight = true;
        layout.childControlWidth = false;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;
    }
}
