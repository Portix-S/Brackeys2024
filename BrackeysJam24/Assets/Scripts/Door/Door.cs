using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator _animator;
    private static readonly int OpenDoor1 = Animator.StringToHash("OpenDoor");
    private static readonly int OpenDoor2 = Animator.StringToHash("OpenAndFadeDoor");
    public bool activeDoor;
    [SerializeField] private bool gameplayDoor;
    [SerializeField] private SpriteRenderer _itemSprite;
    private DoorBehaviour _doorBehaviour;
    // Have a doorBehaviour Class, which will be decided by the manager, and will be called by the door
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void SetDoorBehaviour(DoorBehaviour doorBehaviour)
    {
        _doorBehaviour = doorBehaviour;
        if(doorBehaviour.sprite != null)
            _itemSprite.sprite = doorBehaviour.sprite;
    }

    public void OpenDoor(bool active)
    {
        activeDoor = active; // Just to see if the door is active - can be removed later
        GetComponent<BoxCollider2D>().enabled = false;
        if (active)
        {
            _animator.SetTrigger(OpenDoor1);
            if(!gameplayDoor)
                GetItem();
        }
        else
        {
            if(gameplayDoor)
                _animator.SetTrigger(OpenDoor2);
            else
                _animator.SetTrigger(OpenDoor1);
        }
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

    public void GetItem()
    {
        Debug.Log("Pegou Item");
        // Active item in Player
        _doorBehaviour.ActivateDoor();
        if(_itemSprite != null)
            _itemSprite.enabled = false;
    }
}
