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
    public InputField _targettext;
    [SerializeField] private Keyboard _keyboard;

    [SerializeField] private TextAsset _dictionary;
    [SerializeField] private TextAsset _targetPhrases;

    //[SerializeField] private GameObject keyboard;

    [SerializeField] private Material keyMaterial;
    [SerializeField] private Material textMaterial;
    private Vector3 originalsize;

    public Keyboard keyboard
    {
        get { return _keyboard; }
        set { SetKeyboard(value); }
    }

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
            //getSuggestion();
        }

        if (keyPress == "/s")
        {
            WriteString(_outputtext.text);
            WriteString(Timerr.time.ToString("0.000"));
            Timerr.resetbutton();
            Timerr.startbutton();
            _outputtext.text = "";
            if (i < 27)
            {
                i += 1;
            }
            else
            {
                if (j <= 0)
                {
                    i = 0;
                    j++;
                }
            }

            int index = j * 27 + i;
            Debug.Log(index);
            if (index % 9 == 0)
            {
                ChangeKeyboardParam(index);
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

    public void onActionRelease()
    {
        getSuggestion();
        _inputfield.text += ' ';
    }

    private void getSuggestion()
    {
        Debug.Log(_inputfield.text);
        string word = _inputfield.text.Split(' ').Last();
        var s = simpleSwipeType.GetSuggestion(word, 1); //change to 3?
        Debug.Log(s);
        if (s.Any())
        {
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

    private void ChangeKeyboardParam(int iter)
    {
        if (iter == 9)
        {
            //keyboard.transform.position += new Vector3(0.5f, 0.5f, 0.5f)*.16f;
            // keyboard.transform.localScale = originalsize * 1.3f;
            keyboard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        else if (iter == 18)
        {
            keyboard.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        if (iter == 27)
        {
            keyboard.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            Color newColor = keyMaterial.color;
            newColor.a = 0.5f;
            keyMaterial.color = newColor;
            Color textColor = textMaterial.color;
            textColor.a = 0.5f;
            textMaterial.color = textColor;
        }
        else if (iter == 36)
        {
            Color newColor = keyMaterial.color;
            newColor.a = 0.1f;
            keyMaterial.color = newColor;
            Color textColor = textMaterial.color;
            textColor.a = 0.1f;
            textMaterial.color = textColor;
        }
        else if (iter == 45)
        {
            Color newColor = keyMaterial.color;
            newColor.a = 0.0f;
            keyMaterial.color = newColor;
            Color textColor = textMaterial.color;
            textColor.a = 0.0f;
            textMaterial.color = textColor;
        }
        // else if (iter == 54)
        // {
        //     //return keyboard to default
        //     Color newColor = keyMaterial.color;
        //     newColor.a = 1.0f;
        //     keyMaterial.color = newColor;
            // Color textColor = textMaterial.color;
            // textColor.a = 1.0f;
            // textMaterial.color = textColor;
        // }
    }
    // Start is called before the first frame update

    private string[] phraseList;
    private int j;
    private int i;

    void Start()
    {
        AudioListener.volume = 0f;
        Debug.Log("Hello world!");
        var wordList = _dictionary.text.Split();
        simpleSwipeType = new MatchSwipeType(wordList);
        phraseList = _targetPhrases.text.Split('\n');
        _targettext.text = phraseList[i];
        _inputfield.text = "";
        // _outputtext.text = _inputfield.text;
        Color newColor = keyMaterial.color;
        newColor.a = 1.0f;
        keyMaterial.color = newColor;

    }

    // Update is called once per frame
    void Update()
    {
    }
}