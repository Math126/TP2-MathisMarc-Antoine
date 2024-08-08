using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public delegate void PrimaryChange(string main, bool state);
public delegate void SecondChange(string main, bool state);
public delegate void GripperStateChange(string main, bool state);
public delegate void TriggerStateChange(string main, bool state);
public delegate void ThumbstickClickStateChange(string main, bool state);
public delegate void ThumbstickTouchStateChange(string main, bool state);
public delegate void GripperValueChange(string main, float value);
public delegate void TriggerValueChange(string main, float value);
public delegate void ThumbstickValueChange(string main, Vector2 value);
public delegate void HandPositionChange(string main, Vector3 value);
public delegate void HandRotationChange(string main, Vector3 value);
public delegate void HandSpeedChange(string main, Vector3 value);

public delegate void HeadPositionChange(string head, Vector3 value);
public delegate void HeadRotationChange(string head, Vector3 value);
public delegate void HeadSpeedChange(string head, Vector3 value);
public class XRInputManager : MonoBehaviour
{
    public class Hand
    {
        public static event PrimaryChange onPrimaryChange;
        public static event SecondChange onSecondaryChange;
        public static event GripperStateChange onGripperStateChange;
        public static event TriggerStateChange onTriggerStateChange;
        public static event ThumbstickClickStateChange onThumbstickClickStateChange;
        public static event ThumbstickTouchStateChange onThumbstickTouchStateChange;
        public static event GripperValueChange onGripperValueChange;
        public static event TriggerValueChange onTriggerValueChange;
        public static event ThumbstickValueChange onThumbstickValueChange;
        public static event HandPositionChange onHandPositionChange;
        public static event HandRotationChange onHandRotationChange;
        public static event HandSpeedChange onHandSpeedChange;

        public bool ControllerPrimaryBoutonState, ControllerSecondaryBoutonState, ControllerGripperState, ControllerTriggerState, ControllerThumbstickClickState, ControllerThumbstickTouchState;
        public float ControllerGripperValue, ControllerTriggerValue;
        public Vector2 ControllerThumbstickValue;
        public Vector3 ControllerPositionValue, ControllerRotationValue, ControllerSpeedValue;

        private InputDevice Device;
        public string DeviceName;

        public Hand(InputDevice hand, string deviceName)
        {
            Device = hand;
            DeviceName = deviceName;
        }

        public void UpdatePrimaryBouton()
        {
            bool LastState = ControllerPrimaryBoutonState;
            Device.TryGetFeatureValue(CommonUsages.primaryButton, out ControllerPrimaryBoutonState);

            if(LastState != ControllerPrimaryBoutonState)
            {
                onPrimaryChange(DeviceName, ControllerPrimaryBoutonState);
            }
        }

        public void UpdateSecondaryBouton()
        {
            bool LastState = ControllerSecondaryBoutonState;
            Device.TryGetFeatureValue(CommonUsages.secondaryButton, out ControllerSecondaryBoutonState);

            if(LastState != ControllerSecondaryBoutonState)
            {
                onSecondaryChange(DeviceName, ControllerSecondaryBoutonState);
            }
        }

        public void UpdateGripperState()
        {
            bool LastState = ControllerGripperState;
            Device.TryGetFeatureValue(CommonUsages.gripButton, out ControllerGripperState);

            if(LastState != ControllerGripperState)
            {
                onGripperStateChange(DeviceName, ControllerGripperState);
            }
        }

        public void UpdateTriggerState()
        {
            bool LastState = ControllerTriggerState;
            Device.TryGetFeatureValue(CommonUsages.triggerButton, out ControllerTriggerState);

            if(LastState != ControllerTriggerState)
            {
                onTriggerStateChange(DeviceName, ControllerTriggerState);
            }
        }

        public void UpdateThumbstickClick()
        {
            bool LastState = ControllerThumbstickClickState;
            Device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out ControllerThumbstickClickState);

            if(LastState != ControllerThumbstickClickState)
            {
                onThumbstickClickStateChange(DeviceName, ControllerThumbstickClickState);
            }
        }

        public void UpdateThumbstickTouch()
        {
            bool LastState = ControllerThumbstickTouchState;
            Device.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out ControllerThumbstickTouchState);

