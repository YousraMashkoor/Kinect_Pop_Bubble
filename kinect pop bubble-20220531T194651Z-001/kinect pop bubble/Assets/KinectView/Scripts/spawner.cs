using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    // Use this for initialization
    public GameObject bubble;
	void Start () {
        InvokeRepeating("CreateBubble", 3, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateBubble() {
        Instantiate(bubble, new Vector3(Random.Range(10, -10), Random.Range(3.5f, -1.5f), transform.position.z),transform.rotation);
    }
}
