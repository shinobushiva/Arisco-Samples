using UnityEngine;
using System.Collections;
using System.Linq;

public class WalkerModelChanger : MonoBehaviour {

	public int num;
	private int currentCharacter = -1;

	public Animator[] characters;

	public RuntimeAnimatorController controller;

	// Use this for initialization
	void Start () {
//		ChangeCharacter (num);
	}
	
	// Update is called once per frame
	void Update () {
		ChangeCharacter (num);
	}

	void ChangeCharacter(int n){

		if (currentCharacter == n)
			return;

		if (n > characters.Length - 1) {
			return;
		}
		
		currentCharacter = n;

		Animator current = GetComponentInChildren<Animator> ();
//		print (current);
		if (current != null) {
			Transform parent = current.transform.parent;
			Quaternion orgRot = current.transform.localRotation;
			Destroy (current.gameObject);
//			print("Destory:"+current.gameObject);

			Animator nAnim = Instantiate<Animator> (characters [n]);
			nAnim.runtimeAnimatorController = controller;
			nAnim.transform.SetParent (parent);
			nAnim.transform.localPosition = Vector3.zero;
			nAnim.transform.localRotation = orgRot;

			AnimatorDelegate ad = nAnim.gameObject.AddComponent<AnimatorDelegate> ();
			ad.target = gameObject;

			MonoBehaviour[] monos = gameObject.GetComponents<MonoBehaviour> ();
			foreach (MonoBehaviour mono in monos) {
				mono.SendMessage ("OnModelChanged",nAnim, SendMessageOptions.DontRequireReceiver);
			}

		}
	}
}
