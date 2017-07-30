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

    public MainLayoutInterface(InterfaceController inter) { _interface = inter; }

    public void create() {
        _mainBattleField = _interface._canvas.transform.Find("BattleField").gameObject;
        if (_mainBattleField == null) { _mainBattleField = createMainBattleField(_interface._canvas); }
        _middleBattleField = _mainBattleField.transform.Find("MiddleBattleField").gameObject;
        if (_middleBattleField == null) { _middleBattleField = createMiddleBattleField(_mainBattleField); }
        _leftBattleField = _mainBattleField.transform.Find("LeftBattleField").gameObject;
        if (_leftBattleField == null) { _leftBattleField = createLeftBattleField(_mainBattleField); }
        _rightBattleField = _mainBattleField.transform.Find("RightBattleField").gameObject;
        if (_rightBattleField == null) { _rightBattleField = createRightBattleField(_mainBattleField); }

        _topBar = _interface._canvas.transform.Find("TopBar").gameObject;
        if (_topBar == null) { _topBar = createTopBar(_interface._canvas); }
        _mainMenu = _interface._canvas.transform.Find("MainMenu").gameObject;
        if (_mainMenu == null) { _mainMenu = createMainMenu(_interface._canvas); }
        _leftMenu = _mainMenu.transform.Find("LeftMenu").gameObject;
        if (_leftMenu == null) { _leftMenu = createLeftMenu(_mainMenu); }
        _middleMenu = _mainMenu.transform.Find("MiddleMenu").gameObject;
        if (_middleMenu == null) { _middleMenu = createMiddleMenu(_mainMenu); }
        _rightMenu = _mainMenu.transform.Find("RightMenu").gameObject;
        if (_rightMenu == null) { _rightMenu = createRightMenu(_mainMenu); }
        _bottomBar = _interface._canvas.transform.Find("BottomBar").gameObject;
        if (_bottomBar == null) { _bottomBar = createBottomBar(_interface._canvas); }

        _enemyTableMask = _topBar.transform.Find("EnemyTableMask").gameObject;
        if (_enemyTableMask == null) { _enemyTableMask = createEnemyTableMask(_topBar); }
        _actionBase = _leftMenu.transform.Find("ActionBase").gameObject;
        if (_actionBase == null) { _actionBase = createActionBase(_leftMenu); }
        
        _leftStatus = _interface._canvas.transform.Find("LeftStatus").gameObject;
        if (_leftStatus == null) { _leftStatus = createLeftStatus(_interface._canvas); }
        _rightStatus = _interface._canvas.transform.Find("RightStatus").gameObject;
        if (_rightStatus == null) { _rightStatus = createRightStatus(_interface._canvas); }
        
        _topBar.SetActive(false);
        _bottomBar.SetActive(false);
        _mainMenu.SetActive(false);
    }
    public void initial() {
        _topBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 60f);
        _bottomBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30f);
        _mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200f);
        _topBar.SetActive(true);
        _bottomBar.SetActive(true);
        _mainMenu.SetActive(true);
    }

    private GameObject createMainBattleField(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "BattleField");
        CanvasFactory.setWholeRect(rectObj);
        return rectObj;
    }
    private GameObject createMiddleBattleField(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "MiddleBattleField");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.4f, 0f), new Vector2(0.6f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createLeftBattleField(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "LeftBattleField");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0f, 0f), new Vector2(0.45f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createRightBattleField(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "RightBattleField");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.55f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createTopBar(GameObject parent) {
        GameObject rectObj = CanvasFactory.createImage(parent, "TopBar");
        CanvasFactory.setImageSprite(rectObj, "Sprite/BackGround/topBar");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0f, 0.9f), Vector2.one, Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createBottomBar(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "BottomBar");
        CanvasFactory.setImageSprite(rectObj, "Sprite/BackGround/bottomBar");
        CanvasFactory.setRectTransformAnchor(rectObj, Vector2.zero, new Vector2(1f, 0.03f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    
    private GameObject createMainMenu(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "MainMenu");
        CanvasFactory.setImageSprite(rectObj, "Sprite/BackGround/buttonBack");
        CanvasFactory.setRectTransformAnchor(rectObj, Vector2.zero, new Vector2(1f, 0.35f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createLeftMenu(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "LeftMenu");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0f, 0f), new Vector2(0.6f, 1f), Vector2.zero, new Vector2(-60f, 0f));
        return rectObj;
    }
    private GameObject createMiddleMenu(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "MiddleMenu");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.55f, 0f), new Vector2(0.65f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createRightMenu(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "RightMenu");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.6f, 0f), new Vector2(1f, 1f), new Vector2(60f, 0f), new Vector2(0f, 0f));
        return rectObj;
    } 
    private GameObject createLeftStatus(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(_interface._canvas, "LeftStatus");
        CanvasFactory.setRectTransformAnchor(_leftStatus, new Vector2(0f, 0f), new Vector2(0.45f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }
    private GameObject createRightStatus(GameObject parent){
        GameObject rectObj = CanvasFactory.createEmptyRect(_interface._canvas, "RightStatus");
        CanvasFactory.setRectTransformAnchor(_rightStatus, new Vector2(0.55f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        return rectObj;
    }

    private GameObject createEnemyTableMask(GameObject parent) {
        GameObject rectObj = CanvasFactory.createEmptyRect(parent, "EnemyTableMask");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.6f, -3f), new Vector2(1f, 0.9f), new Vector2(60f, 0f), Vector2.zero);
        _enemyTableMask.AddComponent<Mask>().showMaskGraphic = false;
        return rectObj;
    }
    private GameObject createActionBase(GameObject parent) {
        GameObject rectObj = CanvasFactory.createImage(parent, "ActionBase");
        CanvasFactory.setImageSprite(rectObj, "Sprite/BackGround/mainBack");
        CanvasFactory.setRectTransformAnchor(rectObj, new Vector2(0.1f, 0.15f), new Vector2(0.95f, 0.7f), Vector2.zero, Vector2.zero);

        Mask mask = _actionBase.AddComponent<Mask>();
        mask.showMaskGraphic = true;

        HorizontalLayoutGroup layout = rectObj.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.UpperLeft;
        layout.childControlHeight = true;
        layout.childControlWidth = false;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;

        return rectObj;
    }
}



// 基本指令選單按鈕
public class MenuButtonInterface {
    public InterfaceController _interface;

    public GameObject _exit { get; private set; }
    public GameObject _next { get; private set; }
    public GameObject _throw { get; private set; }

    public MenuButtonInterface(InterfaceController inter) { _interface = inter; }
    public void update() { }

    public void showNextButton() { _next.SetActive(true); }
    public void hideNextButton() { _next.SetActive(false); }
    public void showThrowButton() { _throw.SetActive(true); }
    public void hideThrowButton() { _throw.SetActive(false); }

    public void create() {
        createExitButton();
        createNextButton();
        createThrowButton();
    }
    public void initial(){
        hideNextButton();
        hideThrowButton();
    }
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