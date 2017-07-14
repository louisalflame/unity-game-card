using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRecord : MonoBehaviour {
    public TeamMember _team { get; private set; }
    
    void Start() {
        GameObject.DontDestroyOnLoad(this.gameObject);

        _team = new TeamMember();

        // 隊伍初始設定
        _team.setCharacters(new string[] { Player_0.Char._id, Player_1.Char._id, Player_2.Char._id });
        _team.setGroundDices(new string[] { 
            NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType,
            NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType, NorDice.dice._diceType,
            AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType,
            AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType, AtkDice.dice._diceType,
        });
        _team.setTeamDices(new string[] {
            AtkDice.dice._diceType, AtkDice.dice._diceType,
        });
         
    }
}

public class TeamMember {
    public string[] _chars { get; protected set; }
    public string[] _groundDices { get; protected set; } //TODO: 場地骰實際上應該在任務地圖上決定
    public string[] _teamDices { get; protected set; }

    public void setCharacters(string[] chars) {
        _chars = chars;
    }
    public void setGroundDices(string[] dices) {
        _groundDices = dices;
    }
    public void setTeamDices(string[] dices) {
        _teamDices = dices;
    }
}
