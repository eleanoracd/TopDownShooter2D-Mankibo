using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions inputActions;

    public Vector2 MousePosition { get; private set; }
    public bool IsShooting { get; private set; }

    public event System.Action OnShoot;
    public event System.Action<int> OnSwitchBullet;

    private void Awake()
    {
        // Ensure only one instance of InputManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInputActions();

        // Set up input bindings
        inputActions.Player.LookAt.performed += ctx => MousePosition = Mouse.current.position.ReadValue();
        inputActions.Player.Shoot.started += ctx => IsShooting = true;
        inputActions.Player.Shoot.canceled += ctx => IsShooting = false;

        inputActions.Player.Shoot.performed += ctx => OnShoot?.Invoke();

        inputActions.Player.SwitchBullet1.performed += _ => OnSwitchBullet?.Invoke(1);
        inputActions.Player.SwitchBullet2.performed += _ => OnSwitchBullet?.Invoke(2);
        inputActions.Player.SwitchBullet3.performed += _ => OnSwitchBullet?.Invoke(3);
        inputActions.Player.SwitchBullet4.performed += _ => OnSwitchBullet?.Invoke(4);
    }

    private void OnEnable()
    {
        // Ensure input actions are enabled
        if (inputActions != null)
        {
            inputActions.Player.Enable();
        }
    }

    private void OnDisable()
    {
        // Disable input actions and cleanup
        if (inputActions != null)
        {
            inputActions.Player.Disable();
        }
    }

    private void OnDestroy()
    {
        // Cleanup event subscriptions to avoid memory leaks
        if (inputActions != null)
        {
            inputActions.Player.LookAt.performed -= ctx => MousePosition = Mouse.current.position.ReadValue();
            inputActions.Player.Shoot.started -= ctx => IsShooting = true;
            inputActions.Player.Shoot.canceled -= ctx => IsShooting = false;

            inputActions.Player.Shoot.performed -= ctx => OnShoot?.Invoke();

            inputActions.Player.SwitchBullet1.performed -= _ => OnSwitchBullet?.Invoke(1);
            inputActions.Player.SwitchBullet2.performed -= _ => OnSwitchBullet?.Invoke(2);
            inputActions.Player.SwitchBullet3.performed -= _ => OnSwitchBullet?.Invoke(3);
            inputActions.Player.SwitchBullet4.performed -= _ => OnSwitchBullet?.Invoke(4);
        }

        // Nullify instance reference when destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }
}