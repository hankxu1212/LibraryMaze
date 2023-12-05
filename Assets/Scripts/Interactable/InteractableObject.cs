using UnityEngine;

/// <summary>
/// A base class for interactable objects
/// </summary>

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string ID; // object id, should uniquely identifies an object
    [SerializeField] public string interactText;
    [SerializeField] protected int maxInteractions;
    protected int interactionsOccured = 0;
    protected bool disabled;
    
    #region IInteractable
    
    public virtual void Interact()
    {
        Debug.Log("Interacted!");
    }

    public virtual bool CanInteract()
    {
        return !disabled && interactionsOccured < maxInteractions;
    }

    public virtual void Disable()
    {
        disabled = true;
    }
    
    public virtual void Enable()
    {
        disabled = false;
    }
    
    public virtual string GetInteractText() => interactText;
    
    public virtual Transform GetTransform() => transform;

    #endregion
}
