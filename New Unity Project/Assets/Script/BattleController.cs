using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleController {

    // 回合管理
    public TurnManager _turnManager { get; private set; }
    // 攻防戰鬥管理
    public BattleManager _battleManager { get; private set; }
    // 骰子管理
    public DiceManager _diceManager { get; private set; }
    // 點數選擇管理
    public AttrDecisionManager _attrDecisionManager { get; private set; }
    // 點數儲存管理
    public TowerManager _towerManager { get; private set; }
    // 隊伍角色管理 我方/敵方
    public TeamManager _playerManager { get; private set; }
    public TeamManager _enemyManager { get; private set; }
    // 介面總管理
    public InterfaceController _interface { get; private set; }
    // 玩家可輸入開關
    public bool _inputValid { get; private set; }

    List<GameObject> dices;

    public BattleController() {
        _diceManager = new DiceManager(this);
        _towerManager = new TowerManager(this);
        _playerManager = new TeamManager(this);
        _enemyManager = new TeamManager(this);
        _attrDecisionManager = new AttrDecisionManager(this);
        _interface = new InterfaceController(this);
        _inputValid = true;
        
        // 回合從起始階段開始，最後執行
        _turnManager = new TurnManager();
        _turnManager.initSetting(this, new PrepareTurn(this, _turnManager));
    }

    // update管理 ============================================================================
    public void update() {
        _interface.update();
        _turnManager.update();
    }
    //========================================================================================
    
    public void nextTurn() { _turnManager.nextTurn();  }
    public void newPrepareTurn() {
        // 製作所有dicebox 的骰子
        for (int i = 0; i < 10; i++) { _diceManager.addDicesUnused( new NorDice() ); }
        for (int i = 0; i < 10; i++) { _diceManager.addDicesUnused( new AtkDice() ); }
        // 顯示基本物件
        _interface.showNextButton();
        // 製作角色
        _playerManager.setCharacters(10,11,12);
        _enemyManager.setCharacters(0,1,2);

        nextTurn();
    }
    public void newStartTurn() {
        _battleManager = new BattleManager(this);
        _interface.hideNextButton();
        _interface.showThrowButton();
        _interface.showDiceBox(_diceManager.getDicesUnused());
    }
    public void endStartTurn() {
        _inputValid = true;
    }
    public void newDecisionTurn() {
        _attrDecisionManager.setDicesResult(_interface.getDicesResult(), _diceManager.getDicesUsing());
        _interface.setAttrDecision(_attrDecisionManager);
        _interface.showAttrDecision();
        _interface.removeDices3D();
        // 顯示移動階段動作選擇按鈕
        _interface.showNextButton();
        _interface.showMoveActionButton();

        // TODO 敵方隊伍AI選擇移動階段動作
        _enemyManager.getActiveChar().setMoveAction(1);
    }
    public void endDecisionTurm() {
        _towerManager.collectBasePoint(_attrDecisionManager.getFacesBase());
        _towerManager.collectAttrPoint(_attrDecisionManager.getFacesAttr());
        _diceManager.recycleDices();

        _interface.setTowerStatus(_towerManager._towers);
        _interface.setAttrNums(_towerManager._attrNums);
        _interface.removeFaceDecision();
        _interface.hideMoveActionButton();

        _battleManager.judgePlayerFirst(_playerManager, _enemyManager);
    }
    public void newMoveCountTurn() { 
        // 移動階段動畫
        // 若敵方更換角色 顯示動畫
        // 若更換戰鬥角色則顯示更換選項，若否則結束此階段
        if (_playerManager.isChangeActiveChar()) {
            _interface.showTeamRearrangeButton();
        }
        else { nextTurn();}
    }
    public void endMoveCountTurn() {  }

    public void newPlayerAttackTurn() {
        //以先攻者開始設定攻防參數
        _battleManager.resetBattleUnit();
        _battleManager.setPlayerAttacking();
    }
    public void endPlayerAttackTurn() { }
    public void newEnemyDefenseTurn() { 
        // 敵方隊伍AI選擇防禦動作
        nextTurn();
    }
    public void endEnemyDefenseTurn() { }
    public void newEnemyAttackTurn() {
        //以先攻者開始設定攻防參數
        _battleManager.resetBattleUnit();
        _battleManager.setEnemyAttacking();

        // 敵方隊伍AI選擇攻擊動作
        nextTurn();
    }
    public void endEnemyAttackTurn() {  }
    public void newPlayerDefenseTurn() { }
    public void endPlayerDefenseTurn() { }
    public void newBattleCountTurn() { 
        //顯示對戰動畫
        nextTurn();
    }
    public void endBattleCountTurn() {
        _battleManager.runBattle();
    }

    public void newAnalysisTurn() { 
        //顯示回合結束動畫
        nextTurn();
    }
    public void endAnalysisTurn() {
        //回合結束 釋放多餘點數
        _towerManager.filterAttrPoints();
        _interface.setAttrNums(_towerManager._attrNums);
    }
    public void newRearrangeTurn() { }
    public void endRearrangeTurn() { }

    // player指令動作 ========================================================================
    public bool isInputValid() { return _inputValid; }
    public void CountResultAndNextTurn(){
        _turnManager.nextTurn();
    }
    public void throwDices() {
        _inputValid = false;
        _interface.hideThrowButton();
        //抓出box前5個dices
        _diceManager.addDicesUsing(5);
        _interface.showDiceBox(_diceManager.getDicesUnused());
        //製作dice 3D模型
        _interface.showDicePlay(_diceManager.getDicesUsing());
        //下一階段 等待骰子 隨機擲出 記錄骰值 
        _interface.startWaitDicesAnimate(); 
    }
    public void decisionAttr(int id) {
        _attrDecisionManager.decisionAttr(id);
        _interface.showAttrDecision();
    }
    public void decisionBase(int id) {
        _attrDecisionManager.decisionBase(id);
        _interface.showAttrDecision();
    }

    public void moveAction(int action) {
        _playerManager.getActiveChar().setMoveAction( action ); 
    }

    public void changeActiveChar(int charNum) {
        Debug.Log("change active char to :" + charNum);
        _interface.hideTeamRearrangeButton();
    }
    //========================================================================================
}

