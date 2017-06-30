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

    // 預備骰查看
    public static string getDiceBoxString(int type) { return NameCoder.diceBoxLabel + type.ToString(); }
    public static bool isBelongDiceBox(string str) {
        if (str.StartsWith(NameCoder.diceBoxLabel)) return isNumValid(str, 1);
        else return false;
    }
    public static int getDiceBoxNum(string str) { return int.Parse(str.Split('-')[1]); }

    // 骰面之行動點數/建築點數選擇
    public static string getAttrDecisionString(int num) { return NameCoder.decisionAttrLabel + num.ToString(); }
    public static string getBaseDecisionString(int num) { return NameCoder.decisionBaseLabel + num.ToString(); }
    public static bool isBelongAttrDecision(string str) {
        if (str.StartsWith(NameCoder.decisionAttrLabel)) return isNumValid(str, 1);
        else return false;
    }
    public static bool isBelongBaseDecision(string str) {
        if (str.StartsWith(NameCoder.decisionBaseLabel)) return isNumValid(str, 1);
        else return false;
    }
    public static int getDecisionNum(string str) { return int.Parse(str.Split('-')[1]); }

    // 更換角色之選擇
    public static string getChangeCharString(int num) { return NameCoder.changeCharLabel + num.ToString(); }
    public static bool isBelongChangeChar(string str) {
        if (str.StartsWith(NameCoder.changeCharLabel)) return isNumValid(str, 1);
        else return false;
    }
    public static int getChangeCharNum(string str) { return int.Parse(str.Split('-')[1]); }

    // 移動指令
    public static bool isBelongMoveAction(string str) { return str.StartsWith(NameCoder.moveActionLabel); }
    // 攻擊指令
    public static bool isBelongAttackAction(string str) { return str.StartsWith(NameCoder.attackActionLabel); }
    // 防禦指令
    public static bool isBelongDefenseAction(string str) { return str.StartsWith(NameCoder.defenseActionLabel); }

}

public class NameCoder{
    public static void setButtonLabel_ID(GameObject o, string[] Label_ID) {
        o.transform.Find("text").GetComponent<TextMesh>().text = Label_ID[1];
        o.GetComponent<Button>().ButtonID = Label_ID[0];
    }
    public static string getLabel(string[] namePair) { return namePair[0]; }
    public static string getText(string[] namePair) { return namePair[1]; }

    // 基本按鈕
    public static readonly string[] StartButton = new string[] { "start", "Start" };
    public static readonly string[] NextButton = new string[] { "next", "Next" };
    public static readonly string[] ExitButton = new string[] { "exit", "Exit" };
    public static readonly string[] ThrowButton = new string[] { "throw", "Throw" };

    // 預備骰
    public const string diceBoxLabel = "diceBox-";

    // 骰面選擇 點數/建築
    public const string decisionAttrLabel = "decisionAttr-";
    public const string decisionBaseLabel = "decisionBase-";

    // 更換角色
    public const string changeCharLabel = "changeChar-";

    // 移動指令
    public const string moveActionLabel = "movAct-";
    public static readonly string[] Move_GetFirst = new string[] { moveActionLabel + "get_first", "Get First" };
    public static readonly string[] Move_Exchange = new string[] { moveActionLabel + "exchange", "Exchange" };
    public static readonly string[] Move_Standby = new string[] { moveActionLabel + "standby", "Standby" };

    // 攻擊指令
    public const string attackActionLabel = "atkAct-";
    public static readonly string[] Simple_Attack = new string[] { attackActionLabel + "simple", "Attack" };
    public static readonly string[] Strike_Attack = new string[] { attackActionLabel + "strike", "Strike" };

    // 防禦指令
    public const string defenseActionLabel = "defAct-";
    public static readonly string[] Simple_Defense = new string[] { defenseActionLabel + "simple", "Defense" };


}