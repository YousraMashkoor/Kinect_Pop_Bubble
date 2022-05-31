using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missBubble : MonoBehaviour {

    private int count;

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Bubble"))
        {
            count += 1;
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(count == 3)
        {
            Application.Quit();
        }

	}
}
