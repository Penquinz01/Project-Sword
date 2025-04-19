using StarterAssets_InputSystem;
using UnityEngine;

public class Horse : MonoBehaviour,IInteractable
{
    private bool mounted = false;
    
    [SerializeField]private Transform SaddlePoint;
    public Transform saddlePoint { get => SaddlePoint; private set => SaddlePoint = value; }
    [SerializeField] private Transform unMountPosition;
    private StarterAssetsInputs playerInput;

    public void Interact()
    {
        Debug.Log("Mounting");
        playerInput.SwitchActionMap("Horse");
        Mount();
    }

    void Start()
    {
        playerInput = Player.Instance._input;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.unMounted)
        {
            playerInput.unMounted = false;
            Mount();
        }
    }
    private void Mount()
    {
        if (!mounted) {
            mounted = true;
            Player.Instance.transform.SetParent(SaddlePoint);
            Player.Instance.transform.localPosition = Vector3.zero;

        }
        else
        {
            mounted = false;
            Player.Instance.transform.SetParent(null);
            Player.Instance.transform.position = unMountPosition.position;
            playerInput.SwitchActionMap("Player");
        }
    }
}
