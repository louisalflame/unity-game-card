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
        scaler.referenceResolution = new Vector2(1024, 576);
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
        image.raycastTarget = false;

        return imageObj;
    }

    public static GameObject createButton(GameObject parent, string name, string label) {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent.transform);

        RectTransform rect = buttonObj.AddComponent<RectTransform>();
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        Image image = buttonObj.AddComponent<Image>();
        image.raycastTarget = true;
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
    public static void setTextScaleSize(GameObject txtObj, float scale, int size) {
        txtObj.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
        if (txtObj.GetComponent<Text>() != null) txtObj.GetComponent<Text>().fontSize = size;
    }
    public static void setTextColor(GameObject txtObj, Color c) {
        if (txtObj.GetComponent<Text>() != null) txtObj.GetComponent<Text>().color = c;
    }
    public static void setTextAnchor(GameObject txtObj, TextAnchor anchor) {
        if (txtObj.GetComponent<Text>() != null) txtObj.GetComponent<Text>().alignment = anchor;
    }

    public static GameObject setRectTransformAnchor(GameObject obj, Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax) {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.offsetMin = offsetMin;
        rect.offsetMax = offsetMax;
        return obj;
    }
    public static GameObject setRectTransformPosition(GameObject obj, Vector2 anchorMin, Vector2 anchorMax, Vector2 position, Vector2 size) {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
        return obj;
    }
    public static GameObject setZeroPosition(GameObject obj, Vector2 anchor) {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
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
    public static GameObject setRectPivot(GameObject obj, Vector2 pivot) {
        obj.GetComponent<RectTransform>().pivot = pivot;
        return obj;
    }

    public static void setPointerImageNORMAL_DOWN_UP_ENTER_EXIT(GameObject obj, string normal, string down, string up, string enter, string exit) {
        EventTrigger trigger = (obj.GetComponent<EventTrigger>() == null) ? obj.AddComponent<EventTrigger>() : obj.GetComponent<EventTrigger>();
        Sprite _normal = Resources.Load<Sprite>(normal);
        Sprite _down = Resources.Load<Sprite>(down); if (_down == null) { _down = _normal; }
        Sprite _up = Resources.Load<Sprite>(up); if (_up == null) { _up = _normal; }
        Sprite _enter = Resources.Load<Sprite>(enter); if (_enter == null) { _enter = _normal; }
        Sprite _exit = Resources.Load<Sprite>(exit); if (_exit == null) { _exit = _normal; }

        setImageSprite(obj, normal);
        addPointerDownImageSprite(trigger, obj, _down);
        addPointerUpImageSprite(trigger, obj, _up);
        addPointerEnterImageSprite(trigger, obj, _enter);
        addPointerExitImageSprite(trigger, obj, _exit);
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
    public static EventTrigger addPointerEnterCallback(GameObject imageObj, UnityEngine.Events.UnityAction<BaseEventData> callback) {
        EventTrigger trigger = (imageObj.GetComponent<EventTrigger>() == null) ?
            imageObj.AddComponent<EventTrigger>() : imageObj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( callback );
        trigger.triggers.Add(entry);
        return trigger;
    }
    public static EventTrigger addPointerExitCallback(GameObject imageObj, UnityEngine.Events.UnityAction<BaseEventData> callback) {
        EventTrigger trigger = (imageObj.GetComponent<EventTrigger>() == null) ?
            imageObj.AddComponent<EventTrigger>() : imageObj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener( callback );
        trigger.triggers.Add(entry);
        return trigger;
    }

    public static void setImageSprite(GameObject obj, string path) {
        if (obj.GetComponent<Image>() != null) {
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        }
    }
    public static void setImageNatureSize(GameObject obj) {
        if (obj.GetComponent<Image>() != null) {
            obj.GetComponent<Image>().SetNativeSize();
        }
    }

    public static void setAspectRatioFitter(GameObject obj, AspectRatioFitter.AspectMode mode, float ratio)  {
        AspectRatioFitter fitter = obj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = mode;
        fitter.aspectRatio = ratio;
    }

    // MenuScene ===================
    public static GameObject create_MenuScene_StartBtn(GameObject parent) {
        GameObject startBtn = CanvasFactory.createButton(parent, "start", NameCoder.getLabel(NameCoder.StartButton));
        GameObject txt = CanvasFactory.createText(startBtn, "txt", NameCoder.getText(NameCoder.StartButton));

        startBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/buttonNor");
        setRectTransformAnchor(startBtn, new Vector2(0.4f, 0.5f), new Vector2(0.6f, 0.6f), Vector2.zero, Vector2.zero);

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

    //==================================================
    // BattleScene 
    //==================================================

    // PointStatus  *************
    public static GameObject create_PointStatus_Unit(GameObject parent, string name, string spritePath, string iconPath) {
        GameObject imageObj = createImage(parent, name);
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(spritePath);

        GameObject iconObj = createImage(imageObj, "attrIcon");
        iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(iconPath);
        setRectTransformAnchor(iconObj, new Vector2(0.1f, 0.05f), new Vector2(0.9f, 0.45f), Vector2.zero, Vector2.zero);

        GameObject numObj = createImage(imageObj, "num");
        numObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/PointTable/numBase");
        setRectTransformAnchor(numObj, new Vector2(0.1f, 0.55f), new Vector2(0.9f, 0.95f), Vector2.zero, Vector2.zero);

        GameObject txt = createText(numObj, "txt", "0");
        setTextScaleSize(txt, 0.1f, 300);
        setTextColor(txt, Color.white); 

        return imageObj;
    }

    // TowerStatus  *************
    public static GameObject create_TowerStatus_Unit(GameObject parent) {
        GameObject imageObj = createImage(parent, "TowerImage");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/PointTable/numBase");

        GameObject iconObj = createImage(imageObj, "TowerIcon");
        iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        setRectTransformAnchor(iconObj, new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f), Vector2.zero, Vector2.zero);

        return imageObj;
    }

    // DiceBox  *************
    public static GameObject create_DiceBox_Unit(GameObject parent, string name, string buttonID, string label ){
        GameObject imageObj = createButton(parent, name, buttonID);
        setRectPivot(imageObj, new Vector2(0f, 1f)); 

        GameObject txt = createText(imageObj, "txt", label);
        setZeroPosition(txt, new Vector2(0.7f, 0.5f));
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 250;
        txt.GetComponent<Text>().color = Color.white;

        GameObject layoutObj = createEmptyRect(imageObj, "Layout");
        setRectTransformAnchor(layoutObj, new Vector2(0.7f, 0.5f), new Vector2(0.7f, 0.5f), Vector2.zero, Vector2.zero);
        HorizontalLayoutGroup layout = layoutObj.AddComponent<HorizontalLayoutGroup>();
        layout.padding = new RectOffset(0, 0, 0, 0);
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.MiddleLeft;
        layout.childControlHeight = false;
        layout.childControlWidth = false;
        layout.childForceExpandHeight = false;
        layout.childForceExpandWidth = false;

        return imageObj;
    }

    //  Team character Status  *************
    public static void create_CharStatus_Unit_Info(GameObject parent){
        GameObject txtHp = createText(parent, "txtHp", "HP");
        setZeroPosition(txtHp, new Vector2(0.05f, 0.8f));
        setRectPivot(txtHp, new Vector2(0f, 0.5f));
        txtHp.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txtHp.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        txtHp.GetComponent<Text>().fontSize = 180;
        txtHp.GetComponent<Text>().color = Color.white;
        GameObject txtATK = createText(parent, "txtATK", "ATK");
        setZeroPosition(txtATK, new Vector2(0.05f, 0.5f));
        setRectPivot(txtHp, new Vector2(0f, 0.5f));
        txtATK.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txtATK.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        txtATK.GetComponent<Text>().fontSize = 180;
        txtATK.GetComponent<Text>().color = Color.white;
        GameObject txtDEF = createText(parent, "txDEF", "DEF");
        setZeroPosition(txtDEF, new Vector2(0.05f, 0.2f));
        setRectPivot(txtHp, new Vector2(0f, 0.5f));
        txtDEF.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txtDEF.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        txtDEF.GetComponent<Text>().fontSize = 180;
        txtDEF.GetComponent<Text>().color = Color.white;
    }

    public static GameObject create_CharStatus_Unit_NumHp(GameObject parent) { 
        GameObject numHP = createText(parent, "numHP", "0");
        setZeroPosition(numHP, new Vector2(0.9f, 0.8f));
        setRectPivot(numHP, new Vector2(1f, 0.5f));
        numHP.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numHP.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numHP.GetComponent<Text>().fontSize = 180;
        numHP.GetComponent<Text>().color = Color.white;
        return numHP;
    }
    public static GameObject create_CharStatus_Unit_NumATK(GameObject parent) {
        GameObject numATK = createText(parent, "numATK", "0");
        setZeroPosition(numATK, new Vector2(0.9f, 0.5f));
        setRectPivot(numATK, new Vector2(1f, 0.5f));
        numATK.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numATK.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numATK.GetComponent<Text>().fontSize = 180;
        numATK.GetComponent<Text>().color = Color.white;
        return numATK; 
    }
    public static GameObject create_CharStatus_Unit_NumDEF(GameObject parent) { 
        GameObject numDEF = createText(parent, "numDEF", "0");
        setZeroPosition(numDEF, new Vector2(0.9f, 0.2f));
        setRectPivot(numDEF, new Vector2(1f, 0.5f));
        numDEF.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numDEF.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numDEF.GetComponent<Text>().fontSize = 180;
        numDEF.GetComponent<Text>().color = Color.white;
        return numDEF;
    }
}
