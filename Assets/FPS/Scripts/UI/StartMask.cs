using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMask : MonoBehaviour
{

    public GameObject maskPanel;

    void Start()
    {
        maskPanel.SetActive(true);
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            maskPanel.SetActive(false);
        }
    }

}