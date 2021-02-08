using UnityEngine;
using System.Collections;

public class SelfDestructAfterTime : MonoBehaviour {

    [SerializeField] float destroyTime = 2.2f;
    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
