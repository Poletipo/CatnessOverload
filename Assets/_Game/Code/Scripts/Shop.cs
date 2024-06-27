using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{

    public Action<Shop> OnEntering;
    public Action OnExiting;
    public Action<bool> OnTryingShopping;

    public int Cost;
    public float CostIncreaseMultiplier = 1.5f;
    public Color color;
    public AudioClip BuySound;
    public AudioClip DenySound;

    public bool _playerInZone = false;
    public Player _player;

    public string stringPrompt;

    abstract public void Upgrade();

    private void Start()
    {

        if (GameManager.Instance.UIManager == null) {
            return;
        }

        GameUI UI = GameManager.Instance.UIManager.
            GetUIWidget(typeof(GameUI)).GetComponent<GameUI>();
        UI.ShopMenu.AddShopListener(this);
    }

    public bool TryShopping()
    {

        if (_player == null) {
            return false;
        }
        bool validPayment = _player.Pay(Cost);

        if (validPayment) {
            GameManager.Instance.AudioManager.PlayOneShot(BuySound);
            Upgrade();
        }
        else {
            GameManager.Instance.AudioManager.PlayOneShot(DenySound);
        }

        OnTryingShopping?.Invoke(validPayment);

        return validPayment;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            _player = other.GetComponent<Player>();
            _playerInZone = true;
            OnEntering?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            _player = null;
            _playerInZone = false;
            OnExiting?.Invoke();
        }
    }

}
