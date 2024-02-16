using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHelper : MonoBehaviour
{
    [SerializeField] MoveTowardsLimits moveTowardsLimits;

    public void ResetTakingDamage()
    {
        moveTowardsLimits.ResetTakingDamage();
    }
    
}
