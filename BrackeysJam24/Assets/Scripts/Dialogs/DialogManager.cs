using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI enemyTextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI enemyTitleTextMeshProUGUI;
    private Animator _animator;
    private bool _onDialog = true;
    private static readonly int Continue = Animator.StringToHash("Continue");
    // [SerializeField] List<string> dialog;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && _onDialog)
        {
            _animator.SetTrigger(Continue);       
        }

    }

    public void Prologue1()
    {
        enemyTitleTextMeshProUGUI.text = "Asmodey";
        enemyTextMeshProUGUI.text = "WELCOME GHOULS AND ALL THE OTHER NASTY CREATURES! It’s time for another episode of “WHAT’S BEHIND THE DOOR?”, with me, your favorite undead, Asmodey! And our new contestant - Tyler!";
    }
    
    public void Prologue2()
    {
        playerTextMeshProUGUI.text = "Wait, what? Which door?...Who are you?";
    }
    
    public void Prologue3()
    {
        enemyTextMeshProUGUI.text = "Don’t play a fool, Tyler! Everyone knows who I am - and *what* I am. So, if you are done with your nonsense, it’s time to play!";
    }
    
    public void Prologue4()
    { 
        enemyTextMeshProUGUI.text = "As you know, every week we kidnap a different lunch, and give them the chance to fight for their life! - and, for our entertainment! \nTyler, you must choose 1 between those 3 doors - the possibilities are: a terrible death and we will eat you like, immediately; you instant freedom and, my personal favorite, a little adventure where you can have another chance - even if it’s a small chance - to escape\n";
    }
    
    public void Prologue5()
    {
        playerTextMeshProUGUI.text = "Weirdest dream ever";
    }
    
    public void Prologue6()
    {
        enemyTextMeshProUGUI.text = "Haha yeah, yeah. Deny as much as you want, kid! So, which door will it be? Will it be door number 1, door number 2 or door number 3? Go ahead, make your… choice ";
    }
    
    public void Prologue7()
    {
        _onDialog = true;
        enemyTextMeshProUGUI.text = "Oh, it looks like you won’t die now! Are you sure about your choice, or do you want to change it?";
    }
    
    public void Prologue8()
    {
        enemyTextMeshProUGUI.text = "Well, well, well, Tyler! You'd be a lucky one, if you had any choice in this first challenge! It’s a TV show, kid! ";
    }
    
    public void Prologue9()
    {
        enemyTextMeshProUGUI.text = "But, don’t worry! You still have a chance to escape! You just need to survive the next challenge! ";
    }

    public void PostPrologue()
    {
        enemyTextMeshProUGUI.text =
            "Alright, Tyler, it’s your chance to conquer your freedom! It’s all about luck and choices from now - and I pinky promise, no more interventions - we all like a reality show!";
    }

    public void SpawnPlayer()
    {
        _onDialog = false;
    }

    public void StartGame()
    {
        _onDialog = false;
    }
    
}
