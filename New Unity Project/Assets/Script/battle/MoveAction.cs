using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction {
    public abstract string[] getLabel_ID();
    public static Dictionary<string, MoveAction> dictionary = new Dictionary<string, MoveAction>() {
        { NameCoder.getLabel( NameCoder.Move_GetFirst ), Move_GetFirst.action },
        { NameCoder.getLabel( NameCoder.Move_Exchange ), Move_Exchange.action },
        { NameCoder.getLabel( NameCoder.Move_Standby ), Move_Standby.action}
    };

    public abstract int getMoveSpeed(TeamManager team);
}

public class Move_GetFirst : MoveAction { 
    // Singleton 設計
    protected static MoveAction _action;
    public static MoveAction action {
        get { if (_action == null) { _action = new Move_GetFirst(); } return _action; }
    }
    private Move_GetFirst() { }

    public override string[] getLabel_ID() { return NameCoder.Move_GetFirst; }

    public override int getMoveSpeed(TeamManager team) {
        return team.ActiveChar._mov;
    }
}

public class Move_Exchange : MoveAction {
    // Singleton 設計
    protected static MoveAction _action;
    public static MoveAction action {
        get { if (_action == null) { _action = new Move_Exchange(); } return _action; }
    }
    private Move_Exchange() { }

    public override string[] getLabel_ID() { return NameCoder.Move_Exchange; }

    public override int getMoveSpeed(TeamManager team) {
        return 0;
    }
}

public class Move_Standby : MoveAction{
    // Singleton 設計
    protected static MoveAction _action;
    public static MoveAction action {
        get { if (_action == null) { _action = new Move_Standby(); } return _action; }
    }
    private Move_Standby() { }

    public override string[] getLabel_ID() { return NameCoder.Move_Standby; }

    public override int getMoveSpeed(TeamManager team) {
        return 1;
    }
}