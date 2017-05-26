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
        if (str.StartsWith("decision-attr-")) return isNumValid(str, 2); return false;
    }
    public static bool isBelongBaseDecision(string str) {
        if (str.StartsWith("decision-base-")) return isNumValid(str, 2); return false;
    }
    public static int getDecisionNum(string str) {
        return int.Parse(str.Split('-')[2]);
    }

    // 更換角色之選擇
    public static string getChangeCharString(int num) { return "changeTo_char-" + num.ToString(); }
    public static bool isBelongChangeChar(string str) {
        if (str.StartsWith("changeTo_char")) return isNumValid(str, 1); return false;
    }
    public static int getChangeCharNum(string str) {
        return int.Parse(str.Split('-')[1]);
    }

}