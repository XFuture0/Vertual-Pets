using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PetAnim : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public void OnHide()
    {
        anim.SetBool("speed", false);
    }
    public void OnRun()
    {
        anim.SetBool("speed", true);
    }

}
