using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorBehaviour : ScriptableObject
{
    public Sprite sprite;
    // Reference player script?
    public abstract void ActivateDoor();
}
