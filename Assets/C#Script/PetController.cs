using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject MenuBox;
    private Rigidbody2D rb;
    private PetAnim anim;
    private Vector2 Pet_Position;
    public float Speed;
    private bool isOpenMenu;
    [Header("ÏÐ¹ä¼ÆÊ±Æ÷")]
    private Vector2 RoundPosition;
    private float RoundTime;
    private float RoundTime_Count = -1;
    [Header("×´Ì¬")]
    private bool isHide;
    private bool isWalk;
    private bool isRound;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO HideEvent;
    public VoidEventSO WalkEvent;
    public VoidEventSO RoundEvent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PetAnim>();
    }
    private void Update()
    {
        var Position = Input.mousePosition;
        Pet_Position = Position;
        MenuBox.transform.localScale = transform.localScale;
        PetMove();
        OnRoundTime();
    }
    private void PetMove()
    {
        if (math.abs(Pet_Position.x - transform.position.x) > 10 && !isOpenMenu && !isHide && !isRound)
        {
            anim.OnRun();
            if (rb.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (rb.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            var Move = (Pet_Position - (Vector2)transform.position).normalized;
            rb.velocity = new Vector2(Move.x * Speed * 2, Move.y * Speed * 2);
        }
        if(math.abs(Pet_Position.x - transform.position.x) > 10 && !isOpenMenu && !isHide && isRound)
        {
            anim.OnRun();
            if (rb.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (rb.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            var Move = (RoundPosition - (Vector2)transform.position).normalized;
            rb.velocity = new Vector2(Move.x * Speed, Move.y * Speed);
        }
        if ((math.abs(Pet_Position.x - transform.position.x) <= 10 && !isRound) || isOpenMenu)
        {
            rb.velocity = Vector2.zero;
            anim.OnHide();
        }
    }
    public void OpenMenu()
    {
        isOpenMenu = !isOpenMenu;
        MenuBox.SetActive(isOpenMenu);
    }
    private void OnRoundTime()
    {
        if (isRound)
        {
            if (RoundTime_Count >= 0)
            {
                RoundTime_Count -= Time.deltaTime;
            }
            if (RoundTime_Count < 0)
            {
                RoundTime = UnityEngine.Random.Range(1, 5);
                RoundPosition.x = UnityEngine.Random.Range(1, 2560);
                RoundPosition.y = UnityEngine.Random.Range(1, 1600);
                RoundTime_Count = RoundTime;
            }
        }
    }
    private void OnEnable()
    {
        HideEvent.OnEventRaised += OnHide;
        WalkEvent.OnEventRaised += OnWalk;
        RoundEvent.OnEventRaised += OnRound;
    }
    private void OnHide()
    {
       isHide = true;
       isWalk = false;
       isRound = false;
       isOpenMenu = false;
       MenuBox.SetActive(false);
    }
    private void OnWalk()
    {
        isHide = false;
        isWalk = true;
        isRound = false;
        isOpenMenu = false;
        MenuBox.SetActive(false);
    }
    private void OnRound()
    {
        isHide = false;
        isWalk = false;
        isRound = true;
        isOpenMenu = false;
        MenuBox.SetActive(false);
    }
    private void OnDisable()
    {
        HideEvent.OnEventRaised -= OnHide;
        WalkEvent.OnEventRaised -= OnWalk;
        RoundEvent.OnEventRaised -= OnRound;
    }
    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
        isOpenMenu = false;
        MenuBox.SetActive(isOpenMenu);
    }
}
