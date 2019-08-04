using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DirectionalMovement : MonoBehaviour
{
	public Rigidbody body;
	public float thrustMax, thrustMin, accelPeriod;
	private float thrust, thrustTimer;
	public float speedModifier=1.2f;
    public float energy = 0.5f;
    public float energyFadeRate;
	public AudioManager mAudio;
    public Text energyText;
    public float fruitGains;
    public CanvasGroup fader;
    public Text hintText;
    
	// Start is called before the first frame update
    void Start()
    {
        energyText.text = "Energy: " + Mathf.FloorToInt(energy * 100) + "%";
        thrust = thrustMax;
		thrustTimer=0;
    }

    // Update is called once per frame
    void Update()
    {
		float vert = Input.GetAxis("Vertical");
		float hor = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(hor,0,vert);

		//if movement is released, reset thrust
		if(movement.sqrMagnitude < 0.1f){
			thrust=thrustMin;
			thrustTimer=0;
			mAudio.SetNone();
		}
		else{
			thrust = Mathf.Lerp(thrustMin, thrustMax, thrustTimer/accelPeriod);
			thrustTimer+=Time.deltaTime;
            energy -= Time.deltaTime* energyFadeRate;
            if(energy <= 0)
            {
                StartCoroutine(Death());
            }
            energyText.text = "Energy: " + Mathf.FloorToInt(energy * 100) + "%";
            if (thrustTimer>=accelPeriod)
				mAudio.SetWalk(speedModifier);
			else
				mAudio.SetWalk(0.6f);
			
		}
		//body.AddForce(Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up)*movement*thrust);
		transform.parent.position+=Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up)*movement*thrust*speedModifier*Time.deltaTime;
    }

    public void EatFruit()
    {
        energy += fruitGains;
        if(energy > 1)
        {
            energy = 1;
        }
        energyText.text = "Energy: " + Mathf.FloorToInt(energy * 100) + "%";
    }


    public IEnumerator Death()
    {
        int hintNo = Random.Range(0, 4);
        string hintString = "Hint: ";
        switch (hintNo)
        {
            case 0:
                hintString += "Stock up on apples to enable exploration in the off seasons";
                break;
            case 1:
                hintString += "Don't waste any energy. Once your trees are planted, take a meditation break";
                break;
            case 2:
                hintString += "Consume apples sparingly. You can't have more than 100% energy";
                break;
            case 3:
                hintString += "Apples only yield during the summer and autumn seasons.";
                break;
            default:
                break;
        }
        hintText.text = hintString;
        float timer = 0;
        while (timer < 3f)
        {
            fader.alpha = timer / 3f;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartMenu");
    }
}
