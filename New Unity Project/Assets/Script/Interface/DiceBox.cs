using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 待使用骰子顯示
public class DiceBoxInterface {
    public InterfaceController _interface;
    public GameObject _diceBox { get; private set; }
    public GameObject _dicesGround { get; private set; }
    public GameObject _dicesTeam { get; private set; }
    public GameObject _dicesPerson { get; private set; }
    public List<GameObject> _dices { get; private set; }

    private DiceBoxMode _mode;
    public void update() { }

    public DiceBoxInterface(InterfaceController inter) {
        _interface = inter;
        _mode = new DiceBoxNormalMode(this); 

        _dices = new List<GameObject>();
    }

    public void create() { 
         create_BattleScene_DiceBox(_interface.getImageLeftStatus());
         _diceBox.SetActive(false);
    }
    public void initial() {
        _diceBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400f, 0f);
        _diceBox.SetActive(true);
    }

    public void clear() {
        foreach (GameObject d in _dices) { MonoBehaviour.Destroy(d); }
        _dices.Clear();
    } 

    public void checkDiceBox(int type) {
        _mode = _mode.getNextMode( _mode.getID(), type );
        
        //_mode.showDiceBox( _dices );
    }
    public AnimateWork[] getModeAnimates() { return _mode.getModeAnimates(); }
    public AnimateWork getDiceStackShowAnimate() { return _mode.getDiceStackShowAnimate(); }
    
    // DiceBox  *************
    public void create_BattleScene_DiceBox(GameObject parent) {
        _diceBox = CanvasFactory.createEmptyRect(parent, "DiceBox");
        CanvasFactory.setRectTransformPosition(_diceBox, new Vector2(0f, 0.88f), new Vector2(0f, 0.88f), new Vector2(-100f, 0f), Vector2.zero );

        _dicesPerson = CanvasFactory.create_DiceBox_Unit(_diceBox, "DicesPerson", StringCoder.getDiceBoxString(3), "個人");
        CanvasFactory.setImageSprite(_dicesPerson, "Sprite/Button/dicesPerson");
        CanvasFactory.setRectTransformPosition(_dicesPerson, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(200f, 0f), new Vector2(200f, 50f));

        _dicesTeam = CanvasFactory.create_DiceBox_Unit(_diceBox, "DicesTeam", StringCoder.getDiceBoxString(2), "隊伍");
        CanvasFactory.setImageSprite(_dicesTeam, "Sprite/Button/dicesTeam");
        CanvasFactory.setRectTransformPosition(_dicesTeam, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(100f, 0f), new Vector2(200f, 50f));

        _dicesGround = CanvasFactory.create_DiceBox_Unit(_diceBox, "DicesGround", StringCoder.getDiceBoxString(1), "場地");
        CanvasFactory.setImageSprite(_dicesGround, "Sprite/Button/dicesGround");
        CanvasFactory.setRectTransformPosition(_dicesGround, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(200f, 50f));         
    }
}

public abstract class DiceBoxMode { 
    public DiceBoxInterface _diceBox { get; protected set; }
    public DiceBoxMode getNextMode(int nowID, int nextID) {
        if (nowID == nextID) return new DiceBoxNormalMode(_diceBox);
        else if (nextID == DiceBoxGroundMode.id)    return new DiceBoxGroundMode(_diceBox);
        else if (nextID == DiceBoxTeamMode.id)      return new DiceBoxTeamMode(_diceBox);
        else if (nextID == DiceBoxPersonMode.id)    return new DiceBoxPersonMode(_diceBox);
        else return this;  
    }
    public abstract int getID();
    public abstract AnimateWork[] getModeAnimates();
    public abstract AnimateWork getDiceStackShowAnimate();
    public abstract void showDiceBox( List<GameObject> dices );
}

