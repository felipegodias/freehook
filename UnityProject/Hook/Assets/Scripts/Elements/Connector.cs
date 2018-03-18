using System;
using System.Collections.Generic;

using DG.Tweening;
using DG.Tweening.Core;

using UnityEngine;

[ExecuteInEditMode]
public class Connector : GameElement {

    [SerializeField]
    private SpriteRenderer[] lines;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private bool isWifi;

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
                line.size = new Vector2(0.001f, 0.001f);
            } else if (isWifi && gameElement is Connector && (gameElement as Connector).isWifi) {
                line.size = new Vector2(0.001f, 0.001f);
            } else {
                Vector3 dir = (gameElement.transform.position - this.transform.position).normalized;
                float distance = Vector3.Distance(gameElement.transform.position, this.transform.position);
                dir.z = 0;
                line.transform.right = dir;
                line.size = new Vector3(distance, 1);
            }
        }
    }

    protected override void LateUpdate() {
        if (!Application.isPlaying) {
            return;
        }
        base.LateUpdate();

        List<SpriteRenderer> renderersToFade = new List<SpriteRenderer>();
        for (int i = 0; i < this.GameElements.Length; i++) {
            SpriteRenderer line = this.lines[i];
            if (!line.enabled) {
                continue;
            }
            if (line.color == ColorUtils.LineColor && this.GameElements[i].IsHidden) {
                renderersToFade.Add(line);
            }
        }
        if (renderersToFade.Count > 0) {
            this.HideElements(renderersToFade.ToArray());
        }
    }

    private void HideElements(SpriteRenderer[] renderers) {
        List<SpriteRenderer> renderersToFade = new List<SpriteRenderer>();
        foreach (SpriteRenderer line in renderers) {
            if (line.color == ColorUtils.LineColor) {
                renderersToFade.Add(line);
            }
        }

        foreach (SpriteRenderer spriteRenderer in renderersToFade) {
            spriteRenderer.sortingOrder = RenderUtils.GetDrawOrder();
        }

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            Color a = ColorUtils.LineColor;
            Color b = ColorUtils.BackgroundColor;
            Color c = ColorUtils.Lerp(a, b, f);
            foreach (SpriteRenderer spriteRenderer in renderersToFade)
            {
                spriteRenderer.color = c;
            }
        };

        DOTween.To(getter, setter, 1, 0.33f);
    }

    public override void Hide() {
        List<SpriteRenderer> renderersToFade = new List<SpriteRenderer>();
        renderersToFade.Add(this.spriteRenderer);
        renderersToFade.AddRange(this.lines);
        this.HideElements(renderersToFade.ToArray());
    }

    private void OnDrawGizmos() {
        if (!this.isWifi) {
            return;
        }
        for (int i = 0; i < this.lines.Length; i++) {
            GameElement gameElement = this.GameElements[i];
            if (isWifi && gameElement is Connector && (gameElement as Connector).isWifi) {
                Gizmos.DrawLine(this.transform.position, gameElement.transform.position);
            }
        }
    }

}
