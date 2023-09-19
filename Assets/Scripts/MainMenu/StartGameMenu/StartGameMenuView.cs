using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class StartGameMenuView : MonoBehaviour
    {
        [SerializeField] ConfirmationWidget _confirmation;
        

        public IMediator Mediator;

        private StartGameMenuController _controller;

        public StartGameMenuView Init(StartGameMenuController controller)
        {
            _controller = controller;

            _confirmation.OnClickNo += delegate () { Mediator.Notify(this, "Back"); };
            _confirmation.OnClickYes += delegate () { _controller.StartGame(); };
            
            return this;
        }
    }
}