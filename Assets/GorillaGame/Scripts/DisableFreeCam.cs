using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFreeCam : MonoBehaviour {

    public Component scriptToControl;
    public MonoBehaviour[] scripts;
    public Transform target;
	// Use this for initialization
	void Start () {
        scripts = this.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour mb in scripts)
        {
            Debug.Log(mb.GetType().Name);
        }
		
	}
	
	// Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            if (scripts[0].enabled)
            {
                scripts[0].enabled = false;
                //scripts[2].enabled = true;
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                Debug.Log("Free Look disabled, player follow enabled");
            }
            else
            {
                scripts[0].enabled = true;

                //scripts[2].enabled = false;
                Debug.Log("free look enabled, player follow sorta disabled");
            }
        }
		
	}
}
