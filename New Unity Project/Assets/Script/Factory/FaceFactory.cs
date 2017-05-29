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
    public override string getImage() { return ImgPath.Nor_Nor3; }
    public override string getBaseImage() { return ImgPath.NorBaseDouble; }
}
public class NorBaseAtk3 : DiceFace {
    public NorBaseAtk3() : base(3, Attribute.Attack, Attribute.Normal) { }
    public override string getImage() { return ImgPath.Nor_Atk3; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseDef3 : DiceFace {
    public NorBaseDef3() : base(3, Attribute.Deffense, Attribute.Normal) { }
    public override string getImage() { return ImgPath.Nor_Def3; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseMov3 : DiceFace{
    public NorBaseMov3() : base(3, Attribute.Move, Attribute.Normal) { }
    public override string getImage() { return ImgPath.Nor_Mov3; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseSpc3 : DiceFace{
    public NorBaseSpc3() : base(3, Attribute.Special, Attribute.Normal) { }
    public override string getImage() { return ImgPath.Nor_Spc3; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}
public class NorBaseHeal3 : DiceFace{
    public NorBaseHeal3() : base(3, Attribute.Health, Attribute.Normal) { }
    public override string getImage() { return ImgPath.Nor_Heal3; }
    public override string getBaseImage() { return ImgPath.NorBase; }
}

public class AtkBaseNor3 : DiceFace {
    public AtkBaseNor3() : base(3, Attribute.Normal, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Nor3; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseAtk3 : DiceFace {
    public AtkBaseAtk3() : base(3, Attribute.Attack, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Atk3; }
    public override string getBaseImage() { return ImgPath.AtkBaseDouble; }
}
public class AtkBaseDef3 : DiceFace {
    public AtkBaseDef3() : base(3, Attribute.Deffense, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Def3; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseMov3 : DiceFace{
    public AtkBaseMov3() : base(3, Attribute.Move, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Mov3; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseSpc3 : DiceFace{
    public AtkBaseSpc3() : base(3, Attribute.Special, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Spc3; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}
public class AtkBaseHeal3 : DiceFace{
    public AtkBaseHeal3() : base(3, Attribute.Health, Attribute.Attack) { }
    public override string getImage() { return ImgPath.Atk_Heal3; }
    public override string getBaseImage() { return ImgPath.AtkBase; }
}

public class DefBaseNor3 : DiceFace {
    public DefBaseNor3() : base(3, Attribute.Normal, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Nor3; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseAtk3 : DiceFace {
    public DefBaseAtk3() : base(3, Attribute.Attack, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Atk3; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseDef3 : DiceFace {
    public DefBaseDef3() : base(3, Attribute.Deffense, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Def3; }
    public override string getBaseImage() { return ImgPath.DefBaseDouble; }
}
public class DefBaseMov3 : DiceFace{
    public DefBaseMov3() : base(3, Attribute.Move, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Mov3; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseSpc3 : DiceFace{
    public DefBaseSpc3() : base(3, Attribute.Special, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Spc3; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}
public class DefBaseHeal3 : DiceFace{
    public DefBaseHeal3() : base(3, Attribute.Health, Attribute.Deffense) { }
    public override string getImage() { return ImgPath.Def_Heal3; }
    public override string getBaseImage() { return ImgPath.DefBase; }
}

public class MovBaseNor3 : DiceFace {
    public MovBaseNor3() : base(3, Attribute.Normal, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Nor3; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseAtk3 : DiceFace {
    public MovBaseAtk3() : base(3, Attribute.Attack, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Atk3; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseDef3 : DiceFace {
    public MovBaseDef3() : base(3, Attribute.Deffense, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Def3; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseMov3 : DiceFace{
    public MovBaseMov3() : base(3, Attribute.Move, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Mov3; }
    public override string getBaseImage() { return ImgPath.MovBaseDouble; }
}
public class MovBaseSpc3 : DiceFace{
    public MovBaseSpc3() : base(3, Attribute.Special, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Spc3; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}
public class MovBaseHeal3 : DiceFace{
    public MovBaseHeal3() : base(3, Attribute.Health, Attribute.Move) { }
    public override string getImage() { return ImgPath.Mov_Heal3; }
    public override string getBaseImage() { return ImgPath.MovBase; }
}

public class SpcBaseNor3 : DiceFace {
    public SpcBaseNor3() : base(3, Attribute.Normal, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Nor3; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseAtk3 : DiceFace {
    public SpcBaseAtk3() : base(3, Attribute.Attack, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Atk3; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseDef3 : DiceFace {
    public SpcBaseDef3() : base(3, Attribute.Deffense, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Def3; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseMov3 : DiceFace{
    public SpcBaseMov3() : base(3, Attribute.Move, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Mov3; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}
public class SpcBaseSpc3 : DiceFace{
    public SpcBaseSpc3() : base(3, Attribute.Special, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Spc3; }
    public override string getBaseImage() { return ImgPath.SpcBaseDouble; }
}
public class SpcBaseHeal3 : DiceFace{
    public SpcBaseHeal3() : base(3, Attribute.Health, Attribute.Special) { }
    public override string getImage() { return ImgPath.Spc_Heal3; }
    public override string getBaseImage() { return ImgPath.SpcBase; }
}

public class HealBaseNor3 : DiceFace {
    public HealBaseNor3() : base(3, Attribute.Normal, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Nor3; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseAtk3 : DiceFace {
    public HealBaseAtk3() : base(3, Attribute.Attack, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Atk3; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseDef3 : DiceFace {
    public HealBaseDef3() : base(3, Attribute.Deffense, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Def3; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseMov3 : DiceFace{
    public HealBaseMov3() : base(3, Attribute.Move, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Mov3; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseSpc3 : DiceFace{
    public HealBaseSpc3() : base(3, Attribute.Special, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Spc3; }
    public override string getBaseImage() { return ImgPath.HealBase; }
}
public class HealBaseHeal3 : DiceFace{
    public HealBaseHeal3() : base(3, Attribute.Health, Attribute.Health) { }
    public override string getImage() { return ImgPath.Heal_Heal3; }
    public override string getBaseImage() { return ImgPath.HealBaseDouble; }
}