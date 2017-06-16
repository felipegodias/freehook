using UnityEngine;

public class UILoadingFade : MonoBehaviour {

	private void Start () {
	    LeanTween.alphaCanvas(this.GetComponent<CanvasGroup>(), 0, 1).setDelay(0.25f)
            .setOnComplete(() => {
                Destroy(this.gameObject);
	        }
        ).setEaseOutSine();
	}

}
