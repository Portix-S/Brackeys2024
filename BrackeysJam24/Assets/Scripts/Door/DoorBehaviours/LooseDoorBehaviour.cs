using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LooseDoorBehaviour", menuName = "LooseDoorBehaviour")]

public class LooseDoorBehaviour : DoorBehaviour
{
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Loosing Door Activated!");
    }
}
