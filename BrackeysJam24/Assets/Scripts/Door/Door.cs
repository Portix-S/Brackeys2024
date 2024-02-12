using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator _animator;
    private static readonly int OpenDoor1 = Animator.StringToHash("OpenDoor");
    public bool activeDoor;

    private DoorBehaviour _doorBehaviour;
    // Have a doorBehaviour Class, which will be decided by the manager, and will be called by the door
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetDoorBehaviour(DoorBehaviour doorBehaviour)
    {
        _doorBehaviour = doorBehaviour;
    }

    public void OpenDoor(bool active)
    {
        _animator.SetTrigger(OpenDoor1);
        activeDoor = active;
        GetComponent<BoxCollider2D>().enabled = false;
        if(active)
            _doorBehaviour.ActivateDoor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            GameManager.Instance.SetCurrentDoor(gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoseCurrentDoor(gameObject);
        }
    }
}
