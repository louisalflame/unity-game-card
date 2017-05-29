using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        if      (counter._attr == Attribute.Normal)    _tower = new NorTower1();
        else if (counter._attr == Attribute.Attack)    _tower = new AtkTower1();
        else if (counter._attr == Attribute.Deffense)  _tower = new DefTower1();
        else if (counter._attr == Attribute.Move)      _tower = new MovTower1();
        else if (counter._attr == Attribute.Special)   _tower = new SpcTower1();
        else if (counter._attr == Attribute.Health)    _tower = new HealTower1();
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
    public override string getImage() { return ImgPath.TowerBase; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class NorTower1 : TowerMode {
    public NorTower1() {
        _attr = Attribute.Normal;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerNor1; }
    public override TowerMode upgrade() { return new NorTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class NorTower2 : TowerMode {
    public NorTower2() {
        _attr = Attribute.Normal;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerNor2; }
    public override TowerMode upgrade() { return new NorTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >=1; }
}
public class NorTower3 : TowerMode
{
    public NorTower3() {
        _attr = Attribute.Normal;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerNor3; }
    public override TowerMode upgrade() { return new NorTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >=1; }
}
public class NorTower4 : TowerMode
{
    public NorTower4() {
        _attr = Attribute.Normal;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerNor4; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class AtkTower1 : TowerMode {
    public AtkTower1() {
        _attr = Attribute.Attack;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerAtk1; }
    public override TowerMode upgrade() { return new AtkTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class AtkTower2 : TowerMode {
    public AtkTower2() {
        _attr = Attribute.Attack;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerAtk2; }
    public override TowerMode upgrade() { return new AtkTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class AtkTower3 : TowerMode {
    public AtkTower3() {
        _attr = Attribute.Attack;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerAtk3; }
    public override TowerMode upgrade() { return new AtkTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class AtkTower4 : TowerMode {
    public AtkTower4() {
        _attr = Attribute.Attack;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerAtk4; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class DefTower1 : TowerMode {
    public DefTower1() {
        _attr = Attribute.Deffense;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerDef1; }
    public override TowerMode upgrade() { return new DefTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class DefTower2 : TowerMode {
    public DefTower2() {
        _attr = Attribute.Deffense;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerDef2; }
    public override TowerMode upgrade() { return new DefTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class DefTower3 : TowerMode {
    public DefTower3() {
        _attr = Attribute.Deffense;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerDef3; }
    public override TowerMode upgrade() { return new DefTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class DefTower4 : TowerMode {
    public DefTower4() {
        _attr = Attribute.Deffense;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerDef4; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class MovTower1 : TowerMode {
    public MovTower1() {
        _attr = Attribute.Move;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerMov1; }
    public override TowerMode upgrade() { return new MovTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class MovTower2 : TowerMode {
    public MovTower2() {
        _attr = Attribute.Move;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerMov2; }
    public override TowerMode upgrade() { return new MovTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class MovTower3 : TowerMode {
    public MovTower3() {
        _attr = Attribute.Move;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerMov3; }
    public override TowerMode upgrade() { return new MovTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class MovTower4 : TowerMode {
    public MovTower4() {
        _attr = Attribute.Move;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerMov4; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class SpcTower1 : TowerMode {
    public SpcTower1() {
        _attr = Attribute.Special;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerSpc1; }
    public override TowerMode upgrade() { return new SpcTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class SpcTower2 : TowerMode {
    public SpcTower2() {
        _attr = Attribute.Special;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerSpc2; }
    public override TowerMode upgrade() { return new SpcTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class SpcTower3 : TowerMode {
    public SpcTower3() {
        _attr = Attribute.Special;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerSpc3; }
    public override TowerMode upgrade() { return new SpcTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class SpcTower4 : TowerMode {
    public SpcTower4() {
        _attr = Attribute.Special;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerSpc4; }
    public override TowerMode upgrade() { Debug.Log("ERROR: cannot upgrade!"); return this; }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return false; }
}
public class HealTower1 : TowerMode {
    public HealTower1() {
        _attr = Attribute.Health;
        _level = 1;
        _capacity = 5;
    }
    public override string getImage() { return ImgPath.TowerHeal1; }
    public override TowerMode upgrade() { return new HealTower2(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2; }
}
public class HealTower2 : TowerMode {
    public HealTower2() {
        _attr = Attribute.Health;
        _level = 2;
        _capacity = 10;
    }
    public override string getImage() { return ImgPath.TowerHeal2; }
    public override TowerMode upgrade() { return new HealTower3(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 1 && counter._weight >= 1; }
}
public class HealTower3 : TowerMode {
    public HealTower3() {
        _attr = Attribute.Health;
        _level = 3;
        _capacity = 15;
    }
    public override string getImage() { return ImgPath.TowerHeal3; }
    public override TowerMode upgrade() { return new HealTower4(); }
    public override bool isValidUpgrade(AttrBaseCounter counter) { return counter._attr == _attr && counter._base >= 2 && counter._weight >= 1; }
}
public class HealTower4 : TowerMode {
    public HealTower4() {
        _attr = Attribute.Health;
        _level = 4;
        _capacity = 20;
    }
    public override string getImage() { return ImgPath.TowerHeal4; }
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