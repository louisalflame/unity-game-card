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
    public int _active { get; private set; }
    public CharManager ActiveChar { get { return _characters[_active]; } private set { ActiveChar = value; } }

    // 角色戰鬥中的行動，不同角色有不同種類的行動選擇
    public MoveAction _moveAction { get; private set; }
    public AttackAction _attackAction { get; private set; }
    public DefenseAction _defenseAction { get; private set; }

    // 場地骰子/隊伍骰子/角色骰子
    public DiceManager _groundDices { get; private set; }
    public DiceManager _teamDices { get; private set; }
    public DiceManager _personDices { get; private set; }

    // 行動點數和儲存塔
    public TowerManager _towerManager { get; private set; }

    public TeamManager(BattleController battle) {
        _battle = battle;
        
        // 管理點數和儲存塔
        _towerManager = new TowerManager(_battle);
    }
    

    public void setTeamMember(TeamMember members) {
        setCharacters(members._chars);
        setGroundDices(members._groundDices);
        setTeamDices(members._teamDices);
    }
    // 初始設定隊伍腳色
    public void setCharacters(string[] characters) {
        _teamSize = characters.Length;
        _characters = new CharManager[_teamSize];
        for(int i = 0; i < _teamSize; i++) {
            _characters[i] = new CharManager();
            _characters[i].setCharacter(characters[i]);
        }
        resetPersonDice();
    }
    // 初始設定場地骰/隊伍骰/角色骰
    public void setGroundDices(string[] dices) {
        _groundDices = new DiceManager(_battle, 5);
        _groundDices.importDices(dices);
    }
    public void setTeamDices(string[] dices) {
        _teamDices = new DiceManager(_battle, 2);
        _teamDices.importDices(dices);
    }
    // 主戰角色更換時，重置角色骰
    public void resetPersonDice() {
        _personDices = new DiceManager(_battle, 1);
        _personDices.importDices(ActiveChar._personDice);
    }
    public int findCharIndex(CharManager charM) {
        for (int i = 0; i < _characters.Length; i++) {
            if (_characters[i] == charM) return i;
        }
        return -1;
    }

    // 開始擲骰 和 結束回收骰
    public void startDiceUsing() {
        _groundDices.addDicesUsing();
        _teamDices.addDicesUsing();
        _personDices.addDicesUsing();
    }
    public void recycleDices() {
        _groundDices.recycleDices();
        _teamDices.recycleDices();
        _personDices.recycleDices();
    }

    public bool isCharActive(CharManager charM) { return ActiveChar == charM; }
    // 檢察隊伍生命狀態
    public bool isPlayerSafe() { return isCharSafe(ActiveChar); }
    public bool isCharSafe(CharManager charM) { return charM._hp > 0; }
    public bool isTeamSafe() {
        foreach (CharManager charM in _characters) {
            if (isCharSafe(charM)) { return true; }
        }
        return false;
    }

    // 更換角色
    public bool isChangeActiveChar() {
        return _moveAction == Move_Exchange.action;
    }
    public void changeActiveCharTo(int selected) {
        _active = selected;
    }
    
    // 決定行動
    public void setMoveAction(MoveAction action)        { _moveAction = action; }
    public void setAttackAction(AttackAction action)    { _attackAction = action; }
    public void setDefenseAction(DefenseAction action)  { _defenseAction = action; }
    
    public int getMoveSpeed() {
        return _moveAction.getMoveSpeed(this);
    }
    public int getAttack()  { return _attackAction.getAttack(this); }
    public int getDefense() { return _defenseAction.getDefense(this); }

    // 造成傷害
    public void getDamage(int damage) {
        ActiveChar.getDamage( damage );
    }
}