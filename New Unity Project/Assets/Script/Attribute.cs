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