using MGS.EventManager;
using UnityEngine;

public class IAPManager : MonoBehaviour {

    [SerializeField]
    private Purchaser purchaser;

    private void Awake() {
        EventManager.AddListener<OnRemoveAdsButtonClicked>(this.OnRemoveAdsButtonClicked);
    }

    private void OnRemoveAdsButtonClicked(object sender, OnRemoveAdsButtonClicked eventArgs) {
        EventManager.Dispatch(new OnProcessPurchaseStart());
        purchaser.BuyNonConsumable();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnRemoveAdsButtonClicked>(this.OnRemoveAdsButtonClicked);
    }

}
