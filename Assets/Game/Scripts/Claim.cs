using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Claim : MonoBehaviour
{
    public ClaimOperator m_ClaimOperator;
    public int m_OperatorValue;
    public TextMeshProUGUI txt_OperatorValue;

    private void OnEnable()
    {
        if (m_ClaimOperator == ClaimOperator.MULTIPLY)
        {
            txt_OperatorValue.text = "X" + m_OperatorValue;
        }
    }
}

public enum ClaimOperator
{
    MULTIPLY,
    DIVIDE,
    ADD,
    SUBTRACT,
}
