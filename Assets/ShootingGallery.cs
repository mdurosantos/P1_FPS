using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGallery : MonoBehaviour
{
    private int minScore = 10;
    private int actualScore;
    private bool completed = false;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textTime;
    [SerializeField] private Text textInfo;

    private List<Transform> targets = new List<Transform>();


    private int last_points = 0;
    //[SerializeField] GameObject door;
    private int maxTime = 30;   
    private int time;

    public void ScorePoitns(int points)
    {   
        actualScore += points / (points == last_points ? 3:1);
        last_points = points;
        UpdateTextScore(actualScore);
        if(!completed)
        {
            completed = true;
            OpenDoor();
            textInfo.text = textInfo.text + "\n\n <color=green>Task completed</color>";
        }

    }

    public void Repeat()
    {
        EndShootingGallery();
        StartShootingGallery();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            if(targets.Count == 0)
            {
                GenerateTargetsList();
            }

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
        actualScore = 0;
        textScore.enabled = true;
        time = maxTime;
        textTime.enabled = true;
        textInfo.enabled = true;
        textTime.text = "Time: " + time;
        UpdateTextScore(actualScore);
        EnableTargets();
        
        InvokeRepeating("TimeToComplete", 0f, 1.0f);
    }

    private void EndShootingGallery()
    {
        CancelInvoke("TimeToComplete");
        
        if(actualScore >= minScore)
        {
            OpenDoor();
        }

        textScore.enabled = false;
        textTime.enabled = false;
        textInfo.enabled = false;

        DisableTargets();
    }

    private void UpdateTextScore(int points)
    {
        Debug.Log(points);
        textScore.text = "Score: " + points + "/" + minScore;
    }

    private void OpenDoor()
    {
        //Open da Door
        //door.GetComponent<Animator>().SetBool("open", true);
        Debug.Log("Open door");
    }

    private void TimeToComplete()
    {
        time -= 1;
        textTime.text = "Time: " + time;
        if(time <= 0)
        {
            EndShootingGallery();
        }
    }

    private void EnableTargets()
    {
        foreach(Transform target in targets)
        {
            target.GetComponent<target_script>().EnableTarget();
        }
    }
    
    private void DisableTargets()
    {
        foreach(Transform target in targets)
        {
            target.GetComponent<target_script>().DisableTarget();
            
        }
    }

    private void GenerateTargetsList()
    {
        foreach(Transform child in transform)
        {
            if(child.childCount > 0)
            {
                foreach(Transform child_ in child)
                {
                    if(child_.GetComponent<target_script>())
                    {
                        Debug.Log(child_);
                        targets.Add(child_);
                    }
                }

            }
            if(child.GetComponent<target_script>())
            {
                Debug.Log(child);
                targets.Add(child);
            }
        }
    }
}
