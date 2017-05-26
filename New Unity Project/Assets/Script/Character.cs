using UnityEngine;
using System.Collections;

// 角色管理
public class CharManager
{
    // 角色數值基本值 不做更動
    public Character _character { get; private set; }
    // 角色戰鬥中的數值，隨時更動
    public int _hp { get; private set; }
    public int _atk { get; private set; }
    public int _def { get; private set; }
    public int _mov { get; private set; }

    // 角色戰鬥中的行動，不同角色有不同種類的行動選擇
    public int _moveAction { get; private set; }
    public int _attackAction { get; private set; }
    public int _defenseAction { get; private set; }

    public CharManager() { }

    public void setCharacter(int id) {
        _character = CharacterFactory.createChracter(id);
        _hp = _character._hp;
        _atk = _character._atk;
        _def = _character._def;
        _mov = _character._mov;
    }
    public void setMoveAction(int action) { _moveAction = action; }
    public int getMoveSpeed() {
        switch (_moveAction) {
            // get_first
            case 1: return _mov;
            // exchange
            case 2: return 0;
            // standby
            default:
            case 3: return 1;
        }
    }
    public void setAttackAction(int action) { _attackAction = action; }
    public void setDefenseAction(int action) { _defenseAction = action; }
}

// 角色生成
public class CharacterFactory {
    public static Character createChracter(int id)  {
        switch(id){
            case 0: return new Enemy_0();
            case 1: return new Enemy_1();
            case 2: return new Enemy_2();
            case 10: return new Player_0();
            case 11: return new Player_1();
            case 12: return new Player_2();
            default: return new NullCharacter();
        }
    }
}

public class Character {
    public int _hp { get; protected set; }
    public int _atk { get; protected set; }
    public int _def { get; protected set; }
    public int _mov { get; protected set; }
    public Character() { }
    
}

public class NullCharacter : Character {
    public NullCharacter() {
        _hp = 0;
        _atk = 0;
        _def = 0;
        _mov = 0;
    }
}

public class Enemy_0 : Character {
    public Enemy_0() {
        _hp = 20;
        _atk = 7;
        _def = 2;
        _mov = 3;
    }
}
public class Enemy_1 : Character {
    public Enemy_1() {
        _hp = 24;
        _atk = 11;
        _def = 5;
        _mov = 6;
    }
}
public class Enemy_2 : Character {
    public Enemy_2() {
        _hp = 25;
        _atk = 17;
        _def = 6;
        _mov = 7;
    }
}

public class Player_0 : Character {
    public Player_0() {
        _hp = 30;
        _atk = 12;
        _def = 5;
        _mov = 6;
    }
}
public class Player_1 : Character {
    public Player_1() {
        _hp = 33;
        _atk = 9;
        _def = 8;
        _mov = 2;
    }
}
public class Player_2 : Character {
    public Player_2() {
        _hp = 40;
        _atk = 14;
        _def = 3;
        _mov = 7;
    }
}