using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰子管理(未使用/使用中/使用過)
public class DiceManager {
    public BattleController _battle;
    public int _useStack { get; private set; }
    public List<Dice> _dicesUnused { get; private set; }
    public List<Dice> _dicesUsing { get; private set; }
    public List<Dice> _dicesUsed { get; private set; }

    public DiceManager(BattleController battle, int stack) {
        _battle = battle;
        _useStack = stack;
        _dicesUnused = new List<Dice>();
        _dicesUsing = new List<Dice>();
        _dicesUsed = new List<Dice>();
    }

    // 置入場地骰/隊伍骰(技能會增加骰)
    public void importDices(string[] dices) {
        foreach (string str in dices) {
            _dicesUnused.Add(DiceFactory.dictionary[str]);
        }
    }
    // 設定個人骰(重設時全清空)
    public void setDicesUnused(string[] dices) {
        _dicesUnused = new List<Dice>();
        _dicesUsing = new List<Dice>();
        _dicesUsed = new List<Dice>();
        foreach (string str in dices) {
            _dicesUnused.Add(DiceFactory.dictionary[str]);
        }
    }

    public void addDicesUnused(Dice dice) {
        _dicesUnused.Add(dice);
    }

    public void addDicesUsing(){
        if (_dicesUnused.Count < _useStack) { resetDices();  }
        for (int i = 0; i < _useStack && _dicesUnused.Count > 0; i++) {
            Dice _newUsing = _dicesUnused[0];
            _dicesUnused.RemoveAt(0);
            _dicesUsing.Add(_newUsing);
        }
        if (_dicesUnused.Count < _useStack) { resetDices(); }
    }
    
    public void resetDices() {
        foreach (Dice d in _dicesUsed) { _dicesUnused.Add(d); }
        _dicesUsed.Clear();
    }
    public void recycleDices() {
        foreach (Dice d in _dicesUsing) { _dicesUsed.Add(d); }
        _dicesUsing.Clear();
        if (_dicesUnused.Count < _useStack) { resetDices(); }
    }

}