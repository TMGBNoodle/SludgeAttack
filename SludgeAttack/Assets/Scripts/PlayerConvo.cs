using UnityEditor;
using UnityEngine;

public class PlayerConvo : MonoBehaviour
{

    [SerializeField] float talkDistance = 2;


    private string[] dialogueText;

    bool inConverstaion = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inConverstaion)
            {
                Interact();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.EndDialogue();
        }
    }
    void Interact()
    {
        Debug.Log("Interact");

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.up,0, LayerMask.GetMask("NPC"));

        if (hit)
        {
            Debug.Log("Hit Something Dialogue");

            if (hit.collider.gameObject.TryGetComponent(out NPC npc))
            {
                if(npc.dialogueAsset.name == "Coffee") {
                    PlayerMovement.Instance.stamina = PlayerMovement.Instance.maxStamina;
                }
                GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.dialogueAsset.name, npc.StartPosition);
            }

        }

    }

}
