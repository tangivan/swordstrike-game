using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for(int i = 0; i < heartContainers.initialValue; i ++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RunTimeValue / 2;
        for(int i = 0; i < heartContainers.RunTimeValue; i++)
        {
            if(i<= tempHealth-1)
            {
                //Full Heart
                hearts[i].sprite = fullHeart;
            }else if (i>= tempHealth)
            {
                //empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //half heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
