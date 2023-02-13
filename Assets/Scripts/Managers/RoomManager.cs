using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose;
    [SerializeField] bool CloseDoorOnPlayerEnter;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (CloseDoorOnPlayerEnter)
            {
                foreach (GameObject door in doorsToClose)
                {
                    door.SetActive(true);
                }
            }
        }
    }
}
