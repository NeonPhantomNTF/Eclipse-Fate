using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Update()
    {
        // Get movement input (Vector2)
        Vector2 movement = _inputActions.Player.Move.ReadValue<Vector2>();
        // Move the player character
        transform.Translate(movement * Time.deltaTime * 5f);

        // Get jump input (e.g., Space key or button)
        if (_inputActions.Player.Jump.triggered)
        {
            // Handle jump action
            Debug.Log("Jump!");
        }
    }
}
