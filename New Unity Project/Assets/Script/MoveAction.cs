using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction {
    public string label { get; protected set; }
    public string text { get; protected set; }
    public void setLabelAndText(string[] label_text) { label = label_text[0]; text = label_text[1]; }

    public abstract int getMoveSpeed(TeamManager team);
    public static Dictionary<string, MoveAction> dictionary = new Dictionary<string, MoveAction>() {
        { Move_GetFirst.action.label, Move_GetFirst.action },
        { Move_Exchange.action.label, Move_Exchange.action },
        { Move_Standby.action.label, Move_Standby.action}
    };
}

public class Move_GetFirst : MoveAction { 
    // Singleton 設計
    protected static MoveAction _action;
    public static MoveAction action {
        get { if (_action == null) { _action = new Move_GetFirst(); } return _action; }
    }

    private Move_GetFirst() { setLabelAndText(Name.Move_GetFirst); }

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

    private Move_Exchange() { setLabelAndText(Name.Move_Exchange); }

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

    private Move_Standby() { setLabelAndText(Name.Move_Standby); }

    public override int getMoveSpeed(TeamManager team) {
        return 1;
    }
}