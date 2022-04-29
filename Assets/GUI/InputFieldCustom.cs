using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.UI;

public class InputFieldCustom : MonoBehaviour
{
    //[SerializeField]
    //private Text _text;

    public InputField _inputfield;

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
    void Start()
    {
        _inputfield = GetComponentInChildren<InputField>();
        _inputfield.text = "lmao";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
