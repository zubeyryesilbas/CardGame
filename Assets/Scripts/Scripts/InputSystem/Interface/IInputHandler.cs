using System;

namespace InputSystem
{   
    using UnityEngine;
    public interface IInputHandler
    {
        void ActivateOrDeactivateInput(bool val);
        void HandleInput();
        GameObject GetSelectedCard();
        Action<GameObject> OnCardClicked { get; set; }
        Action<GameObject> OnCardDropped { get; set; }
        Action<GameObject> OnCardStartDragging { get; set; }
        void SwitchDragging(bool enabled);
    }

}
