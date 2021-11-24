using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : Parent
{
    public GameObject explode;
    GameObject manager;
    CameraController cam;
    new Camera camera;
    CarManager c;

    Vector3 currentPos;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager");
        cam = manager.GetComponent<CameraController>();
        camera = cam.GetCamera();
        c = manager.GetComponent<CarManager>();
        currentPos = Vector3.zero;
        t = this.transform;
    }

    public void OnDrag()
    {
        Vector3 pos = t.position;
        float dist = Vector3.Distance(pos, camera.transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = dist;
        currentPos = camera.ScreenToWorldPoint(mousePos);

        int rot = (int)Math.Round(t.eulerAngles.y);
        float z = cam.Nearest(currentPos.z);
        float x = cam.Nearest(currentPos.x);
        switch (rot)
        {
            case (int)Direction.Dir.NORTH:
                if (pos.z < z && rearFlag || pos.z > z && backFlag)
                {
                    pos.z = z;
                }
                break;
            case (int)Direction.Dir.EAST:
                if(pos.x < x && rearFlag || pos.x > x && backFlag)
                {
                    pos.x = x;
                }
                break;
            case (int)Direction.Dir.SOUTH:
                if (pos.z > z && rearFlag || pos.z < z && backFlag)
                {
                    pos.z = z;
                }
                break;
            case (int)Direction.Dir.WEST:
                if (pos.x > x && rearFlag || pos.x < x && backFlag)
                {
                    pos.x = x;
                }
                break;
            default: break;
        }
        t.position = pos;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Instantiate(explode, transform.position, Quaternion.identity) ;
            c.DeleteCar(gameObject);
            t.position = new Vector3(-20, -20, -20);
        }
    }
}
