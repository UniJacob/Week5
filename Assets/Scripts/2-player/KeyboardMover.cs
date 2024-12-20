using UnityEngine;
using UnityEngine.InputSystem;


/**
 * This component allows the player to move by clicking the arrow keys.
 */
public class KeyboardMover : MonoBehaviour
{

    [SerializeField] InputAction moveAction;
    [HideInInspector] public Vector2 LastDirection = new(0, 1);

    void OnValidate()
    {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveAction == null)
        {
            moveAction = new InputAction(type: InputActionType.Button);
        }
        if (moveAction.bindings.Count == 0)
        {
            moveAction.AddCompositeBinding("2DVector(mode=1)")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
        }
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    protected Vector3 NewPosition()
    {
        if (moveAction.WasPerformedThisFrame())
        {
            Vector3 movement = moveAction.ReadValue<Vector2>(); // Implicitly convert Vector2 to Vector3, setting z=0.
            if (movement.x != 0)
            {
                movement.x /=Mathf.Abs(movement.x);
            }
            if (movement.y != 0)
            {
                movement.y /=Mathf.Abs(movement.y);
            }
            //Debug.Log("movement: " + movement);
            LastDirection = movement;
            return transform.position + movement;
        }
        else
        {
            return transform.position;
        }
    }


    void Update()
    {
        transform.position = NewPosition();
    }
}
