using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// This script reads input made by the player and sends those messages to the scripts that are listening for those inputs
/// This script does nothing but listen for input and send a message
/// </summary>
public class InputListener : MonoBehaviour
{
    [SerializeField] private UnityEvent OnInteract;             //What methods are called when interacting
    [SerializeField] private UnityEvent OnReload;               //What methods are called when reloading
    [SerializeField] private UnityEvent<bool> OnAttack;         //What methods are called when attacking
    [SerializeField] private UnityEvent<Vector2> OnMove;        //What methods are called when moving
    [SerializeField] private UnityEvent<Vector2> OnMouseMove;   //What methods are called when moving the mouse

    private PlayerInput _playerInput;       //Input system provided by Unity (New Input System)
    private InputAction _movement;          //Action that tracks the player's movement
    private InputAction _mouseMovement;     //Action that tracks the player's mouse movement

    private bool _attackHeld = false;       //Tracks if the attack button is being held

    /// <summary>
    /// At start of game, set up the input system
    /// </summary>
    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    /// <summary>
    /// On enable, make sure this object is listening for state change events
    /// Then activate the state change based on the current state to make sure the player is fully set up
    /// </summary>
   

    /// <summary>
    /// When the object is disabled
    /// Unsubscribe from state change
    /// Ideally, all other subscriptions would also be removed
    /// </summary>
    

    /// <summary>
    /// On Update, send the movement and mouse movement variables to the input handler
    /// </summary>
    private void Update()
    {
        OnMouseMove.Invoke(_mouseMovement.ReadValue<Vector2>());
        OnMove.Invoke(_movement.ReadValue<Vector2>());
    }

    /// <summary>
    /// When the state changes, this method is fired and turns on/off all the interactions that are/aren't allowed in that state
    /// </summary>
    

    /// <summary>
    /// When the attack button is pressed down, start a coroutine that will continue until _attack held it false
    /// The value becomes false when the button is released
    /// </summary>
    private void Attack(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            _attackHeld = true;
            StartCoroutine(Attacking());
        }
        else
        {
            _attackHeld = false;
        }
    }

    /// <summary>
    /// Sends a message to the weapon system to attack
    /// If the weapon is not automatic it only does this on the first request
    /// If it is automatic, it will send a message all the time
    /// </summary>
    private IEnumerator Attacking()
    {
        bool firstAttack = true;

        while(_attackHeld == true)
        {
            OnAttack?.Invoke(firstAttack);
            firstAttack = false;
            yield return null;
        }
    }

    /// <summary>
    /// Turns on and subscribes all gameplay related features
    /// </summary>
    

    /// <summary>
    /// When called, sends a message to Reload listener to perform the reload action
    /// </summary>
    private void Reload(InputAction.CallbackContext obj)
    {
        OnReload?.Invoke();
    }

    /// <summary>
    /// When called, sends a message to Interact listener to perform the interact action
    /// </summary>
    private void Interact(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }

    /// <summary>
    /// Turns off and subscribes all gameplay related features
    /// </summary>
    
}
