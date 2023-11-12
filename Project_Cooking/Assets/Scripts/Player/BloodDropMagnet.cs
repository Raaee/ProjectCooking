using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDropMagnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
      
        var bloodDropPickup = collision.gameObject.GetComponent<AutomaticPickup>();
        if (!bloodDropPickup)
            return;

        bloodDropPickup.SetTargetPosition(transform.parent);
    }
}