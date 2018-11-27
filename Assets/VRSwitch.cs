using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class VRSwitch : MonoBehaviour
{
    [SerializeField]
    bool isRunningVR;

    [SerializeField]
    Text debug;

    [SerializeField]
    int numberOfFingers = 2;
    [SerializeField]
    float holdTime = 1.5f;

    [SerializeField]
    Dictionary<int, float> fingerHoldTimes = new Dictionary<int, float>();

    // Use this for initialization
    void Start()
    {
        isRunningVR = GvrIntent.IsLaunchedFromVr();
    }

    void Update()
    {
        //  Update finger hold times
        foreach (Touch t in Input.touches)
        {
            switch (t.phase)
            {
                //  Add any new fingers
                case TouchPhase.Began:
                case TouchPhase.Moved:
                    if (!fingerHoldTimes.ContainsKey(t.fingerId))
                    {
                        fingerHoldTimes.Add(t.fingerId, 0);
                    }
                    else
                    {
                        fingerHoldTimes[t.fingerId] = 0;
                    }
                    break;
                //  Remove any gone fingers
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    fingerHoldTimes.Remove(t.fingerId);
                    break;
                //  Update the hold time for each finger
                case TouchPhase.Stationary:
                    fingerHoldTimes[t.fingerId] += t.deltaTime;
                    break;
            }
        }
        //  Check for 2 long hold press
        if (fingerHoldTimes.Count >= numberOfFingers)
        {
            int longHoldCount = 0;

            foreach (float t in fingerHoldTimes.Values)
            {
                if (t >= holdTime)
                    ++longHoldCount;
            }

            if (longHoldCount >= numberOfFingers)
            {
                fingerHoldTimes.Clear();
                ToggleCamera();
            }
        }

        if (XRSettings.enabled)
        {
            // Unity takes care of updating camera transform in VR.
            return;
        }

        // android-developers.blogspot.com/2010/09/one-screen-turn-deserves-another.html
        // developer.android.com/guide/topics/sensors/sensors_overview.html#sensors-coords
        //
        //     y                                       x
        //     |  Gyro upright phone                   |  Gyro landscape left phone
        //     |                                       |
        //     |______ x                      y  ______|
        //     /                                       \
        //    /                                         \
        //   z                                           z
        //
        //
        //     y
        //     |  z   Unity
        //     | /
        //     |/_____ x
        //

        transform.localRotation =

          // Neutral position is phone held upright, not flat on a table.
          Quaternion.Euler(90f, 0f, 0f) *

          // Sensor reading, assuming default `Input.compensateSensors == true`.
          Input.gyro.attitude *

          // So image is not upside down.
          Quaternion.Euler(0f, 0f, 180f);
    }

    public void ToggleCamera()
    {
        if (isRunningVR)
        {
            StartCoroutine(SwitchTo2D());
        }
        else
        {
            StartCoroutine(SwitchToVR());
        }

        isRunningVR = !isRunningVR;
    }

    IEnumerator SwitchToVR()
    {
        Input.gyro.enabled = false;
        // Device names are lowercase, as returned by `XRSettings.supportedDevices`.
        string desiredDevice = "cardboard";

        // Some VR Devices do not support reloading when already active, see
        // https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
        if (String.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(desiredDevice);

            // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
            yield return null;
        }

        // Now it's ok to enable VR mode.
        XRSettings.enabled = true;
    }

    IEnumerator SwitchTo2D()
    {
        // Empty string loads the "None" device.
        XRSettings.LoadDeviceByName("");

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Restore 2D camera settings.
        ResetCameras();
    }

    // Resets camera transform and settings on all enabled eye cameras.
    void ResetCameras()
    {
        // Camera looping logic copied from GvrEditorEmulator.cs
        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {
                // Reset local position.
                // Only required if you change the camera's local position while in 2D mode.
                cam.transform.localPosition = Vector3.zero;

                // Reset local rotation.
                // Only required if you change the camera's local rotation while in 2D mode.
                cam.transform.localRotation = Quaternion.identity;
            }
        }

        Input.gyro.enabled = true;
    }
}