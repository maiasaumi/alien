﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorY : MonoBehaviour
{
    public Color32 color;

    private Transform Objective;
    private Camera MainCam;

    private float spriteWidth;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        MainCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        Objective = transform.parent.parent.Find("Objective");
        spriteWidth = GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        //this converts the Objective's position to a position reletive to the Camera view.
        //(0, 0) ~ (1, 1) is within the camera view
        Vector3 objPosition = MainCam.WorldToViewportPoint(Objective.position);

        if (objPosition.y > 1)  // Objective is over the top
        {
            //make self yellow
            GetComponent<Image>().color = color;

            y = (Screen.height - spriteWidth) / 2;
            SetX(objPosition);
        }
        else if (objPosition.y < 0)  // Objective is below the bottom, 
        {
            //make self yellow
            GetComponent<Image>().color = color;

            y = -(Screen.height - spriteWidth) / 2;
            SetX(objPosition);

        }
        else
        {
            //make self transparent
            GetComponent<Image>().color = new Color32(0,0,0,0);
        }


        transform.localPosition = new Vector3(x, y, 0);
    }

    void SetX(Vector3 objPosition)
    {
        if (objPosition.x > 1)  // on the right
        {
            x = Screen.width / 2;
        }
        else if (objPosition.x < 0) // on the left
        {
            x = -Screen.width / 2;
        }
        else //between the width of the screen
        {
            x = (Screen.width * objPosition.x) - (Screen.width / 2);

        }
    }
}