using UnityEditor;
using UnityEngine;

public class PlayerConvo : MonoBehaviour
{

    [SerializeField] float talkDistance = 2;


    private string[] dialogueText;

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

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.up,0, LayerMask.GetMask("NPC"));

        if (hit)
        {
            Debug.Log("Hit Something");

            if (hit.collider.gameObject.TryGetComponent(out NPC npc))
            {
                GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.npcName, npc.StartPosition);
            }

        }

    }

}
