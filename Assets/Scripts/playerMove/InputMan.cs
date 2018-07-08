using UnityEngine;

public class InputMan {
    public static float getAxisRaw(AXIS a, int controlNumber){
        switch(a) {
            case AXIS.H:
                return Input.GetAxisRaw("Horizontal" + controlNumber);
            case AXIS.V:
                return Input.GetAxisRaw("Vertical" + controlNumber);
            default:
                return 0.0f;
        }
    }

    public static bool GetButton(BUTTON b, int controlNumber) {
        switch(b) {
            case BUTTON.A:
                return Input.GetButtonDown("Punch" + controlNumber);
            case BUTTON.B:
                return Input.GetButtonDown("Kick" + controlNumber);
            default:
                return false;
        }
    }

    public enum AXIS {
        H, 
        V
    }

    public enum BUTTON {
        A,
        B,
        Y, 
        Z,
    }
}