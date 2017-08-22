using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 擲骰骰面選擇
public class AttrDecisionManager{
    public BattleController _battle;
    private List<DiceFace> _toAttr;
    private List<DiceFace> _toBase;
    public AttrDecisionManager(BattleController battle) {
        _battle = battle;
    }

    public void setDicesResult(List<DiceFace> result) {
        _toAttr = new List<DiceFace>();
        _toBase = new List<DiceFace>();
        foreach(DiceFace face in result) {
            _toAttr.Add(face);
        }
    }
    public void decisionAttr(int id) {
        if (id < _toAttr.Count) {
            DiceFace face = _toAttr[id];
            _toAttr.RemoveAt(id);
            _toBase.Add(face);
        }
    }
    public void decisionBase(int id) {
        if (id < _toBase.Count) {
            DiceFace face = _toBase[id];
            _toBase.RemoveAt(id);
            _toAttr.Add(face);
        }
    }
    public List<DiceFace> getFacesAttr() { return _toAttr; }
    public List<DiceFace> getFacesBase() { return _toBase; }
}
