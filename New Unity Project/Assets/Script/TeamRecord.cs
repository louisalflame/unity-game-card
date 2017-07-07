using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRecord : MonoBehaviour {
    public int[] _chars { get; private set; }
    public string[] _groundDices { get; private set; } //TODO: 場地骰實際上應該在任務地圖上決定
    public string[] _teamDices { get; private set; }
    
    void Start() {
        GameObject.DontDestroyOnLoad(this.gameObject);

        // 隊伍初始設定
        _chars = new int[] { 10, 11, 12, 12 };
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
