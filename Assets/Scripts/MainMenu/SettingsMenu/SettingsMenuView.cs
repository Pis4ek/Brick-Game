using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class SettingsMenuView : MonoBehaviour
    {
        [SerializeField] ConfirmationWidget _confirmation;

        public IMediator Mediator;

        public SettingsMenuView Init()
        {
            _confirmation.OnClickNo += delegate () { Mediator.Notify(this, "Back"); };

            return this;
        }
    }
}