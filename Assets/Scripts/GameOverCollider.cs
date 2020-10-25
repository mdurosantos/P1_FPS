using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FPSController>() != null)
        {
            Debug.Log("Game over");
            Time.timeScale = 0.0f;
        }
    }
}
