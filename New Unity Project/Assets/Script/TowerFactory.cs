using UnityEngine;
using System.Collections;

public class TowerFactory {
    public TowerFactory() { }
    public static Tower createTower() {
        return new Tower();
    }
}

public class Tower{}
