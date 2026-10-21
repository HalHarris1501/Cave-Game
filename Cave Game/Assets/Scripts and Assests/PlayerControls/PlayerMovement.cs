using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Local Reference Variables
    public InputActionAsset InputActions;

    private InputAction _moveAction;
    private InputAction _lookAction;

    private Vector2 _moveAmount;
    private Vector2 _lookAmount;
    private Rigidbody _rigidBody;
    #endregion

    #region Variables
    [SerializeField][Range(0f, 100f)]
    private float _moveSpeed = 5f;
    [SerializeField][Range(5f, 300f)]
    private float _lookSensitivity = 100f;
    #endregion

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");

        _rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input value for looking and moving
        _moveAmount = _moveAction.ReadValue<Vector2>();
        _lookAmount = _lookAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Walk();
        Rotate();
    }

    //move player character
    private void Walk()
    {
        //set x value to be the Player's forward value, and Y to be the sideways value
        Vector3 moveVector = (transform.forward * _moveAmount.x) + (transform.right * _moveAmount.y);
        //move player
        _rigidBody.MovePosition(_rigidBody.position + (_moveSpeed * Time.deltaTime * (moveVector.normalized)));
    }

    //rotate player character
    private void Rotate()
    {
        //get rotation amount from input and calculate it using rotate speed
        float rotationAmount = _lookAmount.x * _lookSensitivity * Time.deltaTime;
        //set rotation amount to a Quaternion
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
        //rotate rigidbody
        _rigidBody.MoveRotation(_rigidBody.rotation * deltaRotation);
    }

}
