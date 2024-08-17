using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite[] m_SpriteArray;
    public float m_Speed = 0.02f;
    private int m_IndexSprite;
    private float m_Timer;
    bool IsAnimating;

    void Start()
    {
        Func_PlayUIAnim();
    }

    void Update()
    {
        if (IsAnimating)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_Speed)
            {
                m_Timer = 0f;
                m_IndexSprite = (m_IndexSprite + 1) % m_SpriteArray.Length;
                m_Image.sprite = m_SpriteArray[m_IndexSprite];
            }
        }
    }

    public void Func_PlayUIAnim()
    {
        IsAnimating = true;
        m_Timer = 0f;
        m_IndexSprite = 0;
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
    }

    public void Func_StopUIAnim()
    {
        IsAnimating = false;
    }
}
