using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this AdManager is a singleton. 
//it will exist in every scene. 

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private void Awake()
    {
        // if this already exists, destroy to make sure there is only one.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        //if this is the first one, set the instance to this and put it in DontDestroyOnLoad.
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
