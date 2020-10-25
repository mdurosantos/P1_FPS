using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;
    private List<CheckPoint> checkPoints;
    private CheckPoint actual_checkPoint;


    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        
        //GetAllCheckPoints();
    }

    public static CheckPointController GetInstance()
    {
        return instance;
    }

    public CheckPoint ActualCheckPoint()
    {
        return actual_checkPoint;
    }

    public void changeCheckPoint(CheckPoint checkPoint)
    {
        //if(checkPoints.Contains(checkPoint))
        if(actual_checkPoint != null)
        {
            if(!actual_checkPoint.Equals(checkPoint))
            {
                actual_checkPoint = checkPoint;
            }
        }
    }

    /*private void GetAllCheckPoints()
    {
        foreach(CheckPoint checkPoint in transform)
        {
            checkPoints.Add(checkPoint);
        }

    }*/
}
