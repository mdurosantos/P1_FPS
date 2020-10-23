using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGallery : MonoBehaviour
{
    private int minScore = 1000;
    private int actualScore;
    private bool inside = false;
    private Text textScore;
    private int last_points = 0;
    //[SerializeField] GameObject door;

    public void Awake()
    {
        //textScore = GetComponentInChildren<Text>();
    }

    public void ScorePoitns(int points)
    {   
        actualScore += points / (points == last_points ? 3:1);
        last_points = points;
        Debug.Log(actualScore);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            StartShootingGallery();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            EndShootingGallery();
        }
    }


    private void StartShootingGallery()
    {
        inside = true;
        actualScore = 0;
    }

    private void EndShootingGallery()
    {
        if(actualScore >= minScore)
        {
            OpenDoor();
        }

        inside = false;
        actualScore = 0;
    }

    private void OpenDoor()
    {
        //Open da Door
        //door.GetComponent<Animator>().SetBool("open", true);
    }
}
