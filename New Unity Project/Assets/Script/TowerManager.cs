using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//儲存塔管理
public class TowerManager {
    public BattleController _battle;
    public AttrTower[] _towers { get; private set; }
    public int[] _attrNums {get; private set;}
    public int[] _attrMax { get; private set;} 
    public TowerManager(BattleController battle) {
        _battle = battle;
        _towers = new AttrTower[6] { new AttrTower(0), new AttrTower(1), new AttrTower(2), 
                                     new AttrTower(3), new AttrTower(4), new AttrTower(5) };
        _attrNums = new int[6] { 0, 0, 0, 0, 0, 0 };
        _attrMax = new int[6] { 0, 0, 0, 0, 0, 0 };
    }


    public void collectAttrPoint(List<DiceFace> faces) {
        foreach (DiceFace face in faces) {
            _attrNums[face._attr] += face._num;
        }
    }
    public void countAttrMax() {
        _attrMax = new int[6] { 5, 5, 5, 5, 5, 5 };
        foreach (AttrTower t in _towers) {
            if (t._level > 0) _attrMax[t._attr] += t._capacity;
        }
    }

    public void collectBasePoint(List<DiceFace> bases) {
        // 以玩家選擇的屬性建造點數排列優先順序:  點數高且權重高的點數優先
        List<AttrBaseCounter> counters = findAttrPriority(bases);
        // 以塔的等級排列優先順序: 等級高的塔優先檢查點數是否足夠升級
        List<List<AttrTower>> towerGroups = findTowerPriority();
        // 以塔的優先順序比對屬性的優先順序決定升級哪座塔
        upgradeMostLevelValidTower(counters, towerGroups);
        // 升級/興建完後，更新屬性塔的容量
        countAttrMax();
    }
    public void upgradeMostLevelValidTower(List<AttrBaseCounter> counters, List<List<AttrTower>> towerGroups) {        
        //先按照塔等級的順序，越高等級的塔越優先
        foreach (List<AttrTower> levelTowers in towerGroups) {
            //同等級的塔，以點數加權大者優先
            foreach (AttrBaseCounter counter in counters) { 
                //如果此屬性沒有建造點數，跳過
                if (counter._base <= 0) continue;
                //尋找此等級的塔是否有點數可足夠升級的屬性
                foreach (AttrTower t in levelTowers) {  
                    // 如果此屬性和塔相同且足夠升級 => 開始升級
                    if(t._attr == counter._attr && t.isValidUpgrade(counter) ) {
                        t.upgrade(counter); 
                        return; 
                    }
                    //如果此塔沒有等級(還未興建)且屬性足夠 => 開始興建
                    else if (t._level == 0 && t.isValidBuild(counter)) {
                        t.build(counter);
                        return;
                    }
                }
            }
        }
    }
    public List<AttrBaseCounter> findAttrPriority(List<DiceFace> bases) {
        //先整理所有升級用點數和其屬性
        AttrBaseCounter[] counters = new AttrBaseCounter[6] { 
            new AttrBaseCounter(Attribute.Normal), new AttrBaseCounter(Attribute.Attack), new AttrBaseCounter(Attribute.Deffense),
            new AttrBaseCounter(Attribute.Move), new AttrBaseCounter(Attribute.Special), new AttrBaseCounter(Attribute.Health) };
        List<AttrBaseCounter> countersWithWeight = new List<AttrBaseCounter>();
        List<AttrBaseCounter> countersWithoutWeight = new List<AttrBaseCounter>();
        List<AttrBaseCounter> countersSorted = new List<AttrBaseCounter>();
        //先累計所有點數和加倍點數
        foreach (DiceFace face in bases) {
            counters[face._base]._base += 1;
            counters[face._base]._weight += face._baseWeight;
        }
        //分為有無weight兩部分，再排序，以降冪
        foreach (AttrBaseCounter counter in counters) {
            if (counter._weight != 0) countersWithWeight.Add(counter);
            else countersWithoutWeight.Add(counter);
        }
        countersWithWeight.Sort((x, y) => y._base.CompareTo(x._base));
        countersWithoutWeight.Sort((x, y) => y._base.CompareTo(x._base));
        //有weight在前，無weight在後
        foreach (AttrBaseCounter c in countersWithWeight) { countersSorted.Add(c); }
        foreach (AttrBaseCounter c in countersWithoutWeight) { countersSorted.Add(c); }
        return countersSorted;
    }
    public List<List<AttrTower>> findTowerPriority() {
        AttrTower[] tempTowers = new AttrTower[6] { null, null, null, null, null, null };
        //先複製陣列，讓塔sort by lv，升級順序降冪以高lv優先
        Array.Copy(_towers, 0, tempTowers, 0, _towers.Length);
        Array.Sort(tempTowers, (x,y)=>y._level.CompareTo(x._level) );
        List<List<AttrTower>> towerGroups = new List<List<AttrTower>>();
        List<AttrTower> levelGroup = new List<AttrTower>();
        foreach (AttrTower t in tempTowers) { 
            if (levelGroup.Count == 0 || t._level == levelGroup[0]._level) {
                levelGroup.Add(t);
            } else {
                levelGroup.Sort((x,y)=>x._positionID.CompareTo(y._positionID));
                towerGroups.Add(levelGroup);
                levelGroup = new List<AttrTower>() {t};
            }
        }
        if (levelGroup.Count != 0) {
            //同等級以位置左->右排列
            levelGroup.Sort((x,y)=>x._positionID.CompareTo(y._positionID));
            towerGroups.Add(levelGroup);        
        }

        return towerGroups;
    }

    public void filterAttrPoints() {
        for (int i = 0; i < _attrNums.Length && i < _attrMax.Length; i++) {
            _attrNums[i] = Mathf.Min( _attrNums[i], _attrMax[i] );
        }
    }
}
