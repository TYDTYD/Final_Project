using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;
public class StoryUI : MonoBehaviour
{
    [SerializeField] MaskVariation GetMask;
    [SerializeField] List<Sprite> images;
    [SerializeField] Image BackGround;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI person;

    Action ChangeSprite;
    List<string> Texts = new List<string>();
    List<string> Person_Texts = new List<string>();
    private void OnEnable() => UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    private void OnDisable() => UpdateManager.Instance.UnSubscribeUpdate(UpdateMethod);
    int pos = 0;
    int textPos = 0;

    private void Start()
    {
        ChangeSprite += Change;
        StartCoroutine(GetMask.Brighter());

        Texts.Add("She lived an enviable life.");
        Texts.Add("She graduated from a good university and seemed to have a flat life after graduation.");
        Texts.Add("But the reality did not go her way.");
        Texts.Add("She was a job applicant for three years after graduating from college.");
        Texts.Add("As the period of employment became longer, her stress intensified and she had frequent conflicts with her family.");
        Texts.Add("How long are you going to keep preparing for a job? Get a part-time job!");
        Texts.Add("If I work part-time, I lose time! I have to concentrate on preparing for employment to get a job! When can I get a job if I work part-time!");
        Texts.Add("You're not getting a job because you're preparing with such a weak spirit! In my time, I did everything I could! You're not in a position to get a job!");
        Texts.Add("I'm so annoyed. I'm preparing hard. Why do you keep saying things? I'm annoyed right now. I'm annoyed by this reality! I'm frustrated, too! Do you think I'm doing this because I want to do this? I want to get a job soon, too! But what should I do when I can't? The society is not accepting new employees. What should I do?");
        Texts.Add("Where are you talking back? Hey! Are you done? What are you doing!");
        Texts.Add( "Oh, stop it. My kid must have thoughts. Just leave her alone. She's working hard.");
        Texts.Add("No! She didn't show anything! She should do something to bring the result. What's wrong with her.");
        Texts.Add("No one understands me! (jumping into the bed of the room)");
        Texts.Add("So she fell asleep sobbing...");

        Person_Texts.Add("");
        Person_Texts.Add("");
        Person_Texts.Add("");
        Person_Texts.Add("");
        Person_Texts.Add("");
        Person_Texts.Add("Dad");
        Person_Texts.Add("Me");
        Person_Texts.Add("Dad");
        Person_Texts.Add("Me");
        Person_Texts.Add("Dad");
        Person_Texts.Add("Mom");
        Person_Texts.Add("Dad");
        Person_Texts.Add("Me");
        Person_Texts.Add("");
        BackGround.sprite = images[pos];
        text.text = Texts[textPos];
        person.text = Person_Texts[textPos];
    }
    void UpdateMethod()
    {
        bool EnterPressed = Input.GetKeyDown(KeyCode.Return);

        if (EnterPressed)
        {
            if (textPos == Texts.Count - 1)
                StartCoroutine(GameManager.Instance.PreloadScene(1, GetMask.Darker()));
            if (pos < images.Count - 1)
            {
                if (textPos < 4 || textPos > 10)
                    StartCoroutine(GetMask.Darker(ChangeSprite, GetMask.Brighter()));
            }
            if (textPos < Texts.Count - 1)
            {
                text.text = Texts[++textPos];
                person.text = Person_Texts[textPos];
            }
        }
    }

    void Change() => BackGround.sprite = images[++pos];
}