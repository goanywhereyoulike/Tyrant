using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField]private float textSpeed = 50f;

    private readonly List<Punctuation> puncuations = new List<Punctuation>() //list of punctuations
    {
        new Punctuation(new HashSet<char>(){'.','!','?'},0.6f),
        new Punctuation(new HashSet<char>(){',',';',':'},0.3f)
        // you can add more like this format
    };

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType,textLabel));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        
        float t = 0;
        int characterIndex = 0;

        while (characterIndex<textToType.Length)
        {
            int lastCharacterIndex = characterIndex;
            t += Time.deltaTime * textSpeed;
            characterIndex = Mathf.FloorToInt(t);
            characterIndex = Mathf.Clamp(characterIndex, 0, textToType.Length);

            for (int i = lastCharacterIndex; i < characterIndex; i++)
            {
                bool isLastString = i >= textToType.Length - 1; //check last string for punctuations

                textLabel.text = textToType.Substring(0, i + 1);

                if (isPunctuation(textToType[i], out float waitTime)&& !isLastString&& !isPunctuation(textToType[i+1],out _))//check if the last character is a punctuation or not
                {
                    yield return new WaitForSeconds(waitTime); //if is punctuation, define the wait time above(line 12,13)
                }
            }
            yield return null;
        }
        textLabel.text = textToType;
    }

    private bool isPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in puncuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char>punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
