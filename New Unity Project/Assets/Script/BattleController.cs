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
        // 製作角色
        _playerManager.setCharacters(new int[] { 10, 11, 12 });
        _enemyManager.setCharacters(new int[] { 0, 1, 2 });

        nextTurn();
    }
    public void newStartTurn() {
        // 建立新的一回合攻防戰鬥
        _battleManager = new BattleManager(this);
        // 顯示執骰按鈕
        _interface.hideNextButton();
        _interface.showThrowButton();
        // 顯示基本物件(可用骰)
        _interface.showDiceBox(_diceManager.getDicesUnused());
    }
    public void endStartTurn() {
        _inputValid = true;
    }
    public void newDecisionTurn() {
        // 顯示骰面 玩家可選擇行動點數或建築點數
        _attrDecisionManager.setDicesResult(_interface.getDicesResult(), _diceManager.getDicesUsing());
        _interface.setAttrDecision(_attrDecisionManager);
        _interface.showAttrDecision();
        _interface.removeDices3D();
        // 顯示移動階段動作選擇按鈕
        _interface.showMoveActionButton();

        // TODO 敵方隊伍AI選擇移動階段動作
        _enemyManager.setMoveAction(Move_GetFirst.moveAction);
    }
    public void endDecisionTurm() {
        // 收集已選擇之行動和建築點數
        _towerManager.collectBasePoint(_attrDecisionManager.getFacesBase());
        _towerManager.collectAttrPoint(_attrDecisionManager.getFacesAttr());
        // 回收已使用骰子
        _diceManager.recycleDices();

        // 更新容量塔和儲存點數
        _interface.setTowerStatus(_towerManager._towers);
        _interface.setAttrNums(_towerManager._attrNums);
        // 隱藏移動階段行動選擇按鈕
        _interface.removeFaceDecision();
        _interface.hideMoveActionButton();

        // TODO 移動階段技能發動
        // 依行動選擇判定先後攻
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

    public void moveAction(MoveAction action) {
        _playerManager.setMoveAction( action );
        _interface.showNextButton();
    }

    public void changeActiveChar(int charNum) {
        Debug.Log("change active char to :" + charNum);
        _playerManager.changeActiveCharTo(charNum);
        _interface.hideTeamRearrangeButton();
        nextTurn();
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
        int playerMov = player.getMoveSpeed();
        Debug.Log("player move speed: "+playerMov);
        int enemyMov = enemy.getMoveSpeed();
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
        Debug.Log("Run Battle Atk:"+_attacker.ActiveChar._atk + ", Def:"+_defenser.ActiveChar._def);
    }
}


