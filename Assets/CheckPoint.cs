using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointController checkPointController;
    private Light light;

    public void Awake()
    { 
        light = GetComponent<Light>();
    }
    
    private void OnTriggerEnter(Collider collider)
    {

        if(checkPointController == null)
        {
            checkPointController = CheckPointController.GetInstance();
        }
        light.color = Color.yellow;
        checkPointController.changeCheckPoint(this);
        
    }
}
