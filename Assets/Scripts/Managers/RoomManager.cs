using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] doorsToClose;
    [SerializeField] bool CloseDoorOnPlayerEnter, openDoorsWhenEnemiesDie;

    [SerializeField] List<Collider2D> enemies = new List<Collider2D>();

    private Collider2D roomCollider;

    private ContactFilter2D contactFilter2D;

    void Start()
    {
        roomCollider = GetComponent<Collider2D>();
        contactFilter2D.SetLayerMask(LayerMask.GetMask("Enemy"));

        roomCollider.OverlapCollider(contactFilter2D, enemies);
    }

    void Update()
    {
        for(int i = enemies.Count -1; i > -1; i--)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }

        if(enemies.Count == 0)
        {
            for(int i = 0; i < doorsToClose.Length; i++)
            {
                doorsToClose[i].SetActive(false);
            }
        }
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
