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
    public override void newTurn() { _battle.newPrepareTurn(); }
    public override TurnState getNextTurn() { return new StartTurn(_battle, _turnManager); }
}
// 起始階段，開始擲骰
public class StartTurn : TurnState {
    public StartTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init start turn"); }
    public override void newTurn() { _battle.newStartTurn(); }
    public override void endTurn() { _battle.endStartTurn(); }
    public override TurnState getNextTurn() { return new DecisionTurn(_battle, _turnManager); }
}
// 決定階段
public class DecisionTurn : TurnState {
    public DecisionTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init decide turn"); }
    public override void newTurn() { _battle.newDecisionTurn(); }
    public override void endTurn() { _battle.endDecisionTurm(); }
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
