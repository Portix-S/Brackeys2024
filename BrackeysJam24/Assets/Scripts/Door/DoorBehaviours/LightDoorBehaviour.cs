using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LightDoorBehaviour", menuName = "LightDoorBehaviour")]

public class LightDoorBehaviour : DoorBehaviour
{
    public Color color;
    public float lightIntensity;
    public float lightRange;
    public float lightFallOff;
    // Reference player script?
    public override void ActivateDoor()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LightManager>().ChangeLightParams(color, lightIntensity, lightRange, lightFallOff);
        Debug.Log("Light Door Activated with" + lightIntensity + " intensity and " + lightRange + " range!");
    }
}
