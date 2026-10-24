using System;
using System.Collections;
using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    [SerializeField] private GameObject cockpitCamera;
    [SerializeField] private GameObject trainCarCamera;
    [SerializeField] private GameObject outsideCamera;
    [SerializeField] private GameObject trainCarPlayer;
    [SerializeField] private GameObject outsidePlayer;

    void Start()
    {
        if (cockpitCamera == null || trainCarCamera == null || outsideCamera == null)
        {
            Debug.LogWarning("The cameras aren't connected to cameraController GameObject.");
            return;
        }
        if (trainCarPlayer == null || outsidePlayer == null)
        {
            Debug.LogWarning("The players aren't connected to cameraController GameObject.");
            return;
        }

        cockpitCamera.SetActive(true);
        trainCarCamera.SetActive(false);
        outsideCamera.SetActive(false);
        trainCarPlayer.SetActive(false);
        outsidePlayer.SetActive(false);
    }

    public void SwitchToCockpitCam()
    {
        cockpitCamera.SetActive(true);
        trainCarCamera.SetActive(false);
        outsideCamera.SetActive(false);
        trainCarPlayer.SetActive(false);
        outsidePlayer.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SwitchToTrainCarCam()
    {
        cockpitCamera.SetActive(false);
        trainCarCamera.SetActive(true);
        outsideCamera.SetActive(false);
        trainCarPlayer.SetActive(true);
        outsidePlayer.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SwitchToOutsideCamera()
    {
        cockpitCamera.SetActive(false);
        trainCarCamera.SetActive(false);
        outsideCamera.SetActive(true);
        trainCarPlayer.SetActive(false);
        outsidePlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
