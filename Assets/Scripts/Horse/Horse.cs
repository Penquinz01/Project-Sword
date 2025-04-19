using UnityEngine;

public class Horse : MonoBehaviour,IInteractable
{
    private bool mounted;

    public void Interact()
    {
        Debug.Log("Mounting");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
