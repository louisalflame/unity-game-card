using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction {
    public abstract int getMoveSpeed(TeamManager team);
}

public class Move_GetFirst : MoveAction { 
    // Singleton 設計
    protected static MoveAction _moveAction;
    public static MoveAction moveAction {
        get { if (_moveAction == null) { _moveAction = new Move_GetFirst(); } return _moveAction; }
    }

    public static string label = "Move_GetFirst";
    public static string text = "Get First";
    private Move_GetFirst() { }

    public override int getMoveSpeed(TeamManager team) {
        return team.ActiveChar._mov;
    }
}

public class Move_Exchange : MoveAction {
    // Singleton 設計
    protected static MoveAction _moveAction;
    public static MoveAction moveAction {
        get { if (_moveAction == null) { _moveAction = new Move_Exchange(); } return _moveAction; }
    }

    public static string label = "Move_Exchange";
    public static string text = "Exchange";
    private Move_Exchange() { }

    public override int getMoveSpeed(TeamManager team) {
        return 0;
    }
}

public class Move_Standby : MoveAction{
    // Singleton 設計
    protected static MoveAction _moveAction;
    public static MoveAction moveAction {
        get { if (_moveAction == null) { _moveAction = new Move_Standby(); } return _moveAction; }
    }

    public static string label = "Move_Standby";
    public static string text = "Standby";
    private Move_Standby() { }

    public override int getMoveSpeed(TeamManager team) {
        return 1;
    }
}