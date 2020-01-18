using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour
{
    public Transform target;
    public GameObject throwObject;
   
    public bool launching = false;

    void Start(){
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            if(!launching){
                StartCoroutine("ProjectileLaunch");
            }
            //Debug.Log("Fire away!");
            //GameObject projectile = Instantiate(throwObject, transform.position, transform.rotation);
            //Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
            //projectileRB.velocity = ProjectileVelocity(target);
            //Destroy(projectile, 10);
        }
    }

    public IEnumerator ProjectileLaunch() {
        launching = true;
        //private Vector3 ProjectileVelocity(Transform t) {
        GameObject projectile = Instantiate(throwObject, transform.position, transform.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        Vector3 direction = target.position - transform.position;
        float heightDif = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        direction.y = distance;
        distance += heightDif;
       
        Physics.gravity = new Vector3(0, -15, 0);
        float newVelocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        projectileRB.velocity = newVelocity * direction.normalized;
        yield return new WaitForSeconds(3);
        Destroy(projectile, 5);
        launching = false;

    }
}