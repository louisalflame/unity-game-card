using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction {
    public abstract string[] getLabel_ID();
    public static Dictionary<string, AttackAction> dictionary = new Dictionary<string, AttackAction>() {
        { NameCoder.getLabel(NameCoder.Simple_Attack), Simple_Attack.action },
        { NameCoder.getLabel(NameCoder.Strike_Attack), Strike_Attack.action }
    };

    public abstract int getAttack(TeamManager team);
}

public class Simple_Attack : AttackAction { 
    // Singleton 設計
    protected static AttackAction _action;
    public static AttackAction action {
        get { if (_action == null) { _action = new Simple_Attack(); } return _action; }
    }
    private Simple_Attack() { }

    public override string[] getLabel_ID() { return NameCoder.Simple_Attack; }

    public override int getAttack(TeamManager team) { 
        return team.ActiveChar._atk;
    }
}

public class Strike_Attack : AttackAction {
    // Singleton 設計
    protected static AttackAction _action;
    public static AttackAction action {
        get { if (_action == null) { _action = new Strike_Attack(); } return _action; }
    }
    private Strike_Attack() { }

    public override string[] getLabel_ID() { return NameCoder.Strike_Attack; }

    public override int getAttack(TeamManager team) { 
        return team.ActiveChar._atk * 2; 
    }
}
