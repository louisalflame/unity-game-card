using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 隊伍管理
public class TeamManager {
    public BattleController _battle;
    public CharManager[] _characters { get; private set; }
    public int _teamSize { get; private set; }

    // 角色戰鬥中的行動，不同角色有不同種類的行動選擇
    public MoveAction _moveAction { get; private set; }
    public int _attackAction { get; private set; }
    public int _defenseAction { get; private set; }

    // 取得正在戰鬥腳色
    private int _active = 0;
    public CharManager ActiveChar { get { return _characters[_active]; } private set { ActiveChar = value; } }

    public TeamManager(BattleController battle) {
        _battle = battle;
    }
    
    // 初始設定隊伍腳色
    public void setCharacters(int[] characters) {
        _teamSize = characters.Length;
        _characters = new CharManager[_teamSize];
        for(int i = 0; i < _teamSize; i++) {
            _characters[i] = new CharManager();
            _characters[i].setCharacter(characters[i]);
        }
    }
    
    public bool isPlayerSafe() { return ActiveChar._hp > 0; }

    public bool isChangeActiveChar() {
        return _moveAction == Move_Exchange.moveAction;
    }

    public bool changeActiveCharTo(int selected) {
        if (_characters[selected]._hp > 0) {
            _active = selected;
            return true;
        }
        else return false;
    }

    public void setMoveAction(MoveAction action)    { _moveAction = action; }
    public void setAttackAction(int action)         { _attackAction = action; }
    public void setDefenseAction(int action)        { _defenseAction = action; }
    
    public int getMoveSpeed() {
        return _moveAction.getMoveSpeed(this);
    }


}