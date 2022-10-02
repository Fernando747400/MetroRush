using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private int _currency;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Prepare();
    }

    public void AddCurrency(int amount)
    {
        _currency += amount;
    }

    public void RemoveCurrency(int amount)
    {
        _currency -= amount;
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
        Debug.Log("Saved currency " + _currency);
        PlayerPrefs.SetInt("Currency", _currency);
    }

    private void OnApplicationQuit()
    {
        SaveCurrency();
    }
}