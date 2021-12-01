using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerPref : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private bool deleteAll;
    [SerializeField]
    private string key;
    [SerializeField]
    private bool deleteKey;
    [SerializeField]
    private string secondKey;
    [SerializeField]
    private float value;
    [SerializeField] 
    private bool changeKey;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deleteAll)
        {
            PlayerPrefs.DeleteAll();
        }

        if (deleteKey)
        {
            PlayerPrefs.DeleteKey(key);
        }

        if (changeKey)
        {
            PlayerPrefs.SetFloat(secondKey,value);
        }
    }
}
