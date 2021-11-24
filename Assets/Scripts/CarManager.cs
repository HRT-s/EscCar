using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public List<GameObject> cars;
    int numCar;

    // Start is called before the first frame update
    void Start()
    {
        cars = new List<GameObject>();
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.CompareTag("Car"))
            {
                cars.Add(obj);
            }
        }
        numCar = cars.Count;
    }

    public void DeleteCar(GameObject obj)
    {
        cars.Remove(obj);
    }

    public void GameEnd()
    {
        if(cars.Count == numCar)
        {
            FadeManager.FadeOut("Good");
        }else if(cars.Count == 1)
        {
            FadeManager.FadeOut("TrueEnd");
        }
        else
        {
            FadeManager.FadeOut("BadEnd");
        }
    }
}
