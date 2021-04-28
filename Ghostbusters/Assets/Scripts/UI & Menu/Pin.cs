using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    GameObject child;
    [SerializeField] List<Sprite> stars = new List<Sprite>();
    public int starCount = 0;

    [SerializeField] GameObject starUI;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        starUI.GetComponent<SpriteRenderer>().sprite = stars[starCount];
    }

    public GameObject GetChild()
    {
        return child;
    }
}
