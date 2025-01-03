using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(InputHandler))]
public class PlayerListener : MonoBehaviour
{
    [Header("Interact Parameters")]
    [SerializeField] private float interactDistance = 2f;

    public KitchenTable LastObject { get; private set; }

    private InputHandler _inputHandler;
    private PlayerInventory _inventory;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

    private void OnEnable()
    {
        _inputHandler.OnPickupOrDropAction += OnPickupOrDrop;
        _inputHandler.OnInteractAction += OnInteract;
    }

    private void OnDisable()
    {
        _inputHandler.OnPickupOrDropAction -= OnPickupOrDrop;
        _inputHandler.OnInteractAction -= OnInteract;
    }

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        UpdateClosestObject();
    }

    private void OnPickupOrDrop()
    {
        if (LastObject != null)
        {
            LastObject.OnPickupOrDrop(_inventory);
        }
    }

    private void OnInteract()
    {
        if (LastObject != null)
        {
            LastObject.Interact();
        }
    }

    private KitchenTable GetClosestObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

        KitchenTable closestObject = null;
        float minObjectDistance = interactDistance + 1.0f;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<KitchenTable>(out KitchenTable obj))
            {
                float objDistance = Vector3.Distance(transform.position, obj.transform.position);

                if (objDistance < minObjectDistance)
                {
                    minObjectDistance = objDistance;
                    closestObject = obj;
                }
            }
        }

        return closestObject;
    }

    private void UpdateClosestObject()
    {
        KitchenTable newFoundObject = GetClosestObject();
        if (newFoundObject != null)
        {
            if (newFoundObject != LastObject)
            {
                if (LastObject != null)
                {
                    // убрать эффект взаимодействия со старого LastObject-а
                    LastObject.OnPlayerLeaveInteractZone();
                    LastObject.HideOutline();
                }

                // добавить эффект взаимодействия на новый LastObject
                LastObject = newFoundObject;
                LastObject.ShowOutline();
            }
        }
        else
        {
            if (LastObject != null)
            {
                // убрать эффект взаимодействия со старого LastObject-а
                LastObject.OnPlayerLeaveInteractZone();
                LastObject.HideOutline();
                LastObject = null;
            }
        }
    }
}
