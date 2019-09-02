using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {
	
	public enum GunType {Auto, Burst, Semi};

	public LayerMask collisionMask;
	public GunType gunType;
	private AudioSource playing;
	public float rpm;
	public float damage;

	public Transform spawn;
	public Transform shellDropPoint;
	public Rigidbody shell;
	private LineRenderer tracer;

	private float secondsBetweenShots;
	private float nextShootTime;

	void Start() {
		secondsBetweenShots = 60 / rpm;
		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer> ();
		}
		playing = GetComponent<AudioSource> ();
	}

	public void Shoot() {

		if (CanShoot()) {
		Ray ray = new Ray(spawn.position, spawn.forward);
		RaycastHit hit;

		float shotDistance = 20;

			if (Physics.Raycast(ray, out hit, shotDistance, collisionMask)) {
			shotDistance = hit.distance;

				if (hit.collider.GetComponent<Entity>()) {
					hit.collider.GetComponent<Entity>().TakeDamage(damage);
				}
		}

			nextShootTime = Time.time + secondsBetweenShots;

			playing.Play();

			if (tracer) {
				StartCoroutine("RenderTracer", ray.direction * shotDistance);
			}

			Rigidbody newShell = Instantiate(shell, shellDropPoint.position, Quaternion.identity) as Rigidbody;
			newShell.AddForce(shellDropPoint.forward * Random.Range(150f,200f) + spawn.forward * Random.Range(-10f,10f));
		}
	}

	public void ShootAuto() {
		if (gunType == GunType.Auto) {
			Shoot();
		}
	}

	private bool CanShoot() {
			bool canShoot = true;

		if (Time.time < nextShootTime) {
				canShoot = false;
			}

			return canShoot;
		}

	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition(0, spawn.position);
		tracer.SetPosition(1, spawn.position + hitPoint);
		yield return null;
		tracer.enabled = false;
	}

}
