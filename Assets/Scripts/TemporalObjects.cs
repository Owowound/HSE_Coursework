using System.Collections;
using UnityEngine;

public class TemporalObjects : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(this.gameObject);
    }
}
