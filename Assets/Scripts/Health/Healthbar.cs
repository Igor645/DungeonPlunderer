using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health objectHealth = null;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private GameObject[] healthbars;
   [SerializeField] private bool isPlayer;

    void Start()
    {
        if (isPlayer && GameObject.FindGameObjectWithTag("Player") != null)
        {
            objectHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            totalhealthbar.fillAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().startingHealth / 10;
            isPlayer = true;
        }
        //DontDestroyOnLoad(gameObject);
    }

    /*private void OnLevelWasLoaded(int level)
    {
        healthbars = GameObject.FindGameObjectsWithTag("healthbar");

        if(healthbars.Length > 1)
        {
            Destroy(healthbars[1]);
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!isPlayer)
        {
            objectHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
            totalhealthbar.fillAmount = objectHealth.startingHealth / 10;
        }

        if(currenthealthbar != null && objectHealth != null)
        {
            currenthealthbar.fillAmount = objectHealth.currentHealth / 10;
        }
    }
}
