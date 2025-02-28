using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ChacaterData chacaterdata;
    public GameObject MenuBox;
    public GameObject Working;
    private Rigidbody2D rb;
    private PetAnim anim;
    private Vector2 Pet_Position;
    public float Speed;
    private bool isOpenMenu;
    [Header("时间盒")]
    private bool IsTime;
    private GameObject NowTime;
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject TimeTip;
    public TextMeshProUGUI TipSecond;
    public TextMeshProUGUI TipHour;
    [Header("计时器")]
    private int Runtime;
    private float RunHtime;
    [Header("闲逛计时器")]
    private Vector2 RoundPosition;
    private float RoundTime;
    private float RoundTime_Count = -1;
    [Header("状态")]
    private bool isHide;
    private bool isWalk;
    private bool isRound;
    private void Awake()
    {
        chacaterdata = GetComponent<ChacaterData>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PetAnim>();
        
    }
    private void Start()
    {
        Gamemanager.Instance.InitializedPlayer(chacaterdata);
        RunHtime = DataManager.Instance.LoadData();
    }
    private void Update()
    {
        var Position = Input.mousePosition;
        Pet_Position = Position;
        MenuBox.transform.localScale = transform.localScale;
        PetMove();
        OnRoundTime();
        RunTime();
        UpdateRunHTime();
        FixTimeBox();
    }
    
    private void PetMove()
    {
        if (math.abs(Pet_Position.x - transform.position.x) > 10 && !isOpenMenu && !isHide && !isRound)
        {
            anim.OnRun();
            Working.SetActive(false);
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
            Working.SetActive(false);
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
            Working.SetActive(true);
            Working.transform.localPosition = new Vector3(transform.localPosition.x + 62,transform.localPosition.y + 94,transform.localPosition.z);
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
       Menu.Instance.HideEvent += OnHide;
       Menu.Instance.WalkEvent += OnWalk;
       Menu.Instance.RoundEvent += OnRound;
       Menu.Instance.TimeTipEvent += OnTimeTip;
    }

    private void OnTimeTip()
    {
        IsTime = true;
        NowTime = TimeTip;
        TimeTip.SetActive(true);
        var ShowTime = Runtime / 60;
        TipSecond.text = ShowTime.ToString() + "分";
        var ShowHTime = Mathf.Round(DataManager.Instance.LoadData() * 10f) / 10f;
        TipHour.text = ShowHTime.ToString() + "时";
        MenuBox.SetActive(false);
        StartCoroutine(HoldTime(TimeTip));
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
        Menu.Instance.HideEvent -= OnHide;
        Menu.Instance.WalkEvent -= OnWalk;
        Menu.Instance.RoundEvent -= OnRound;
        Menu.Instance.TimeTipEvent += OnTimeTip;
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
    private void RunTime()
    {
        Runtime = (int)Time.realtimeSinceStartup;
        switch (Runtime)
        {
            case 3600:
                NowTime = One;
                One.SetActive(true);
                IsTime = true;
                StartCoroutine(HoldTime(One));
                break;
            case 7200:
                NowTime = Two;
                Two.SetActive(true);
                IsTime = true;
                StartCoroutine(HoldTime(Two));
                break;
            case 10800:
                NowTime = Three;
                Three.SetActive(true);
                IsTime = true;
                StartCoroutine(HoldTime(Three));
                break;
        }
    }
    private IEnumerator HoldTime(GameObject time)
    {
        yield return new WaitForSeconds(10f);
        IsTime = false;
        time.SetActive(false);
    }
    private void FixTimeBox()
    {
        if (IsTime)
        {
            if(NowTime == One)
            {
                One.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
            if (NowTime == Two)
            {
                Two.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
            if (NowTime == Three)
            {
                Three.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
            if(NowTime == TimeTip)
            {
                TimeTip.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            }
        }
    }
    private void UpdateRunHTime()
    {
        var ThisHTime = ((float)Runtime / 3600);
        chacaterdata.Time = RunHtime + ThisHTime;

    }
}
