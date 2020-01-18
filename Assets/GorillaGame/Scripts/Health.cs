using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public int health = 5;
    public bool hit = false;
    public Text healthtext;
	// Use this for initialization
	void Start () {
        SetCountText();
	}

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "projectile") {
            hit = true;
            health -= 1;
            SetCountText();
        }

    }
	private void OnCollisionExit(Collision collision)
	{
        hit = false;
	}

	// Update is called once per frame
	void Update () {
        if (health == 0) {
            Debug.Log("HI THERE");
            SceneManager.LoadScene(2);
        }
		
	}
    void SetCountText () {
        healthtext.text = "Health: " + health.ToString();
    }
}
