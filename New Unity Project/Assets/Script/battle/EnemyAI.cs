using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI {
    public BattleController _battle;
    public TeamManager _team { get; private set; }
    private List<DiceFace> _toAttr;
    private List<DiceFace> _toBase;
     
    public EnemyAI( BattleController battle, TeamManager team){
        _battle = battle;
        _team = team;

        _toAttr = new List<DiceFace>();
        _toBase = new List<DiceFace>();
    }

    // 隨機擲骰獲得骰面
    public List<DiceFace> getRandomDiceFaces() {
        List<Dice> dices = new List<Dice>();
        foreach (Dice d in _team._groundDices._dicesUsing) { dices.Add(d); }
        foreach (Dice d in _team._teamDices._dicesUsing)   { dices.Add(d); }
        foreach (Dice d in _team._personDices._dicesUsing) { dices.Add(d); }

        List<DiceFace> diceFaces = new List<DiceFace>();
        foreach (Dice d in dices) {
            int rand = Random.Range(1, 7); 
            diceFaces.Add( d.getFace(rand) );
        }
        return diceFaces;
    }
    // 從骰面隨機選擇點數
    public void selectRandomPoints(List<DiceFace> diceFaces) {
        _toAttr.Clear();
        _toBase.Clear();

        // 隨機選一面當建築點數
        int rand = Random.Range(0, diceFaces.Count);
        for (int i = 0; i < diceFaces.Count; i++) {
            if (i == rand) { _toBase.Add(diceFaces[i]); }
            else { _toAttr.Add(diceFaces[i]); }
        }
    }
    public AttrTower getBuildTower() {
        return _team._towerManager.collectBasePoint(_toBase);
    }
    public int[] getAddNums() {
        return _team._towerManager.collectAttrPoint(_toAttr);
    }

    // 選擇移階行動
    public void selectMoveAction() {
        Debug.Log("AI set mov first");
        _team.setMoveAction(Move_GetFirst.action);
    }
    // 選擇攻擊行動
    public void selectAttackAction() {
        Debug.Log("AI set simp atk");
        _team.setAttackAction(Simple_Attack.action);
    }
    // 選擇防禦行動
    public void selectDefenseAction() {
        Debug.Log("AI set simp def");
        _team.setDefenseAction(Simple_Defense.action);
    }


    // 更換角色
    public int changeActiveChar() {
        //找HP最大的
        int select = _team._active;
        for (int i = 0; i < _team._teamSize; i++) {
            if (_team._characters[i]._hp > _team._characters[select]._hp) {
                select = i; 
            }
        }
        _team.changeActiveCharTo(select);

        return select;
    }
}

public class EnemyTeam01 : TeamMember {
    public EnemyTeam01() {
        _chars = new string[] { Enemy_0.Char._id, Enemy_1.Char._id, Enemy_2.Char._id };
        _groundDices = new string[] { 
            NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType,
            NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType,
            AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType,
            AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType,
        };
        _teamDices = new string[] {
            AtkDice.dice._diceType, AtkDice.dice._diceType,
        };
    }
}