using UnityEngine;
using System.Collections;

public class TurnManager {
    private TurnState _turn;
    private BattleController _battle;
    public TurnManager() {}

    // 初始設定,由第一個state開始
    public void initSetting(BattleController battleController, TurnState initTurn) {
        _battle = battleController;
        _turn = initTurn;
        _turn.newTurn();
    }
    // 結束此階段,進入下一階段
    public void nextTurn() {
        _turn.endTurn();
        _turn = _turn.getNextTurn();
        _turn.newTurn();
    }

    public void update() { _turn.update(); }
}

public abstract class TurnState{
    protected BattleController _battle;
    protected TurnManager _turnManager;
    public TurnState(BattleController battleController, TurnManager turnManager) {
        _battle = battleController; _turnManager = turnManager;
    }
    public virtual void newTurn() { }
    public virtual void endTurn() { }
    public virtual void update() { }
    public abstract TurnState getNextTurn();
}

//準備階段 產生所有需要物件
public class PrepareTurn : TurnState {
    public PrepareTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init prepare turn"); }
    public override void newTurn() {
        // 製作所有dicebox 的骰子
        _battle.createDices();
        _battle.showBasicInterface();
    }
    public override TurnState getNextTurn() { return new StartTurn(_battle, _turnManager); }
}
// 起始階段，開始擲骰
public class StartTurn : TurnState {
    public StartTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init start turn"); }
    public override void newTurn() {
        _battle.hideNextButton();
        _battle.showThrowButton();
        _battle.showDiceBox();
    }
    public override TurnState getNextTurn() { return new WaitDiceStopTurn(_battle, _turnManager); }
}
// 等待骰子階段
public class WaitDiceStopTurn : TurnState{
    public WaitDiceStopTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init waitDiceStop turn"); }
    public override void update() { if (_battle.isAllDicesStop()) { _turnManager.nextTurn(); } }
    public override TurnState getNextTurn() { return new WaitDiceCollectTurn(_battle, _turnManager); }
}
// 骰子回收階段
public class WaitDiceCollectTurn : TurnState {
    public WaitDiceCollectTurn(BattleController battle,TurnManager turn) : base(battle, turn) { Debug.Log("init WaitDiceCollect turn"); }
    public override void newTurn() { _battle.startCollectDices(); }
    public override void update() {
        if (!_battle.isAllDicesCollectReady()) { _battle.collectDices(); }
        else { _battle.recycleDices(); _turnManager.nextTurn(); }
    }
    public override void endTurn() { _battle.showNextButton(); }
    public override TurnState getNextTurn() { return new DecideTurn(_battle, _turnManager); }
}
// 決定階段
public class DecideTurn : TurnState {
    public DecideTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init decide turn"); }
    public override void endTurn() { _battle.removeDices(); }
    public override TurnState getNextTurn() { return new PlayerAttackTurn(_battle, _turnManager); }
}
// 玩家攻擊階段
public class PlayerAttackTurn : TurnState {
    public PlayerAttackTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init player atk turn"); }
    public override TurnState getNextTurn() { return new EnemyDefenseTurn(_battle, _turnManager); }
}
// 玩家防禦階段
public class PlayerDefenseTurn : TurnState {
    public PlayerDefenseTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init player def turn"); }
    public override TurnState getNextTurn() { return new PrepareTurn(_battle, _turnManager); }
}
// 敵方攻擊階段
public class EnemyAttackTurn : TurnState {
    public EnemyAttackTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init enemy atk turn"); }
    public override TurnState getNextTurn() { return new PlayerDefenseTurn(_battle, _turnManager); }
}
// 敵方防禦階段
public class EnemyDefenseTurn : TurnState {
    public EnemyDefenseTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init enemy def turn"); }
    public override TurnState getNextTurn() { return new EnemyAttackTurn(_battle, _turnManager); }
}
