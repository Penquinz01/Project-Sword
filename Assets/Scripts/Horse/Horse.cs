using StarterAssets_InputSystem;
using UnityEngine;

public class Horse : MonoBehaviour,IInteractable
{
    private bool mounted;
    
    [SerializeField]private Transform SaddlePoint;
    public Transform saddlePoint { get => SaddlePoint; private set => SaddlePoint = value; }
    private StarterAssetsInputs playerInput;
    private Player player;

    public void Interact(Player player)
    {
        Debug.Log("Mounting");
        this.player = player;
        playerInput = player._input;
        playerInput.SwitchActionMap("Horse");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
