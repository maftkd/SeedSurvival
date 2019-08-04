using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalMovement : MonoBehaviour
{
	public Rigidbody body;
	public float thrustMax, thrustMin, accelPeriod;
	private float thrust, thrustTimer;
	public float speedModifier=1.2f;
    private float energy = 0.5f;
    public float energyFadeRate;
	public AudioManager mAudio;
    public Text energyText;
    
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
			if(thrustTimer>=accelPeriod)
				mAudio.SetWalk(speedModifier);
			else
				mAudio.SetWalk(0.6f);
			
		}
		//body.AddForce(Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up)*movement*thrust);
		transform.parent.position+=Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up)*movement*thrust*speedModifier*Time.deltaTime;
    }
}
