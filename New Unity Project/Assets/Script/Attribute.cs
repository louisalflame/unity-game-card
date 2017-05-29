using UnityEngine;
using System.Collections;

public class Attribute  {
    public const int Normal      = 0;
    public const int Attack      = 1;
    public const int Deffense    = 2;
    public const int Move        = 3;
    public const int Special     = 4;
    public const int Health      = 5;
    public const int NONE        = -1;

    public const int NormalDouble   = 10;
    public const int AttackDouble   = 11;
    public const int DeffenseDouble = 12;
    public const int MoveDouble     = 13;
    public const int SpecialDouble  = 14;
    public const int HealthDouble   = 15;
}

public class ConstNum {
    public const int DiceCollectSpeed = 12;
    public const int DiceRotateSpeed = 300;
}

public class ImgPath {
    public const string NorBase         = "Sprite/DiceBase/NorTowerBase";
    public const string NorBaseDouble   = "Sprite/DiceBase/NorTower";
    public const string AtkBase         = "Sprite/DiceBase/AtkTowerBase";
    public const string AtkBaseDouble   = "Sprite/DiceBase/AtkTower";
    public const string DefBase         = "Sprite/DiceBase/DefTowerBase";
    public const string DefBaseDouble   = "Sprite/DiceBase/DefTower";
    public const string MovBase         = "Sprite/DiceBase/MovTowerBase";
    public const string MovBaseDouble   = "Sprite/DiceBase/MovTower";
    public const string SpcBase         = "Sprite/DiceBase/SpcTowerBase";
    public const string SpcBaseDouble   = "Sprite/DiceBase/SpcTower";
    public const string HealBase        = "Sprite/DiceBase/HealTowerBase";
    public const string HealBaseDouble  = "Sprite/DiceBase/HealTower";
    

    public const string NorDiceIcon = "Sprite/DiceIcon/DiceNor";
    public const string AtkDiceIcon = "Sprite/DiceIcon/DiceAtk";
    public const string DefDiceIcon = "Sprite/DiceIcon/DiceDef";
    public const string MovDiceIcon = "Sprite/DiceIcon/DiceMov";
    public const string SpcDiceIcon = "Sprite/DiceIcon/DiceSpc";
    public const string HealDiceIcon = "Sprite/DiceIcon/DiceHeal";


    public const string Nor_Nor3 = "Sprite/DiceFace/NorBase/NorBaseNor3";
    public const string Nor_Atk3 = "Sprite/DiceFace/NorBase/NorBaseAtk3";
    public const string Nor_Def3 = "Sprite/DiceFace/NorBase/NorBaseDef3";
    public const string Nor_Mov3 = "Sprite/DiceFace/NorBase/NorBaseMov3";
    public const string Nor_Spc3 = "Sprite/DiceFace/NorBase/NorBaseSpc3";
    public const string Nor_Heal3 = "Sprite/DiceFace/NorBase/NorBaseHeal3";

    public const string Atk_Nor3 = "Sprite/DiceFace/AtkBase/AtkBaseNor3";
    public const string Atk_Atk3 = "Sprite/DiceFace/AtkBase/AtkBaseAtk3";
    public const string Atk_Def3 = "Sprite/DiceFace/AtkBase/AtkBaseDef3";
    public const string Atk_Mov3 = "Sprite/DiceFace/AtkBase/AtkBaseMov3";
    public const string Atk_Spc3 = "Sprite/DiceFace/AtkBase/AtkBaseSpc3";
    public const string Atk_Heal3 = "Sprite/DiceFace/AtkBase/AtkBaseHeal3";

    public const string Def_Nor3 = "Sprite/DiceFace/DefBase/DefBaseNor3";
    public const string Def_Atk3 = "Sprite/DiceFace/DefBase/DefBaseAtk3";
    public const string Def_Def3 = "Sprite/DiceFace/DefBase/DefBaseDef3";
    public const string Def_Mov3 = "Sprite/DiceFace/DefBase/DefBaseMov3";
    public const string Def_Spc3 = "Sprite/DiceFace/DefBase/DefBaseSpc3";
    public const string Def_Heal3 = "Sprite/DiceFace/DefBase/DefBaseHeal3";

    public const string Mov_Nor3 = "Sprite/DiceFace/MovBase/MovBaseNor3";
    public const string Mov_Atk3 = "Sprite/DiceFace/MovBase/MovBaseAtk3";
    public const string Mov_Def3 = "Sprite/DiceFace/MovBase/MovBaseDef3";
    public const string Mov_Mov3 = "Sprite/DiceFace/MovBase/MovBaseMov3";
    public const string Mov_Spc3 = "Sprite/DiceFace/MovBase/MovBaseSpc3";
    public const string Mov_Heal3 = "Sprite/DiceFace/MovBase/MovBaseHeal3";

