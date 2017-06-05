using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction {
    public string label { get; protected set; }
    public string text { get; protected set; }
    public void setLabelAndText(string[] label_text) { label = label_text[0]; text = label_text[1]; }

    public abstract int getAttack(TeamManager team);
    public static Dictionary<string, AttackAction> dictionary = new Dictionary<string, AttackAction>() {
        { Simple_Attack.action.label, Simple_Attack.action },
        { Strike_Attack.action.label, Strike_Attack.action }
    };
}

public class Simple_Attack : AttackAction { 
    // Singleton 設計
    protected static AttackAction _action;
    public static AttackAction action {
        get { if (_action == null) { _action = new Simple_Attack(); } return _action; }
    }

    private Simple_Attack() { setLabelAndText(Name.Simple_Attack); }

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

    private Strike_Attack() { setLabelAndText(Name.Strike_Attack); }

    public override int getAttack(TeamManager team) { 
        return team.ActiveChar._atk * 2; 
    }
}
