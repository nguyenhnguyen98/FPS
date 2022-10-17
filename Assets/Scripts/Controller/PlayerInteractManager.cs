using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField]
    private float _distance = 3f;
    [SerializeField]
    private LayerMask _mask;
    private UIManager _uiManager;
    private InputManager _inputManager;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<PlayerLook>().cam;
        _uiManager = GetComponent<UIManager>();
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _uiManager.UpdateText(string.Empty);

        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * _distance);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distance, _mask))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                _uiManager.UpdateText(interactable.promptMessage);
                if (_inputManager._onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
        
    }
}
