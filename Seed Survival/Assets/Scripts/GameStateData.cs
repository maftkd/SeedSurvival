using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameStateData
{
    public FruitTreeData[] trees;
    public int seedCount;
    public int fruitCount;
    public float playerEnergy;
    public Vector3 playerPos;
    public Quaternion playerRot;
    public int seasonCode;
    public int dayCode;
}
