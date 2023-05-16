using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

    public enum LookAtControls {
        Mouse,
        Controller
    }

    public static LookAtControls LookAtControl = LookAtControls.Mouse;

    [SerializeField] private MovementController _mc;
    [SerializeField] private WeaponsManager _wm;
    [SerializeField] private Laser _laser;
    [SerializeField] private LookToward _lookToward;
    private CatnessOverloadInputs _inputs;

    // Start is called before the first frame update
    void Awake()
    {
        _inputs = new CatnessOverloadInputs();
        _inputs.Enable();

        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Move.canceled += Move_performed;

        _inputs.Player.PrimaryFire.performed += Fire_performed;
        _inputs.Player.PrimaryFire.canceled += Fire_performed;

        _inputs.Player.LookGamepad.performed += Look_Gamepad;
        _inputs.Player.LookMouse.performed += Look_Mouse;

        _inputs.Player.Laser.performed += Laser_performed;

        _inputs.Player.Pause.performed += Pause_performed;

        _inputs.Debug.Test01.performed += Test01_performed;

    }

    private void Test01_performed(InputAction.CallbackContext obj)
    {
        PlayerSpawner.Instance.InstantiateObject();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        PauseManager.PauseGame();
    }

    private void Look_Mouse(InputAction.CallbackContext obj)
    {
        _lookToward.LookAtMouse(obj.ReadValue<Vector2>());
        LookAtControl = LookAtControls.Mouse;
    }

    private void Look_Gamepad(InputAction.CallbackContext obj)
    {
        _lookToward.LookAtDirection(obj.ReadValue<Vector2>());
        LookAtControl = LookAtControls.Controller;
    }

    private void Laser_performed(InputAction.CallbackContext obj)
    {
        if (!_laser.Activated) {
            _laser.TurnOn();
        }
        else {
            _laser.TurnOff();
        }
    }

    private void Fire_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed) {
            _wm.ActivatePrincipalAction();
        }
        else {
            _wm.DeactivatePrincipalAction();
        }
    }

    private void Move_performed(InputAction.CallbackContext ctx)
    {
        _mc.MoveInput = ctx.performed ? ctx.ReadValue<Vector2>() : Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookAtControl == LookAtControls.Mouse) {
            _lookToward.LookAtDirection(_inputs.Player.LookMouse.ReadValue<Vector2>());
        }
    }
}
