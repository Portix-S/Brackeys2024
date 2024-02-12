using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDoorBehaviour", menuName = "GameDoorBehaviour")]

public class GameDoorBehaviour : DoorBehaviour
{
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Game Door Activated!");
    }
}
