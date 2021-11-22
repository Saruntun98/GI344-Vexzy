using UnityEngine;
using System.Collections;

public class RootsNature : MonoBehaviour {

	public float rootEnable = 2f;
	public float rootDisable = 3f;
	public GameObject[] roots;

	void OnEnable () {
		Invoke ("RootsEnable", rootEnable);
	}

	void RootsEnable(){
		for (int i = 0; i < roots.Length; i++) {
			roots [i].SetActive (true);
		}
		if (gameObject.activeInHierarchy)
		StartCoroutine ("RootsDisable");
	}

	IEnumerator RootsDisable(){
		
		yield return new WaitForSeconds (rootDisable);

		for (int i = 0; i < roots.Length; i++) {
			roots [i].transform.GetChild(0).GetComponent<Animator> ().Play ("rootDissapear");
		}

		yield return new WaitForSeconds (1.5f);

		for (int i = 0; i < roots.Length; i++) {
			roots [i].SetActive (false);
		}
	}
}
