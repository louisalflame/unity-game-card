using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAction {
    public abstract int getAttackAtk(TeamManager team);
}

public class Simple_Attack : AttackAction { 
    // Singleton 設計
    protected static AttackAction _attackAction;
    public static AttackAction attackAction {
        get { if (_attackAction == null) { _attackAction = new Simple_Attack(); } return _attackAction; }
    }

    public static string label = "simple_attack";
    public static string text = "Simple Attack";
    private Simple_Attack() { }

    public override int getAttackAtk(TeamManager team) { return team.ActiveChar._atk; }
}

public class Strike_Attack : AttackAction
{
    // Singleton 設計
    protected static AttackAction _attackAction;
    public static AttackAction attackAction {
        get { if (_attackAction == null) { _attackAction = new Strike_Attack(); } return _attackAction; }
    }

    public static string label = "strike_attack";
    public static string text = "Strike";
    private Strike_Attack() { }

    public override int getAttackAtk(TeamManager team) { return team.ActiveChar._atk; }
}