using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    protected bool rearFlag = true;
    protected bool backFlag = true;

    public virtual void RelayTriggerStay(int number,Collider collider)
    {
        if (collider.CompareTag("Car"))
        {
            if (number == 0)
            {
                rearFlag = false;
            }
            if (number == 1)
            {
                backFlag = false;
            }
        }
    }

    public virtual void RelayTriggerExit(int number,Collider collider)
    {
        if (collider.CompareTag("Car"))
        {
            if (number == 0)
            {
                rearFlag = true;
            }
            if (number == 1)
            {
                backFlag = true;
            }
        }
    }
}
