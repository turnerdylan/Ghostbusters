﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public virtual void Use()
    {
        print("using " + this.name);
    }
}
