using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasBehaviour : MonoBehaviour
{
    GameObject gameOverText;
    GameObject player;

    private float howLongDead;

    void Start()
    {
        gameOverText = GameObject.FindGameObjectWithTag("GameOverText");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<Health>() != null && player.GetComponent<Health>().dead)
        {
            howLongDead += Time.deltaTime;
            gameOverText.SetActive(true);

            if(howLongDead < 0.1f)
            {
                StartCoroutine(FadeTextToFullAlpha(100f, gameOverText.GetComponent<Text>()));
            }
        }
        else
        {
            gameOverText.SetActive(false);
            howLongDead = 0;
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
