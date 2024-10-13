using System;
using Unity.VisualScripting;
using UnityEngine;

namespace InputSystem
{
    public class DesktopInputHandler : MonoBehaviour, IInputHandler
    {
        [SerializeField] private GameObject selectedCard;
        [SerializeField] private LayerMask cardLayerMask;
        public Action<GameObject> OnCardClicked { get; set; }
        public Action<GameObject> OnCardDropped { get; set; }
        public Action<GameObject> OnCardStartDragging { get; set; }
        private bool _draggingEnabled = false;
        private bool _inputsEnabled = true;
        public void SwitchDragging(bool enabled)
        {
            _draggingEnabled = enabled;
        }

        private void Update()
        {
            if (_inputsEnabled)
            {
                HandleInput();
            }
        }
      
        public void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {   
                OnPointerDown(Input.mousePosition);
            }

            if (_draggingEnabled)
            {
                if (Input.GetMouseButton(0))
                {   
                    OnPointerDrag(Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {   
                    OnPointerUp();
                }
            }
        }

        public void ActivateOrDeactivateInput(bool val)
        {
            _inputsEnabled = val;
        }

        public GameObject GetSelectedCard()
        {
            return selectedCard;
        }

        private void OnPointerDown(Vector3 inputPosition)
        {   
            Vector3 mousePosition = Input.mousePosition;

            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // Perform the raycast, make sure cardLayerMask is used correctly
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cardLayerMask))
            {
                // Check if we hit an object
                Debug.Log("Hit object: " + hit.collider.name);
                selectedCard = hit.transform.gameObject;
                if (_draggingEnabled)
                {
                    OnCardStartDragging?.Invoke(selectedCard);
                }
                else
                {
                    OnCardClicked?.Invoke(selectedCard);
                }
            }
            else
            {
                Debug.Log("No object hit");
            }
        }


        private void OnPointerDrag(Vector3 inputPosition)
        {
            if (selectedCard != null)
            {   
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 10f));
               selectedCard.transform.position = new Vector3(worldPosition.x, worldPosition.y, -0.2f);
            }
        }

        private void OnPointerUp()
        {
            if (selectedCard != null)
            {
                OnCardDropped?.Invoke(selectedCard);
                selectedCard = null;
            }
        }
        
    }
}

