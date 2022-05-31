using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideBubble : MonoBehaviour {

    // Use this for initialization
    private Vector3 mMovementDirection = Vector3.zero;
    private Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        //movement 
        transform.position += mMovementDirection * Time.deltaTime * 0.5f;

        //rotation
        transform.Rotate(Vector3.forward * Time.deltaTime * mMovementDirection.x * 20, Space.Self);
        StartCoroutine(DirectionChanger());
        // transform.position = new Vector3(Random.Range(3, -3) * Time.deltaTime, Random.Range(3.5f, -1.5f) * Time.deltaTime, transform.position.z);
    }
    private IEnumerator DirectionChanger()
    {
        while (gameObject.activeSelf)
        {
            mMovementDirection = new Vector2(Random.Range(-17, 17) * 0.01f, Random.Range(100, 100) * 0.01f);
            yield return new WaitForSeconds(5.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //score
            KeepScore.scoreValue += 10;

            //sound
            SoundManagerScript.PlaySound("popSound");

            //Destroy bubble
            anim.enabled = true;
            Destroy(this.gameObject,.5f);
        }
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
        }

}
