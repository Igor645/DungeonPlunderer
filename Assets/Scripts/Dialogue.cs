using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    CanvasGroup cg;
    [SerializeField] AudioSource bossTheme;
    Animator boss;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        }
        cg = gameObject.GetComponent<CanvasGroup>();
        cg.interactable = false;
        cg.alpha = 0;
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        cg.interactable = true;
        cg.alpha = 1;
        index = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerJump>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShield>().enabled = false;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerJump>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShield>().enabled = true;
            if(GameObject.FindGameObjectWithTag("music") != null && GameObject.FindGameObjectWithTag("bossmusic") != null)
            {
                GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>().Stop();
                bossTheme.Play();
            }
            boss.SetBool("BossFightRunning", true);
            //gameObject.SetActive(false);
            cg = gameObject.GetComponent<CanvasGroup>();
            cg.interactable = false;
            cg.alpha = 0;
            Destroy(GameObject.FindGameObjectWithTag("DialogueStarter"));
        }
    }
}
