using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinDoorBehaviour", menuName = "WinDoorBehaviour")]

public class WinDoorBehaviour : DoorBehaviour
{
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Winning Door Activated!");
    }
}
