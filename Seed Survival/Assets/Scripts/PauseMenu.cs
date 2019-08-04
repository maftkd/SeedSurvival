using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public CanvasGroup settingsScreen,gameOverScreen;
    public AudioManager mAudio;
	private bool dead=false;
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
}
