using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class Arrow : InGameObject
{
    public int m_ClusterNo;
    public float m_AxisX;
    public bool m_Change = false;

    public override void OnEnable()
    {
        base.OnEnable();
        m_AxisX = transform.position.x;
    }

    public override void AddListener()
    {
        EventManager.AddListener(GameEvent.ARROW_POSITION, ChangePosition);
        EventManager.AddListener(GameEvent.RETURN_POSITION, ReturnPositionOrigin);
        // EventManager1<int>.AddListener(GameEvent.RETURN_POSITION, SetupCluster);
    }

    public override void RemoveListener()
    {
        EventManager.RemoveListener(GameEvent.ARROW_POSITION, ChangePosition);
        EventManager.RemoveListener(GameEvent.RETURN_POSITION, ReturnPositionOrigin);
        // EventManager1<int>.RemoveListener(GameEvent.RETURN_POSITION, SetupCluster);
    }

    public void ChangePosition()
    {
        Transform tf = transform;
        if ((tf.position.x > 0f || tf.position.x < 0f) && !m_Change)
        {
            m_Change = true;
            float move = 0f;
            tf.DOKill();
            tf.DOLocalMoveX(move, 0.1f);
        }
        // else if (tf.position.x < 0f && !m_Change)
        // {
        //     m_Change = true;
        //     float move = tf.position.x / 2f;
        //     tf.DOMoveX(move, 1f);
        // }
    }

    public void ReturnPositionOrigin()
    {
        Transform tf = transform;
        if ((tf.position.x > 0f || tf.position.x < 0f) && m_Change)
        {
            m_Change = false;
            tf.DOKill();
            tf.DOLocalMoveX(m_AxisX, 0.1f);
        }
        // else if (tf.position.x < 0f && m_Change)
        // {
        //     m_Change = false;
        //     tf.DOMoveX(m_AxisX, 1f);
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

            other.enabled = false;

            Claim claim = other.GetComponent<Claim>();

            ArrowManager.Instance.m_TotalArrow -= 3;

            int result = ArrowManager.Instance.m_TotalArrow / 3;

            for (int i = 0; i < ArrowManager.Instance.m_Arrows.Count; i++)
            {
                if (i <= result - 1)
                {
                    ArrowManager.Instance.m_Arrows[i].gameObject.SetActive(true);
                }
                else
                {
                    ArrowManager.Instance.m_Arrows[i].gameObject.SetActive(false);
                }
            }
        }
        else if (other.tag.Equals("Claim"))
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

            other.enabled = false;

            Claim claim = other.GetComponent<Claim>();

            if (claim.m_ClaimOperator == ClaimOperator.MULTIPLY)
            {
                ArrowManager.Instance.m_TotalArrow *= claim.m_OperatorValue;
            }
            else if (claim.m_ClaimOperator == ClaimOperator.ADD)
            {
                ArrowManager.Instance.m_TotalArrow += claim.m_OperatorValue;
            }
            else if (claim.m_ClaimOperator == ClaimOperator.SUBTRACT)
            {
                ArrowManager.Instance.m_TotalArrow -= claim.m_OperatorValue;
                if (ArrowManager.Instance.m_TotalArrow <= 0)
                {
                    ArrowManager.Instance.m_TotalArrow = 2;
                }
            }
            else if (claim.m_ClaimOperator == ClaimOperator.DIVIDE)
            {
                ArrowManager.Instance.m_TotalArrow /= claim.m_OperatorValue;
                if (ArrowManager.Instance.m_TotalArrow <= 0)
                {
                    ArrowManager.Instance.m_TotalArrow = 2;
                }
            }

            int result = ArrowManager.Instance.m_TotalArrow / 3;
            ArrowManager.Instance.txt_ArrowNo.text = ArrowManager.Instance.m_TotalArrow.ToString();

            for (int i = 0; i < ArrowManager.Instance.m_Arrows.Count; i++)
            {
                if (i <= result - 1)
                {
                    ArrowManager.Instance.m_Arrows[i].gameObject.SetActive(true);
                }
                else
                {
                    ArrowManager.Instance.m_Arrows[i].gameObject.SetActive(false);
                }
            }
        }
        else if (other.tag.Equals("Ending"))
        {
            other.enabled = false;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    public void SetupCluster(int result)
    {
        if (result <= m_ClusterNo)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
