using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int poolSize = 25;

    private GameObject[] decalsList;

    private int count = 0;


    private void Awake()
    {
        StartPool();
    }

    private void StartPool()
    {
        decalsList = new GameObject[poolSize];
        for (int i = 0; i<poolSize; i++)
        {
            var instanceToAdd = Instantiate(prefab);
            instanceToAdd.transform.SetParent(transform);
            instanceToAdd.SetActive(false);
            decalsList[i] = instanceToAdd;
        }
    }



    public GameObject GetNextElement()
    {
        var instance = decalsList[count];
        instance.SetActive(true);

        count++;
        if (count >= poolSize) count = 0;

        return instance;      
    }
}
