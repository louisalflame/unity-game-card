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
            face.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>( d.getFaceImage(i) );
            face.transform.localScale = new Vector3(1, 1, 1);
        }
        return dice;
    }

    public static List<GameObject> showDices(int num=1) {
        List<GameObject> dices = new List<GameObject>();/*
        for (int n = 0; n < num; n++) {
            GameObject dice = DiceFactory.createDice3D();
            //設定左下>右上的初始位置
            dice.transform.localPosition = new Vector3(-4 + (float)((n % 3) * 1.5 + (n / 3) * 1.5), 5 + (float)((n / 3) * 1.5), -15 + (float)((n / 3) * 3));
            //設定初始隨機角度
            dice.transform.localRotation = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            //設定初始旋轉方向和隨機力度
            dice.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.value * 100, Random.value * 100, Random.value * 100));
            dice.GetComponent<Rigidbody>().useGravity = true;
            dices.Add(dice);
        }*/
        return dices;
    }
}

public class DiceFace {
    protected int _num;
    protected int _attr;
    protected int _base;
    public DiceFace(int num, int attr, int b) {
        _num = num;
        _attr = attr;
        _base = b;
    }
    public virtual string getImage() { return ""; }
}
public class NorFace : DiceFace {
    public NorFace() : base(3, Attribute.Normal, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseNor3"; }
}
public class AtkFace : DiceFace {
    public AtkFace() : base(3, Attribute.Attack, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseAtk3"; }
}
public class DefFace : DiceFace {
    public DefFace() : base(3, Attribute.Deffense, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseDef3"; }
}


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
        if( n==1 ) return _bFace;
        else if( n==2 ) return _fFace;
        else if( n==3 ) return _rFace;
        else if( n==4 ) return _lFace;
        else if( n==5 ) return _uFace;
        else return _dFace;
    }
    public virtual string getFaceImage(int n) { return getFace(n).getImage(); }
}

public class NorDice : Dice {
    public NorDice(){
        _diceType = "NorDice";
        _uFace = new AtkFace(); _dFace = new AtkFace();
        _fFace = new NorFace(); _bFace = new NorFace();
        _rFace = new DefFace(); _lFace = new DefFace();
    }
    public override string getIconImage() { return "Sprite/DiceIcon/DiceNor"; }
}