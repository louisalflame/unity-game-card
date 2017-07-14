using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI {
    public BattleController _battle;
    public TeamManager _team { get; private set; }
     
    public EnemyAI( BattleController battle, TeamManager team){
        _battle = battle;
        _team = team;
    }

    // 隨機擲骰獲得骰面
    public List<DiceFace> getRandomDiceFaces() {
        List<Dice> dices = new List<Dice>();
        foreach (Dice d in _team._groundDices._dicesUsing) { dices.Add(d); }
        foreach (Dice d in _team._teamDices._dicesUsing)   { dices.Add(d); }
        foreach (Dice d in _team._personDices._dicesUsing) { dices.Add(d); }

        List<DiceFace> diceFaces = new List<DiceFace>();
        foreach (Dice d in dices) {
            int rand = Random.Range(0, 5); Debug.Log(rand);
            diceFaces.Add( d.getFace(rand) );
        }
        return diceFaces;
    }
    // 從骰面隨機選擇點數
    public void selectRandomPoints(List<DiceFace> diceFaces) {
        List<DiceFace> attrFaces = new List<DiceFace>();
        List<DiceFace> baseFaces = new List<DiceFace>();

        // 隨機選一面當建築點數
        int rand = Random.Range(0, diceFaces.Count);
        for (int i = 0; i < diceFaces.Count; i++) {
            if (i == rand) { baseFaces.Add(diceFaces[i]); }
            else { attrFaces.Add(diceFaces[i]); }
        }

        _team._towerManager.collectAttrPoint(attrFaces);
        _team._towerManager.collectBasePoint(baseFaces);
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