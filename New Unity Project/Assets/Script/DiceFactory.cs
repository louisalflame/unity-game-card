using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiceFactory  {

    public static Dice createDice() {
        return new NorDice();
    }

    public static GameObject createDice2D() {
        GameObject dice = MonoBehaviour.Instantiate(Resources.Load("DiceIcon")) as GameObject;
        return dice;
    }
    public static GameObject createDice3D(Dice d) {
        GameObject dice = MonoBehaviour.Instantiate(Resources.Load("DiceBase")) as GameObject;
        //設定6個骰面圖案 UP:5 FORWARD:2 RIGHT:3 DOWN:6 BACK:1 LEFT:4
        for (int i = 0; i < 6; i++) {
            GameObject face = dice.transform.GetChild(i).gameObject;
            face.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( d.getFaceImage(i+1) );
            face.transform.localScale = new Vector3(1, 1, 1);
        }
        return dice;
    }
}


// 設定6個骰面圖案 UP:5 FORWARD:2 RIGHT:3 DOWN:6 BACK:1 LEFT:4
public class Dice {
    protected DiceFace _uFace = null;
    protected DiceFace _fFace = null;
    protected DiceFace _rFace = null;
    protected DiceFace _dFace = null;
    protected DiceFace _bFace = null;
    protected DiceFace _lFace = null;
    protected string _diceType = null;
    public Dice() { }
    public virtual string getIconImage() { return "";  }
    public virtual DiceFace getFace(int n){
        if (n == 1) return _bFace;
        else if (n == 2) return _fFace;
        else if (n == 3) return _rFace;
        else if (n == 4) return _lFace;
        else if (n == 5) return _uFace;
        else if (n == 6) return _dFace;
        else { Debug.Log("ERROR:Wrong face number"); return _dFace; }
    }
    public virtual string getFaceImage(int n) { return getFace(n).getImage(); }
}

public class NorDice : Dice {
    public NorDice(){
        _diceType = "NorDice";
        _uFace = new SpcBaseSpc3(); _dFace = new HealBaseHeal3();
        _fFace = new AtkBaseAtk3(); _bFace = new MovBaseMov3();
        _rFace = new DefBaseDef3(); _lFace = new NorBaseNor3();
    }
    public override string getIconImage() { return "Sprite/DiceIcon/DiceNor"; }
}

public class AtkDice : Dice {
    public AtkDice(){
        _diceType = "AtkDice";
        _uFace = new AtkBaseSpc3(); _dFace = new NorBaseHeal3();
        _fFace = new AtkBaseAtk3(); _bFace = new NorBaseMov3();
        _rFace = new AtkBaseDef3(); _lFace = new NorBaseNor3();
    }
    public override string getIconImage() { return "Sprite/DiceIcon/DiceAtk"; }
}


