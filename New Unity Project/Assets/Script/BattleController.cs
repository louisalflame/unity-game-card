using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController {

    private TurnManager _turnManager = null;
    private DiceManager _diceManager = null;
    private TowerManager _towerManager = null;
    private TeamManager _teamManager = null;
    // 介面總管理
    private InterfaceController _interface = null;

    List<GameObject> dices;

    public BattleController() {
        _diceManager = new DiceManager();
        _towerManager = new TowerManager();
        _teamManager = new TeamManager();
        _interface = new InterfaceController();
        _turnManager = new TurnManager();
        _turnManager.initSetting(this, new PrepareTurn(this, _turnManager));
    }

    public void update() {
        _interface.update();
        _turnManager.update();
    }

    // 介面管理
    public void showBasicInterface() {
        showNextButton();
    }
    public void hideThrowButton() { _interface.hideThrowButton(); }
    public void showThrowButton() { _interface.showThrowButton(); }
    public void hideNextButton() { _interface.hideNextButton(); }
    public void showNextButton() { _interface.showNextButton(); }
    public void showDiceBox() { _interface.showDiceBox( _diceManager.getDicesUnused() ); }
    public void showDicePlay() { _interface.showDicePlay( _diceManager.getDicesUsing() ); }

    // 準備階段 製作所有骰子
    public void createDices() {
        for (int i = 0; i < 10; i++) { _diceManager.addDicesUnused(DiceFactory.createDice()); }
    }
    public void startCollectDices() { _interface.startCollectDices(); }
    public void collectDices() { _interface.collectDices3D(); }
    public void removeDices() { _interface.removeDices3D(); }
    public void recycleDices() { _diceManager.recycleDices(); }

    // update管理 ============================================================================
    public bool isAllDicesStop() { return _interface.isAllDicesStop(); }
    public bool isAllDicesCollectReady() { return _interface.isAllDicesCollectReady(); }
    public void checkDices() {
        _interface.getDicesValue();
        _turnManager.nextTurn();
    }

    //========================================================================================

    // player指令動作 ========================================================================
    public void CountResultAndNextTurn(){
        _turnManager.nextTurn();
    }
    public void throwDices() {
        hideThrowButton();
        //抓出box前5個dices
        _diceManager.addDicesUsing(5);
        showDiceBox();
        //製作dice 3D模型
        showDicePlay();
        //下一階段 等待骰子 隨機擲出 記錄骰值
        _turnManager.nextTurn();
    }
    //========================================================================================
	
}

//骰子管理(未使用/使用中/使用過)
public class DiceManager {
    private List<Dice> _dicesUnused = null;
    private List<Dice> _dicesUsing = null;
    private List<Dice> _dicesUsed = null;

    public DiceManager() {
        _dicesUnused = new List<Dice>();
        _dicesUsing = new List<Dice>();
        _dicesUsed = new List<Dice>();
    }
    public void addDicesUnused(Dice dice) {
        _dicesUnused.Add(dice);
    }
    public void addDicesUsing(int n=0){
        for (int i = 0; i < n && _dicesUnused.Count > 0; i++) {
            Dice _newUsing = _dicesUnused[0];
            _dicesUnused.RemoveAt(0);
            _dicesUsing.Add(_newUsing);
        }
    }
    public void recycleDices() {
        foreach (Dice d in _dicesUsing) {
            _dicesUsed.Add(d);
            _dicesUsing.Remove(d);
        }
    }

    public List<Dice> getDicesUnused() { return _dicesUnused; }
    public List<Dice> getDicesUsing() { return _dicesUsing; }
}
//儲存塔管理
public class TowerManager {
    private int[] towerAttrs = new int[6];
    public TowerManager() { }
}

public class TeamManager {
    private CharManager _char1 = null;
    private CharManager _char2 = null;
    private CharManager _char3 = null;
    public TeamManager() {
        _char1 = new CharManager();
        _char2 = new CharManager();
        _char3 = new CharManager();
    }
}
public class CharManager {
    private Character _character = null;
    public CharManager() {
        _character = new Character();
    }
}