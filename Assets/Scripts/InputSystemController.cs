using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    public void HandleAttack(InputAction.CallbackContext context)
    {
        print(context.phase);

        if (context.performed)
        {
            print("Attack performed");
        }
        else if (context.started)
        {
            print("Attack started");
        }
        else if (context.canceled)
        {
            print("Attack canceled");
        }
    }
}
