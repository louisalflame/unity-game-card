using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//骰子管理(未使用/使用中/使用過)
public class DiceManager {
    public BattleController _battle;
    private List<Dice> _dicesUnused = null;
    private List<Dice> _dicesUsing = null;
    private List<Dice> _dicesUsed = null;

    public DiceManager(BattleController battle) {
        _battle = battle;
        _dicesUnused = new List<Dice>();
        _dicesUsing = new List<Dice>();
        _dicesUsed = new List<Dice>();
    }
    public void addDicesUnused(Dice dice) {
        _dicesUnused.Add(dice);
    }
    public void addDicesUsing(int n=0){
        if (_dicesUnused.Count < n) {
            foreach (Dice d in _dicesUsed) { _dicesUnused.Add(d); }
            _dicesUsed = new List<Dice>();
        }
        for (int i = 0; i < n && _dicesUnused.Count > 0; i++) {
            Dice _newUsing = _dicesUnused[0];
            _dicesUnused.RemoveAt(0);
            _dicesUsing.Add(_newUsing);
        }
    }
    public void recycleDices() {
        foreach (Dice d in _dicesUsing) { _dicesUsed.Add(d); }
        _dicesUsing.Clear();
    }

    public List<Dice> getDicesUnused() { return _dicesUnused; }
    public List<Dice> getDicesUsing() { return _dicesUsing; }
}