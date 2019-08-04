using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
	public float sensitivity, maxAngle, mouseClamp, smoothVal;
    public float inverted = 1f;

    //cam tilt component
    public float smooth = 3.0f;
    public float tiltAngle = 5.0f;
    public float tiltAngleY = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible=false;
		StartCoroutine(ResetCamera());		
    }

	private float mouseX, mouseY;

    // Update is called once per frame
    void Update()
    {
        mouseX = Mathf.Lerp(mouseX, Input.GetAxis("Mouse X"),smoothVal);//smoothMouse.x;
		mouseY = Mathf.Lerp(mouseY, Input.GetAxis("Mouse Y")*inverted,smoothVal);//smoothMouse.y;        
        transform.localEulerAngles += new Vector3(-mouseY, mouseX,0)*sensitivity*Time.deltaTime;

        float camX = transform.localEulerAngles.x;
		//print("camX: "+camX);
		if(camX>maxAngle && camX<180f){
			transform.localEulerAngles = new Vector3(maxAngle, transform.localEulerAngles.y, 0);
			print("camera too low");
		}
		else if(camX<-maxAngle && camX >-180f){
			transform.localEulerAngles = new Vector3(-maxAngle, transform.localEulerAngles.y, 0);	
			print("camera too high");
		}
		else if(camX<360-maxAngle && camX>180f){
			transform.localEulerAngles = new Vector3(-maxAngle, transform.localEulerAngles.y, 0);	
			print("camera too high");
		}	
    }

	private IEnumerator ResetCamera(){
		yield return new WaitForSeconds(0.1f);
		transform.eulerAngles = Vector3.zero;
	}

    public void AdjustSensitivity(Slider s)
    {
        this.sensitivity = 400f * s.value;
    }

}
