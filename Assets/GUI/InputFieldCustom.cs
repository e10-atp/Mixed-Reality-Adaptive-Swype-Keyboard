using System;
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
    [SerializeField]
    private Keyboard _keyboard;

    [SerializeField] private TextAsset _dictionary;
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
        string text = _inputfield.text;

        if (keyPress == "\b")
        {
            // Backspace
            /*if (text.LastIndexOf(' ') > 0) 
            {
                tex = text.Remove(text.LastIndexOf(' ')).TrimEnd();
            }*/
            text = "";
        }
        if (keyPress == " ")
        {
            // space
            if (text.Length > 0)
                getSuggestion();
            text = "";
        }
        if (keyPress == "/s")
        {
            WriteString(_outputtext.text);
            text = "";
            _outputtext.text = "";
        }
        else
        {
            // Regular key press
            text += keyPress;
        }

        //_text.text = text;
        _inputfield.text = text;
    }

    private void getSuggestion()
    {
        var s = simpleSwipeType.GetSuggestion(_inputfield.text, 1);
        // Debug.Log(s);
        // Debug.Log(s.ElementAt(0));
        _outputtext.text += s.ElementAt(0) + " ";
    }

    private void WriteString(string s)
    {
        string path = Application.persistentDataPath + "/result.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
    }
    // Start is called before the first frame update
    

    void Start()
    {
        
        Debug.Log("Hello world!");
        var wordList = _dictionary.text.Split();
        simpleSwipeType = new MatchSwipeType(wordList);
        _inputfield.text = "";
        // _outputtext.text = _inputfield.text;
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
