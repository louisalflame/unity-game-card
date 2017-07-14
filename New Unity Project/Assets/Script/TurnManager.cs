using UnityEngine;
using System.Collections;

public class TurnManager {
    private TurnState _turn;
    private BattleController _battle;

    public int _turnNum { get; private set; }

    public TurnManager() {
        _turnNum = 0;
    }

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
    public void addNewTurn() { _turnNum += 1; }
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
    public override void newTurn() { 
        _turnManager.addNewTurn(); 
        _battle.newStartTurn();
    }
    public override void endTurn() { _battle.endStartTurn(); }
    public override TurnState getNextTurn() { return new DecisionTurn(_battle, _turnManager); }
}
// 決定階段
public class DecisionTurn : TurnState {
    public DecisionTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init decide turn"); }
    public override void newTurn() { _battle.newDecisionTurn(); }
    public override void endTurn() { _battle.endDecisionTurm(); }
    public override TurnState getNextTurn() {
        if (_battle._playerManager.isPlayerSafe() && _battle._enemyManager.isPlayerSafe()) {
            return new MoveCountTurn(_battle, _turnManager);
        }
        else return new RearrangeTurn(_battle, _turnManager);
    }
}

// 移動結算階段 區分先後攻
public class MoveCountTurn : TurnState {
    public MoveCountTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init moveCount turn"); }
    public override void newTurn() { _battle.newMoveCountTurn(); }
    public override void endTurn() { _battle.endMoveCountTurn(); }
    public override TurnState getNextTurn() { 
        if (_battle._battleManager._playerFirst) return new PlayerAttackTurn(_battle, _turnManager);
        else return new EnemyAttackTurn(_battle, _turnManager);
    }
}

// 玩家攻擊階段
public class PlayerAttackTurn : TurnState {
    public PlayerAttackTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init player atk turn"); }
    public override void newTurn() { _battle.newPlayerAttackTurn(); }
    public override void endTurn() { _battle.endPlayerAttackTurn(); }
    public override TurnState getNextTurn() { return new EnemyDefenseTurn(_battle, _turnManager); }
}
// 敵方防禦階段
public class EnemyDefenseTurn : TurnState {
    public EnemyDefenseTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init enemy def turn"); }
    public override void newTurn() { _battle.newEnemyDefenseTurn(); }
    public override void endTurn() { _battle.endEnemyDefenseTurn(); }
    public override TurnState getNextTurn() { return new BattleCountTurn(_battle, _turnManager); }
}
// 敵方攻擊階段
public class EnemyAttackTurn : TurnState {
    public EnemyAttackTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init enemy atk turn"); }
    public override void newTurn() { _battle.newEnemyAttackTurn(); }
    public override void endTurn() { _battle.endEnemyAttackTurn(); }
    public override TurnState getNextTurn() { return new PlayerDefenseTurn(_battle, _turnManager); }
}
// 玩家防禦階段
public class PlayerDefenseTurn : TurnState {
    public PlayerDefenseTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init player def turn"); }
    public override void newTurn() { _battle.newPlayerDefenseTurn(); }
    public override void endTurn() { _battle.endPlayerDefenseTurn(); }
    public override TurnState getNextTurn() { return new BattleCountTurn(_battle, _turnManager); }
}

// 攻防計算階段
public class BattleCountTurn : TurnState {
    public BattleCountTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init battle count turn"); }
    public override void newTurn() { _battle.newBattleCountTurn(); }
    public override void endTurn() { _battle.endBattleCountTurn(); }
    public override TurnState getNextTurn() {
        if ( _battle._playerManager.isPlayerSafe() && _battle._enemyManager.isPlayerSafe() ) {
            if (_battle._battleManager._playerAttacked && _battle._battleManager._enemyAttacked) {
                return new AnalysisTurn(_battle, _turnManager);
            } else if (_battle._battleManager._playerAttacked && !_battle._battleManager._enemyAttacked) {
                return new EnemyAttackTurn(_battle, _turnManager);
            } else if (!_battle._battleManager._playerAttacked && _battle._battleManager._enemyAttacked) {
                return new PlayerAttackTurn(_battle, _turnManager);
            } else return this;
        } else return new AnalysisTurn(_battle, _turnManager); 
    }
}

// 結算階段
public class AnalysisTurn : TurnState {
    public AnalysisTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init analysis turn"); }
    public override void newTurn() { _battle.newAnalysisTurn(); }
    public override void endTurn() { _battle.endAnalysisTurn(); }
    public override TurnState getNextTurn() {
        if (_battle._playerManager.isPlayerSafe() && _battle._enemyManager.isPlayerSafe()) {
            return new StartTurn(_battle, _turnManager);
        } 
        // 如果有一方全員陣亡 則判斷勝負
        else if (!_battle._playerManager.isTeamSafe()) {
            return new LoseTurn(_battle, _turnManager);
        }
        else if (!_battle._enemyManager.isTeamSafe()) {
            return new VictoryTurn(_battle, _turnManager);
        }
        else return new RearrangeTurn(_battle, _turnManager); 
    }
}

// 重新選擇新戰鬥角色階段，然後直接開始新回合
public class RearrangeTurn : TurnState {
    public RearrangeTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init rearrange turn"); }
    public override void newTurn() { _battle.newRearrangeTurn(); }
    public override void endTurn() { _battle.endRearrangeTurn(); }
    public override TurnState getNextTurn() {
 	    return new StartTurn(_battle, _turnManager);
    }
}

// 勝利階段
public class VictoryTurn : TurnState {
    public VictoryTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init victory turn"); }
    public override void newTurn() { _battle.newVictoryTurn(); }
    public override TurnState getNextTurn() { return this; }
}
// 失敗階段
public class LoseTurn : TurnState {
    public LoseTurn(BattleController battle, TurnManager turn) : base(battle, turn) { Debug.Log("init lose turn"); }
    public override void newTurn() { _battle.newLoseTurn(); }
    public override TurnState getNextTurn() { return this; }
}
