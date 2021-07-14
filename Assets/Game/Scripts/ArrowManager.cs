using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlFreak2;

public class ArrowManager : Singleton<ArrowManager>
{
    public CharacterController cc_Owner;

    public TouchTrackPad m_JoystickTrackPad;
    public TouchJoystick m_JoystickTrackPad1;
    public float m_MoveFactor;
    public bool m_OutRange = false;

    public int m_TotalArrow;

    public List<Arrow> m_Arrows;

    public override void OnEnable()
    {
        base.OnEnable();
        m_TotalArrow = 3;
        for (int i = 0; i < m_Arrows.Count; i++)
        {
            m_Arrows[i].m_ClusterNo = i + 1;
        }

        int result = m_TotalArrow / 3;

        for (int i = 0; i < m_Arrows.Count; i++)
        {
            if (i <= result - 1)
            {
                m_Arrows[i].gameObject.SetActive(true);
            }
            else
            {
                m_Arrows[i].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        // if (m_JoystickTrackPad1.Pressed())
        // if (m_JoystickTrackPad.Pressed())
        // {
        // Vector3 moveInput = new Vector3(CF2Input.GetAxis("Mouse X"), 0f, 0f);
        Vector3 moveInput = new Vector3(CF2Input.GetAxis("Joystick Move X"), 0f, 2f);
        // Vector3 moveInput = new Vector3(CF2Input.GetAxis("Mouse X"), 0f, 0f);
        // Debug.Log(moveInput.x);
        moveInput = moveInput.normalized;
        // transform.position += new Vector3(moveInput.x / 100f, moveInput.y, moveInput.z);

        // cc_Owner.Move(new Vector3(moveInput.x / m_MoveFactor, transform.position.y, transform.position.z));
        // cc_Owner.Move(new Vector3(moveInput.x / m_MoveFactor, transform.position.y, transform.position.z));

        moveInput = moveInput.normalized;

        float gravity = 0f;

        gravity -= 9.81f * Time.deltaTime;
        cc_Owner.Move(new Vector3(moveInput.x / m_MoveFactor, gravity, moveInput.z * 0.25f));
        if (cc_Owner.isGrounded) gravity = 0;

        if ((transform.position.x < -2.2f || transform.position.x > 2.2f))
        {
            if (!m_OutRange)
            {
                m_OutRange = true;
                EventManager.CallEvent(GameEvent.ARROW_POSITION);
            }
        }
        else
        {
            if (m_OutRange)
            {
                m_OutRange = false;
                EventManager.CallEvent(GameEvent.RETURN_POSITION);
            }
        }

        // }

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     EventManager.CallEvent(GameEvent.ARROW_POSITION);
        // }
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     EventManager.CallEvent(GameEvent.RETURN_POSITION);
        // }
    }
}
