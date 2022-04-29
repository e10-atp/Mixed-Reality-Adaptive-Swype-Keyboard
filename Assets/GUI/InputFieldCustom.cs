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
    public Keyboard keyboard { get { return _keyboard; } set { SetKeyboard(value); } }


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
            if (text.Length > 0)
                text = text.Remove(text.Length - 1);
        }
        else
        {
            // Regular key press
            text += keyPress;
        }

        //_text.text = text;
        _inputfield.text = text;
    }
    // Start is called before the first frame update

    //private SwipeType.SwipeType simpleSwipeType;
    void Start()
    {
        SwipeType.SwipeType simpleSwipeType = new MatchSwipeType(File.ReadAllLines("EnglishDictionary.txt"));
        //_inputfield = GetComponentInChildren<InputField>();
        _inputfield.text = "yuioiu";
        //_outputtext = GetComponentInChildren<Outputtext>();
        _outputtext.text = _inputfield.text;
        // var s = simpleSwipeType.GetSuggestion(_inputfield.text, 1);
        // _outputtext.text = s.ElementAt(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
