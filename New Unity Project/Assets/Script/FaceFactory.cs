using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public class NorBaseNor3 : DiceFace {
    public NorBaseNor3() : base(3, Attribute.Normal, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseNor3"; }
    public override string getBaseImage() { return ImgPath.NorBaseDouble; }
}
public class NorBaseAtk3 : DiceFace {
    public NorBaseAtk3() : base(3, Attribute.Attack, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseDef3 : DiceFace {
    public NorBaseDef3() : base(3, Attribute.Deffense, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseDef3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseMov3 : DiceFace{
    public NorBaseMov3() : base(3, Attribute.Move, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseMov3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseSpc3 : DiceFace{
    public NorBaseSpc3() : base(3, Attribute.Special, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseHeal3 : DiceFace{
    public NorBaseHeal3() : base(3, Attribute.Health, Attribute.Normal) { }
    public override string getImage() { return "Sprite/DiceFace/NorBase/NorBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}

public class AtkBaseNor3 : DiceFace {
    public AtkBaseNor3() : base(3, Attribute.Normal, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseNor3"; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseAtk3 : DiceFace {
    public AtkBaseAtk3() : base(3, Attribute.Attack, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.AtkBaseDouble; }
}
public class AtkBaseDef3 : DiceFace {
    public AtkBaseDef3() : base(3, Attribute.Deffense, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseDef3"; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseMov3 : DiceFace{
    public AtkBaseMov3() : base(3, Attribute.Move, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseMov3"; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseSpc3 : DiceFace{
    public AtkBaseSpc3() : base(3, Attribute.Special, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseHeal3 : DiceFace{
    public AtkBaseHeal3() : base(3, Attribute.Health, Attribute.Attack) { }
    public override string getImage() { return "Sprite/DiceFace/AtkBase/AtkBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}

public class DefBaseNor3 : DiceFace {
    public DefBaseNor3() : base(3, Attribute.Normal, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseNor3"; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseAtk3 : DiceFace {
    public DefBaseAtk3() : base(3, Attribute.Attack, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseDef3 : DiceFace {
    public DefBaseDef3() : base(3, Attribute.Deffense, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseDef3"; }
    public override string getBaseImage() { return ImgPath.DefBaseDouble; }
}
public class DefBaseMov3 : DiceFace{
    public DefBaseMov3() : base(3, Attribute.Move, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseMov3"; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseSpc3 : DiceFace{
    public DefBaseSpc3() : base(3, Attribute.Special, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseHeal3 : DiceFace{
    public DefBaseHeal3() : base(3, Attribute.Health, Attribute.Deffense) { }
    public override string getImage() { return "Sprite/DiceFace/DefBase/DefBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}

public class MovBaseNor3 : DiceFace {
    public MovBaseNor3() : base(3, Attribute.Normal, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseNor3"; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseAtk3 : DiceFace {
    public MovBaseAtk3() : base(3, Attribute.Attack, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseDef3 : DiceFace {
    public MovBaseDef3() : base(3, Attribute.Deffense, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseDef3"; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseMov3 : DiceFace{
    public MovBaseMov3() : base(3, Attribute.Move, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseMov3"; }
    public override string getBaseImage() { return ImgPath.MovBaseDouble; }
}
public class MovBaseSpc3 : DiceFace{
    public MovBaseSpc3() : base(3, Attribute.Special, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseHeal3 : DiceFace{
    public MovBaseHeal3() : base(3, Attribute.Health, Attribute.Move) { }
    public override string getImage() { return "Sprite/DiceFace/MovBase/MovBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}

public class SpcBaseNor3 : DiceFace {
    public SpcBaseNor3() : base(3, Attribute.Normal, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseNor3"; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseAtk3 : DiceFace {
    public SpcBaseAtk3() : base(3, Attribute.Attack, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseDef3 : DiceFace {
    public SpcBaseDef3() : base(3, Attribute.Deffense, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseDef3"; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseMov3 : DiceFace{
    public SpcBaseMov3() : base(3, Attribute.Move, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseMov3"; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseSpc3 : DiceFace{
    public SpcBaseSpc3() : base(3, Attribute.Special, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.SpcBaseDouble; }
}
public class SpcBaseHeal3 : DiceFace{
    public SpcBaseHeal3() : base(3, Attribute.Health, Attribute.Special) { }
    public override string getImage() { return "Sprite/DiceFace/SpcBase/SpcBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}

public class HealBaseNor3 : DiceFace {
    public HealBaseNor3() : base(3, Attribute.Normal, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseNor3"; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseAtk3 : DiceFace {
    public HealBaseAtk3() : base(3, Attribute.Attack, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseAtk3"; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseDef3 : DiceFace {
    public HealBaseDef3() : base(3, Attribute.Deffense, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseDef3"; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseMov3 : DiceFace{
    public HealBaseMov3() : base(3, Attribute.Move, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseMov3"; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseSpc3 : DiceFace{
    public HealBaseSpc3() : base(3, Attribute.Special, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseSpc3"; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseHeal3 : DiceFace{
    public HealBaseHeal3() : base(3, Attribute.Health, Attribute.Health) { }
    public override string getImage() { return "Sprite/DiceFace/HealBase/HealBaseHeal3"; }
    public override string getBaseImage() { return ImgPath.HealBaseDouble; }
}