using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Dialogue : MonoBehaviour
{
    public GameObject window;
    public GameObject indicator;
    public List<string> dialogues;
    private int index;
    private int charIndex;
    private bool started;
    public TMP_Text dialogueText;
    public float writingSpeed;
    private bool waitForNext;
    private void Awake()
    {
        ToggleWindow(false);
        ToggleIndicator(false);
    }
    public void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    public void StartDialogue()
    {
        if (started)
        {
            return;
        }
        started = true;
        ToggleWindow(true);
        ToggleIndicator(false);
        GetDialogue(0);
    }
    public void EndDialogue()
    {
        started = false;
        ToggleWindow(false);
        ToggleIndicator(true);
    }
    private void GetDialogue(int index)
    {
        this.index = index;
        charIndex = 0;
        dialogueText.text = "";
        StartCoroutine(WriteDialogue());
    }
    private IEnumerator WriteDialogue()
    {
        string currentDialogue = dialogues[index];
        dialogueText.text += currentDialogue[charIndex];
        charIndex++;
        if (charIndex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(WriteDialogue());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            if (index < dialogues.Count - 1)
            {
                GetDialogue(index + 1);
            }
            else
            {
                EndDialogue();
            }
            waitForNext = true;
        }
    }
    private void Update()
    {
        if (!started)
        {
            return;
        }
        if (waitForNext && Input.GetKeyDown(KeyCode.Space))
        {
            waitForNext = false;
            index++;
            if (index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
