using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDoorBehaviour", menuName = "WeaponDoorBehaviour")]

public class WeaponDoorBehaviour : DoorBehaviour
{
    public string weaponName;
    public float damage;
    public float range;
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Weapon Door Activated with" + damage + " damage and " + range + " range!");
    }
}
