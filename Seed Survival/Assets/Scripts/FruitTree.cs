using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitTree : MonoBehaviour
{
    private bool alive = true;
    private bool growing = false;
    private bool fruiting = false;
    private bool hasTrunk = false;
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
    public List<Transform> liveBranches, allBranches;


    private MeditationManager mMan;
    // Start is called before the first frame update
    void Start()
    {
        liveBranches = new List<Transform>();
        allBranches = new List<Transform>();
        debugText.text = "Planted";
        mMan = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<MeditationManager>();
        StartCoroutine(BreakSurface());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(GlobalTreeGrowth());
    }

    private IEnumerator CreateBranch(float growTime, Vector3 startPos, Quaternion startRot)
    {
        if (hasTrunk)
            Debug.Log("Creating branch");
        else
            Debug.Log("Creating trunk");
        //instantiate branch prefab at startPos and startRotation
        Transform branch = Instantiate(branchPrefab, startPos, startRot,transform);
        //track alive branches for saving and loading
        liveBranches.Add(branch);

        Vector3 startScale = branch.localScale;
        Vector3 endScale = startScale + branchScaleVector * branchGrowthScale;
        
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
        
        for (int i = 0; i < branchRate; i++)
        {
            Vector3 euler = startRot.eulerAngles;
            Quaternion newRot = Quaternion.Euler(euler.x + Random.Range(-branchAxisRandom, branchAxisRandom), euler.y + Random.Range(-branchAxisRandom, branchAxisRandom),
                euler.z + Random.Range(-branchAxisRandom, branchAxisRandom));
            StartCoroutine(CreateBranch(growTime * growthRateDelay, branch.position + branch.up * branch.localScale.y*transform.localScale.y*0.01f, newRot));
        }
        //pop the branch from the live branches
        liveBranches.Remove(branch);
        allBranches.Add(branch);
    }

    private IEnumerator GlobalTreeGrowth()
    {
        while (alive)
        {
            transform.localScale += Vector3.one * globalGrowthRate * Time.deltaTime * mMan.timeScale;
            yield return null;
        }        
    }
}