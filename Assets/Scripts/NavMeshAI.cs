using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshAI : MonoBehaviour {

	public Transform target;
	public Transform home;	
	UnityEngine.AI.NavMeshAgent agent;

	void Start () {  
		agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();  
	}  

	void Update () {  
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		agent.SetDestination(player.transform.position);

		if (Vector3.Distance(agent.transform.position, player.transform.position) < 100) { 
			target = player.transform; 
		} else {
			target = home; 
		}
	}

	void Awake() {
		home = transform.parent.transform;
	}
}
