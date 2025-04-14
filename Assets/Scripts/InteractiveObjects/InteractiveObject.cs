using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private bool isPointed = false;


    public virtual void OnPointed()
    {
        Debug.Log($"Object {this.name} is pointed");
        isPointed = true;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public virtual void OnExit()
    {
        Debug.Log($"Object {this.name} is not pointed");
        isPointed = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void Interact()
    {
        Debug.Log($"Object {this.name} is activated");
    }

}
