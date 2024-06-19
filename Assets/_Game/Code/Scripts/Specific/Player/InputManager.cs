using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public enum LookAtControls
    {
        Mouse,
        Controller
    }

    public static LookAtControls LookAtControl = LookAtControls.Mouse;

    [SerializeField] private Player _player;
    [SerializeField] private MovementController _mc;
    [SerializeField] private WeaponManager _wm;
    [SerializeField] private Laser _laser;
    [SerializeField] private LookToward _lookToward;
    [SerializeField] private Interacter _interacter;
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

        _inputs.Player.Interact.performed += Interact_performed;

        _inputs.Debug.Test01.performed += Test01_performed;

    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        _interacter.Interact();
    }

    private void Test01_performed(InputAction.CallbackContext obj)
    {
        PlayerSpawner.Instance.InstantiateObject();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        PauseManager.PauseGame();

        if (PauseManager.GetPauseStatus()) {
            DisableInputs();
        }
        else {
            EnableInputs();
        }

    }

    private void EnableInputs()
    {
        _inputs.Player.PrimaryFire.Enable();
        _inputs.Player.Laser.Enable();
        _inputs.Player.Interact.Enable();
    }

    private void DisableInputs()
    {
        _inputs.Player.PrimaryFire.Disable();
        _inputs.Player.Laser.Disable();
        _inputs.Player.Interact.Disable();
    }

    private void Look_Mouse(InputAction.CallbackContext obj)
    {
        LookAtControl = LookAtControls.Mouse;
        //_lookToward.LookAtMouse(obj.ReadValue<Vector2>());
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
    void FixedUpdate()
    {
        if (LookAtControl == LookAtControls.Mouse) {
            _lookToward.LookAtMouse(_inputs.Player.LookMouse.ReadValue<Vector2>());
        }
    }
}
