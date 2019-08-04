using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitTree : MonoBehaviour
{
    public bool alive = true;
    public bool growing = false;
    public bool fruiting = false;
    public bool hasTrunk = false;
    private enum LeafStates { NONE, GROWING, CHILLING, FALLING };
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
    public int maxBranchLevel;
    public List<Vector3> liveBranchPositions, allBranchPositions, oldBranchPositions;
    public List<Quaternion> liveBranchRotations, allBranchRotations, oldBranchRotations;
    public List<Vector3> allBranchScales;

    private bool loaded = false;

    private DayCycle dayCycle;
    private MeditationManager mMan;
    // Start is called before the first frame update
    void Start()
    {
        mMan = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<MeditationManager>();
        dayCycle = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayCycle>();

        if (!loaded)
        {
            liveBranchPositions = new List<Vector3>();
            allBranchPositions = new List<Vector3>();
            liveBranchRotations = new List<Quaternion>();
            allBranchRotations = new List<Quaternion>();
            allBranchScales = new List<Vector3>();
            debugText.text = "Planted";
            
            StartCoroutine(BreakSurface());
        }
        else
        {
            //do stuff
            
            if (allBranchPositions.Count < 1)
            {
                liveBranchPositions.Clear();
                liveBranchRotations.Clear();
                StartCoroutine(BreakSurface());                
            }
            else
            {
                for(int i=0; i<allBranchPositions.Count; i++)
                {
                    CreateBranchInstantly(allBranchPositions[i], allBranchRotations[i], allBranchScales[i]);
                }
                int branchLevel = allBranchPositions.Count / branchRate + 1;
                float growTime = branchTime * Mathf.Pow(growthRateDelay, branchLevel);
                for (int i=0; i<oldBranchPositions.Count; i++)
                {
                    StartCoroutine(CreateBranch(growTime, oldBranchPositions[i], oldBranchRotations[i]));
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += Vector3.one * globalGrowthRate * Time.deltaTime * mMan.timeScale;
        
    }

    private IEnumerator BreakSurface()
    {
        float timer = 0;
        while(timer < breakTime)
        {
            timer += Time.deltaTime * mMan.timeScale;
            yield return null;
        }
        debugText.text = "Broke surface";
        StartCoroutine(CreateBranch(branchTime,transform.position, Quaternion.Euler(Random.Range(-trunkAxisRandom, trunkAxisRandom),0,Random.Range(-trunkAxisRandom, trunkAxisRandom))));        
    }

    private IEnumerator CreateBranch(float growTime, Vector3 startPos, Quaternion startRot)
    {
        //instantiate branch prefab at startPos and startRotation
        Transform branch = Instantiate(branchPrefab, startPos, startRot,transform);
        
        //track alive branches for saving and loading
        if (hasTrunk)
        {
            liveBranchPositions.Add(branch.position);
            liveBranchRotations.Add(branch.rotation);
        }

        int branchLevel;
        if (!hasTrunk)
        {
            branchLevel = 1;
        }
        else
        {
            branchLevel = allBranchPositions.Count / branchRate + 2;
        }
        Vector3 startScale = branch.localScale;
        Vector3 endScale = startScale + branchScaleVector * branchGrowthScale*(1f-(float)branchLevel/(float)10);

        float timer = 0;
        while(timer < growTime)
        {
            
            float growthDelta = Time.deltaTime * mMan.timeScale;
            //scale branch by growth delta
            branch.localScale = Vector3.Lerp(startScale, endScale, timer / growTime);
            //track growth
            timer += growthDelta;
            
            yield return null;
        }
        if (!hasTrunk)
        {
            debugText.text = "Branching";
            hasTrunk = true;
        }
        else
        {
            liveBranchPositions.RemoveAt(liveBranchPositions.Count - 1);
            liveBranchRotations.RemoveAt(liveBranchRotations.Count - 1);
        }

        
        if (branchLevel <= maxBranchLevel)
        {
            for (int i = 0; i < branchRate; i++)
            {
                Vector3 euler = startRot.eulerAngles;
                Quaternion newRot = Quaternion.Euler(euler.x + Random.Range(-branchAxisRandom, branchAxisRandom), euler.y + Random.Range(-branchAxisRandom, branchAxisRandom),
                    euler.z + Random.Range(-branchAxisRandom, branchAxisRandom));
                StartCoroutine(CreateBranch(growTime * growthRateDelay, branch.position + branch.up * branch.localScale.y * transform.localScale.y * 0.01f, newRot));
            }
        }
        //pop the branch from the live branches        
        allBranchPositions.Add(branch.localPosition);
        allBranchRotations.Add(branch.localRotation);
        allBranchScales.Add(branch.localScale);
    }

    private void CreateBranchInstantly(Vector3 pos, Quaternion rot, Vector3 localS)
    {
        Transform branch = Instantiate(branchPrefab, transform);
        branch.localPosition = pos;
        branch.localRotation = rot;
        branch.localScale = localS;
    }

    public FruitTreeData Serialize()
    {
        FruitTreeData mData = new FruitTreeData();
        mData.myPos = transform.position;
        mData.myScale = transform.localScale;
        mData.branchType = 0; //use this later when we have different types of branches
        mData.breakTime = breakTime;
        mData.branchTime = branchTime;
        mData.trunkAxisRandom = trunkAxisRandom;
        mData.branchAxisRandom = branchAxisRandom;
        mData.branchRate = branchRate;
        mData.growthRateDelay = growthRateDelay;
        mData.branchScaleVector = branchScaleVector;
        mData.branchGrowthScale = branchGrowthScale;
        mData.globalGrowthRate = globalGrowthRate;
        mData.alive = alive;
        mData.growing = growing;
        mData.fruiting = fruiting;
        mData.hasTrunk = hasTrunk;
        mData.liveBranchPositions = liveBranchPositions;
        mData.liveBranchRotations = liveBranchRotations;
        mData.allBranchPositions = allBranchPositions;
        mData.allBranchRotations = allBranchRotations;
        mData.allBranchScales = allBranchScales;
        mData.maxBranchLevel = maxBranchLevel;

        return mData;
    }

    public void Deserialize(FruitTreeData mData)        
    {
        transform.localScale = mData.myScale;
        breakTime = mData.breakTime;
        branchTime = mData.branchTime;
        trunkAxisRandom = mData.trunkAxisRandom;
        branchAxisRandom = mData.branchAxisRandom;
        branchRate = mData.branchRate;
        growthRateDelay = mData.growthRateDelay;
        branchScaleVector = mData.branchScaleVector;
        branchGrowthScale = mData.branchGrowthScale;
        globalGrowthRate = mData.globalGrowthRate;
        alive = mData.alive;
        growing = mData.growing;
        fruiting = mData.fruiting;
        hasTrunk = mData.hasTrunk;
        oldBranchPositions = mData.liveBranchPositions;
        oldBranchRotations = mData.liveBranchRotations;
        allBranchPositions = mData.allBranchPositions;
        allBranchRotations = mData.allBranchRotations;
        allBranchScales = mData.allBranchScales;
        maxBranchLevel = mData.maxBranchLevel;

        loaded = true;
    }
}