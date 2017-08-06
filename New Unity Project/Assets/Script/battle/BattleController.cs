using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleController {

    // 場景管理
    public SceneController _sceneCtrl { get; private set; }
    // 回合管理
    public TurnManager _turnManager { get; private set; }
    // 攻防戰鬥管理
    public BattleManager _battleManager { get; private set; }

    // 點數選擇管理(玩家)
    public AttrDecisionManager _attrDecisionManager { get; private set; }
    // 自動選擇AI(敵方)
    public EnemyAI _enemyAI { get; private set; }

    // 隊伍角色管理 我方/敵方
    // 隊伍管理含: 骰子 行動點數 骰面選擇 容量塔 角色
    public TeamManager _playerManager { get; private set; }
    public TeamManager _enemyManager { get; private set; }

    // 介面總管理
    public InterfaceController _interface { get; private set; }

    // 玩家可輸入開關
    public bool _inputValid { get; private set; }

    public BattleController(SceneController sceneCtrl) {
        _sceneCtrl = sceneCtrl;

        _playerManager = new TeamManager(this);
        _enemyManager = new TeamManager(this);
        _attrDecisionManager = new AttrDecisionManager(this);
        _enemyAI = new EnemyAI(this, _enemyManager);
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
        // 製作角色
        _playerManager.setTeamMember( GameObject.Find("GameLoop").GetComponent<TeamRecord>()._team );
        _enemyManager.setTeamMember( new EnemyTeam01() );

        // 隊伍狀態,骰子組成 由角色決定後在更新
        _interface.create();
        _interface.initial();

        _interface.animate_PrepareShiftIn();
    }
    public void newStartTurn() {
        // 建立新的一回合攻防戰鬥
        _battleManager = new BattleManager(this);

        _interface.animate_StartMovTurn();
        // 重製行動攻防按鈕
        _interface.resetActionButtons(_playerManager.ActiveChar);
    }
    public void endStartTurn() {
        _inputValid = true;
    }
    public void newDecisionTurn() {
        // 顯示骰面 玩家可選擇行動點數或建築點數
        _attrDecisionManager.setDicesResult();

        // 顯示骰面選擇欄位
        _interface.animate_CollectFaceDecision();

        // 回收已使用骰子
        _playerManager.recycleDices();
    }
    public void endDecisionTurm() {
        // 收集已選擇之行動和建築點數
        _playerManager._towerManager.collectBasePoint(_attrDecisionManager.getFacesBase());
        _playerManager._towerManager.collectAttrPoint(_attrDecisionManager.getFacesAttr());

        // 更新容量塔和儲存點數
        _interface.setTowerStatus(_playerManager._towerManager._towers);
        _interface.setAttrNums(_playerManager._towerManager._attrNums);
        // 隱藏移動階段行動選擇按鈕
        _interface.hideFaceDecision();
        _interface.hideMoveActionButton();
        _interface.hideNextButton();

        // [AI] 敵方隊伍擲骰獲得隨機點數
        List<DiceFace> diceFaces = _enemyAI.getRandomDiceFaces();
        _enemyAI.selectRandomPoints(diceFaces);
        _enemyManager.recycleDices();
        // 更新顯示敵方容量塔和儲存點數
        _interface.setTowerStatusEnemy(_enemyManager._towerManager._towers);
        _interface.setAttrNumsEnemy(_enemyManager._towerManager._attrNums);
        // [AI] 敵方隊伍選擇移動階段動作
        _enemyAI.selectMoveAction();

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
        } else { nextTurn();}
    }
    public void endMoveCountTurn() { }

    public void newPlayerAttackTurn() {
        // 顯示為玩家攻擊階段
        _interface.animate_StartPlayerAtkTurn();

        // 以先攻者開始設定攻防參數
        _battleManager.resetBattleUnit();
        _battleManager.setPlayerAttacking();
        // 顯示攻擊選擇
        _interface.showAttackActionButton();
    }
    public void endPlayerAttackTurn() {
        _interface.hideNextButton();
        _interface.hideAttackActionButton();
    }
    public void newEnemyDefenseTurn() {
        // TODO:[AI] 敵方隊伍選擇防禦動作
        _enemyAI.selectDefenseAction();
        nextTurn();
    }
    public void endEnemyDefenseTurn() { }
    public void newEnemyAttackTurn() {
        // 顯示為玩家防禦階段
        _interface.animate_StartPlayerDefTurn();

        //以先攻者開始設定攻防參數
        _battleManager.resetBattleUnit();
        _battleManager.setEnemyAttacking();

        // TODO:[AI] 敵方隊伍選擇攻擊動作
        _enemyAI.selectAttackAction();
        nextTurn();
    }
    public void endEnemyAttackTurn() {  }
    public void newPlayerDefenseTurn() {
        _interface.showDefenseActionButton();
    }
    public void endPlayerDefenseTurn() {
        _interface.hideNextButton();
        _interface.hideDefenseActionButton();
    }
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
        _playerManager._towerManager.filterAttrPoints();
        _interface.setAttrNums(_playerManager._towerManager._attrNums);
        _enemyManager._towerManager.filterAttrPoints();
        _interface.setAttrNumsEnemy(_enemyManager._towerManager._attrNums);
    }
    public void newRearrangeTurn() {
        // 戰鬥角色死亡 我方隊伍選擇換人
        if (!_playerManager.isPlayerSafe()) {
            _interface.showTeamRearrangeButton();
        } else {
            nextTurn();
        }

    }
    public void endRearrangeTurn() { 
        // [AI] 敵方隊伍選擇換人
        if (!_enemyManager.isPlayerSafe()) {
            Debug.Log("enemy Change");
            int select = _enemyAI.changeActiveChar();
            _interface.changeEnemyActiveCharTo(select);
            _interface.placeEnemyTeamStatus();
        }
    }

    public void newVictoryTurn() {
        _sceneCtrl.setScene(new MenuScene(_sceneCtrl));
    }
    public void newLoseTurn() {
        _sceneCtrl.setScene(new MenuScene(_sceneCtrl));
    }

    // player指令動作 ========================================================================
    public bool isInputValid() { return _inputValid; }
    public void CountResultAndNextTurn(){
        _turnManager.nextTurn();
    }
    public void throwDices() {
        _inputValid = false;
        _interface.hideThrowButton();
        //抓出box前n個dices
        _playerManager.startDiceUsing();
        _enemyManager.startDiceUsing();
        //製作dice 3D模型
        _interface.showDicePlay();
        //下一階段 等待骰子 隨機擲出 記錄骰值 
        _interface.startWaitDicesAnimate(); 
    }
    public void checkDiceBox(int type) {
        _interface._animator.checkDiceBox(type);
    }
    public void decisionAttr(int id) {
        _attrDecisionManager.decisionAttr(id);
        _interface.changeAttrDecision();
    }
    public void decisionBase(int id) {
        _attrDecisionManager.decisionBase(id);
        _interface.changeAttrDecision();
    }

    public void setMoveAction(MoveAction action) {
        _playerManager.setMoveAction(action);
        _interface.showNextButton();
    }
    public void setAttackAction(AttackAction action) {
        _playerManager.setAttackAction(action);
        _interface.showNextButton();
    }
    public void setDefenseAction(DefenseAction action) {
        _playerManager.setDefenseAction(action);
        _interface.showNextButton();
    }

    public void changeActiveChar(int charNum) {
        //點選欲交換的角色
        Debug.Log("change active char to :" + charNum);
        _playerManager.changeActiveCharTo(charNum);
        _interface.changePlayerActiveCharTo(charNum);
        _interface.hideTeamRearrangeButton();

        //更換角色 重製行動攻防按鈕
        _interface.resetActionButtons(_playerManager.ActiveChar);

        nextTurn();
    }
    //========================================================================================
    // 按鈕輸入處理
    public void inputProcess(string input) {

        if (input == NameCoder.NextButton[0]) {
            CountResultAndNextTurn();
        } 
        else if (input == NameCoder.ThrowButton[0]) {
            throwDices();
        }
        else if (StringCoder.isBelongDiceBox(input)){
            checkDiceBox(StringCoder.getDiceBoxNum(input));
        }
        else if (StringCoder.isBelongMoveAction(input)) {
            setMoveAction( MoveAction.dictionary[input] );
        }
        else if (StringCoder.isBelongAttackAction(input)) {
            setAttackAction(AttackAction.dictionary[input]);
        }
        else if (StringCoder.isBelongDefenseAction(input)) {
            setDefenseAction(DefenseAction.dictionary[input]);
        }
        else if (StringCoder.isBelongAttrDecision(input)) {
            decisionAttr(StringCoder.getDecisionNum(input));
        }
        else if (StringCoder.isBelongBaseDecision(input)) {
            decisionBase(StringCoder.getDecisionNum(input));
        }
        else if (StringCoder.isBelongChangeChar(input)) {
            changeActiveChar(StringCoder.getChangeCharNum(input));
        }
    }
}

