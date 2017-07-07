using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position {
    public static Vector3 getVector3(float[] arr) { return new Vector3(arr[0], arr[1], arr[2]); }

    // 系統按鈕
    public static readonly float[] systemBtn = new float[] { -13f, 0, 0};
    // 主要確認按鈕
    public static readonly float[] mainBtn = new float[] { 3f, -1f, 0 };

    // 技能按鈕
    public static readonly List<float[]> skillBtn = new List<float[]> { 
        new float[]{-13f, 2.3f, 0}, new float[]{-9f, 2.3f, 0}, new float[]{-5f, 2.3f, 0}, new float[]{-1f, 2.3f, 0},
    };

    // 儲存塔表 & 點數表
    public static readonly float[] towerTable = new float[] { 10f, 2f, 0 };
    public static readonly float[] pointTable = new float[] { 10f, -0.5f, 0 };

    // 我方(敵方)角色隊伍欄位
    public static Vector3 getCharPlayerShowStatusPosition(int i) { return new Vector3(-12.2f, 4.5f - (i * 1.7f), 0); }
    public static Vector3 getCharPlayerHideStatusPosition(int i) { return new Vector3(-15.5f, 4.5f - (i * 1.7f), 0); }
    public static Vector3 getCharEnemyShowStatusPosition(int i) { return new Vector3(12.2f, 1.2f + (i * 1.7f), 0); }
    public static Vector3 getCharEnemyHideStatusPosition(int i) { return new Vector3(15.5f, 1.2f + (i * 1.7f), 0); }
    
    // 備用骰
    public static readonly float[] diceBoxPosition = new float[] { -13f, 6.3f, 0 };
    public static readonly List<List<float[]>> diceBoxModePosition = new List<List<float[]>> { 
        new List<float[]>{ new float[3] {-3,0,-2}, new float[3] {0,0,-1}, new float[3] {3,0,0} },
        new List<float[]>{ new float[3] {-1,0,-2}, new float[3] {0,0,-1}, new float[3] {1,0,0} },
        new List<float[]>{ new float[3] {-5,0,-2}, new float[3] {0,0,-1}, new float[3] {1,0,0} },
        new List<float[]>{ new float[3] {-5,0,-2}, new float[3]{-4,0,-1}, new float[3] {1,0,0} },
    };
    public static readonly float[] diceBoxLabelOriginPosition = new float[] { 1.3f, 0, 0 };
    public static readonly float[] diceBoxLabelCheckPosition = new float[] { -0.6f, 0, 0 };

    // 行動類型按鈕
    public static Vector3 getActionButtonPosition(int i) {
        return new Vector3( -13.5f + (i*2.2f), -0.5f, 0 );
    }

    //擲骰平面
    public static readonly float[] planePosition = new float[] { 0, 0, -10 };
    public static readonly float[] planeRotation = new float[] { -40, 0, 0 };
    public static readonly float[] planeScale = new float[] { 100, 100, 100 };
    // 骰子大小
    public static readonly float[] diceScale = new float[] { 1.8f, 1.8f, 1.8f };
    // 擲骰起始點
    public static Vector3 getThrowDicePosition(int i) {
        return new Vector3(-4 + ((i % 3) * 2f + (i / 3) * 2f), 10 + ((i / 3) * 2f), -18 + ((i / 3) * 2f));
    }
    // 擲骰完畢回收位置
    public static Vector3 getDiceCollectPosition(int i, int total) {
        return new Vector3(2f*( (i+0.5f) - (float)total/2 ), 4.5f, -1);
    }
    // 骰面選擇(行動點數)位置
    public static Vector3 getDiceFaceDecisionPosition(int i, int total) {
        return new Vector3(2f*( (i+0.5f) - (float)total / 2), 0, -1);
    }
    // 骰面選擇(建築點數)位置
    public static Vector3 getDiceBaseDesicionPosition(int i, int total) {
        return new Vector3(2f*( (i+0.5f) - (float)total / 2), 0, -1);
    }
    // 骰面選擇背景
    public static readonly float[] decisionAttrBackPosition = new float[] { 0, 4.5f, 0 };
    public static readonly float[] decisionBaseBackPosition = new float[] { 0, 0.5f, 0 };

    // 回合顯示圖位置
    public static readonly float[] turnStatusPosition = new float[] { 0, 2.5f, 0 };
    public static readonly float[] turnPlayerStatusPosition = new float[] { -2.2f, 0, 0 };
    public static readonly float[] turnEnemyStatusPosition  = new float[] {  2.2f, 0, 0 };
    public static readonly float[] turnStatusLabelCenterPosition = new float[] { 0, 1.5f, 0 };
}