            if(LastState != ControllerThumbstickTouchState)
            {
                onThumbstickTouchStateChange(DeviceName, ControllerThumbstickTouchState);
            }
        }

        public void UpdateGripperValue()
        {
            float LastValue = ControllerGripperValue;
            Device.TryGetFeatureValue(CommonUsages.grip, out ControllerGripperValue);

            if(LastValue != ControllerGripperValue)
            {
                onGripperValueChange(DeviceName, ControllerGripperValue);
            }
        }

        public void UpdateTriggerValue()
        {
            float LastValue = ControllerTriggerValue;
            Device.TryGetFeatureValue(CommonUsages.trigger, out ControllerTriggerValue);

            if(LastValue != ControllerTriggerValue)
            {
                onTriggerValueChange(DeviceName, ControllerTriggerValue);
            }
        }

        public void UpdateThumbstickValue()
        {
            Vector2 LastValue = ControllerThumbstickValue;
            Device.TryGetFeatureValue(CommonUsages.primary2DAxis, out ControllerThumbstickValue);

            if(LastValue != ControllerThumbstickValue)
            {
                onThumbstickValueChange(DeviceName, ControllerThumbstickValue);
            }
        }

        public void UpdatePositionValue()
        {
            Vector3 LastValue = ControllerPositionValue;
            Device.TryGetFeatureValue(CommonUsages.devicePosition, out ControllerPositionValue);

            if(LastValue != ControllerPositionValue)
            {
                onHandPositionChange(DeviceName, ControllerPositionValue);
            }
        }

        public void UpdateRotationValue()
        {
            Vector3 LastValue = ControllerRotationValue;
            Quaternion Rotation;
            Device.TryGetFeatureValue(CommonUsages.deviceRotation, out Rotation);
            ControllerRotationValue = Rotation.eulerAngles;

            if(LastValue != ControllerRotationValue)
            {
                onHandRotationChange(DeviceName, ControllerRotationValue);
            }
        }

        public void UpdateSpeedValue()
        {
            Vector3 LastValue = ControllerSpeedValue;
            Device.TryGetFeatureValue(CommonUsages.deviceVelocity, out ControllerSpeedValue);

            if(LastValue != ControllerSpeedValue)
            {
                onHandSpeedChange(DeviceName, ControllerSpeedValue);
            }
        }
    }


    public class Head
    {
        public static event HeadPositionChange onHeadPositionChange;
        public static event HeadRotationChange onHeadRotationChange;
        public static event HeadSpeedChange onHeadSpeedChange;

        public Vector3 HeadControllerPositionValue, HeadControllerRotationValue, HeadControllerSpeedValue;
        private InputDevice Device;
        public string DeviceName;

        public Head(InputDevice head, string deviceName)
        {
            this.Device = head;
            this.DeviceName = deviceName;
        }

        public void UpdateHeadPositionValue()
        {
            Vector3 LastPosition = HeadControllerPositionValue;
            Device.TryGetFeatureValue(CommonUsages.devicePosition, out HeadControllerPositionValue);

            if(LastPosition != HeadControllerPositionValue)
            {
                onHeadPositionChange(DeviceName, HeadControllerPositionValue);
            }
        }

        public void UpdateHeadRotationValue()
        {
            Vector3 LastRotation = HeadControllerRotationValue;
            Quaternion Rotation;
            Device.TryGetFeatureValue(CommonUsages.deviceRotation, out Rotation);
            HeadControllerRotationValue = Rotation.eulerAngles;

            if(LastRotation != HeadControllerRotationValue)
            {
                onHeadRotationChange(DeviceName, HeadControllerRotationValue);
            }
        }

        public void UpdateHeadSpeedValue()
        {
            Vector3 LastSpeed = HeadControllerSpeedValue;
            Device.TryGetFeatureValue(CommonUsages.deviceVelocity, out HeadControllerSpeedValue);

            if(LastSpeed != HeadControllerSpeedValue)
            {
                onHeadSpeedChange(DeviceName, HeadControllerSpeedValue);
            }
        }
    }

    private XRNode XRLeftNode = XRNode.LeftHand;
    private InputDevice LeftHand, RightHand, head;                
    private List<InputDevice> LeftDevices = new List<InputDevice>();
    private List<InputDevice> RightDevices = new List<InputDevice>();
    private List<InputDevice> HeadDevices = new List<InputDevice>();

    public bool LeftDeviceFind = false, RightDeviceFind = false, HeadDeviceFind = false;
    Hand rightHand, leftHand;
    Head headDevice;

    private void Update()
    {
        if(!LeftDeviceFind)
            GetLeftDevice();
        else
        {
            leftHand = new Hand(LeftHand, "LeftHand");


            leftHand.UpdatePrimaryBouton();
            leftHand.UpdateSecondaryBouton();
            leftHand.UpdateGripperState();
            leftHand.UpdateTriggerState();
            leftHand.UpdateThumbstickClick();
            leftHand.UpdateThumbstickTouch();
            leftHand.UpdateGripperValue();
            leftHand.UpdateTriggerValue();
            leftHand.UpdateThumbstickValue();
            leftHand.UpdatePositionValue();
            leftHand.UpdateRotationValue();
            leftHand.UpdateSpeedValue();
        }

        if (!RightDeviceFind)
            GetRightDevice();
        else
        {
            rightHand = new Hand(RightHand, "RightHand");


            rightHand.UpdatePrimaryBouton();
            rightHand.UpdateSecondaryBouton();
            rightHand.UpdateGripperState();
            rightHand.UpdateTriggerState();
            rightHand.UpdateThumbstickClick();
            rightHand.UpdateThumbstickTouch();
            rightHand.UpdateGripperValue();
            rightHand.UpdateTriggerValue();
            rightHand.UpdateThumbstickValue();
            rightHand.UpdatePositionValue();
            rightHand.UpdateRotationValue();
            rightHand.UpdateSpeedValue();
        }

        if (!HeadDeviceFind)
            GetHeadDevice();
        else
        {
            headDevice = new Head(head, "head");
            headDevice.UpdateHeadPositionValue();
            headDevice.UpdateHeadRotationValue();
            headDevice.UpdateHeadSpeedValue();
        }
    }

    public void GetLeftDevice()
    {
        InputDevices.GetDevicesAtXRNode(XRLeftNode, LeftDevices);
        if (LeftDevices.Count > 0)
        {
            LeftHand = LeftDevices.FirstOrDefault();
            LeftDeviceFind = true;
        }
    }

    public void GetRightDevice()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, RightDevices);
        if (LeftDevices.Count > 0)
        {
            RightHand = RightDevices.FirstOrDefault();
            RightDeviceFind = true;
        }
    }

    public void GetHeadDevice()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.Head, HeadDevices);
        if (LeftDevices.Count > 0)
        {
            head = HeadDevices.FirstOrDefault();
            HeadDeviceFind = true;
        }
    }


    public bool GetPrimaryBouton(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerPrimaryBoutonState;

        return leftHand.ControllerPrimaryBoutonState;
    }

    public bool GetSecondaryBouton(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerSecondaryBoutonState;

        return leftHand.ControllerSecondaryBoutonState;
    }

    public bool GetGripperState(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerGripperState;

        return leftHand.ControllerGripperState;
    }

    public bool GetTriggerState(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerTriggerState;

        return leftHand.ControllerTriggerState;
    }

    public bool GetThumbstickClick(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerThumbstickClickState;

        return leftHand.ControllerThumbstickClickState;
    }

    public bool GetThumbstickTouch(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerThumbstickTouchState;

        return leftHand.ControllerThumbstickTouchState;
    }

    public float GetGripperValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerGripperValue;

        return leftHand.ControllerGripperValue;
    }

    public float GetTriggerValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerTriggerValue;

        return leftHand.ControllerTriggerValue;
    }

    public Vector2 GetThumbstickValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerThumbstickValue;

        return leftHand.ControllerThumbstickValue;
    }

    public Vector3 GetPositionValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerPositionValue;

        return leftHand.ControllerPositionValue;
    }

    public Vector3 GetRotationValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerRotationValue;

        return leftHand.ControllerRotationValue;
    }

    public Vector3 GetSpeedValue(string device)
    {
        if (device == "RightHand")
            return rightHand.ControllerSpeedValue;

        return leftHand.ControllerSpeedValue;
    }
}