using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public string itemName;
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogueContents;
    protected int collisionCount = 0; // Changed to protected

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void UseItem()
    {
        // By default, do nothing when using the item.
        // You can override this method in derived classes.
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("MakeContact");
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionCount++;
            ShowDialogue();
            UseItem();
        }
    }

    protected void ShowDialogue() // Changed to protected
    {
        Debug.Log("StartContext");
        if (collisionCount == 1 || collisionCount == 3 || collisionCount == 5)
        {
            int dialogueIndex = (collisionCount - 1) / 2;
            if (dialogueIndex < dialogueContents.Length)
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogueContents[dialogueIndex];
            }
        }
    }

    // Abstract method for items to appear (customize this in derived classes)
    public abstract void Appear();
}