using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;
    //timmer
    public int timeLeft = 60;
    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        //get time
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        
        score.text = "Score: " + scoreValue;

        //change time
        countdownText.text = ("Seconds Left = " + timeLeft);

        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            countdownText.text = "Times Up!";
        }
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
