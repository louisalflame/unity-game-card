using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DefenseAction {
    public string label { get; protected set; }
    public string text { get; protected set; }
    public void setLabelAndText(string[] label_text) { label = label_text[0]; text = label_text[1]; }

    public abstract int getDefense(TeamManager team);
    public static Dictionary<string, DefenseAction> dictionary = new Dictionary<string, DefenseAction>() {
        { Simple_Defense.action.label, Simple_Defense.action } 
    };
}

public class Simple_Defense : DefenseAction {
    protected static DefenseAction _action;
    public static DefenseAction action {
        get { if (_action == null) { _action = new Simple_Defense(); } return _action; }
    }

    private Simple_Defense() { setLabelAndText(Name.Simple_Defense); }
    public override int getDefense(TeamManager team) { return team.ActiveChar._def; }
}