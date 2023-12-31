using System.Collections;
using System.Collections.Generic
using UnityEngine;

public class Playercam : MonoBehavior
{
public float sensX;
public float sensY;

public Transform orientation;

float xRotation;
float yRotation;

private void start()

{

	Cursor.lockstate = CursorLockMode.Locked;
	Cursor.visible = false;

}

	private void Update()

	{

		float mouseX = input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;	

		yRotation += mouseX;

		xRotation += mousey;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		orientation.rotation = Quaternion.Euler(0, yRotation 0);
		
	}
}
