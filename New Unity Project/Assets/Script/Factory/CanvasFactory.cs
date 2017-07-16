using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasFactory { 

    public static GameObject createCanvas() {
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas.planeDistance = 100f;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2( 1024, 576 );
        scaler.referencePixelsPerUnit = 32;
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;
        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject eventObj = MonoBehaviour.Instantiate(Resources.Load("Prefab/Button/EventSystem")) as GameObject;
        eventObj.transform.SetParent(canvasObj.transform);

        return canvasObj;
    }

    public static GameObject createBasicRatioPanel(GameObject canvas) {
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvas.transform);

        RectTransform rect = panelObj.AddComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;

        AspectRatioFitter fitter = panelObj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        fitter.aspectRatio = 16f / 9f;

        return panelObj;
    }

    public static GameObject createEmptyRect(GameObject parent, string name) {
        GameObject rectObj = new GameObject(name);
        rectObj.transform.SetParent(parent.transform);

        RectTransform rect = rectObj.AddComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        return rectObj;
    }

    public static GameObject createImage(GameObject parent, string name) {
        GameObject imageObj = new GameObject(name);
        imageObj.transform.SetParent(parent.transform);

        RectTransform rect = imageObj.AddComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        Image image = imageObj.AddComponent<Image>();

        return imageObj;
    }

    public static GameObject createButton(GameObject parent, string name, string label) {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent.transform);

        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        Image image = buttonObj.AddComponent<Image>();
        Button btn = buttonObj.AddComponent<Button>();
        btn.interactable = true;
        btn.onClick.AddListener(() => { InputController.Inputs.addInput(label); });
        btn.transition = Selectable.Transition.None;

        return buttonObj; 
    }

    public static GameObject createText(GameObject parent, string name, string str) {
        GameObject textObj = MonoBehaviour.Instantiate(Resources.Load("Font/ArialText")) as GameObject;
        textObj.transform.name = name;
        textObj.transform.SetParent(parent.transform);

        RectTransform rect = textObj.GetComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Text text = textObj.GetComponent<Text>();
        text.text = str;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;

        return textObj;
    }
    
    public static GameObject setRectTransform(GameObject obj, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax) {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
        return obj;
    }
    public static GameObject setWholeRect(GameObject obj) {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        return obj;
    }

    public static EventTrigger addPointerDownImageSprite(EventTrigger trigger, GameObject imageObj, Sprite sprite) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { imageObj.GetComponent<Image>().sprite = sprite; });
        trigger.triggers.Add(entry);
        return trigger;
    }
    public static EventTrigger addPointerUpImageSprite(EventTrigger trigger, GameObject imageObj, Sprite sprite) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { imageObj.GetComponent<Image>().sprite = sprite; });
        trigger.triggers.Add(entry);
        return trigger;
    }
    public static EventTrigger addPointerEnterImageSprite(EventTrigger trigger, GameObject imageObj, Sprite sprite) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { imageObj.GetComponent<Image>().sprite = sprite; });
        trigger.triggers.Add(entry);
        return trigger;
    }
    public static EventTrigger addPointerExitImageSprite(EventTrigger trigger, GameObject imageObj, Sprite sprite) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { imageObj.GetComponent<Image>().sprite = sprite; });
        trigger.triggers.Add(entry);
        return trigger;
    }


    // MenuScene ===================
    public static GameObject create_MenuScene_StartBtn(GameObject parent) {
        GameObject startBtn = CanvasFactory.createButton(parent, "start", NameCoder.getLabel(NameCoder.StartButton));
        GameObject txt = CanvasFactory.createText(startBtn, "txt", NameCoder.getText(NameCoder.StartButton));

        startBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/buttonNor");
        setRectTransform(startBtn, new Vector2(0.4f, 0.5f), new Vector2(0.6f, 0.6f), Vector2.zero, Vector2.zero);

        EventTrigger trigger = startBtn.AddComponent<EventTrigger>();
        addPointerDownImageSprite(  trigger, startBtn, Resources.Load<Sprite>("Sprite/Button/buttonPressed"));
        addPointerUpImageSprite(    trigger, startBtn, Resources.Load<Sprite>("Sprite/Button/buttonNor"));
        addPointerEnterImageSprite( trigger, startBtn, Resources.Load<Sprite>("Sprite/Button/buttonOver"));
        addPointerExitImageSprite(  trigger, startBtn, Resources.Load<Sprite>("Sprite/Button/buttonNor"));

        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.black;

        return startBtn;
    }

    // BattleScene ===================

    public static GameObject create_BattleScene_TopBar(GameObject parent) {
        GameObject imageObj = createImage(parent, "TopBar");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/BackGround/topBar");
        setRectTransform(imageObj, new Vector2(0f, 0.9f), Vector2.one, Vector2.zero, Vector2.zero);
        return imageObj;
    }
    public static GameObject create_BattleScene_MainButtonBase(GameObject parent) {
        GameObject imageObj = createImage(parent, "MainBase");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/BackGround/buttonBack");
        setRectTransform(imageObj, Vector2.zero, new Vector2(1f, 0.35f), Vector2.zero, Vector2.zero);
        return imageObj;
    }
    public static GameObject create_BattleScene_BottomBar(GameObject parent) {
        GameObject imageObj = createImage(parent, "BottomBar");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/BackGround/bottomBar");
        setRectTransform(imageObj, Vector2.zero, new Vector2(1f, 0.03f), Vector2.zero, Vector2.zero);
        return imageObj;
    }
    public static GameObject create_BattleScene_ExitBtn(GameObject parent) {
        GameObject exitBtn = CanvasFactory.createButton(parent, "ExitBtn", NameCoder.getLabel(NameCoder.ExitButton));
        GameObject txt = CanvasFactory.createText(exitBtn, "txt", NameCoder.getText(NameCoder.ExitButton));

        exitBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/buttonNor");
        setRectTransform(exitBtn, new Vector2(0.1f, 0.1f), new Vector2(0.2f, 0.9f), Vector2.zero, Vector2.zero);

        EventTrigger trigger = exitBtn.AddComponent<EventTrigger>();
        addPointerDownImageSprite(  trigger, exitBtn, Resources.Load<Sprite>("Sprite/Button/buttonPressed"));
        addPointerUpImageSprite(    trigger, exitBtn, Resources.Load<Sprite>("Sprite/Button/buttonNor"));
        addPointerEnterImageSprite( trigger, exitBtn, Resources.Load<Sprite>("Sprite/Button/buttonOver"));
        addPointerExitImageSprite(  trigger, exitBtn, Resources.Load<Sprite>("Sprite/Button/buttonNor"));

        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.black;

        return exitBtn;
    }
    public static GameObject create_BattleScene_NextBtn(GameObject parent) {
        GameObject nextBtn = CanvasFactory.createButton(parent, "NextBtn", NameCoder.getLabel(NameCoder.NextButton));
        GameObject txt = CanvasFactory.createText(nextBtn, "txt", NameCoder.getText(NameCoder.NextButton));

        nextBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/okNor");
        setRectTransform(nextBtn, new Vector2(0.55f, 0.15f), new Vector2(0.635f, 0.6f), Vector2.zero, Vector2.zero);

        EventTrigger trigger = nextBtn.AddComponent<EventTrigger>();
        addPointerDownImageSprite(  trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okPressed"));
        addPointerUpImageSprite(    trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okNor"));
        addPointerEnterImageSprite( trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okOver"));
        addPointerExitImageSprite(  trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okNor"));

        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.black;
        
        return nextBtn;
    }
    public static GameObject create_BattleScene_ThrowBtn(GameObject parent) {
        GameObject nextBtn = CanvasFactory.createButton(parent, "ThrowBtn", NameCoder.getLabel(NameCoder.ThrowButton));
        GameObject txt = CanvasFactory.createText(nextBtn, "txt", NameCoder.getText(NameCoder.ThrowButton));

        nextBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/okNor");
        setRectTransform(nextBtn, new Vector2(0.55f, 0.15f), new Vector2(0.635f, 0.6f), Vector2.zero, Vector2.zero);

        EventTrigger trigger = nextBtn.AddComponent<EventTrigger>();
        addPointerDownImageSprite(  trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okPressed"));
        addPointerUpImageSprite(    trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okNor"));
        addPointerEnterImageSprite( trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okOver"));
        addPointerExitImageSprite(  trigger, nextBtn, Resources.Load<Sprite>("Sprite/Button/okNor"));

        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.black;
        
        return nextBtn;
    }
    public static GameObject create_BattleScene_ActionButtonBase(GameObject parent) {
        GameObject imageObj = createImage(parent, "ActionBase");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/BackGround/mainBack");
        setRectTransform(imageObj, new Vector2(0.04f, 0.15f), new Vector2(0.53f, 0.7f), Vector2.zero, Vector2.zero);

        Mask mask = imageObj.AddComponent<Mask>();
        mask.showMaskGraphic = true;

        HorizontalLayoutGroup layout = imageObj.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.UpperLeft;
        layout.childControlHeight = true;
        layout.childControlWidth = false;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;

        return imageObj;
    }

    public static GameObject create_BattleScene_MoveActionBtn(GameObject parent, string[] label_ID) {
        GameObject actionBtn = createButton(parent, "actionBtn", NameCoder.getLabel(label_ID));
        actionBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActBack");
        RectTransform rect = actionBtn.GetComponent<RectTransform>();

        AspectRatioFitter fitter = actionBtn.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        fitter.aspectRatio = 2f / 3f;

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        setRectTransform(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 100;
        txt.GetComponent<Text>().color = Color.white;

        return actionBtn;
    }
    public static GameObject create_BattleScene_AttackActionBtn(GameObject parent, string[] label_ID) {
        GameObject actionBtn = createButton(parent, "actionBtn", NameCoder.getLabel(label_ID));
        actionBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActBack");
        RectTransform rect = actionBtn.GetComponent<RectTransform>();

        AspectRatioFitter fitter = actionBtn.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        fitter.aspectRatio = 2f / 3f;

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        setRectTransform(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 100;
        txt.GetComponent<Text>().color = Color.white;

        return actionBtn;
    }
    public static GameObject create_BattleScene_DefenseActionBtn(GameObject parent, string[] label_ID) {
        GameObject actionBtn = createButton(parent, "actionBtn", NameCoder.getLabel(label_ID));
        actionBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActBack");
        RectTransform rect = actionBtn.GetComponent<RectTransform>();

        AspectRatioFitter fitter = actionBtn.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        fitter.aspectRatio = 2f / 3f;

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, "txt", NameCoder.getText(label_ID));
        setRectTransform(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 100;
        txt.GetComponent<Text>().color = Color.white;

        return actionBtn;
    }
    public static Dictionary<string, GameObject[]> create_BattleScene_PlayerPointStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerPointStatus");
        setRectTransform(rectObj, new Vector2(0.65f, 0.15f), new Vector2(0.95f, 0.6f), Vector2.zero, Vector2.zero);

        GameObject norObj = create_PointStatus_Unit(rectObj, "attrNor", "Sprite/PointTable/pointTableNor", "Sprite/AttrIcon/AttrNor");
        setRectTransform(norObj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject atkObj = create_PointStatus_Unit(rectObj, "attrAtk", "Sprite/PointTable/pointTableAtk", "Sprite/AttrIcon/AttrAtk");
        setRectTransform(atkObj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject defObj = create_PointStatus_Unit(rectObj, "attrDef", "Sprite/PointTable/pointTableDef", "Sprite/AttrIcon/AttrDef");
        setRectTransform(defObj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject movObj = create_PointStatus_Unit(rectObj, "attrMov", "Sprite/PointTable/pointTableMov", "Sprite/AttrIcon/AttrMov");
        setRectTransform(movObj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject spcObj = create_PointStatus_Unit(rectObj, "attrSpc", "Sprite/PointTable/pointTableSpc", "Sprite/AttrIcon/AttrSpc");
        setRectTransform(spcObj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject healObj = create_PointStatus_Unit(rectObj, "attrHeal", "Sprite/PointTable/pointTableHeal", "Sprite/AttrIcon/AttrHeal");
        setRectTransform(healObj, new Vector2(5f/6, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]>();
        dict["PointTable"] = new GameObject[] { rectObj };
        dict["PointAttrs"] = new GameObject[] { norObj, atkObj, defObj, movObj, spcObj, healObj };
        dict["PointTexts"] = new GameObject[] {
            norObj.transform.Find("num/txt").gameObject,
            atkObj.transform.Find("num/txt").gameObject,
            defObj.transform.Find("num/txt").gameObject,
            movObj.transform.Find("num/txt").gameObject,
            spcObj.transform.Find("num/txt").gameObject,
            healObj.transform.Find("num/txt").gameObject,
        };

        return dict;
    }
    public static GameObject create_PointStatus_Unit(GameObject parent, string name, string spritePath, string iconPath) {
        GameObject imageObj = createImage(parent, name);
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);

        GameObject iconObj = createImage(imageObj, "attrIcon");
        iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(iconPath);
        setRectTransform(iconObj, new Vector2(0.1f, 0.05f), new Vector2(0.9f, 0.45f), Vector2.zero, Vector2.zero);

        GameObject numObj = createImage(imageObj, "num");
        numObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/PointTable/numBase");
        setRectTransform(numObj, new Vector2(0.1f, 0.55f), new Vector2(0.9f, 0.95f), Vector2.zero, Vector2.zero);

        GameObject txt = createText(numObj, "txt", "0");
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.white;

        return imageObj;
    }

    public static Dictionary<string, GameObject[]> create_BattleScene_PlayerTowerStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerTowerStatus");
        setRectTransform(rectObj, new Vector2(0.65f, 0.7f), new Vector2(0.95f, 0.9f), Vector2.zero, Vector2.zero);

        GameObject t1Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t1Obj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t2Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t2Obj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t3Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t3Obj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t4Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t4Obj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t5Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t5Obj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t6Obj = create_TowerStatus_Unit(rectObj);
        setRectTransform(t6Obj, new Vector2(5f/6, 0f), new Vector2(6f/6, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]>();
        dict["TowerTable"] = new GameObject[] { rectObj };
        dict["TowerIcons"] = new GameObject[] {
            t1Obj.transform.Find("TowerIcon").gameObject,
            t2Obj.transform.Find("TowerIcon").gameObject,
            t3Obj.transform.Find("TowerIcon").gameObject,
            t4Obj.transform.Find("TowerIcon").gameObject,
            t5Obj.transform.Find("TowerIcon").gameObject,
            t6Obj.transform.Find("TowerIcon").gameObject,
        };
        return dict;
    }
    public static GameObject create_TowerStatus_Unit(GameObject parent) {
        GameObject imageObj = createImage(parent, "TowerImage");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/PointTable/numBase");

        GameObject iconObj = createImage(imageObj, "TowerIcon");
        iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        setRectTransform(iconObj, new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f), Vector2.zero, Vector2.zero);

        return imageObj;
    }

}
