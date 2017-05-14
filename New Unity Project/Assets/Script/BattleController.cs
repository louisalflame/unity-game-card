using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleController {

    private TurnManager _turnManager = null;
    private DiceManager _diceManager = null;
    private TowerManager _towerManager = null;
    private TeamManager _teamManager = null;
    private AttrDecisionManager _attrDecisionManager = null;
    // 介面總管理
    private InterfaceController _interface = null;
    private bool _inputValid = false;

    List<GameObject> dices;

    public BattleController() {
        _diceManager = new DiceManager(this);
        _towerManager = new TowerManager(this);
        _teamManager = new TeamManager(this);
        _attrDecisionManager = new AttrDecisionManager(this);
        _interface = new InterfaceController(this);
        _inputValid = true;

        _turnManager = new TurnManager();
        _turnManager.initSetting(this, new PrepareTurn(this, _turnManager));
    }

    // update管理 ============================================================================
    public void update() {
        _interface.update();
        _turnManager.update();
    }
    //========================================================================================

    // 介面管理 ==============================================================================
    public void showBasicInterface() {
        showNextButton();
    }
    public void hideThrowButton() { _interface.hideThrowButton(); }
    public void showThrowButton() { _interface.showThrowButton(); }
    public void hideNextButton() { _interface.hideNextButton(); }
    public void showNextButton() { _interface.showNextButton(); }
    public void showDiceBox() { _interface.showDiceBox( _diceManager.getDicesUnused() ); }
    public void showDicePlay() { _interface.showDicePlay(_diceManager.getDicesUsing()); }
    //========================================================================================
    
    public void nextTurn() { _turnManager.nextTurn();  }
    public void newPrepareTurn() {
        // 製作所有dicebox 的骰子
        for (int i = 0; i < 10; i++) { _diceManager.addDicesUnused( new NorDice() ); }
        for (int i = 0; i < 10; i++) { _diceManager.addDicesUnused( new AtkDice() ); }
        // 顯示基本物件
        showBasicInterface();
    }
    public void newStartTurn() {
        hideNextButton();
        showThrowButton();
        showDiceBox();
    }
    public void endStartTurn() {
        _inputValid = true;
    }
    public void newDecisionTurn() {
        _attrDecisionManager.setDicesResult(_interface.getDicesResult(), _diceManager.getDicesUsing());
        _interface.setAttrDecision(_attrDecisionManager);
        _interface.showAttrDecision();
        _interface.removeDices3D();
        _interface.showNextButton();
    }
    public void endDecisionTurm() {
        _towerManager.collectBasePoint(_attrDecisionManager.getFacesBase());
        _towerManager.collectAttrPoint(_attrDecisionManager.getFacesAttr());
        _interface.setTowerStatus(_towerManager._towers);
        _interface.setAttrNums(_towerManager._attrNums);
        _interface.removeFaceDecision();
        _diceManager.recycleDices();
    }

    // player指令動作 ========================================================================
    public bool isInputValid() { return _inputValid; }
    public void CountResultAndNextTurn(){
        _turnManager.nextTurn();
    }
    public void throwDices() {
        _inputValid = false;
        hideThrowButton();
        //抓出box前5個dices
        _diceManager.addDicesUsing(5);
        //製作dice 3D模型
        showDicePlay();
        //下一階段 等待骰子 隨機擲出 記錄骰值 
        _interface.startWaitDicesAnimate(); 
    }
    public void decideFace(string str) {
        _attrDecisionManager.decideFace(str);
        _interface.showAttrDecision();
    }
    //========================================================================================
}

//骰子管理(未使用/使用中/使用過)
public class DiceManager {
    public BattleController _battle;
    private List<Dice> _dicesUnused = null;
    private List<Dice> _dicesUsing = null;
    private List<Dice> _dicesUsed = null;