    public const string Spc_Nor3 = "Sprite/DiceFace/SpcBase/SpcBaseNor3";
    public const string Spc_Atk3 = "Sprite/DiceFace/SpcBase/SpcBaseAtk3";
    public const string Spc_Def3 = "Sprite/DiceFace/SpcBase/SpcBaseDef3";
    public const string Spc_Mov3 = "Sprite/DiceFace/SpcBase/SpcBaseMov3";
    public const string Spc_Spc3 = "Sprite/DiceFace/SpcBase/SpcBaseSpc3";
    public const string Spc_Heal3 = "Sprite/DiceFace/SpcBase/SpcBaseHeal3";

    public const string Heal_Nor3 = "Sprite/DiceFace/HealBase/HealBaseNor3";
    public const string Heal_Atk3 = "Sprite/DiceFace/HealBase/HealBaseAtk3";
    public const string Heal_Def3 = "Sprite/DiceFace/HealBase/HealBaseDef3";
    public const string Heal_Mov3 = "Sprite/DiceFace/HealBase/HealBaseMov3";
    public const string Heal_Spc3 = "Sprite/DiceFace/HealBase/HealBaseSpc3";
    public const string Heal_Heal3 = "Sprite/DiceFace/HealBase/HealBaseHeal3";


    public const string TowerBase = "Sprite/TowerIcon/TowerBase";
    public const string TowerNor1 = "Sprite/TowerIcon/TowerNor1";
    public const string TowerNor2 = "Sprite/TowerIcon/TowerNor2";
    public const string TowerNor3 = "Sprite/TowerIcon/TowerNor3";
    public const string TowerNor4 = "Sprite/TowerIcon/TowerNor4";

    public const string TowerAtk1 = "Sprite/TowerIcon/TowerAtk1";
    public const string TowerAtk2 = "Sprite/TowerIcon/TowerAtk2";
    public const string TowerAtk3 = "Sprite/TowerIcon/TowerAtk3";
    public const string TowerAtk4 = "Sprite/TowerIcon/TowerAtk4";

    public const string TowerDef1 = "Sprite/TowerIcon/TowerDef1";
    public const string TowerDef2 = "Sprite/TowerIcon/TowerDef2";
    public const string TowerDef3 = "Sprite/TowerIcon/TowerDef3";
    public const string TowerDef4 = "Sprite/TowerIcon/TowerDef4";

    public const string TowerMov1 = "Sprite/TowerIcon/TowerMov1";
    public const string TowerMov2 = "Sprite/TowerIcon/TowerMov2";
    public const string TowerMov3 = "Sprite/TowerIcon/TowerMov3";
    public const string TowerMov4 = "Sprite/TowerIcon/TowerMov4";

    public const string TowerSpc1 = "Sprite/TowerIcon/TowerSpc1";
    public const string TowerSpc2 = "Sprite/TowerIcon/TowerSpc2";
    public const string TowerSpc3 = "Sprite/TowerIcon/TowerSpc3";
    public const string TowerSpc4 = "Sprite/TowerIcon/TowerSpc4";

    public const string TowerHeal1 = "Sprite/TowerIcon/TowerHeal1";
    public const string TowerHeal2 = "Sprite/TowerIcon/TowerHeal2";
    public const string TowerHeal3 = "Sprite/TowerIcon/TowerHeal3";
    public const string TowerHeal4 = "Sprite/TowerIcon/TowerHeal4";
}


public class StringCoder {
    public static bool isNumValid(string str, int pos) {
        int id;
        if (int.TryParse(str.Split('-')[pos], out id)) return true;
        return false;
    }
    // 骰面之行動點數/建築點數選擇
    public static string getAttrDecisionString(int num) { return "decision-attr-" + num.ToString(); }
    public static string getBaseDecisionString(int num) { return "decision-base-" + num.ToString(); }
    public static bool isBelongAttrDecision(string str) {
        if (str.StartsWith("decision-attr-")) return isNumValid(str, 2);
        else return false;
    }
    public static bool isBelongBaseDecision(string str) {
        if (str.StartsWith("decision-base-")) return isNumValid(str, 2);
        else return false;
    }
    public static int getDecisionNum(string str) {
        return int.Parse(str.Split('-')[2]);
    }

    // 更換角色之選擇
    public static string getChangeCharString(int num) { return "changeTo_char-" + num.ToString(); }
    public static bool isBelongChangeChar(string str) {
        if (str.StartsWith("changeTo_char")) return isNumValid(str, 1);
        else return false;
    }
    public static int getChangeCharNum(string str) {
        return int.Parse(str.Split('-')[1]);
    }

}