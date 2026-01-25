using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header ("UI")]
    [SerializeField] private Text _dialogText;
    [SerializeField] private Image _faceImage;

    [Header("dialogue content")]
    [SerializeField] private TextAsset _textFile;
    [SerializeField] private int _textIndex;
    [SerializeField] private float _textSpeed;
    [SerializeField] private List<string> _textList = new();
    [SerializeField] private bool _isTextFinished;
    [SerializeField] private bool _isInstantText;

    [Header("face sprite")]
    [SerializeField] private Sprite _face01;
    [SerializeField] private Sprite _face02;

    // Start is called before the first frame update
    void Awake()
    {
        // get dialog content from file
        GetTextFromFile(_textFile);     
    }

    private void OnEnable()
    {
        StartCoroutine(SetTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        // the last content has been typed, then close the text window 
        if (Input.GetKeyDown(KeyCode.R) && _textIndex == _textList.Count)
        {
            gameObject.SetActive(false);
            _textIndex = 0;
            return;
        }

        // type by time or type all in a second
        if (Input.GetKeyDown(KeyCode.R))
        {
            // default typing
            if (_isTextFinished && !_isInstantText)
            {
                StartCoroutine(SetTextUI());
            }
            // instant text
            else if (!_isTextFinished && !_isInstantText)
            {
                _isInstantText = true;
            }
        }
    }

    /// <summary>
    /// get text context from file
    /// </summary>
    /// <param name="file"></param>
    void GetTextFromFile(TextAsset  file)
    {
        // initialize
        _textList.Clear();
        _textIndex = 0;

        // Split the data by newline character to get each line of content.
        var lineData =  file.text.Split('\n');
        foreach (var line in lineData)
        {
            _textList.Add(line);       
        }
    }

    IEnumerator SetTextUI()
    {
        _isTextFinished = false;
        _dialogText.text = "";

        // get every dialog content in textlist
        switch (_textList[_textIndex].Trim())
        {
            case "A":
                _faceImage.sprite = _face01;
                _textIndex++;
                break;

            case "B":
                _faceImage.sprite = _face02;
                _textIndex++;
                break;
        }

        // text appears one character at a time.
        int letter = 0;
        while (!_isInstantText && letter < _textList[_textIndex].Length - 1)
        {
            _dialogText.text += _textList[_textIndex][letter];
            letter++;
            yield return new WaitForSeconds(_textSpeed);
        }

        // instant text
        _dialogText.text = _textList[_textIndex];
        _isInstantText = false;
        _isTextFinished = true;
        _textIndex++;
    }
}
