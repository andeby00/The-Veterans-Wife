using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private Keybindings keybindings;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public KeyCode GetKeyForAction(KeybindingActions keybindingAction)
    {
        // Find KeyCode
        foreach(Keybindings.KeybindingCheck keybindingCheck in keybindings.KeybindingChecks)
        {
            if(keybindingCheck.actions == keybindingAction)
            {
                return keybindingCheck.keyCode;
            }
        }
        return KeyCode.None;

    }

    public bool GetKeyDown(KeybindingActions key)
    {
        // Check for key press
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.KeybindingChecks)
        {
            if (keybindingCheck.actions == key)
            {
                return Input.GetKeyDown(keybindingCheck.keyCode);
            }
        }

        return false;
    }

    public bool GetKey(KeybindingActions key)
    {
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.KeybindingChecks)
        {
            if (keybindingCheck.actions == key)
            {
                return Input.GetKey(keybindingCheck.keyCode);
            }
        }

        return false;
    }

    public bool GetKeyUp(KeybindingActions key)
    {
        foreach (Keybindings.KeybindingCheck keybindingCheck in keybindings.KeybindingChecks)
        {
            if (keybindingCheck.actions == key)
            {
                return Input.GetKeyUp(keybindingCheck.keyCode);
            }
        }

        return false;
    }
}
