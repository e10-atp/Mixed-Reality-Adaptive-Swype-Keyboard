using System;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class AutoGrab : MonoBehaviour
{
    public XRInteractionManager interactionManager;
    public XRGrabInteractable grabInteractable;
    //public XRDirectInteractor directInteractor;
    public XRRayInteractor rayInteractor;
    
    
    
    public void Grab()
    {
        interactionManager.ForceSelect(rayInteractor, grabInteractable);
        Debug.Log("AutoGrab");
    }

    private void Awake()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
    }

    private void Update()
    {
        Grab();
    }
}