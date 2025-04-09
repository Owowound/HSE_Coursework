using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private bool isPointed = false;


    public virtual void OnPointed()
    {
        Debug.Log($"На объект {this.name} наведены");
        isPointed = true;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public virtual void OnExit()
    {
        Debug.Log($"На объект {this.name} наведены");
        isPointed = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void Interact()
    {
        Debug.Log($"Объект {this.name} активирован");
    }

}
