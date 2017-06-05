using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 隊伍管理
public class TeamManager {
    public BattleController _battle;
    // 角色隊伍 含主要戰鬥角色
    public CharManager[] _characters { get; private set; }
    public int _teamSize { get; private set; }
    // 取得正在戰鬥腳色
    private int _active = 0;
    public CharManager ActiveChar { get { return _characters[_active]; } private set { ActiveChar = value; } }

    // 角色戰鬥中的行動，不同角色有不同種類的行動選擇
    public MoveAction _moveAction { get; private set; }
    public AttackAction _attackAction { get; private set; }
    public DefenseAction _defenseAction { get; private set; }

    // 場地骰子/隊伍骰子/角色骰子
    public DiceManager _groundDices { get; private set; }

    // 行動點數和儲存塔
    public TowerManager _towerManager { get; private set; }

    public TeamManager(BattleController battle) {
        _battle = battle;
        _groundDices = new DiceManager(battle);
        _towerManager = new TowerManager(battle);

        // 製作所有dicebox 的骰子
        for (int i = 0; i < 10; i++) { _groundDices.addDicesUnused(new NorDice()); }
        for (int i = 0; i < 10; i++) { _groundDices.addDicesUnused(new AtkDice()); }
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
        return _moveAction == Move_Exchange.action;
    }

    public bool changeActiveCharTo(int selected) {
        if (_characters[selected]._hp > 0) {
            _active = selected;
            return true;
        }
        else return false;
    }
     
    public void setMoveAction(MoveAction action)     { _moveAction = action; }
    public void setAttackAction(AttackAction action) { _attackAction = action; }
    public void setDefenseAction(DefenseAction action)         { _defenseAction = action; }
    
    public int getMoveSpeed() {
        return _moveAction.getMoveSpeed(this);
    }
    public int getAttack() { return _attackAction.getAttack(this); }
    public int getDefense() { return _defenseAction.getDefense(this); }
}