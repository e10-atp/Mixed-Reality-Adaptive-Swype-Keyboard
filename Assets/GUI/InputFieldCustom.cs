using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Normal.UI;
using SwipeType;
using TMPro;

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
            if (i < 15)
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

            int index = j * 15 + i;
            Debug.Log(index);
            ChangeKeyboardParam(index);

            _targettext.text = phraseList[i];
        }
        else
        {
            _inputfield.text += keyPress;
        }
    }

    public AudioSource release;
    public void onActionRelease()
    {
        getSuggestion();
        release.Play();
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

    public Material textMat1;
    public Material textMat2;
    public Material textMat3;
    public GameObject Outline1;
    public GameObject Outline2;
    public GameObject Outline3;
    public GameObject Outline4;

    private void ChangeKeyboardParam(int iter)
    {
        var letters = keyboard.transform.Find("Keys/Letters");
        foreach (Transform key in letters)
        {
            key.position = key.position;
        } //shouldn't really fix anything, but the problem might be in the individual keys or their geometries

        if (iter is >= 5 and < 11)
        {
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else if (iter is >= 11 and < 16)
        {
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        if (iter is >= 16 and < 21)
        {
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            Color newColor = keyMaterial.color;
            newColor.a = 0.5f;
            keyMaterial.color = newColor;
            foreach (Transform key in letters)
            {
                var tmp = key.Find("Key/Text").gameObject.GetComponent<TextMeshPro>();
                tmp.fontMaterial = textMat1;
            }
        }
        else if (iter is >= 21 and < 26)
        {
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            Color newColor = keyMaterial.color;
            newColor.a = 0.1f;
            keyMaterial.color = newColor;
            foreach (Transform key in letters)
            {
                var tmp = key.Find("Key/Text").gameObject.GetComponent<TextMeshPro>();
                tmp.fontMaterial = textMat2;
            }
        }
        else if (iter is >= 26 and < 30)
        {
            Outline1.SetActive(true);
            Outline2.SetActive(true);
            Outline3.SetActive(true);
            Outline4.SetActive(true);
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            Color newColor = keyMaterial.color;
            newColor.a = 0.0f;
            keyMaterial.color = newColor;
            foreach (Transform key in letters)
            {
                var tmp = key.Find("Key/Text").gameObject.GetComponent<TextMeshPro>();
                tmp.fontMaterial = textMat3;
            }
        }
        else if (iter == 30)
        {
            //return keyboard to default
            keyboard.transform.position = originalPos;
            keyboard.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            Color newColor = keyMaterial.color;
            newColor.a = 1.0f;
            keyMaterial.color = newColor;
            Color textColor = textMaterial.color;
            textColor.a = 1.0f;
            textMaterial.color = textColor;
            foreach (Transform key in letters)
            {
                var tmp = key.Find("Key/Text").gameObject.GetComponent<TextMeshPro>();
                tmp.fontMaterial = textMaterial;
            }
            WriteString(_inputfield.text);
        }
    }
    // Start is called before the first frame update

    private string[] phraseList;
    private int j = 0;
    private int i = 0;
    private Vector3 originalPos;
    void Start()
    {
        originalPos = keyboard.transform.position;
        //AudioListener.volume = 0f;
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