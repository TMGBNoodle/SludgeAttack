using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set;}

    private int charactersPerSecond = 10;

    public GameObject sludgeParent;

    [SerializeField] public GameObject dialoguePanel;

    [SerializeField] TextMeshProUGUI nameText;

    [SerializeField] TextMeshProUGUI dialogueText;

    public SludgeManager[] sludgeSpawners;


    public static event Action OnDialogueStarted;
    
    public static event Action OnDialogueEnded;

    bool skipLineTriggered;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        sludgeSpawners = sludgeParent.transform.GetComponentsInChildren<SludgeManager>();
        InvokeRepeating("spawnSludge", 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnSludge() {
        foreach (SludgeManager sludge in sludgeSpawners) {
            sludge.spawnSludge();
        }
    }

    public void ShowDialogue (string dialogue, string name)
    {
        nameText.text = name;
        StartCoroutine(dialogue);
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);

    }

    public void StartDialogue(String[] dialogue, string npcName, int startPosition)
    {

        nameText.text = npcName;

        dialoguePanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(RunDialogue(dialogue, startPosition));
    }


    IEnumerator RunDialogue(string[] dialogue, int startPosition)
    {

        skipLineTriggered = false;

        OnDialogueStarted?.Invoke();

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                yield return null;
            }

            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
    }

    IEnumerator TypeTextUncapped(string text)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = text.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];

                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            
        }
    }

}
