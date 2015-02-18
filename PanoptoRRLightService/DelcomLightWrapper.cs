﻿using System.Text;

namespace RRLightProgram
{
    internal class DelcomLightWrapper
    {
        // NOTE: These must stay in sync with the DelcomDll.*LED values
        public enum LightColors : byte
        {
            Green = 0,
            Red = 1,
            Blue = 2,
            Yellow = 2,
        }

        // NOTE: These must stay in sync with the DelcomDll.LED* values
        public enum LightStates : byte
        {
            Off = 0,
            On = 1,
            Flash = 2,
        }

        // NOTE: These map exactly to the values returned by DelcomDll.DelcomGetButtonStatus
        public enum ButtonState : int
        {
            NotPressed = 0,
            Pressed = 1,
            Unknown = 2,
        }

        public static uint OpenDelcomDevice()
        {
            int Result;
            uint hUSB;
            StringBuilder DeviceName = new StringBuilder(Delcom.MAXDEVICENAMELEN);

            // Serach for the first match USB device, For USB IO Chips use Delcom.USBIODS
            // With Generation 2 HID devices, you can pass a TypeId of 0 to open any Delcom device.
            Result = Delcom.DelcomGetNthDevice(Delcom.USBDELVI, 0, DeviceName);

            hUSB = Delcom.DelcomOpenDevice(DeviceName, 0);                      // open the device
            return hUSB;
        }

        public static void CloseDelcomDevice(uint hUSB)
        {
            Delcom.DelcomCloseDevice(hUSB);
        }

        public static void DelcomLEDOn(uint hUSB, LightColors color, LightStates action)
        {
            DelcomLEDOffAction(hUSB);

            Delcom.DelcomLEDControl(hUSB, (byte)color, (byte)action);
        }

        public static void DelcomLEDAllOn(uint hUSB, LightStates action)
        {
            DelcomLEDOffAction(hUSB);
            Delcom.DelcomLEDControl(hUSB, Delcom.REDLED, (byte)action);
            Delcom.DelcomLEDControl(hUSB, Delcom.GREENLED, (byte)action);
            Delcom.DelcomLEDControl(hUSB, Delcom.YELLOWLED, (byte)action);
            Delcom.DelcomLEDControl(hUSB, Delcom.BLUELED, (byte)action);
        }

        public static void DelcomLEDOffAction(uint hUSB)
        {
            Delcom.DelcomLEDControl(hUSB, Delcom.REDLED, (byte)LightStates.Off);
            Delcom.DelcomLEDControl(hUSB, Delcom.GREENLED, (byte)LightStates.Off);
            Delcom.DelcomLEDControl(hUSB, Delcom.YELLOWLED, (byte)LightStates.Off);
            Delcom.DelcomLEDControl(hUSB, Delcom.BLUELED, (byte)LightStates.Off);
        }

        public static ButtonState DelcomGetButtonStatus(uint hUSB)
        {
            return (ButtonState)Delcom.DelcomGetButtonStatus(hUSB);
        }
    }
}