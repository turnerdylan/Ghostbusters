using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool isUnlocked = false;

    public virtual void Use()
    {
        print("using " + this.name);
    }
}
