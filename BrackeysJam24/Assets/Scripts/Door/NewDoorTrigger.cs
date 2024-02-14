using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoorTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private bool prologueTrigger; 
    [SerializeField] private List<DoorBehaviour> badDoorBehaviours;
    [SerializeField] private List<DoorBehaviour> neutralDoorBehaviours;
    [SerializeField] private List<DoorBehaviour> goodDoorBehaviours;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            GameManager.Instance.SetNewDoors(doors, prologueTrigger, badDoorBehaviours, neutralDoorBehaviours, goodDoorBehaviours);
        }
        Destroy(gameObject);
    }
}
