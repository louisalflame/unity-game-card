using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DefenseAction {
    public abstract string[] getLabel_ID();
    public static Dictionary<string, DefenseAction> dictionary = new Dictionary<string, DefenseAction>() {
        { NameCoder.getLabel(NameCoder.Simple_Defense), Simple_Defense.action } 
    };
    public abstract int getDefense(TeamManager team);
}

public class Simple_Defense : DefenseAction {
    protected static DefenseAction _action;
    public static DefenseAction action {
        get { if (_action == null) { _action = new Simple_Defense(); } return _action; }
    }
    private Simple_Defense() { }

    public override string[] getLabel_ID() { return NameCoder.Simple_Defense; }

    public override int getDefense(TeamManager team) { return team.ActiveChar._def; }
}