using UnityEngine;
using System.Collections;

public class AnimatorDelegate : MonoBehaviour {

	public GameObject target;

	void OnAnimatorMove ()
	{
		if(target)
			target.SendMessage ("OnAnimatorMove", SendMessageOptions.DontRequireReceiver);
	}
}
