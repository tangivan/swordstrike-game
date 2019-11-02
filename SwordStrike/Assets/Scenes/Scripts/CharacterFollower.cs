using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    public GameObject player;
    public bool isFire;
    public bool isWater;
    public bool isEarth;
    public bool isAir;

    // Update is called once per frame
    void LateUpdate()
    {
        if(isFire)
        {
            player = GameObject.Find("FireOrb");
        }
        else if(isWater)
        {
            player = GameObject.Find("WaterOrb");
        }
        else if(isEarth)
        {
            player = GameObject.Find("EarthOrb");
        }
        else if(isAir)
        {
            player = GameObject.Find("AirOrb");
        }


        if (player != null) 
        transform.position = player.transform.position;
    }
}
