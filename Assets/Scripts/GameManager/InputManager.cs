using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;}

    private PlayerInputActions inputActions;

    public Vector2 MovementInput { get; private set; }
    public Vector2 MousePosition { get; private set; }

    public event System.Action OnShoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => MovementInput = Vector2.zero;

        inputActions.Player.LookAt.performed += ctx => MousePosition = Mouse.current.position.ReadValue();
        inputActions.Player.Shoot.performed += ctx => OnShoot?.Invoke();
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();
}
