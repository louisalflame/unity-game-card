using UnityEngine;
using System.Collections;

public class TurnManager {
    private TurnState turn;
    public BattleController battle;
    public TurnManager(BattleController battleController, TurnState initTurn) {
        battle = battleController;
        turn = initTurn;
    }

    public void nextTurn() {
        turn.endTurn();
        turn = turn.getNextTurn();
        turn.newTurn();
    }
}

public abstract class TurnState{
    public BattleController battle;
    public TurnState(BattleController battleController) { battle = battleController;  }
    public virtual void newTurn() { }
    public virtual void endTurn() { }
    public abstract TurnState getNextTurn();
}
public class PrepareTurn : TurnState {
    public PrepareTurn(BattleController battleController) : base(battleController) { Debug.Log("init prepare turn"); }

    public override TurnState getNextTurn() { return new MoveTurn(battle); }
}
public class MoveTurn : TurnState {
    public MoveTurn(BattleController battleController) : base(battleController) { Debug.Log("init move turn"); }

    public override void newTurn() { battle.createDice(); }
    public override void endTurn() { battle.checkDices(); battle.collectDices(); }

    public override TurnState getNextTurn() { return new PlayerAttackTurn(battle); }
}
public class PlayerAttackTurn : TurnState {
    public PlayerAttackTurn(BattleController battleController) : base(battleController) { Debug.Log("init player atk turn"); }

    public override void endTurn() { battle.removeDices(); }

    public override TurnState getNextTurn() { return new EnemyDefenseTurn(battle); }
}
public class PlayerDefenseTurn : TurnState {
    public PlayerDefenseTurn(BattleController battleController) : base(battleController) { Debug.Log("init player def turn"); }

    public override TurnState getNextTurn() { return new PrepareTurn(battle); }
}
public class EnemyAttackTurn : TurnState {
    public EnemyAttackTurn(BattleController battleController) : base(battleController) { Debug.Log("init enemy atk turn"); }

    public override TurnState getNextTurn() { return new PlayerDefenseTurn(battle); }
}
public class EnemyDefenseTurn : TurnState {
    public EnemyDefenseTurn(BattleController battleController) : base(battleController) { Debug.Log("init enemy def turn"); }

    public override TurnState getNextTurn() { return new EnemyAttackTurn(battle); }
}
