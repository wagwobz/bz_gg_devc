using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chibi : MonoBehaviour
{
    Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }
}
