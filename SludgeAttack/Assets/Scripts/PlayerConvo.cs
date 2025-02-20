using UnityEditor;
using UnityEngine;

public class PlayerConvo : MonoBehaviour
{

    [SerializeField] float talkDistance = 2;

    bool inConverstaion;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inConverstaion)
            {
                Interact();
            }
        }
    }
    void Interact()
    {
        Debug.Log("Interact");

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.zero);

        if (hit)
        {
            Debug.Log("Hit Something");

            if (hit.collider.gameObject.TryGetComponent(out NPC npc))
            {

                GameManager.Instance.StartDialogue(npc.dialogueAsset, npc.npcName, npc.StartPosition);
            }

        }

    }

}
