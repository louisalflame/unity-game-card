using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position {
    public static Vector3 getVector3(float[] arr) { return new Vector3(arr[0], arr[1], arr[2]); }

    public static readonly float[] systemBtn = new float[] { -13f, 0, 0};
    public static readonly float[] mainBtn = new float[] { 3f, -1f, 0 };

    public static readonly List<float[]> skillBtn = new List<float[]> { 
        new float[]{-13f, 2.3f, 0}, new float[]{-9f, 2.3f, 0}, new float[]{-5f, 2.3f, 0}, new float[]{-1f, 2.3f, 0},
    };

    public static readonly float[] towerTable = new float[] { 10f, 2f, 0 };
    public static readonly float[] pointTable = new float[] { 10f, -0.5f, 0 };

    public static readonly List<float[]> charStatusPosition = new List<float[]> {
        new float[]{-12.2f, 4.5f, 0}, new float[]{-15.5f, 2.75f, 0}, new float[]{-15.5f, 1.0f, 0}, new float[]{-15.5f, -0.75f, 0},
    };
    public static readonly List<float[]> charEnemyStatusPosition = new List<float[]> {
        new float[]{12.2f, 1.2f, 0},  new float[]{15.5f, 2.9f, 0},   new float[]{15.5f, 4.6f, 0},  new float[]{15.5f, 6.3f, 0},
    };
    
    public static readonly float[] diceBoxPosition = new float[] { -13f, 6.3f, -1f };
    public static readonly List<List<float[]>> diceBoxModePosition = new List<List<float[]>> { 
        new List<float[]>{ new float[3] {-3,0,0}, new float[3] {0,0,0}, new float[3] {3,0,0} },
        new List<float[]>{ new float[3] {-1,0,0}, new float[3] {0,0,0}, new float[3] {1,0,0} },
        new List<float[]>{ new float[3] {-5,0,0}, new float[3] {0,0,0}, new float[3] {1,0,0} },
        new List<float[]>{ new float[3] {-5,0,0}, new float[3]{-4,0,0}, new float[3] {1,0,0} },
    };
    public static readonly float[] diceBoxLabelOriginPosition = new float[] { 1.3f, 0, 0 };
    public static readonly float[] diceBoxLabelCheckPosition = new float[] { -0.6f, 0, 0 };

    public static Vector3 getActionButtonPosition(int i) {
        return new Vector3( -13.5f + (i*2.2f), -0.5f, 0 );
    }
}