public class DiceBoxNormalMode : DiceBoxMode {
    public const int id = 0;
    public DiceBoxNormalMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override AnimateWork[] getModeAnimates(){
        return new AnimateWork[] {
            new AnimateMoveTo(_diceBox._dicesGround.GetComponent<RectTransform>(), new Vector2(0f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.GetComponent<RectTransform>(), new Vector2(100f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.GetComponent<RectTransform>(), new Vector2(200f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesGround.transform.Find("txt").GetComponent<RectTransform>(), Vector2.zero, 10),
            new AnimateMoveTo(_diceBox._dicesTeam.transform.Find("txt").GetComponent<RectTransform>(), Vector2.zero, 10),
            new AnimateMoveTo(_diceBox._dicesPerson.transform.Find("txt").GetComponent<RectTransform>(), Vector2.zero, 10)
        };      
    }
    public override AnimateWork getDiceStackShowAnimate() {
        _diceBox.clear(); 
        return new AnimateWork();
    }
    public override void showDiceBox(List<GameObject> diceObjs) { 
        _diceBox.clear();}
}
public class DiceBoxGroundMode : DiceBoxMode {
    public const int id = 1;
    public DiceBoxGroundMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override AnimateWork[] getModeAnimates() {
        return new AnimateWork[] {
            new AnimateMoveTo(_diceBox._dicesGround.GetComponent<RectTransform>(), new Vector2(70f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.GetComponent<RectTransform>(), new Vector2(100f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.GetComponent<RectTransform>(), new Vector2(130f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesGround.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10)
        };
    }
    public override AnimateWork getDiceStackShowAnimate() {
        _diceBox.clear();
        List<Dice> dices = _diceBox._interface.getDicesGroundUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesGround.transform.Find("Layout").gameObject, "icon_"+i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            AnimateWork.setAlpha(imageObj.transform, 0f);
            _diceBox._dices.Add(imageObj);
        }
        return new AnimateOrderFadeIn(_diceBox._dices, 10);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface.getDicesGroundUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesGround.transform.Find("Layout").gameObject, "icon_"+i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            diceObjs.Add(imageObj);
        }
    }
}
public class DiceBoxTeamMode : DiceBoxMode { 
    public const int id = 2;
    public DiceBoxTeamMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override AnimateWork[] getModeAnimates() {
        return new AnimateWork[] {
            new AnimateMoveTo(_diceBox._dicesGround.GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.GetComponent<RectTransform>(), new Vector2(100f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.GetComponent<RectTransform>(), new Vector2(130f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesGround.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10)
        };      
    }
    public override AnimateWork getDiceStackShowAnimate() {
        _diceBox.clear();
        List<Dice> dices = _diceBox._interface.getDicesTeamUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesTeam.transform.Find("Layout").gameObject, "icon_"+i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            AnimateWork.setAlpha(imageObj.transform, 0f);
            _diceBox._dices.Add(imageObj);
        }
        return new AnimateOrderFadeIn(_diceBox._dices, 10);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface.getDicesTeamUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage(_diceBox._dicesTeam.transform.Find("Layout").gameObject, "icon_" + i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            diceObjs.Add(imageObj);
        }
    }
}
public class DiceBoxPersonMode : DiceBoxMode {
    public const int id = 3;
    public DiceBoxPersonMode(DiceBoxInterface dicebox) { _diceBox = dicebox; }
    public override int getID() { return id; }
    public override AnimateWork[] getModeAnimates(){
        return new AnimateWork[] {
            new AnimateMoveTo(_diceBox._dicesGround.GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.GetComponent<RectTransform>(), new Vector2(-30f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.GetComponent<RectTransform>(), new Vector2(130f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesGround.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesTeam.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10),
            new AnimateMoveTo(_diceBox._dicesPerson.transform.Find("txt").GetComponent<RectTransform>(), new Vector2(-60f, 0f), 10)
        };      
    }
    public override AnimateWork getDiceStackShowAnimate() {
        _diceBox.clear();
        List<Dice> dices = _diceBox._interface.getDicesPersonUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage( _diceBox._dicesPerson.transform.Find("Layout").gameObject, "icon_"+i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            AnimateWork.setAlpha(imageObj.transform, 0f);
            _diceBox._dices.Add(imageObj);
        }
        return new AnimateOrderFadeIn(_diceBox._dices, 10);
    }
    public override void showDiceBox(List<GameObject> diceObjs) {
        List<Dice> dices = _diceBox._interface.getDicesPersonUseStack();
        for (int i = 0; i < dices.Count; i++) {
            GameObject imageObj = CanvasFactory.createImage(_diceBox._dicesPerson.transform.Find("Layout").gameObject, "icon_" + i);
            CanvasFactory.setImageSprite(imageObj, dices[i].getIconImage());
            CanvasFactory.setImageNatureSize(imageObj);
            diceObjs.Add(imageObj);
        }
    }
}