    public DiceManager(BattleController battle) {
        _battle = battle;
        _dicesUnused = new List<Dice>();
        _dicesUsing = new List<Dice>();
        _dicesUsed = new List<Dice>();
    }
    public void addDicesUnused(Dice dice) {
        _dicesUnused.Add(dice);
    }
    public void addDicesUsing(int n=0){
        if (_dicesUnused.Count < n) {
            foreach (Dice d in _dicesUsed) { _dicesUnused.Add(d); }
            _dicesUsed = new List<Dice>();
        }
        for (int i = 0; i < n && _dicesUnused.Count > 0; i++) {
            Dice _newUsing = _dicesUnused[0];
            _dicesUnused.RemoveAt(0);
            _dicesUsing.Add(_newUsing);
        }
    }
    public void recycleDices() {
        foreach (Dice d in _dicesUsing) { _dicesUsed.Add(d); }
        _dicesUsing.Clear();
    }

    public List<Dice> getDicesUnused() { return _dicesUnused; }
    public List<Dice> getDicesUsing() { return _dicesUsing; }
}

public class AttrDecisionManager{
    public BattleController _battle;
    private List<DiceFace> _toAttr;
    private List<DiceFace> _toBase;
    public AttrDecisionManager(BattleController battle) {
        _battle = battle;
    }

    public void setDicesResult(List<int> result, List<Dice> diceUsing) {
        _toAttr = new List<DiceFace>();
        _toBase = new List<DiceFace>();
        for (int i = 0; i < diceUsing.Count; i++) {
            int f = result[i];
            DiceFace face = diceUsing[i].getFace(f);
            Debug.Log( f+face.ToString() );
            _toAttr.Add(face);
        }
    }
    public void decideFace(string str) {
        int id;
        if (str.StartsWith("decision-attr-")) {
            if (int.TryParse(str.Split('-')[2], out id)) {
                id = int.Parse(str.Split('-')[2]);
                if (id < _toAttr.Count) {
                    DiceFace face = _toAttr[id];
                    _toAttr.RemoveAt(id);
                    _toBase.Add(face);
                }
            }
        } else if (str.StartsWith("decision-base-")) { 
            if (int.TryParse(str.Split('-')[2], out id)) {
                id = int.Parse(str.Split('-')[2]); 
                if (id < _toBase.Count) {
                    DiceFace face = _toBase[id];
                    _toBase.RemoveAt(id);
                    _toAttr.Add(face);
                }
            }
        }
    }
    public List<DiceFace> getFacesAttr() { return _toAttr; }
    public List<DiceFace> getFacesBase() { return _toBase; }
}

//儲存塔管理
public class TowerManager {
    public BattleController _battle;
    public AttrTower[] _towers { get; private set; }
    private int[] _attrMax = null;
    public int[] _attrNums {get; private set;}
    public TowerManager(BattleController battle) {
        _battle = battle;
        _towers = new AttrTower[6] { new AttrTower(0), new AttrTower(1), new AttrTower(2), 
                                     new AttrTower(3), new AttrTower(4), new AttrTower(5) };
        _attrMax    = new int[6] { 5, 5, 5, 5, 5, 5 };
        _attrNums = new int[6] { 0, 0, 0, 0, 0, 0 };
    }


    public void collectAttrPoint(List<DiceFace> faces) {
        countAttrMax();
        foreach (DiceFace face in faces) {
            _attrNums[face._attr] += face._num;
        }
        for (int i = 0; i < 6; i++) { _attrNums[i] = Mathf.Min(_attrNums[i], _attrMax[i]); }
    }
    public void countAttrMax() {
        _attrMax    = new int[6] { 5, 5, 5, 5, 5, 5 };
        foreach (AttrTower t in _towers) {
            if (t._level > 0) {
                _attrMax[t._attr] += t._capacity;
            }
        }
    }