public class BattleManager {
    private BattleController _battle;
    public BattleUnit _unit { get; private set; }
    public bool _playerFirst { get; private set; }
    public bool _playerAttacked { get; private set; }
    public bool _enemyAttacked { get; private set; }

    public BattleManager(BattleController battle) { 
        _battle = battle;
    }

    public void resetBattleUnit() { _unit = new BattleUnit(this);  }
    public bool judgePlayerFirst(TeamManager player, TeamManager enemy) {
        int playerMov = player.getActiveChar().getMoveSpeed();
        Debug.Log("player move speed: "+playerMov);
        int enemyMov = enemy.getActiveChar().getMoveSpeed();
        Debug.Log("enemymov speed: "+enemyMov);

        if (playerMov != enemyMov) { _playerFirst = playerMov > enemyMov; }
        else { _playerFirst = UnityEngine.Random.value < 0.5f; }
        return _playerFirst;
    }
    public void setPlayerAttacking() { 
        _playerAttacked = true;
        _unit.setAttacker(_battle._playerManager);
        _unit.setDefenser(_battle._enemyManager);
    }
    public void setEnemyAttacking() { 
        _enemyAttacked = true; 
        _unit.setAttacker(_battle._enemyManager);
        _unit.setDefenser(_battle._playerManager);
    }

    public void runBattle() {
        _unit.runBattle();
    }
}

public class BattleUnit{
    private BattleManager _battle;
    public TeamManager _attacker { get; private set; }
    public TeamManager _defenser { get; private set; }
    public BattleUnit(BattleManager battle) { _battle = battle; }

    public void setAttacker(TeamManager attacker) { _attacker = attacker; }
    public void setDefenser(TeamManager defenser) { _defenser = defenser; }

