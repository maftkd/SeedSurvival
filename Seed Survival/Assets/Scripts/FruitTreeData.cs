using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FruitTreeData
{
    public Vector3 myPos;
    public Vector3 myScale;
    public int branchType;
    public float breakTime;
    public float branchTime;
    public float trunkAxisRandom, branchAxisRandom;
    public int branchRate;
    public float growthRateDelay;
    public Vector3 branchScaleVector;
    public float branchGrowthScale;
    public float globalGrowthRate;
    public int maxBranchLevel;
    public bool alive = true;
    public bool growing = false;
    public bool fruiting = false;
    public bool hasTrunk = false;
    public List<Vector3> liveBranchPositions, allBranchPositions;
    public List<Quaternion> liveBranchRotations, allBranchRotations;
    public List<Vector3> allBranchScales;
}
