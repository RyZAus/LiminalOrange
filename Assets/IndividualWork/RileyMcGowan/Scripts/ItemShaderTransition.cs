﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShaderTransition : MonoBehaviour
{
    //Private Vars
    public List<Material> materials;
    private float swapAmount;
    private float swapAmount2;
    private bool swapColor;
    //Public Vars
    [Tooltip("This is how long to swap materials for. Default 3.")]
    public float timeToSwap = 3;
    public Renderer[] renderers;

    private void Awake()
    {
        //We are not swapping color
        swapColor = false;
        swapAmount = 0;
        foreach (Renderer thisRenderer in renderers)
        {
            foreach (Material thisMat in thisRenderer.materials)
            {
                materials.Add(thisMat);
                thisMat.SetFloat("_Blend", 0);
            }
        }
    }
    
    //Used for outside objects
    public void ChangeColor()
    {
        //We are now swapping color
        if (swapColor)
        {
            return;
        }
        swapColor = true;
        swapAmount2 = .01f / timeToSwap; //HACK A1 - Need to fix
    }

    public void StopColor()
    {
        //We are not swapping color
        swapColor = false;
    }

    private void Update()
    {
        //if currently swapping color
        if (swapColor)
        {
            //If we aren't finished swapping
            if (swapAmount < 1)
            {
                //Swap the color slowly
                swapAmount += swapAmount2;
                foreach (Material currentMat in materials)
                {
                    currentMat.SetFloat("_Blend", swapAmount);
                }
            }
            else
            {
                StopColor();
                this.enabled = false;
            }
        }
    }
}
