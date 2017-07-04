using UnityEngine;
using System.Collections;
using System.Linq;

public class WalkerModelChanger : MonoBehaviour {

	public int num;
	private int currentCharacter = -1;

	public WalkerFactory factory;

	public RuntimeAnimatorController controller;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
//	void Update () {
//		ChangeCharacter (num);
//	}

	public void UpdateCharacter(){
		ChangeCharacter (num);
	}

	void ChangeCharacter(int n){

		if (currentCharacter == n)
			return;

		if (factory == null || !factory.HasAWalker(n)) {
			return;
		}
		
		currentCharacter = n;

		Animator current = GetComponentInChildren<Animator> ();
		Debug.Log (current);

		Transform parent = transform;
		Quaternion orgRot = transform.localRotation;

		if (current != null) {
			parent = current.transform.parent;
			orgRot = current.transform.localRotation;
			DestroyImmediate (current.gameObject);
		}

		AAgent agent2 = factory.GetAWalker (n);
		Animator nAnim = agent2.GetComponentInChildren<Animator> ();

		nAnim.runtimeAnimatorController = controller;
		nAnim.transform.SetParent (parent);
		nAnim.transform.localPosition = Vector3.zero;
		nAnim.transform.localRotation = orgRot;

		DestroyImmediate (agent2.gameObject);

		AnimatorDelegate ad = nAnim.gameObject.AddComponent<AnimatorDelegate> ();
		ad.target = gameObject;

		MonoBehaviour[] monos = gameObject.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour mono in monos) {
			mono.SendMessage ("OnModelChanged",nAnim, SendMessageOptions.DontRequireReceiver);
		}

	}
}
