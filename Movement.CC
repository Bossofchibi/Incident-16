using System.Collections;
using System.Collections.Generic
using UnityEngine;

public class Playercam : MonoBehavior

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
