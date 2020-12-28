using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class EntryNumberOneScript : MonoBehaviour {

    public KMBombModule Module;
    public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMSelectable[] Keys;
    public TextMesh Text;
    
    private int Num2;
    private int Num3;
    private int Num4;
    private int Subtract;
    private int Expected;
    private int Input;
    static int _moduleIdCounter = 1;
    int _moduleID = 0;
    private bool Halt;

    private KMSelectable.OnInteractHandler ButtonPressed(int pos)
    {
        return delegate
        {
            Keys[pos].AddInteractionPunch();
            KeyPress(pos);
            StartCoroutine(AnimKeys(pos));
            return false;
        };
    }

    void Awake()
    {
        _moduleID = _moduleIdCounter++;
        Expected = Rnd.Range(10000000,100000000);
        Subtract = Rnd.Range(10000000,100000000);
        Num2 = (Expected - Subtract + 100000000) % 100000000;
        Num3 = (Num2 - Subtract + 100000000) % 100000000;
        Num4 = (Num3 - Subtract + 100000000) % 100000000;
        Debug.LogFormat("[Entry Number One #{0}] Number 2 is {1}, number 3 is {2} and number 4 is {3}, making the added coefficient {4} and the expected value {5}.", _moduleID, Num2.ToString("00000000"), (Num3 % 100000000).ToString("00000000"), (Num4 % 100000000).ToString("00000000"), (100000000 - Subtract).ToString("00000000"), Expected.ToString("00000000"));
        Module.OnActivate += delegate
        {
            Text.text = (Expected.ToString("00000000") + "\n" + Num2.ToString("00000000") + "\n" + Num3.ToString("00000000") + "\n00000000");
        };

        for (int i = 0; i < Keys.Length; i++)
        {
            Keys[i].OnInteract += ButtonPressed(i);
        };
    }

    // idk what to put here iludzhfbliushfndkjl bdfhkjsadeiurpoh
    void Start () {
        StartCoroutine(TextBug());
    }

    private IEnumerator TextBug()
    {
        while (true)
        {
            if (!Halt)
            {
                int[] TextBugRnd = {Rnd.Range(0, 32), Rnd.Range(0, 32), Rnd.Range(0, 32)};
                string Chars = "!£$%^&*()€`¬¦[]{};:@'#~/?<>,.÷_-¢¤¥§©«®™¯°µ¶»¼½¾¿×ØÞß¡²³¹ÐÆÇÖÑþ";
                string ModText = (Input.ToString("00000000") + Num2.ToString("00000000") + Num3.ToString("00000000") + Num4.ToString("00000000"));
                for (int i = 0; i < 3; i++)
                {
                    ModText = (ModText.Substring(0, TextBugRnd[i]) + Chars[Rnd.Range(0, Chars.Length)].ToString() + ModText.Substring(TextBugRnd[i] + 1, 31 - TextBugRnd[i]));
                }
                Text.text = ModText.Substring(0, 8) + "\n" + ModText.Substring(8, 8) + "\n" + ModText.Substring(16, 8) + "\n" + ModText.Substring(24,8);
            }
            yield return new WaitForSeconds(0.06f);
        }
    }

    void KeyPress(int pos) {

        if (!Halt)
        {
            if (pos == 10)
            {
                StartCoroutine(Go());
            }
            else
            {
                Audio.PlaySoundAtTransform("press", transform);
                Input = (Input * 10 + pos) % 100000000;
                Text.text = (Input.ToString("00000000") + "\n" + Num2.ToString("00000000") + "\n" + Num3.ToString("00000000") + "\n" + Num4.ToString("00000000"));
            }
        }
        else
        {
            Audio.PlaySoundAtTransform("press", transform);
        }
    }

    private IEnumerator AnimKeys(int pos)
    {
        {
            for (int i = 0; i < 3; i++)
            {
                Keys[pos].transform.localPosition += new Vector3(0f, 0f, -0.003f);
                yield return new WaitForSeconds(0.02f);
            }
            for (int i = 0; i < 3; i++)
            {
                Keys[pos].transform.localPosition += new Vector3(0f, 0f, 0.003f);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    private IEnumerator Go()
    {
        Debug.LogFormat("[Entry Number One #{0}] You inputted " + Input.ToString("00000000") + ".", _moduleID);
        if (Input == Expected)
        {
            Debug.LogFormat("[Entry Number One #{0}] That was indeed the correct answer. Poggers!", _moduleID);

            string[] SolveText = { "Request\naccepted.\nModule\ndisarmed.", "Entry\nwas\nsuccessful!", "Request\napproved,\nmodule\ndisarmed.", "Answer\nmatches\ncurrent\ndatabase!", "Modular\nsecurity\nsystems\noffline.", "Accepted\nsequence\naddendum.", "Detected\ncorrect\nsubmission.", "Modular\nsector\noffline.", "Submission\nmatches\n" + Rnd.Range(1, 1000000) + "\nresults.", "Please\ntry our\nother\nproducts!" };
            Module.HandlePass();
            Text.text = (SolveText[Rnd.Range(0, 10)]);
            Audio.PlaySoundAtTransform("solve", transform);
            Halt = true;
            yield return null;
        }
        else if (Input == 02496666 || Input == 96024666 || Input == 96666024 || Input == 66696024 || Input == 02466696 || Input == 66602496)
        {
            Halt = true;
            Audio.PlaySoundAtTransform("tyler1", transform);
            for (int i = 0; i < 51; i++)
            {
                Text.text = ("Poggers!\nPoggers!\nPoggers!\nPoggers!");
                yield return new WaitForSeconds(0.1f);
                Text.text = ("");
                yield return new WaitForSeconds(0.1f);
            }
            Text.text = ("00000000" + "\n" + Num2.ToString("00000000") + "\n" + Num3.ToString("00000000") + "\n" + Num4.ToString("00000000"));
            Halt = false;
            Input = 0;
        }
        else
        {
            Module.HandleStrike();
            Debug.LogFormat("[Entry Number One #{0}] That was incorrect. Strike!", _moduleID);
            Halt = true;
            Audio.PlaySoundAtTransform("strike", transform);
            string[] StrikeText = { "Negative.", "False.", "Incorrect.", "Invalid.", "Failure.", "Fatal error.\nPlease\ntry again.", "System\nfailure.", "System\noverride\nfailed.", "Fatal\nexception\ndetected.", "\"" + Input.ToString("00000000") + "\"\ndoes not\nmatch any\nresults.", "Int \"type\"\ndoes not\nequal\n\"solve\".", "Your\ncomputer\nis too hot." };
            Text.text = (StrikeText[Rnd.Range(0, 12)]);
            yield return new WaitForSeconds(1f);
            Text.text = ("00000000" + "\n" + Num2.ToString("00000000") + "\n" + Num3.ToString("00000000") + "\n" + Num4.ToString("00000000"));
            Halt = false;
            Input = 0;
        }
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "Use '!{0} 1234567890' to type in 1234567890 and use '!{0} submit' to press the 'GO' button.";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.ToLowerInvariant();
        string validcmds = "0123456789";
        if (command == "submit")
        {
            Keys[10].OnInteract();
            yield return null;
        }
        else
        {
            for (int i = 0; i < command.Length; i++)
            {
                if (!validcmds.Contains(command[i]))
                {
                    yield return "sendtochaterror Invalid command.";
                    yield break;
                }
            }
            for (int i = 0; i < command.Length; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (command[i] == validcmds[j])
                    {
                        Keys[j].OnInteract();
                        yield return new WaitForSeconds(0.2f);
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator TwitchHandleForcedSolve()
    {
        yield return true;
        string validcmds = "0123456789";

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (Expected.ToString("00000000")[i] == validcmds[j])
                {
                    Keys[j].OnInteract();
                    yield return true;
                }
            }
        }
        Keys[10].OnInteract();
    }

    // ...You haven't died, have you?
}
