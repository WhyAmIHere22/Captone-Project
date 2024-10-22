using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class Coins : MonoBehaviour, IItem
{
    public static event Action<int> OnCoinCollect;
    public int worth = 5;
    public void Collect()
    {
        OnCoinCollect.Invoke(worth);
        SoundEffectManager.Play("Coin");
        gameObject.SetActive(false);
        
    }
} 