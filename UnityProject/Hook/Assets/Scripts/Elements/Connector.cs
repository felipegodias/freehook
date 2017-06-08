﻿using UnityEngine;

[ExecuteInEditMode]
public class Connector : GameElement {

    [SerializeField]
    private SpriteRenderer[] lines;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Update() {
        if (Application.isPlaying) {
            return;
        }

        if (this.lines == null) {
            return;
        }

        for (int i = 0; i < this.lines.Length; i++) {
            SpriteRenderer line = this.lines[i];
            GameElement gameElement = this.GameElements[i];

            if (line == null || gameElement == null) {
                continue;
            }

            if (gameElement is Switch) {
                line.size = Vector2.zero;
            } else {
                Vector3 dir = (gameElement.transform.position - this.transform.position).normalized;
                float distance = Vector3.Distance(gameElement.transform.position, this.transform.position);
                dir.z = 0;
                line.transform.right = dir;
                line.size = new Vector3(distance, 1);
            }
            
        }
    }

    public override void Hide() {
        LeanTween.value(this.gameObject, f => {
            Color a = ColorUtils.LineColor;
            Color b = a;
            b.a = 0;
            Color c = ColorUtils.Lerp(a, b, f);
            this.spriteRenderer.color = c;
            foreach (SpriteRenderer spriteRenderer in lines) {
                spriteRenderer.color = c;
            }
        }, 0, 1, 0.33f).setEase(LeanTweenType.easeOutSine);
    }

}
