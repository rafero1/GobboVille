﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
	public Transform camPivot;
	public Vector3 followCameraDistance = new Vector3(0, 15, -15);
	public bool isFollowing;
	public Transform agent;
	public int agentIndex;
	private GameController game;
	void Start () {
		game = GameController.GetGameController ();
		camPivot = transform.parent;
		isFollowing = false;
	}

	void Update () {
		if (isFollowing) {
			transform.position = agent.position + followCameraDistance;
		}
	}

	public void SwitchCamera () {
		if (isFollowing) {
			transform.position = camPivot.position;
			transform.rotation = camPivot.rotation;
			isFollowing = false;
		} else {
			if (!agent) {
				agent = game.getAgent (0).transform;
				agentIndex = 0;
			}
			isFollowing = true;
		}
	}

	public void PreviousTarget () {
		if (isFollowing) {
			try {
				agent = game.getAgent (agentIndex - 1).transform;
			} catch (System.Exception) {
				return;
			}
			agentIndex--;
		}
	}

	public void NextTarget () {
		if (isFollowing) {
			try {
				agent = game.getAgent (agentIndex + 1).transform;

			} catch (System.Exception) {
				return;
			}
			agentIndex++;
		}
	}
}