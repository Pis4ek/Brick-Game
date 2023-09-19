using DG.Tweening;

namespace UnityEngine.UI
{
    public class ScaledConfirmationWidget : ConfirmationWidget
    {
        [Range(0.0001f, 0.75f)]
        [SerializeField] float _scalingPercent = 0.25f;
        [Range(0.0001f, 0.75f)]
        [SerializeField] float _duration = 0.5f;

        protected override void Start()
        {
            base.Start();

            _yesButton.onClick.AddListener(() => { ScaleButtons(_yesButton, _noButton); });
            _noButton.onClick.AddListener(() => { ScaleButtons(_noButton, _yesButton); });
        }

        protected override void Update()
        {
            base.Update();
        }

        private void ScaleButtons(Button clickedButton, Button unClickedButton)
        {
            DOTween.Sequence()
                .Append(clickedButton.transform.DOScale(1 + _scalingPercent, _duration / 2))
                .Append(clickedButton.transform.DOScale(1, _duration / 2))
                .AppendCallback(() => { });

            DOTween.Sequence()
               .Append(unClickedButton.transform.DOScale(1 - _scalingPercent, _duration / 2))
               .Append(unClickedButton.transform.DOScale(1, _duration / 2))
               .AppendCallback(() => { });

        }
    }
}
