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
        Sprite _down = Resources.Load<Sprite>(down);    if (_down == null) { _down = _normal; }
        Sprite _up = Resources.Load<Sprite>(up);        if (_up == null) { _up = _normal; }
        Sprite _enter = Resources.Load<Sprite>(enter);  if (_enter == null) { _enter = _normal; }
        Sprite _exit = Resources.Load<Sprite>(exit);    if (_exit == null) { _exit = _normal; }

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

    public static void setImageSprite(GameObject obj, string path) {
        if (obj.GetComponent<Image>() != null) {
            obj.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
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
      
    // Menu Point Status *************
    public static Dictionary<string, GameObject[]> create_BattleScene_PlayerPointStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerPointStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width, 280f);
        setRectTransformPosition(rectObj, new Vector2(0.05f, 0.55f), new Vector2(0.05f, 0.55f), Vector2.zero, new Vector2(width, width/3));
        setRectPivot(rectObj, new Vector2(0f, 1f));

        AspectRatioFitter fitter = rectObj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        fitter.aspectRatio = 3f;

        GameObject norObj = create_PointStatus_Unit(rectObj, "attrNor", "Sprite/PointTable/pointTableNor", "Sprite/AttrIcon/AttrNor");
        setRectTransformAnchor(norObj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject atkObj = create_PointStatus_Unit(rectObj, "attrAtk", "Sprite/PointTable/pointTableAtk", "Sprite/AttrIcon/AttrAtk");
        setRectTransformAnchor(atkObj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject defObj = create_PointStatus_Unit(rectObj, "attrDef", "Sprite/PointTable/pointTableDef", "Sprite/AttrIcon/AttrDef");
        setRectTransformAnchor(defObj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject movObj = create_PointStatus_Unit(rectObj, "attrMov", "Sprite/PointTable/pointTableMov", "Sprite/AttrIcon/AttrMov");
        setRectTransformAnchor(movObj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject spcObj = create_PointStatus_Unit(rectObj, "attrSpc", "Sprite/PointTable/pointTableSpc", "Sprite/AttrIcon/AttrSpc");
        setRectTransformAnchor(spcObj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject healObj = create_PointStatus_Unit(rectObj, "attrHeal", "Sprite/PointTable/pointTableHeal", "Sprite/AttrIcon/AttrHeal");
        setRectTransformAnchor(healObj, new Vector2(5f/6, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]>() { 
            { "PointTable", new GameObject[] { rectObj } },
            { "PointAttrs", new GameObject[] { norObj, atkObj, defObj, movObj, spcObj, healObj } },
            { "PointTexts", new GameObject[] {
                norObj.transform.Find("num/txt").gameObject,
                atkObj.transform.Find("num/txt").gameObject,
                defObj.transform.Find("num/txt").gameObject,
                movObj.transform.Find("num/txt").gameObject,
                spcObj.transform.Find("num/txt").gameObject,
                healObj.transform.Find("num/txt").gameObject,
            } }
        };  
        return dict;
    }
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
        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 300;
        txt.GetComponent<Text>().color = Color.white;

        return imageObj;
    }
    // Menu Tower Status *************
    public static Dictionary<string, GameObject[]> create_BattleScene_PlayerTowerStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerTowerStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width, 280f);
        setRectTransformPosition(rectObj, new Vector2(0.05f, 0.6f), new Vector2(0.05f, 0.6f), Vector2.zero, new Vector2(width, width/6));
        setRectPivot(rectObj, new Vector2(0f, 0f));

        AspectRatioFitter fitter = rectObj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        fitter.aspectRatio = 6f;

        GameObject t1Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t1Obj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t2Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t2Obj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t3Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t3Obj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t4Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t4Obj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t5Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t5Obj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t6Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t6Obj, new Vector2(5f/6, 0f), new Vector2(6f/6, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]> { 
            { "TowerTable", new GameObject[] { rectObj } },
            { "TowerIcons", new GameObject[] {
                t1Obj.transform.Find("TowerIcon").gameObject,
                t2Obj.transform.Find("TowerIcon").gameObject,
                t3Obj.transform.Find("TowerIcon").gameObject,
                t4Obj.transform.Find("TowerIcon").gameObject,
                t5Obj.transform.Find("TowerIcon").gameObject,
                t6Obj.transform.Find("TowerIcon").gameObject,
            } }
        }; 
        return dict;
    }
    public static GameObject create_TowerStatus_Unit(GameObject parent) {
        GameObject imageObj = createImage(parent, "TowerImage");
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/PointTable/numBase");

        GameObject iconObj = createImage(imageObj, "TowerIcon");
        iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TowerIcon/TowerBase");
        setRectTransformAnchor(iconObj, new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f), Vector2.zero, Vector2.zero);

        return imageObj;
    }
    // Enemy Point Status *************
    public static Dictionary<string, GameObject[]> create_BattleScene_EnemyPointStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "EnemyPointStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width, 280f);
        setRectTransformPosition(rectObj, new Vector2(0.05f, 1f), new Vector2(0.05f, 1f), Vector2.zero, new Vector2(width, width/3));
        setRectPivot(rectObj, new Vector2(0f, 0f));

        AspectRatioFitter fitter = rectObj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        fitter.aspectRatio = 3f;

        GameObject norObj = create_PointStatus_Unit(rectObj, "attrNor", "Sprite/PointTable/pointTableNor", "Sprite/AttrIcon/AttrNor");
        setRectTransformAnchor(norObj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject atkObj = create_PointStatus_Unit(rectObj, "attrAtk", "Sprite/PointTable/pointTableAtk", "Sprite/AttrIcon/AttrAtk");
        setRectTransformAnchor(atkObj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject defObj = create_PointStatus_Unit(rectObj, "attrDef", "Sprite/PointTable/pointTableDef", "Sprite/AttrIcon/AttrDef");
        setRectTransformAnchor(defObj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject movObj = create_PointStatus_Unit(rectObj, "attrMov", "Sprite/PointTable/pointTableMov", "Sprite/AttrIcon/AttrMov");
        setRectTransformAnchor(movObj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject spcObj = create_PointStatus_Unit(rectObj, "attrSpc", "Sprite/PointTable/pointTableSpc", "Sprite/AttrIcon/AttrSpc");
        setRectTransformAnchor(spcObj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject healObj = create_PointStatus_Unit(rectObj, "attrHeal", "Sprite/PointTable/pointTableHeal", "Sprite/AttrIcon/AttrHeal");
        setRectTransformAnchor(healObj, new Vector2(5f/6, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]>() { 
            { "PointTable", new GameObject[] { rectObj } },
            { "PointAttrs", new GameObject[] { norObj, atkObj, defObj, movObj, spcObj, healObj } },
            { "PointTexts", new GameObject[] {
                norObj.transform.Find("num/txt").gameObject,
                atkObj.transform.Find("num/txt").gameObject,
                defObj.transform.Find("num/txt").gameObject,
                movObj.transform.Find("num/txt").gameObject,
                spcObj.transform.Find("num/txt").gameObject,
                healObj.transform.Find("num/txt").gameObject,
            } }
        };  
        return dict;
    }
    // Enemy Tower Status *************
    public static Dictionary<string, GameObject[]> create_BattleScene_EnemyTowerStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "EnemyTowerStatus");
        float width = Mathf.Min(parent.transform.GetComponent<RectTransform>().rect.width, 280f);
        setRectTransformPosition(rectObj, new Vector2(0.05f, 1f), new Vector2(0.05f, 1f), Vector2.zero, new Vector2(width, width/6));
        setRectPivot(rectObj, new Vector2(0f, 1f));

        AspectRatioFitter fitter = rectObj.AddComponent<AspectRatioFitter>();
        fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        fitter.aspectRatio = 6f;

        GameObject t1Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t1Obj, new Vector2(0f, 0f), new Vector2(1f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t2Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t2Obj, new Vector2(1f/6, 0f), new Vector2(2f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t3Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t3Obj, new Vector2(2f/6, 0f), new Vector2(3f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t4Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t4Obj, new Vector2(3f/6, 0f), new Vector2(4f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t5Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t5Obj, new Vector2(4f/6, 0f), new Vector2(5f/6, 1f), Vector2.zero, Vector2.zero);
        GameObject t6Obj = create_TowerStatus_Unit(rectObj);
        setRectTransformAnchor(t6Obj, new Vector2(5f/6, 0f), new Vector2(6f/6, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]> { 
            { "TowerTable", new GameObject[] { rectObj } },
            { "TowerIcons", new GameObject[] {
                t1Obj.transform.Find("TowerIcon").gameObject,
                t2Obj.transform.Find("TowerIcon").gameObject,
                t3Obj.transform.Find("TowerIcon").gameObject,
                t4Obj.transform.Find("TowerIcon").gameObject,
                t5Obj.transform.Find("TowerIcon").gameObject,
                t6Obj.transform.Find("TowerIcon").gameObject,
            } }
        }; 
        return dict;
    }

    // Menu Skill Table *************
    public static Dictionary<string, GameObject[]> create_BattleScene_PlayerSkillTable(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerSkill");
        setRectTransformAnchor(rectObj, new Vector2(0.1f, 0.75f), new Vector2(0.95f, 0.95f), Vector2.zero, Vector2.zero);

        GameObject skill1 = create_SkillBtn_Unit(rectObj);
        setRectTransformAnchor(skill1, new Vector2(0.01f, 0f), new Vector2(0.24f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill2 = create_SkillBtn_Unit(rectObj);
        setRectTransformAnchor(skill2, new Vector2(0.26f, 0f), new Vector2(0.49f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill3 = create_SkillBtn_Unit(rectObj);
        setRectTransformAnchor(skill3, new Vector2(0.51f, 0f), new Vector2(0.74f, 1f), Vector2.zero, Vector2.zero);
        GameObject skill4 = create_SkillBtn_Unit(rectObj);
        setRectTransformAnchor(skill4, new Vector2(0.76f, 0f), new Vector2(0.99f, 1f), Vector2.zero, Vector2.zero);

        Dictionary<string, GameObject[]> dict = new Dictionary<string, GameObject[]> {
            { "SkillTable",  new GameObject[] { rectObj } },
            { "SkillButtons", new GameObject[] { skill1, skill2, skill3, skill4 } }
        };
        return dict;
    }
    public static GameObject create_SkillBtn_Unit(GameObject parent) {
        GameObject skillBtn = createButton(parent, "SkillBtn", "skill_LABEL");
        GameObject txt = CanvasFactory.createText(skillBtn, "txt", "skill_ID");

        skillBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/skillNor");

        EventTrigger trigger = skillBtn.AddComponent<EventTrigger>();
        addPointerDownImageSprite(  trigger, skillBtn, Resources.Load<Sprite>("Sprite/Button/skillPressed"));
        addPointerUpImageSprite(    trigger, skillBtn, Resources.Load<Sprite>("Sprite/Button/skillNor"));
        addPointerEnterImageSprite( trigger, skillBtn, Resources.Load<Sprite>("Sprite/Button/skillOver"));
        addPointerExitImageSprite(  trigger, skillBtn, Resources.Load<Sprite>("Sprite/Button/skillNor"));

        txt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txt.GetComponent<Text>().fontSize = 200;
        txt.GetComponent<Text>().color = Color.black;

        return skillBtn;
    }
    // DiceBox  *************
    public static Dictionary<string, GameObject> create_BattleScene_DiceBox(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "DiceBox");
        setRectTransformPosition(rectObj, new Vector2(0f, 0.88f), new Vector2(0f, 0.88f), new Vector2(-100f, 0f), Vector2.zero );

        GameObject dicesPerson = create_DiceBox_Unit(rectObj, "DicesPerson", StringCoder.getDiceBoxString(3), "個人");
        dicesPerson.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/dicesPerson");
        setRectTransformPosition(dicesPerson, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(200f, 0f), new Vector2(200f, 50f));

        GameObject dicesTeam = create_DiceBox_Unit(rectObj, "DicesTeam", StringCoder.getDiceBoxString(2), "隊伍");
        dicesTeam.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/dicesTeam");
        setRectTransformPosition(dicesTeam, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(100f, 0f), new Vector2(200f, 50f));

        GameObject dicesGround = create_DiceBox_Unit(rectObj, "DicesGround", StringCoder.getDiceBoxString(1), "場地");
        dicesGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/Button/dicesGround");
        setRectTransformPosition(dicesGround, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(200f, 50f));

        Dictionary<string, GameObject> dict = new Dictionary<string, GameObject> { 
            { "DiceBox", rectObj },
            { "DicesPerson", dicesPerson },
            { "DicesTeam", dicesTeam },
            { "DicesGround", dicesGround }
        };

        return dict;
    }
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
    // Player Team character Status  *************
    public static GameObject create_BattleScene_TeamStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "TeamStatus");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.4f), new Vector2(1f, 0.79f), Vector2.zero, Vector2.zero );

        GameObject char1Obj = createEmptyRect(rectObj, "CharStatus1");
        setRectTransformAnchor(char1Obj, new Vector2(0f, 0.75f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        GameObject char2Obj = createEmptyRect(rectObj, "CharStatus2");
        setRectTransformAnchor(char2Obj, new Vector2(0f, 0.5f), new Vector2(1f, 0.75f), Vector2.zero, Vector2.zero);
        GameObject char3Obj = createEmptyRect(rectObj, "CharStatus3");
        setRectTransformAnchor(char3Obj, new Vector2(0f, 0.25f), new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        GameObject char4Obj = createEmptyRect(rectObj, "CharStatus4");
        setRectTransformAnchor(char4Obj, new Vector2(0f, 0f), new Vector2(1f, 0.25f), Vector2.zero, Vector2.zero);

        return rectObj;
    }
    public static GameObject create_BattleScene_PlayerTeamStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "TeamStatus");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.4f), new Vector2(1f, 0.79f), Vector2.zero, Vector2.zero );

        GameObject char1Obj = createEmptyRect(rectObj, "CharStatus1");
        setRectTransformAnchor(char1Obj, new Vector2(0f, 0.75f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        GameObject char2Obj = createEmptyRect(rectObj, "CharStatus2");
        setRectTransformAnchor(char2Obj, new Vector2(0f, 0.5f), new Vector2(1f, 0.75f), Vector2.zero, Vector2.zero);
        GameObject char3Obj = createEmptyRect(rectObj, "CharStatus3");
        setRectTransformAnchor(char3Obj, new Vector2(0f, 0.25f), new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        GameObject char4Obj = createEmptyRect(rectObj, "CharStatus4");
        setRectTransformAnchor(char4Obj, new Vector2(0f, 0f), new Vector2(1f, 0.25f), Vector2.zero, Vector2.zero);

        return rectObj;
    }
    public static GameObject create_PlayerCharStatus_Unit(GameObject parent, string label) {
        GameObject imageObj = createButton(parent, "CharBar", label);
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprite/CharBar/charBarBack" );
        setRectTransformPosition(imageObj, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(250f, 50f));

        GameObject infoObj = createImage(imageObj, "Info");
        infoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/charBarInfo");
        setRectTransformPosition(infoObj, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(100f, 50f) );
        setRectPivot(infoObj, new Vector2(0f, 0.5f));
        create_CharStatus_Unit_Info(infoObj);

        GameObject barObj = createImage(imageObj, "HpBar");
        barObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/charBarHp");
        setRectTransformPosition(barObj, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(46f, 50f));
        setRectPivot(barObj, new Vector2(1f, 0.5f));
;       Mask barMask = barObj.AddComponent<Mask>();
        barMask.showMaskGraphic = true;

        GameObject maskImage = createImage(barObj, "HpImage");
        maskImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/barImage");
        setWholeRect(maskImage);

        GameObject textObj = createText(imageObj, "txtName", "name");
        setRectTransformAnchor(textObj, new Vector2(0.8f, 0f), new Vector2(0.8f, 0f), Vector2.zero, Vector2.zero);
        textObj.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textObj.GetComponent<Text>().alignment = TextAnchor.LowerRight;
        textObj.GetComponent<Text>().fontSize = 150;
        textObj.GetComponent<Text>().color = Color.white;

        return imageObj;
    }
    public static GameObject create_BattleScene_EnemyTeamStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "TeamStatus");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.5f), new Vector2(1f, 0.89f), Vector2.zero, Vector2.zero );

        GameObject char1Obj = createEmptyRect(rectObj, "CharStatus1");
        setRectTransformAnchor(char1Obj, new Vector2(0f, 0f), new Vector2(1f, 0.25f), Vector2.zero, Vector2.zero);
        GameObject char2Obj = createEmptyRect(rectObj, "CharStatus2");
        setRectTransformAnchor(char2Obj, new Vector2(0f, 0.25f), new Vector2(1f, 0.5f), Vector2.zero, Vector2.zero);
        GameObject char3Obj = createEmptyRect(rectObj, "CharStatus3");
        setRectTransformAnchor(char3Obj, new Vector2(0f, 0.5f), new Vector2(1f, 0.75f), Vector2.zero, Vector2.zero);
        GameObject char4Obj = createEmptyRect(rectObj, "CharStatus4");
        setRectTransformAnchor(char4Obj, new Vector2(0f, 0.75f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        return rectObj;
    }
    public static GameObject create_EnemyCharStatus_Unit(GameObject parent, string label) {
        GameObject imageObj = createButton(parent, "CharBar", label);
        imageObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprite/CharBar/charBarBackEnemy" );
        setRectTransformPosition(imageObj, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(250f, 50f));

        GameObject infoObj = createImage(imageObj, "Info");
        infoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/charBarInfo");
        setRectTransformPosition(infoObj, new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), Vector2.zero, new Vector2(100f, 50f) );
        setRectPivot(infoObj, new Vector2(1f, 0.5f));
        create_CharStatus_Unit_Info(infoObj);

        GameObject barObj = createImage(imageObj, "HpBar");
        barObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/charBarHp");
        setRectTransformPosition(barObj, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(46f, 50f));
        setRectPivot(barObj, new Vector2(0f, 0.5f));
;       Mask barMask = barObj.AddComponent<Mask>();
        barMask.showMaskGraphic = true;

        GameObject maskImage = createImage(barObj, "HpImage");
        maskImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharBar/barImage");
        setWholeRect(maskImage);

        GameObject textObj = createText(imageObj, "txtName", "name");
        setRectTransformAnchor(textObj, new Vector2(0.1f, 0f), new Vector2(0.1f, 0f), Vector2.zero, Vector2.zero);
        textObj.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textObj.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
        textObj.GetComponent<Text>().fontSize = 150;
        textObj.GetComponent<Text>().color = Color.white;

        return imageObj;
    }
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
        GameObject numHP = createText(parent, "numHP", "0");
        setZeroPosition(numHP, new Vector2(0.9f, 0.8f));
        setRectPivot(txtHp, new Vector2(1f, 0.5f));
        numHP.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numHP.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numHP.GetComponent<Text>().fontSize = 180;
        numHP.GetComponent<Text>().color = Color.white;
        GameObject numATK = createText(parent, "numATK", "0");
        setZeroPosition(numATK, new Vector2(0.9f, 0.5f));
        setRectPivot(txtHp, new Vector2(1f, 0.5f));
        numATK.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numATK.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numATK.GetComponent<Text>().fontSize = 180;
        numATK.GetComponent<Text>().color = Color.white;
        GameObject numDEF = createText(parent, "numDEF", "0");
        setZeroPosition(numDEF, new Vector2(0.9f, 0.2f));
        setRectPivot(txtHp, new Vector2(1f, 0.5f));
        numDEF.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        numDEF.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        numDEF.GetComponent<Text>().fontSize = 180;
        numDEF.GetComponent<Text>().color = Color.white;
    }
    // character battle place  *************
    public static GameObject create_BattleScene_PlayerBattlePlace(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "PlayerBattlePlace");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.37f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        GameObject maskObj = createImage(rectObj, "Mask");
        setRectTransformAnchor(maskObj, new Vector2(0f, 0f), new Vector2(2f, 1f), Vector2.zero, Vector2.zero);
        Mask mask = maskObj.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        GameObject charObj = createImage(maskObj, "Character");
        charObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprite/CharPlace/tmpChar" );
        setRectTransformPosition(charObj, new Vector2(0.35f, 0f), new Vector2(0.35f, 0f), Vector2.zero, Vector2.zero);
        setRectPivot(charObj, new Vector2(0.5f, 0f));
        charObj.GetComponent<Image>().SetNativeSize();

        GameObject bottomObj = createImage(rectObj, "BottomLine");
        bottomObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharPlace/bottomLine");
        setRectTransformAnchor(bottomObj, new Vector2(0f, 0f), new Vector2(1f, 0.05f), Vector2.zero, Vector2.zero );

        GameObject postureObj = createImage(rectObj, "Posture");
        postureObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharPlace/posture");
        setRectTransformPosition(postureObj, new Vector2(1f, 0.1f), new Vector2(1f, 0.1f), Vector2.zero, new Vector2(120f, 30f));
        setRectPivot(postureObj, new Vector2(1f, 0f));

        return rectObj;
    }
    public static GameObject create_BattleScene_EnemyBattlePlace(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "EnemyBattlePlace");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.47f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);

        GameObject maskObj = createImage(rectObj, "Mask");
        setRectTransformAnchor(maskObj, new Vector2(-1f, 0f), new Vector2(1f, 1f), Vector2.zero, Vector2.zero);
        Mask mask = maskObj.AddComponent<Mask>();
        mask.showMaskGraphic = false;

        GameObject charObj = createImage(maskObj, "Character");
        charObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprite/CharPlace/tmpChar" );
        setRectTransformPosition(charObj, new Vector2(0.65f, 0f), new Vector2(0.65f, 0f), Vector2.zero, Vector2.zero);
        setRectPivot(charObj, new Vector2(0.5f, 0f));
        charObj.GetComponent<Image>().SetNativeSize();

        GameObject bottomObj = createImage(rectObj, "BottomLine");
        bottomObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharPlace/bottomLine");
        setRectTransformAnchor(bottomObj, new Vector2(0f, 0f), new Vector2(1f, 0.05f), Vector2.zero, Vector2.zero);

        GameObject postureObj = createImage(rectObj, "Posture");
        postureObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/CharPlace/posture");
        setRectTransformPosition(postureObj, new Vector2(0f, 0.1f), new Vector2(0f, 0.1f), Vector2.zero, new Vector2(120f, 30f));
        setRectPivot(postureObj, new Vector2(0f, 0f));

        return rectObj;
    }
    // Turn Status   *************
    public static Dictionary<string, GameObject> create_BattleScene_TurnStatus(GameObject parent) {
        GameObject rectObj = createEmptyRect(parent, "TurnStatus");
        setRectTransformAnchor(rectObj, new Vector2(0f, 0.45f), new Vector2(1f, 0.85f), Vector2.zero, Vector2.zero);

        GameObject movTurnObj = createImage(rectObj, "MovTurn");
        movTurnObj.GetComponent<Image>().sprite = Resources.Load<Sprite>( "Sprite/TurnIcon/movTurn" );
        setRectTransformPosition(movTurnObj, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(240f, 240f));

        GameObject atkTurnObj = createEmptyRect(rectObj, "AtkTurn");
        setWholeRect(atkTurnObj);
        GameObject playerAtkObj = createImage(atkTurnObj, "Player");
        playerAtkObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TurnIcon/playerAtkTurn");
        setRectTransformPosition(playerAtkObj, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        setRectPivot(playerAtkObj, new Vector2(1f, 0.5f));
        GameObject playerAtkTxt = createText(playerAtkObj, "txt", "攻擊\n階段");
        setZeroPosition(playerAtkTxt, new Vector2(0.65f, 0.45f));
        playerAtkTxt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        playerAtkTxt.GetComponent<Text>().fontSize = 200;
        playerAtkTxt.GetComponent<Text>().color = Color.blue;
        GameObject enemyDefObj = createImage(atkTurnObj, "Enemy");
        enemyDefObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TurnIcon/enemyDefTurn");
        setRectTransformPosition(enemyDefObj, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        setRectPivot(enemyDefObj, new Vector2(0f, 0.5f));
        GameObject enemyDefTxt = createText(enemyDefObj, "txt", "防禦\n階段");
        setZeroPosition(enemyDefTxt, new Vector2(0.35f, 0.45f));
        enemyDefTxt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        enemyDefTxt.GetComponent<Text>().fontSize = 200;
        enemyDefTxt.GetComponent<Text>().color = Color.blue;

        GameObject defTurnObj = createEmptyRect(rectObj, "DefTurn");
        setWholeRect(defTurnObj);
        GameObject playerDefObj = createImage(defTurnObj, "Player");
        playerDefObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TurnIcon/playerDefTurn");
        setRectTransformPosition(playerDefObj, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        setRectPivot(playerDefObj, new Vector2(1f, 0.5f));
        GameObject playerDefTxt = createText(playerDefObj, "txt", "防禦\n階段");
        setZeroPosition(playerDefTxt, new Vector2(0.65f, 0.45f));
        playerDefTxt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        playerDefTxt.GetComponent<Text>().fontSize = 200;
        playerDefTxt.GetComponent<Text>().color = Color.blue;
        GameObject enemyAtkObj = createImage(defTurnObj, "Enemy");
        enemyAtkObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprite/TurnIcon/enemyAtkTurn");
        setRectTransformPosition(enemyAtkObj, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(120f, 240f));
        setRectPivot(enemyAtkObj, new Vector2(0f, 0.5f));
        GameObject enemyAtkTxt = createText(enemyAtkObj, "txt", "攻擊\n階段");
        setZeroPosition(enemyAtkTxt, new Vector2(0.35f, 0.45f));
        enemyAtkTxt.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        enemyAtkTxt.GetComponent<Text>().fontSize = 200;
        enemyAtkTxt.GetComponent<Text>().color = Color.blue;

        GameObject txtObj = createText(rectObj, "TurnInfo", "第 1 回合");
        setZeroPosition(txtObj, new Vector2(0.5f, 0.68f));
        txtObj.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        txtObj.GetComponent<Text>().fontSize = 300;
        txtObj.GetComponent<Text>().color = Color.blue;

        Dictionary<string, GameObject> dict = new Dictionary<string, GameObject> { 
            { "TurnStatus", rectObj },
            { "MovTurn", movTurnObj },
            { "AtkTurn", atkTurnObj },
            { "DefTurn", defTurnObj },
            { "TurnInfo", txtObj }
        };
        return dict;
    }
}
