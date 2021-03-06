﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFollower : MonoBehaviour
{

	private const float ROTATE_SPEED = 5f;
	private const float DIST_NORM = -4.5f;
	private const float DIST_MIN = -1.5f;
	private const float ZOOM_SPEED = 5f;

	public GameObject player;

	private Vector3 desiredPosition;
	private Quaternion desiredRotation;
	private Vector3 cameraOffset = new Vector3 (0f, 1.5f, DIST_NORM);


	void Update ()
	{
		if (player) {

			CheckObstacles ();
			desiredPosition = player.transform.TransformPoint (cameraOffset);
			desiredRotation = player.transform.rotation;

			transform.rotation = Quaternion.Lerp (transform.rotation, desiredRotation, Time.deltaTime * ROTATE_SPEED);
			transform.position = desiredPosition;
		}
	}


	void CheckObstacles() {

		Vector3 desPos = player.transform.TransformPoint (cameraOffset);

		// check zoom out to in
		if (Physics.CheckSphere(desPos, 0.5f)) {
			if (cameraOffset.z == DIST_MIN) {
				return;
			}

			cameraOffset.z += Time.deltaTime * ZOOM_SPEED;
			if (cameraOffset.z > DIST_MIN) {
				cameraOffset.z = DIST_MIN;
			}
		} else 
		// check zoom in to out
		if (cameraOffset.z > DIST_NORM) {
				
			Vector3 pos = new Vector3 (cameraOffset.x, cameraOffset.y, cameraOffset.z);
			pos.z -= Time.deltaTime * ZOOM_SPEED;
			if (pos.z < DIST_NORM) {
				pos.z = DIST_NORM;
			}

			desPos = player.transform.TransformPoint (pos);

			if (!Physics.CheckSphere (desPos, 0.5f)) {
				cameraOffset = pos;
			}
		}
		
	}




}
