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

    public static GameObject createText(GameObject parent, string str) {
        GameObject textObj = MonoBehaviour.Instantiate(Resources.Load("Font/ArialText")) as GameObject;
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
        GameObject txt = CanvasFactory.createText(startBtn, NameCoder.getText(NameCoder.StartButton));

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
        GameObject txt = CanvasFactory.createText(exitBtn, NameCoder.getText(NameCoder.ExitButton));

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
        GameObject txt = CanvasFactory.createText(nextBtn, NameCoder.getText(NameCoder.NextButton));

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
        GameObject txt = CanvasFactory.createText(nextBtn, NameCoder.getText(NameCoder.ThrowButton));

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
        rect.sizeDelta = new Vector2(rect.rect.height * 0.66f, rect.rect.height);

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/movActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, NameCoder.getText(label_ID));
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
        rect.sizeDelta = new Vector2(rect.rect.height * 0.66f, rect.rect.height);

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/atkActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, NameCoder.getText(label_ID));
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
        rect.sizeDelta = new Vector2(rect.rect.height * 0.66f, rect.rect.height);

        GameObject top = createImage(actionBtn, "top");
        top.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActUp");
        setWholeRect(top);
        GameObject info = createImage(actionBtn, "info");
        info.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActInfo");
        setWholeRect(info);
        GameObject frame = createImage(actionBtn, "side");
        frame.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/ActionButton/defActFrame");
        setWholeRect(frame);

        GameObject txt = CanvasFactory.createText(actionBtn, NameCoder.getText(label_ID));
        setRectTransform(txt, Vector2.zero, new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 100;
        txt.GetComponent<Text>().color = Color.white;

        return actionBtn;
    }
}
