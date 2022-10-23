using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private string whichTag;
    private GameObject[] objects;
    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag(whichTag);
        if(objects.Length > 1)
        {
            Destroy(objects[1]);
        }
        DontDestroyOnLoad(gameObject);
    }
}
