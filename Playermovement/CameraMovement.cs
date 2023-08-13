using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehavior
{
	public Transform cameraPosition;

	private void Update()
	{
		transform.position = cameraPosition.position;
	}
}
//To Ensure the camera moves with the player
