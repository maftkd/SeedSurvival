using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public CanvasGroup settingsScreen,gameOverScreen;
    public AudioManager mAudio;
	private bool dead=false;
    public Transform treePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && !dead){
            if (settingsScreen.alpha == 0)
            {
                transform.GetComponent<DirectionalMovement>().enabled = false;
                transform.GetComponent<MouseLook>().enabled = false;
                mAudio.SetNone();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                settingsScreen.alpha = 1;
            }
            else
            {
                CloseSettings();
            }
		}
    }

	public void CloseSettings(){
		
		transform.GetComponent<DirectionalMovement>().enabled=true;
		transform.GetComponent<MouseLook>().enabled=true;
		Cursor.visible=false;
		Cursor.lockState= CursorLockMode.Locked;
		settingsScreen.alpha=0;
	}

	public void GameOver(){
		transform.GetComponent<DirectionalMovement>().enabled=false;
		transform.GetComponent<MouseLook>().enabled=false;
		Cursor.visible=true;
		Cursor.lockState = CursorLockMode.None;
		gameOverScreen.alpha=1;
		dead=true;
		StartCoroutine(ResetRoutine());
	}
	
	private IEnumerator ResetRoutine(){
		float timer=0;
		yield return new WaitForSeconds(2f);
		while(timer<28){
			timer+=Time.deltaTime;
			if(Input.anyKeyDown){
				break;
			}
			yield return null;
		}
		print("return to main menu");
		SceneManager.LoadScene("StartMenu");
	}

	public void ResetGame(){
		//SceneManager.LoadScene("MainMenu");
		
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        GameStateData gsd = new GameStateData();
        
        //put all tree data into an array of FruitTreeData
        GameObject [] trees = GameObject.FindGameObjectsWithTag("Tree");
        FruitTreeData[] treeData = new FruitTreeData[trees.Length];
        for(int i=0; i<treeData.Length; i++)
        {
            treeData[i] = trees[i].GetComponent<FruitTree>().Serialize();
        }
        //create a new GameStateData
        gsd.trees = treeData;
        ItemSelector item = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemSelector>();
        //fill in da blanks
        gsd.seedCount = item.numSeeds;
        gsd.fruitCount = item.numFruit;
        gsd.playerEnergy = transform.GetComponent<DirectionalMovement>().energy;
        gsd.playerPos = transform.position;
        gsd.playerRot = Camera.main.transform.rotation;
        DayCycle day = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayCycle>();
        gsd.seasonCode = day.seasonCode;
        gsd.dayCode = day.dayCode;
        //serialize GameStateData to json
        string path = Application.persistentDataPath + "/saveData.json";

        //write json string to file in persistantDataPath
        File.WriteAllText(path, JsonUtility.ToJson(gsd));
        Debug.Log("Save success");
    }

    public void LoadGame(GameStateData gsd)
    {
        
        foreach(FruitTreeData tree in gsd.trees)
        {
            Transform newTree = Instantiate(treePrefab,tree.myPos,Quaternion.identity);
            FruitTree ft = newTree.GetComponent<FruitTree>();
            ft.Deserialize(tree);
        }
        
        ItemSelector item = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemSelector>();
        item.numSeeds = gsd.seedCount;
        item.numFruit = gsd.fruitCount;
        transform.GetComponent<DirectionalMovement>().energy = gsd.playerEnergy;
        transform.position = gsd.playerPos;
        Camera.main.transform.rotation = gsd.playerRot;
        DayCycle day = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayCycle>();
        day.seasonCode = gsd.seasonCode;
        day.dayCode = gsd.dayCode;
        
    }
}
