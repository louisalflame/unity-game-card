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

public class DiceFace {
    public int _num{ get; protected set; }
    public int _attr{ get; protected set; }
    public int _base{ get; protected set; }
    public int _baseWeight { get; protected set; }
    public DiceFace(int num, int attr, int b) {
        _num = num;
        _attr = attr;
        _base = b;
        _baseWeight = _attr == _base % 10 ? 1 : 0;
    }
    public virtual string getImage() { return ""; }
    public virtual string getBaseImage() { return ""; }
    public override string ToString() { return "attr:"+_attr+"; num:"+_num+"; base:"+_base; }
}
public class NorFace : DiceFace {
    public NorFace() : base(3, Attribute.Normal, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseNor3"; }
    public override string getBaseImage() { return ImgPath.NorBaseDouble; }
}
public class AtkFace : DiceFace {
    public AtkFace() : base(3, Attribute.Attack, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class DefFace : DiceFace {
    public DefFace() : base(3, Attribute.Deffense, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseDef3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class MovFace : DiceFace{
    public MovFace() : base(3, Attribute.Move, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseMov3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class SpcFace : DiceFace{
    public SpcFace() : base(3, Attribute.Special, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class HealFace : DiceFace{
    public HealFace() : base(3, Attribute.Health, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
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
        _uFace = new SpcFace(); _dFace = new HealFace();
        _fFace = new AtkFace(); _bFace = new NorFace();
        _rFace = new DefFace(); _lFace = new MovFace();
    }
    public override string getIconImage() { return "Sprite/DiceIcon/DiceNor"; }
}



public class AttrTower {
    public TowerMode _tower;
    public int _attr { get { return _tower._attr; } private set { _tower._attr = value; } }
    public int _level { get { return _tower._level; } private set { _tower._attr = value; } }
    public int _capacity { get { return _tower._capacity; } private set { _tower._attr = value; } }
    public int _positionID;
    public AttrTower(int id) {
        _positionID = id; 
        _tower = new NullTower();
    }
    public TowerMode getTower() { return _tower; }

    //升級條件看個別狀況
    public bool isValidUpgrade(AttrBaseCounter counter) { return _tower.isValidUpgrade(counter); }
    public void upgrade(AttrBaseCounter counter) { _tower = _tower.upgrade(); }
    //從0建塔，只有NONE可以建一級塔
    public bool isValidBuild(AttrBaseCounter counter) { return _attr == Attribute.NONE && counter._base >= 1; }
    public void build(AttrBaseCounter counter) {
        if      (counter._attr == Attribute.Normal) _tower = new NorTower1();
        else if (counter._attr == Attribute.Attack) _tower = new AtkTower1();
    }
    public string getImage() { return _tower.getImage(); }
}

public abstract class TowerMode {
    public int _attr;
    public int _level;
    public int _capacity;
    public abstract string getImage();
    public abstract TowerMode upgrade();
    public abstract bool isValidUpgrade(AttrBaseCounter counter);
}
public class NullTower : TowerMode{
    public NullTower() {
        _attr = Attribute.NONE;
        _level = 0;
        _capacity = 5;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerBase"; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class NorTower1 : TowerMode {
    public NorTower1() {
        _attr = Attribute.Normal;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerNor1"; }
    public override TowerMode upgrade() { return new NorTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class NorTower2 : TowerMode {
    public NorTower2() {
        _attr = Attribute.Normal;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerNor2"; }
    public override TowerMode upgrade() { return new NorTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >=1; }
}
public class NorTower3 : TowerMode
{
    public NorTower3()
    {
        _attr = Attribute.Normal;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerNor3"; }
    public override TowerMode upgrade() { return new NorTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >=1; }
}
public class NorTower4 : TowerMode
{
    public NorTower4()
    {
        _attr = Attribute.Normal;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerNor4"; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class AtkTower1 : TowerMode {
    public AtkTower1() {
        _attr = Attribute.Attack;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerAtk1"; }
    public override TowerMode upgrade() { return new AtkTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class AtkTower2 : TowerMode {
    public AtkTower2() {
        _attr = Attribute.Attack;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerAtk2"; }
    public override TowerMode upgrade() { return new AtkTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class AtkTower3 : TowerMode {
    public AtkTower3() {
        _attr = Attribute.Attack;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerAtk3"; }
    public override TowerMode upgrade() { return new AtkTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class AtkTower4 : TowerMode {
    public AtkTower4() {
        _attr = Attribute.Attack;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerAtk4"; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class DefTower1 : TowerMode {
    public DefTower1() {
        _attr = Attribute.Deffense;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerDef1"; }
    public override TowerMode upgrade() { return new DefTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class DefTower2 : TowerMode {
    public DefTower2() {
        _attr = Attribute.Deffense;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerDef2"; }
    public override TowerMode upgrade() { return new DefTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class DefTower3 : TowerMode {
    public DefTower3() {
        _attr = Attribute.Deffense;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerDef3"; }
    public override TowerMode upgrade() { return new DefTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class DefTower4 : TowerMode {
    public DefTower4() {
        _attr = Attribute.Deffense;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return "Sprite/TowerIcon/TowerDef4"; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}

public class AttrBaseCounter {
    public int _attr { set; get; }
    public int _base { set; get; }
    public int _weight { set; get; }
    public AttrBaseCounter(int attr) {
        _attr = attr;
        _base = 0;
        _weight = 0;
    }
}