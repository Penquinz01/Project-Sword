using UnityEngine;
using StarterAssets_InputSystem;

public class Player : MonoBehaviour
{
    public StarterAssetsInputs _input { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
