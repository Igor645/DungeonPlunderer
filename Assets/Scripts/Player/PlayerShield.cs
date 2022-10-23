using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Animator anim;
    private GameObject shieldingArea;
    public bool shielding = false;
    // Start is called before the first frame update

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("shielding", true);
        }
        else
        {
            anim.SetBool("shielding", false);
            shielding = false;
        }
    }

    private void Shielding()
    {
        shielding = true;
    }
}
