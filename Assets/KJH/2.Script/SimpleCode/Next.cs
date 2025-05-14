using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnClose()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OnOnpe()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
