using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class BossMoves : MonoBehaviour {
    public Transform target;
    public Transform spawn;
    public GameObject[] throwObject;
    public int waitTime;
    public Animator bossController;


    public bool launching = false;
	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        spawn = GameObject.FindWithTag("Spawn").GetComponent<Transform>();
        bossController = GameObject.FindWithTag("Fire").GetComponent<Animator>();
	}

	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, target.position);
        bossController.SetFloat("playerDistance", distance);
        if(target.position.y < 2) {
            transform.LookAt(target);

        }
       ;
       
	}

    void ProjectileLaunch()
    {
        launching = true;
        //private Vector3 ProjectileVelocity(Transform t) {
        GameObject projectile = Instantiate(throwObject[Random.Range(0, throwObject.Length)], spawn.position, spawn.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        Vector3 direction = target.position - spawn.position;
        float heightDif = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        direction.y = distance;
        distance += heightDif;

        Physics.gravity = new Vector3(0, -15, 0);
        float newVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        projectileRB.velocity = newVelocity * direction.normalized;

        //yield return new WaitForSeconds(waitTime);
        Destroy(projectile, 5);
        launching = false;

    }

    void shakingCamera() {
        CameraShakeInstance c = CameraShaker.Instance.ShakeOnce((float)2.57, (float)1.00, (float)0.10, (float)2.00);

        Vector3 posInf = new Vector3((float)0.87, (float)0.82, (float)1.02);
        Vector3 rotInf = new Vector3(1, 1, 1);

        c.PositionInfluence = posInf;
        c.RotationInfluence = rotInf;
    }
}
