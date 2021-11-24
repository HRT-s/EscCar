using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject camera1;
    GameObject camera2;
    GameObject player;
    Rigidbody playerRb;

    int nowCam;
    float[] area;


    public int GetCam()
    {
        return nowCam;
    }

    public Camera GetCamera()
    {
        return Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera1 = GameObject.Find("FirstCamera");
        camera2 = GameObject.Find("SecondCamera");
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody>();

        nowCam = 0;
        area = new float[] {-7.5f, -2.5f, 2.5f, 7.5f};
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            nowCam = (nowCam + 1) % 2;
        }
        if(nowCam == 0)
        {
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
        else
        {
            Complement();
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }

    void Complement()
    {
        int rot = (int)Math.Round(player.transform.eulerAngles.y);
        Vector3 pos = player.transform.position;
        if (rot % 180 == 0)
        {
            pos.z = Nearest(pos.z);
        }
        else
        {
            pos.x = Nearest(pos.x);
        }
        player.transform.position = pos;
    }

    public float Nearest(float t)
    {
        int ind = 0;
        float min = Math.Abs(t - area[ind]);
        for (int i = 1; i < area.Length; i++)
        {
            if (Math.Abs(t - area[i]) < min)
            {
                min = Math.Abs(t - area[i]);
                ind = i;
            }
        }
        return area[ind];
    }
}
