using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightDoorBehaviour", menuName = "LightDoorBehaviour")]

public class LightDoorBehaviour : DoorBehaviour
{
    public float lightIntensity;
    public float lightRange;
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Light Door Activated with" + lightIntensity + " intensity and " + lightRange + " range!");
    }
}
