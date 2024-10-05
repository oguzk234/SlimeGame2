using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float InteractRange;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 rayVec = PlayerMove.Instance.MoveGoChecked.normalized;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, rayVec, InteractRange*0.0675f);
            Debug.DrawRay(transform.position, rayVec * (InteractRange*0.0675f));

            if (ray != default && ray.collider != null && ray.collider.TryGetComponent(out IInteractable interactable))
            {
                //Debug.Log("PICKUP Ray hit: " + ray.collider.name);

                if (DialogManager.Instance.isOnDialogue == true || PlayerMove.Instance.isMoveInputGetting == false){ return; }   //KOSTEBEKLE DIALOG SORUNUNU COZDU BU WORTH
                if(interactable != null) { interactable.Interact(); print("Interacted With " + ray.collider.name); }
            }
        }
    }
}
