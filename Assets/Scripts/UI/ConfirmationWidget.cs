using System;

namespace UnityEngine.UI
{
    public class ConfirmationWidget : MonoBehaviour
    {
        public event Action OnClickYes;
        public event Action OnClickNo;

        [Header("Buttons")]
        [SerializeField] protected Button _yesButton;
        [SerializeField] protected Button _noButton;
        [Header("Input")]
        [SerializeField] bool _useKeyForYesButton = false;
        [SerializeField] bool _useKeyForNoButton = false;
        [Header("Keys")]
        [SerializeField] KeyCode _keyForYesButton = KeyCode.Return;
        [SerializeField] KeyCode _keyForNoButton = KeyCode.Escape;

        protected virtual void Start()
        {
            _yesButton.onClick.AddListener(delegate () { OnClickYes?.Invoke(); });
            _noButton.onClick.AddListener(delegate () { OnClickNo?.Invoke(); });
        }

        protected virtual void Update()
        {
            if (_useKeyForYesButton && Input.GetKeyDown(_keyForYesButton))
                OnClickYes?.Invoke();
            if (_useKeyForNoButton && Input.GetKeyDown(_keyForNoButton))
                OnClickNo?.Invoke();
        }
    }
}