﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Normal.UI;
using SwipeType;

public class InputFieldCustom : MonoBehaviour
{
    //[SerializeField]
    //private Text _text;

    public InputField _inputfield;

    public InputField _outputtext;
    public InputField _targettext;
    [SerializeField]
    private Keyboard _keyboard;

    [SerializeField] private TextAsset _dictionary;
    [SerializeField] private TextAsset _targetPhrases;
    public Keyboard keyboard { get { return _keyboard; } set { SetKeyboard(value); } }

    private SwipeType.SwipeType simpleSwipeType;

    void Awake()
    {
        StartObservingKeyboard(_keyboard);
    }

    void OnDestroy()
    {
        StopObservingKeyboard(_keyboard);
    }

    void StartObservingKeyboard(Keyboard keyboard)
    {
        if (keyboard == null)
            return;

        keyboard.keyPressed += KeyPressed;
    }

    void StopObservingKeyboard(Keyboard keyboard)
    {
        if (keyboard == null)
            return;

        keyboard.keyPressed -= KeyPressed;
    }

    void SetKeyboard(Keyboard keyboard)
    {
        if (keyboard == _keyboard)
            return;

        StopObservingKeyboard(_keyboard);
        StartObservingKeyboard(keyboard);

        _keyboard = keyboard;
    }
    
    void KeyPressed(Keyboard keyboard, string keyPress)
    {

        //string text = _text.text;

        if (keyPress == "\b")
        {
            // Backspace
            /*if (text.LastIndexOf(' ') > 0) 
            {
                text = text.Remove(text.LastIndexOf(' ')).TrimEnd();
            }*/
            //_outputtext.text = _outputtext.text.Remove(_outputtext.text.LastIndexOf(' ')).TrimEnd();
            //_inputfield.text += " ";
        }
        if (keyPress == " ")
        {
            // space
            getSuggestion();
        }
        if (keyPress == "/s")
        {
            WriteString(_outputtext.text);
            WriteString(Timerr.time.ToString("0.000"));
            Timerr.resetbutton();
            Timerr.startbutton();
            _outputtext.text = "";
            if (i < 24)
            {
                i += 1;    
            }
            _targettext.text = phraseList[i];
        }
        else
        {
            // Regular key press
            _inputfield.text += keyPress;
        }

        //_text.text = text;
    }

    private void getSuggestion()
    {
        Debug.Log(_inputfield.text);
        string word = _inputfield.text.Split(' ').Last();
        var s = simpleSwipeType.GetSuggestion(word, 1);
        Debug.Log(s);
        if(s.Any()){
            Debug.Log(s.First());
            _outputtext.text += s.ElementAt(0) + " ";
        }
    }

    private void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/result.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
    // Start is called before the first frame update

    private string[] phraseList;
    private int i;
    void Start()
    {
        
        Debug.Log("Hello world!");
        var wordList = _dictionary.text.Split();
        simpleSwipeType = new MatchSwipeType(wordList);
        phraseList = _targetPhrases.text.Split('\n');
        i = 0;
        _targettext.text = phraseList[i];
        _inputfield.text = "";
        // _outputtext.text = _inputfield.text;
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
