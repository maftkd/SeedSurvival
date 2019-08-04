using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FruitTreeData
{
    public Text debugText;
    public Transform branchPrefab;
    public float breakTime;
    public float branchTime;
    public float trunkAxisRandom, branchAxisRandom;
    public int branchRate;
    public float growthRateDelay;
    public Vector3 branchScaleVector;
    public float branchGrowthScale;
    public float globalGrowthRate;
    private bool alive = true;
    private bool growing = false;
    private bool fruiting = false;
    private bool hasTrunk = false;
    public List<Transform> liveBranches, allBranches;
}
