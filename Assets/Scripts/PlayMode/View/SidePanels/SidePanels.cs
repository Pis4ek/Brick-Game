using PlayMode.BrickSpawnerElements;
using PlayMode.Level;
using PlayMode.Score;
using Services.Timer;
using UnityEngine;
using DG.Tweening;

namespace PlayMode.View
{
    public class SidePanels : MonoBehaviour
    {
        [SerializeField] RectTransform _infoPanel;
        [SerializeField] RectTransform _predication;

        public bool IsShown { get; private set; } = true;

        public SidePanels Init(BrickSpawningHolder brickHolder, BrickSpawnerData brickSpawnerData,
            IReadOnlyScoreData scoreData, IReadOnlyTimerData timer, IReadOnlyLevelData level)
        {
            GetComponentInChildren<InfoPanel>(true).Init(scoreData, timer, level);
            GetComponentInChildren<BrickPredicationView>(true).Init(brickSpawnerData);
            GetComponentInChildren<BrickHolderView>(true).Init(brickHolder);

            return this;
        }

        public void Show()
        {
            if (IsShown == false)
            {
                IsShown = true;
                transform.Activate();

                DOTween.Sequence()
                    .Append(_infoPanel.DOAnchorPos3DX(-125f, 0.3f))
                    .Append(_infoPanel.DOAnchorPos3DX(-100f, 0.1f));

                DOTween.Sequence()
                    .AppendInterval(0.2f)
                    .Append(_predication.DOAnchorPos3DX(-125f, 0.1f))
                    .Append(_predication.DOAnchorPos3DX(-100f, 0.3f));
            }
        }

        public void Hide()
        {
            if (IsShown)
            {
                IsShown = false;

                DOTween.Sequence()
                    .Append(_predication.DOAnchorPos3DX(-125f, 0.1f))
                    .Append(_predication.DOAnchorPos3DX(100f, 0.3f));

                DOTween.Sequence()
                    .AppendInterval(0.2f)
                    .Append(_infoPanel.DOAnchorPos3DX(-125f, 0.1f))
                    .Append(_infoPanel.DOAnchorPos3DX(100f, 0.3f))
                    .AppendCallback(() => { transform.Disactivate(); });
            }
        }
    }
}