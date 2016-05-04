/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This behaviour associates a Virtual Button with a game object. Use the
    /// functionality in ImageTargetBehaviour to create and destroy Virtual Buttons
    /// at run-time.
    /// </summary>
	public class VirtualButtonBehaviour : VirtualButtonAbstractBehaviour
    {
		public GameObject Player;
		public Rigidbody player;
		private float timer =0.0f;
		private bool isTimerValid=false;
		void Start(){
			VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour> ();
			for (int i = 0; i < vbs.Length; ++i) {
		//		vbs[i].RegisterEventHandler (this);
			}

			Player = GameObject.Find ("Player");
			player = Player.GetComponent<Rigidbody>();
		}

		public void Update(){
			if (isTimerValid)
			{
				timer -= Time.deltaTime;
				if (timer <= 0.0f)
				{
					isTimerValid = false;
				}
				player.AddRelativeForce (new Vector3 (0f, 10f, 0f));
			}

		}

		public void OnButtonPressed(VirtualButtonBehaviour vb){
			isTimerValid = true;
			timer = 2.0f;
		}
    }
}
