using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    [SerializeField] int number = 0;
    Parent parent;

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObj = gameObject.transform.parent.gameObject;
        parent = parentObj.GetComponent<Parent>();
    }

    void OnTriggerStay(Collider collider)
    {
        parent.RelayTriggerStay(number, collider);
    }

    void OnTriggerExit(Collider collider)
    {
        parent.RelayTriggerExit(number, collider);
    }
}
