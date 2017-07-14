using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 角色管理
public class CharManager {
    // 角色數值基本值 不做更動
    public Character _character { get; private set; }
    // 角色戰鬥中的數值，隨時更動
    public string _id { get; private set; }
    public string _name { get; private set; }
    public int _hp { get; private set; }
    public int _atk { get; private set; }
    public int _def { get; private set; }
    public int _mov { get; private set; }
    public MoveAction[] _movActions { get; private set; }
    public AttackAction[] _atkActions { get; private set; }
    public DefenseAction[] _defActions { get; private set; }
    public string[] _personDice { get; private set; }
    
    public CharManager() { }

    public void setCharacter(string id) {
        _character = CharacterFactory.dictionary[id];
        _id = _character._id;
        _name = _character._name;
        _hp = _character._hp;
        _atk = _character._atk;
        _def = _character._def;
        _mov = _character._mov;
        _movActions = _character._movActions;
        _atkActions = _character._atkActions;
        _defActions = _character._defActions;
        _personDice = _character._personDice;
    }

    public void getDamage(int damage) {
        _hp = Mathf.Max( 0, _hp - damage );
    }
}

// 角色生成
public class CharacterFactory {
    public static Dictionary<string, Character> dictionary = new Dictionary<string, Character> {
        { NullCharacter.Char._id, NullCharacter.Char },
        { Player_0.Char._id, Player_0.Char },
        { Player_1.Char._id, Player_1.Char },
        { Player_2.Char._id, Player_2.Char },
        { Enemy_0.Char._id, Enemy_0.Char },
        { Enemy_1.Char._id, Enemy_1.Char },
        { Enemy_2.Char._id, Enemy_2.Char },
    };
}

public abstract class Character {
    public string _id { get; protected set; }
    public string _name { get; protected set; }
    public int _hp  { get; protected set; }
    public int _atk { get; protected set; }
    public int _def { get; protected set; }
    public int _mov { get; protected set; }
    public MoveAction[] _movActions { get; protected set; }
    public AttackAction[] _atkActions { get; protected set; }
    public DefenseAction[] _defActions { get; protected set; }
    public string[] _personDice { get; protected set; }
}

public class NullCharacter : Character {
    // Singleton 設計
    protected static NullCharacter _char;
    public static NullCharacter Char {
        get { if (_char == null) { _char = new NullCharacter(); } return _char; }
    }
    public NullCharacter() {
        _id = "null"; 
        _name = "空角色";
        _hp = 0;
        _atk = 0;
        _def = 0;
        _mov = 0;
        _movActions = new MoveAction[] { };
        _atkActions = new AttackAction[] { };
        _defActions = new DefenseAction[] { };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}

public class Enemy_0 : Character {
    // Singleton 設計
    protected static Enemy_0 _char;
    public static Enemy_0 Char {
        get { if (_char == null) { _char = new Enemy_0(); } return _char; }
    }
    public Enemy_0() {
        _id = "Enemy_0";
        _name = "敵人0";
        _hp = 20;
        _atk = 7;
        _def = 2;
        _mov = 3;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}
public class Enemy_1 : Character {
    // Singleton 設計
    protected static Enemy_1 _char;
    public static Enemy_1 Char {
        get { if (_char == null) { _char = new Enemy_1(); } return _char; }
    }
    public Enemy_1() {
        _id = "Enemy_1";
        _name = "敵人1";
        _hp = 24;
        _atk = 11;
        _def = 5;
        _mov = 6;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}
public class Enemy_2 : Character {
    // Singleton 設計
    protected static Enemy_2 _char;
    public static Enemy_2 Char {
        get { if (_char == null) { _char = new Enemy_2(); } return _char; }
    }
    public Enemy_2() {
        _id = "Enemy_2";
        _name = "敵人2";
        _hp = 25;
        _atk = 17;
        _def = 6;
        _mov = 7;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}

public class Player_0 : Character {
    // Singleton 設計
    protected static Player_0 _char;
    public static Player_0 Char {
        get { if (_char == null) { _char = new Player_0(); } return _char; }
    }
    public Player_0() {
        _id = "Player_0";
        _name = "我方0";
        _hp = 30;
        _atk = 2;
        _def = 5;
        _mov = 6;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}
public class Player_1 : Character {
    // Singleton 設計
    protected static Player_1 _char;
    public static Player_1 Char {
        get { if (_char == null) { _char = new Player_1(); } return _char; }
    }
    public Player_1() {
        _id = "Player_1";
        _name = "我方1";
        _hp = 33;
        _atk = 9;
        _def = 8;
        _mov = 2;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}
public class Player_2 : Character {
    // Singleton 設計
    protected static Player_2 _char;
    public static Player_2 Char {
        get { if (_char == null) { _char = new Player_2(); } return _char; }
    }
    public Player_2() {
        _id = "Player_2";
        _name = "我方2";
        _hp = 40;
        _atk = 14;
        _def = 3;
        _mov = 7;
        _movActions = new MoveAction[3] { Move_GetFirst.action, Move_Exchange.action, Move_Standby.action };
        _atkActions = new AttackAction[2] { Simple_Attack.action, Strike_Attack.action };
        _defActions = new DefenseAction[1] { Simple_Defense.action };
        _personDice = new string[] { NorDice.dice._diceType };
    }
}