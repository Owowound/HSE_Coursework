using UnityEngine;

public class InteractiveManager : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject pointedObject;

    [SerializeField]
    private float interactDistance; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;

        int layerMask = LayerMask.GetMask("InteractiveObjects");

        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            GameObject hoveredObject = hit.collider.gameObject;

            InteractiveObject interactiveObject = hoveredObject.GetComponent<InteractiveObject>();
            if (interactiveObject != null)
            {
                interactiveObject.OnPointed();
                pointedObject = hoveredObject;
            }
        }
        else if (pointedObject != null)
        {
            pointedObject.GetComponent<InteractiveObject>().OnExit();
            pointedObject = null;
        }
    }

    public void Interact()
    {
        if (Vector2.Distance(pointedObject.transform.position, transform.position) <= interactDistance)
        {
            pointedObject.GetComponent<InteractiveObject>().Interact();
        }
    }
}
