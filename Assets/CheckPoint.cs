using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private CheckPointController checkPointController;
    
    private void OnTriggerEnter(Collider collider)
    {

        if(checkPointController == null)
        {
            checkPointController = CheckPointController.GetInstance();
        }

        checkPointController.changeCheckPoint(this);
    }
}
