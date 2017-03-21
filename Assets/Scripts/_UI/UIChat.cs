using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIChat : MonoBehaviour {
    [SerializeField] GameObject panel;
    public InputField messageInput;
    [SerializeField] Button sendButton;
    [SerializeField] Transform content;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject textPrefab;
    [SerializeField] KeyCode[] activationKeys = {KeyCode.Return, KeyCode.KeypadEnter};

    [SerializeField] int keepHistory = 100;

    void Update() {
        var player = Utils.ClientLocalPlayer();
        panel.SetActive(player != null);
        if (!player)
            return;


    }

    void AutoScroll() {
        // update first so we don't ignore recently added messages, then scroll
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void AddMessage(MessageInfo msg) {
        // delete an old message if we have too many
        if (content.childCount >= keepHistory)
            Destroy(content.GetChild(0).gameObject);

        // instantiate the text
        var go = (GameObject)Instantiate(textPrefab);

        // set parent to Content object
        go.transform.SetParent(content.transform, false);

        // set text and color
        go.GetComponent<Text>().text = msg.content;
        go.GetComponent<Text>().color = msg.color;

        // TODO set sender for click reply
        //g.GetComponent<ChatTextEntry>().sender = sender;

        AutoScroll();
    }
}
