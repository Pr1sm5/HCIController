using System;
using UnityEngine;

public class IPData : MonoBehaviour
{
    public static IPData Instance { get; private set; }
    
    public string ip = "127.0.0.1";
    public int port = 12345;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
