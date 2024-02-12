using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private bool prologueTrigger; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            GameManager.Instance.SetNewDoors(doors, prologueTrigger);
        }
    }
}
