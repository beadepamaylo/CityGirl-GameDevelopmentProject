using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum SIDE {Left, Mid, Right}

public class Character : MonoBehaviour
{
    
    public SIDE m_Side = SIDE.Mid;
    float NewXPOS = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown; 
    public float XValue;
    private CharacterController m_char;
    private Animator m_Animator;
    private float x;
    public float SpeedDodge;
    public float FwdSpeed;
    public float JumpPower = 7f;
    private float y;
    public bool InJump, InRoll;
    private float ColHeight;
    private float ColCenterY;
    private float RollCounter;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerManager.isGameStarted)
            return;
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        if (SwipeLeft && !InRoll)
        {
            if(m_Side == SIDE.Mid)
            {
                NewXPOS = -XValue;
                m_Side = SIDE.Left;
            } else if (m_Side == SIDE.Right)
            {
                NewXPOS = 0;
                m_Side = SIDE.Mid;
            }
        }
        else if (SwipeRight && !InRoll)
        {
            if(m_Side == SIDE.Mid)
            {
                NewXPOS = XValue;
                m_Side = SIDE.Right;
            } else if (m_Side == SIDE.Left)
            {
                NewXPOS = 0;
                m_Side = SIDE.Mid;
            }
        }

        Vector3 moveVector = new Vector3(NewXPOS - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
        //x = Mathf.Lerp(x, NewXPOS, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);
        Jump();
        Roll();
    }

    public void Jump()
    {
        if(SwipeUp)
        {
            y = JumpPower;
            m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
            InJump = true;
        }else
        {
            y -= JumpPower * 2 * Time.deltaTime;
            if(m_char.velocity.y < -0.1f)
            {
                InJump = false;
            }
        }
    }

    public void Roll()
    {
        RollCounter -= Time.deltaTime;
        if(RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0 ,ColCenterY, 0);
            m_char.height = ColHeight;
            InRoll = false;
        }
        if(SwipeDown)
        {
            RollCounter = 0.2f;
            y -= 10f;
            m_char.center = new Vector3(0 ,ColCenterY/2f, 0);
            m_char.height = ColHeight/2f;
            m_Animator.CrossFadeInFixedTime("slide", 0.1f);
            InRoll = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("gameOver");
        }
        if(hit.transform.tag == "FinishLine")
        {
            PlayerManager.NextLevel = true;
            FindObjectOfType<AudioManager>().PlaySound("levelComplete");
        }
    }

}
