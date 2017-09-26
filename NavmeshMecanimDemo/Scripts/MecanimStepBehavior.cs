using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AAgent))]
public class MecanimStepBehavior : AAnimatorBehavior
{
	private UnityEngine.AI.NavMeshAgent agent;
	private int recording = -1;

	void Initialize ()
	{
		recording = -1;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void Update ()
	{

		if (AttachedAgent.World && !AttachedAgent.World.timeTicking) {
			if(recording < 0)
				return;

			//print (""+recording+" : "+(avatar.recorderStopTime - avatar.recorderStartTime));
			if (recording >= 0 && recording < 1) {
				recording++;
			}else if (recording >= 1) {
				Avatar.StopRecording ();
				Avatar.StartPlayback ();
				Avatar.speed = 0;

				if (agent) {
					agent.updateRotation = false;
					agent.updatePosition = false;
				}

				recording = -1;
			}
		}
	}

	void Step ()
	{
		if (recording == -1) {
			Avatar.StopPlayback ();

			if (agent) {
				agent.updateRotation = true;
				agent.updatePosition = true;
			}

			Avatar.StartRecording (1);
			Avatar.speed = 1;
			recording = 0;
		}

		//avatar.SetFloat ("Speed", 1, 0.25f, Time.deltaTime);
		//avatar.speed = 1;

	}
	
	void End ()
	{
		//avatar.SetFloat ("Speed", 0);
		Avatar.StartPlayback ();
		Avatar.speed = 0;
		recording = -1;
	}

}
