using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossDoorBehaviour", menuName = "BossDoorBehaviour")]

public class BossDoorBehaviour : DoorBehaviour
{
    public string bossName;
    public float health;
    public float damage;
    // Reference player script?
    public override void ActivateDoor()
    {
        Debug.Log("Boss Door Activated with" + damage + " damage and " + health + " health!");
    }
}