    public void runBattle() {
        Debug.Log("Run Battle Atk:"+_attacker.getActiveChar()._atk + ", Def:"+_defenser.getActiveChar()._def);
    }
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
    public void decisionAttr(int id) {
        if (id < _toAttr.Count) {
            DiceFace face = _toAttr[id];
            _toAttr.RemoveAt(id);
            _toBase.Add(face);
        }
    }
    public void decisionBase(int id) {
        if (id < _toBase.Count) {
            DiceFace face = _toBase[id];
            _toBase.RemoveAt(id);
            _toAttr.Add(face);
        }
    }
    public List<DiceFace> getFacesAttr() { return _toAttr; }
    public List<DiceFace> getFacesBase() { return _toBase; }
}

//儲存塔管理
public class TowerManager {
    public BattleController _battle;
    public AttrTower[] _towers { get; private set; }
    public int[] _attrNums {get; private set;}
    public int[] _attrMax { get; private set;} 
    public TowerManager(BattleController battle) {
        _battle = battle;
        _towers = new AttrTower[6] { new AttrTower(0), new AttrTower(1), new AttrTower(2), 
                                     new AttrTower(3), new AttrTower(4), new AttrTower(5) };
        _attrNums = new int[6] { 0, 0, 0, 0, 0, 0 };
        _attrMax = new int[6] { 0, 0, 0, 0, 0, 0 };
    }


    public void collectAttrPoint(List<DiceFace> faces) {
        foreach (DiceFace face in faces) {
            _attrNums[face._attr] += face._num;
        }
    }
    public void countAttrMax() {
        _attrMax = new int[6] { 5, 5, 5, 5, 5, 5 };
        foreach (AttrTower t in _towers) {
            if (t._level > 0) _attrMax[t._attr] += t._capacity;
        }
    }

    public void collectBasePoint(List<DiceFace> bases) {
        // 以玩家選擇的屬性建造點數排列優先順序:  點數高且權重高的點數優先
        List<AttrBaseCounter> counters = findAttrPriority(bases);
        // 以塔的等級排列優先順序: 等級高的塔優先檢查點數是否足夠升級
        List<List<AttrTower>> towerGroups = findTowerPriority();
        // 以塔的優先順序比對屬性的優先順序決定升級哪座塔
        upgradeMostLevelValidTower(counters, towerGroups);
        // 升級/興建完後，更新屬性塔的容量
        countAttrMax();
    }
    public void upgradeMostLevelValidTower(List<AttrBaseCounter> counters, List<List<AttrTower>> towerGroups) {        
        //先按照塔等級的順序，越高等級的塔越優先
        foreach (List<AttrTower> levelTowers in towerGroups) {
            //同等級的塔，以點數加權大者優先
            foreach (AttrBaseCounter counter in counters) { 
                //如果此屬性沒有建造點數，跳過
                if (counter._base <= 0) continue;
                //尋找此等級的塔是否有點數可足夠升級的屬性
                foreach (AttrTower t in levelTowers) {  
                    // 如果此屬性和塔相同且足夠升級 => 開始升級
                    if(t._attr == counter._attr && t.isValidUpgrade(counter) ) {
                        t.upgrade(counter); 
                        return; 
                    }
                    //如果此塔沒有等級(還未興建)且屬性足夠 => 開始興建
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

    public void filterAttrPoints() {
        for (int i = 0; i < _attrNums.Length && i < _attrMax.Length; i++) {
            _attrNums[i] = Mathf.Min( _attrNums[i], _attrMax[i] );
        }
    }
}


// 隊伍管理
public class TeamManager {
    public BattleController _battle;
    public CharManager _char1 { get; private set; }
    public CharManager _char2 { get; private set; }
    public CharManager _char3 { get; private set; }
    private int _active = 0;

    public TeamManager(BattleController battle) {
        _battle = battle;
        _char1 = new CharManager();
        _char2 = new CharManager();
        _char3 = new CharManager();
    }

    public void setCharacters(int fstChar, int sndChar, int trdChar) {
        _char1.setCharacter(fstChar);
        _char2.setCharacter(sndChar);
        _char3.setCharacter(trdChar);
    }

    public CharManager getActiveChar() {
        switch (_active) {
            default:
            case 0: return _char1;
            case 1: return _char2;
            case 2: return _char3;
        }
    }

    public bool isPlayerSafe() {
        return getActiveChar()._hp > 0;
    }

    public bool isChangeActiveChar() {
        return getActiveChar()._moveAction == 2;
    }
}