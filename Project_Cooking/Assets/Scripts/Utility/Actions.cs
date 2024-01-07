using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
/// <summary>
/// This is the class that will be used to call the action methods when pressing buttons.
/// This class speaks to the input when an action is performed.
/// </summary>
public class Actions : MonoBehaviour
{

    [SerializeField] private LevelManager levelManager;
    private PauseMenu pauseMenu;
    private Input input;
    [HideInInspector] public UnityEvent OnItemSelect;
    [HideInInspector] public UnityEvent OnItemDrop;
    [HideInInspector] public UnityEvent OnInteract;
    [HideInInspector] public UnityEvent OnInteractHeld_Started;
    [HideInInspector] public UnityEvent OnInteractHeld_Cancelled;
    [HideInInspector] public UnityEvent OnPause; 

    [Header("Ability Events")]
    [HideInInspector] public UnityEvent OnAttack_Started;
    [HideInInspector] public UnityEvent<InputAction.CallbackContext> OnAttack_Started_Context;
    [HideInInspector] public UnityEvent OnAttack_Cancelled;
    [HideInInspector] public UnityEvent OnSpeed;
    [HideInInspector] public UnityEvent OnHeal;
    [HideInInspector] public UnityEvent OnScreech;

    private void Awake()
    {
        input = GetComponent<Input>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }
    private void Update()
    {
        input.interact.performed += Interact;
        input.interactHeld.started += InteractHeld_Started;
        input.interactHeld.canceled += InteractHeld_Cancelled;
        input.drop.performed += Drop;
        input.slotSelect.performed += SlotSelect;
        input.pause.performed += Pause;

        // abilities:
        input.speedAbilityIA.performed += ActivateSpeedAbility;
        input.healAbilityIA.performed += ActivateHealAbility;
        input.screechAbilityIA.performed += ActivateScreechAbility;
        input.attack.started += Attack_Started;
        input.attack.canceled += Attack_Cancelled;

        InputChange();
    }

    public void InputChange()
    {
        switch (levelManager.GetCurrentArea())
        {
            case Current_Area.LIMBO:
                input.DisableKitchenInputs();
                input.DisableDungeonInputs();
                break;
            case Current_Area.KITCHEN:
                input.EnableKitchenInputs();
                input.DisableDungeonInputs();
                break;
            case Current_Area.DUNGEON:
                input.EnableDungeonInputs();
                input.DisableKitchenInputs();
                break;
        }
    }
    public void Pause(InputAction.CallbackContext context) {
        pauseMenu.isPaused = !pauseMenu.isPaused;
        OnPause.Invoke();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        // This is where u put what interacting does
        // Default keybind is E [Keyboard]
        // Debug.Log("Pressed");
        OnInteract.Invoke();
    }
    public void InteractHeld_Started(InputAction.CallbackContext context)
    {
        // Default keybind is E [Keyboard]

        // Debug.Log("Held Started");
        OnInteractHeld_Started.Invoke();
    }
    public void InteractHeld_Cancelled(InputAction.CallbackContext context)
    {
        // Default keybind is E [Keyboard]

        // Debug.Log("Held Cancelled");
        OnInteractHeld_Cancelled.Invoke();
    }
    public void Attack_Started(InputAction.CallbackContext context)
    {
        // This is where u put what attacking does
        // Default keybind is Left Button [Mouse]
        OnAttack_Started.Invoke();
        OnAttack_Started_Context.Invoke(context);
    }
    public void Attack_Cancelled(InputAction.CallbackContext context)
    {
        // This is where u put what attacking does
        // Default keybind is Left Button [Mouse]
        OnAttack_Cancelled.Invoke();
    }
    public void ActivateSpeedAbility(InputAction.CallbackContext context)
    {
        OnSpeed.Invoke();
    }
    public void ActivateHealAbility(InputAction.CallbackContext context)
    {
        OnHeal.Invoke();
    }
    public void ActivateScreechAbility(InputAction.CallbackContext context)
    {
        OnScreech.Invoke();
    }
    public void Drop(InputAction.CallbackContext context)
    {
        // This is where u put what dropping does
        // Default keybind is Q [Keyboard]
        OnItemDrop.Invoke();
    }
    public void SlotSelect(InputAction.CallbackContext context)
    {
        // This is where u put what slot select does
        // Default keybind is Scroll Wheel Up/Down [Mouse]
        //  UP is 120f,  DOWN is -120f  ----> input.slotSelect.ReadValue<float>()
        OnItemSelect.Invoke();
    }
}