public class BattleManager {
    public BattleController _battle { get; private set; }
    public BattleUnit _unit { get; private set; }
    public bool _playerFirst { get; private set; }
    public bool _playerAttacked { get; private set; }
    public bool _enemyAttacked { get; private set; }

    public BattleManager(BattleController battle) { 
        _battle = battle;
    }

    public void resetBattleUnit() { 
        _unit = new BattleUnit(this);
    }
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
    public BattleManager _battleManager { get; private set; }
    public TeamManager _attacker { get; private set; }
    public TeamManager _defenser { get; private set; }
    public BattleUnit(BattleManager battle) { _battleManager = battle; }

    public void setAttacker(TeamManager attacker) { _attacker = attacker; }
    public void setDefenser(TeamManager defenser) { _defenser = defenser; }

    public void runBattle() {
        int atk = _attacker.getAttack();
        int def = _defenser.getDefense();
        Debug.Log("Run Battle Atk:"+ atk + ", Def:"+ def);

        int damage = atk - def;
        getDamage(damage);

        _battleManager._battle._interface._teamPlayerStatus.updateCharStatusInfo();
        _battleManager._battle._interface._teamEnemyStatus.updateCharStatusInfo();
    }

    public void getDamage(int damage) {
        if (damage > 0) {
            _defenser.getDamage(damage);
        }
    }
}