    public void collectBasePoint(List<DiceFace> bases) {
        foreach (DiceFace f in bases) { Debug.Log(f); }
        List<AttrBaseCounter> counters = findAttrPriority(bases);
        List<List<AttrTower>> towerGroups = findTowerPriority();
        
        //先按照塔等級的順序
        foreach (List<AttrTower> levelTowers in towerGroups) {
            //同等級的塔，以點數加權大者優先
            foreach (AttrBaseCounter counter in counters) { 
                if (counter._base <= 0) continue;
                //尋找此等級的塔是否有點數可升級的屬性
                foreach (AttrTower t in levelTowers) {  
                    if(t._attr == counter._attr && t.isValidUpgrade(counter) ) {
                        t.upgrade(counter); 
                        return; 
                    }
                    else if (t._level == 0 && t.isValidBuild(counter)) {
                        t.build(counter);
                        return;
                    }
                }
            }
        }
    }
    public List<AttrBaseCounter> findAttrPriority(List<DiceFace> bases) {
        //先整理所有升級用點數和其屬性
        AttrBaseCounter[] counters = new AttrBaseCounter[6] { 
            new AttrBaseCounter(Attribute.Normal), new AttrBaseCounter(Attribute.Attack), new AttrBaseCounter(Attribute.Deffense),
            new AttrBaseCounter(Attribute.Move), new AttrBaseCounter(Attribute.Special), new AttrBaseCounter(Attribute.Health) };
        List<AttrBaseCounter> countersWithWeight = new List<AttrBaseCounter>();
        List<AttrBaseCounter> countersWithoutWeight = new List<AttrBaseCounter>();
        List<AttrBaseCounter> countersSorted = new List<AttrBaseCounter>();
        //先累計所有點數和加倍點數
        foreach (DiceFace face in bases) {
            counters[face._base]._base += 1;
            counters[face._base]._weight += face._baseWeight;
        }
        //分為有無weight兩部分，再排序，以降冪
        foreach (AttrBaseCounter counter in counters) {
            if (counter._weight != 0) countersWithWeight.Add(counter);
            else countersWithoutWeight.Add(counter);
        }
        countersWithWeight.Sort((x, y) => y._base.CompareTo(x._base));
        countersWithoutWeight.Sort((x, y) => y._base.CompareTo(x._base));
        //有weight在前，無weight在後
        foreach (AttrBaseCounter c in countersWithWeight) { countersSorted.Add(c); }
        foreach (AttrBaseCounter c in countersWithoutWeight) { countersSorted.Add(c); }
        return countersSorted;
    }
    public List<List<AttrTower>> findTowerPriority() {
        AttrTower[] tempTowers = new AttrTower[6] { null, null, null, null, null, null };
        //先複製陣列，讓塔sort by lv，升級順序降冪以高lv優先
        Array.Copy(_towers, 0, tempTowers, 0, _towers.Length);
        Array.Sort(tempTowers, (x,y)=>y._level.CompareTo(x._level) );
        List<List<AttrTower>> towerGroups = new List<List<AttrTower>>();
        List<AttrTower> levelGroup = new List<AttrTower>();
        foreach (AttrTower t in tempTowers) { 
            if (levelGroup.Count == 0 || t._level == levelGroup[0]._level) {
                levelGroup.Add(t);
            } else {
                levelGroup.Sort((x,y)=>x._positionID.CompareTo(y._positionID));
                towerGroups.Add(levelGroup);
                levelGroup = new List<AttrTower>() {t};
            }
        }
        if (levelGroup.Count != 0) {
            //同等級以位置左->右排列
            levelGroup.Sort((x,y)=>x._positionID.CompareTo(y._positionID));
            towerGroups.Add(levelGroup);        
        }

        return towerGroups;
    }
}

public class TeamManager {
    public BattleController _battle;
    private CharManager _char1 = null;
    private CharManager _char2 = null;
    private CharManager _char3 = null;
    public TeamManager(BattleController battle) {
        _battle = battle;
        _char1 = new CharManager();
        _char2 = new CharManager();
        _char3 = new CharManager();
    }
}
public class CharManager {
    private Character _character = null;
    public CharManager() {
        _character = new Character();
    }
}