using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBackground : MonoBehaviour
{

    public GameObject backgroundObject;

    private void Awake()
    {
        DontDestroyOnLoad(backgroundObject);
        
    }
}
