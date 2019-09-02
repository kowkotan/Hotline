using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {
	
	public float lifeTime;

	void Start() {
		StartCoroutine(ShellDestroy());
	}

	IEnumerator ShellDestroy() {
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}

}