using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    [Header("Shop")]
    GameObject ShopUILayer;
    public Animation ShopBtnAnimation;
    public TextMeshProUGUI ShopPrompt;
    public TextMeshProUGUI ShopCostValue;

    private Shop currentShop;

    // Start is called before the first frame update
    void Start()
    {
        ShopUILayer = transform.GetChild(0).gameObject;
    }

    public void AddShopListener(Shop shop)
    {
        shop.OnEntering += ShowShopPrompt;
        shop.OnExiting += HideShopPrompt;
        shop.OnTryingShopping += ShoppingButtonAnimation;
    }

    public void ShowShopPrompt(Shop currentShop)
    {
        ShopUILayer.SetActive(true);
        ShopCostValue.text = currentShop.Cost.ToString();
        ShopPrompt.text = currentShop.stringPrompt;
        ShopPrompt.color = currentShop.color;
        this.currentShop = currentShop;
    }

    public void HideShopPrompt()
    {
        currentShop = null;
        ShopUILayer.SetActive(false);
    }

    public void TryShop()
    {
        bool validPayment = currentShop.TryShopping();

        ShoppingButtonAnimation(validPayment);

    }

    void ShoppingButtonAnimation(bool validPayment)
    {
        if (validPayment) {
            ShopBtnAnimation.Play("BtnGranted");
            ShopCostValue.text = currentShop.Cost.ToString();
        }
        else {
            ShopBtnAnimation.Play("BtnDenied");
        }
    }


}
