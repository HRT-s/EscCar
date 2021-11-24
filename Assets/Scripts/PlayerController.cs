using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Parent
{
    [SerializeField] Vector3 acceleration = new Vector3(0,0,1);

    Rigidbody rb;
    GameObject manager;
    CameraController cam;

    int rot;
    Vector3 velocity;
    bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("Manager");
        cam = manager.GetComponent<CameraController>();

        rot = (int)Math.Round(this.transform.eulerAngles.y);
        velocity = Vector3.zero;
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        Complement();

        if (cam.GetCam() == 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (isFinish)
                {
                    CarManager cm = manager.GetComponent<CarManager>();
                    cm.GameEnd();
                }
                velocity = velocity + acceleration * Time.deltaTime;

            }
            if (Input.GetMouseButton(1))
            {
                velocity = velocity - acceleration * Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                velocity = Vector3.zero;
            }
        }
        else
        {
            velocity = Vector3.zero;
        }
        rb.velocity = velocity;
    }

    void Complement()
    {
        Vector3 pos = this.transform.position;
        bool inStageFlag = true;
        if (rot % 180 == 0)
        {
            if (pos.z != InStage(pos.z))
            {
                pos.z = InStage(pos.z);
                inStageFlag = false;
            }
        }
        else
        {
            if (pos.x != InStage(pos.x))
            {
                pos.x = InStage(pos.x);
                inStageFlag = false;
            }
        }
        if (!inStageFlag)
        {
            this.transform.position = pos;
            velocity = Vector3.zero;
        }
    }

    float InStage(float p)
    {
        if (p < -7.5)
        {
            p = -7.5f;
        }
        else if (7.5 < p)
        {
            p = 7.5f;
        }
        return p;
    }

    public void OnClick()
    {
        if (cam.GetCam() == 0) return;

        Vector3 pos = this.transform.position;
        float ax = acceleration.z;
        float az = acceleration.x;

        int k = 1;
        if (Input.GetMouseButton(0))
        {
            if (!rearFlag) return;
            rot = (rot + 270) % 360;
            this.transform.eulerAngles = new Vector3(0, rot, 0);
            ax *= -1;
        }else if (Input.GetMouseButton(1))
        {
            if (!backFlag) return;
            k = -1;
            rot = (rot + 90) % 360;
            this.transform.eulerAngles = new Vector3(0, rot, 0);
            az *= -1;
        }
        switch (rot)
        {
            case (int)Direction.Dir.NORTH:
                pos.x += 2.5f;
                pos.z += 2.5f*k;
                break;
            case (int)Direction.Dir.EAST:
                pos.x += 2.5f*k;
                pos.z -= 2.5f;
                break;
            case (int)Direction.Dir.SOUTH:
                pos.x -= 2.5f;
                pos.z -= 2.5f*k;
                break;
            case (int)Direction.Dir.WEST:
                pos.x -= 2.5f*k;
                pos.z += 2.5f;
                break;
            default: break;
        }
        acceleration = new Vector3(ax, 0, az);
        this.transform.position = pos;
    }

    public override void RelayTriggerStay(int number,Collider collider)
    {
        base.RelayTriggerStay(number, collider);
        if (collider.CompareTag("Finish") && number == 2)
        {
            isFinish = true;
        }
    }

    public override void RelayTriggerExit(int number,Collider collider)
    {
        base.RelayTriggerExit(number, collider);
        if(collider.CompareTag("Finish") && number == 2)
        {
            isFinish = false;
        }
    }
}
