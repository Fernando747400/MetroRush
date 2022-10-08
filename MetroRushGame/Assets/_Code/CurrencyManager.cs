using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private int _runCurrency;
    private int _currency;

    public event Action UpdatedCurrency;
    public int RunCurrency { get => _runCurrency; }

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        Prepare();
        _runCurrency = 0;
    }

    public void AddCurrency(int amount)
    {
        _currency += amount;
        _runCurrency += amount;
        UpdatedCurrency?.Invoke();
    }

    public void RemoveCurrency(int amount)
    {
        _currency -= amount;
        _runCurrency -= amount;
        UpdatedCurrency?.Invoke();
        SaveCurrency();
    }

    private void Prepare()
    {
        if (!PlayerPrefs.HasKey("Currency"))
        {
            PlayerPrefs.SetInt("Currency", 0);
            _currency = 0;
        }
        else
        {
            _currency = PlayerPrefs.GetInt("Currency");
        }

        GameManager.Instance.GameOver.AddListener(SaveCurrency);
    }

    private void SaveCurrency()
    {
        UpdatedCurrency?.Invoke();
        Debug.Log("Saved currency " + _currency);
        PlayerPrefs.SetInt("Currency", _currency);
    }

    private void OnApplicationQuit()
    {
        SaveCurrency();
    }
}