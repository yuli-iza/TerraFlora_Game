using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Make Canvas persistent across scenes
    }

}
