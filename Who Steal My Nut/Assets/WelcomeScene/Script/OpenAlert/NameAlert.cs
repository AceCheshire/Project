using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NameAlert : MonoBehaviour
{
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationspeed;
    public GameObject alert;
    public bool alertStatus = false;
    public bool inputStatus = false;
    public bool shiftStatus = false;
    public string playerName;
    private float time = 0;

    IEnumerator ShowAlert(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = 2 * Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationspeed;
            yield return null;
        }
    }

    IEnumerator HideAlert(GameObject gameObject)
    {
        float timer = 0;
        while (timer <= 1)
        {
            gameObject.transform.localScale = 2 * Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationspeed;
            yield return null;
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("playername"))
        {
            GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = 31;
            StartCoroutine(ShowAlert(alert));
            alertStatus = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StartCoroutine(HideAlert(alert));
            alertStatus = false;
            inputStatus = false;
            GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = false;
            GameObject.Find("wordRenderer").GetComponent<TilemapRenderer>().sortingOrder = -1;
            GameObject.Find("wordRenderer").GetComponent<Transform>().position = new Vector3(0, 0, 0);
            PlayerPrefs.SetString("playername", playerName);
            PlayerPrefs.Save();
        }
        if (alertStatus)
        {
            if (Input.GetKeyUp(KeyCode.Tab) && (!inputStatus))
            {
                inputStatus = true;
            }
            else if (Input.GetKeyUp(KeyCode.Tab) && inputStatus)
            {
                inputStatus = false;
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                shiftStatus = true;
            }
            else
            {
                shiftStatus = false;
            }
            if (inputStatus)
            {
                if (shiftStatus && Input.GetKeyUp(KeyCode.A)) playerName += "A";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.A)) playerName += "a";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.B)) playerName += "B";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.B)) playerName += "b";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.C)) playerName += "C";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.C)) playerName += "c";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.D)) playerName += "D";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.D)) playerName += "d";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.E)) playerName += "e";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.E)) playerName += "e";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.F)) playerName += "F";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.F)) playerName += "f";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.G)) playerName += "G";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.G)) playerName += "g";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.H)) playerName += "H";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.H)) playerName += "h";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.I)) playerName += "I";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.I)) playerName += "i";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.J)) playerName += "J";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.J)) playerName += "j";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.K)) playerName += "K";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.K)) playerName += "k";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.L)) playerName += "L";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.L)) playerName += "l";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.M)) playerName += "M";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.M)) playerName += "m";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.N)) playerName += "N";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.N)) playerName += "n";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.O)) playerName += "O";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.O)) playerName += "o";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.P)) playerName += "P";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.P)) playerName += "p";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Q)) playerName += "Q";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Q)) playerName += "q";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.R)) playerName += "R";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.R)) playerName += "r";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.S)) playerName += "S";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.S)) playerName += "s";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.T)) playerName += "T";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.T)) playerName += "t";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.U)) playerName += "U";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.U)) playerName += "u";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.V)) playerName += "V";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.V)) playerName += "v";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.W)) playerName += "W";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.W)) playerName += "w";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.X)) playerName += "X";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.X)) playerName += "x";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Y)) playerName += "Y";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Y)) playerName += "y";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Z)) playerName += "Z";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Z)) playerName += "z";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Quote)) playerName += "\"";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Quote)) playerName += "'";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Equals)) playerName += "+";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Equals)) playerName += "=";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Semicolon)) playerName += ":";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Semicolon)) playerName += ";";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.BackQuote)) playerName += "~";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.BackQuote)) playerName += "`";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha0)) playerName += ")";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha0)) || Input.GetKeyUp(KeyCode.Keypad1)) playerName += "0";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha1)) playerName += "!";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha1)) || Input.GetKeyUp(KeyCode.Keypad1)) playerName += "1";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha2)) playerName += "@";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha2)) || Input.GetKeyUp(KeyCode.Keypad2)) playerName += "2";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha3)) playerName += "#";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha3)) || Input.GetKeyUp(KeyCode.Keypad3)) playerName += "3";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha4)) playerName += "$";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha4)) || Input.GetKeyUp(KeyCode.Keypad4)) playerName += "4";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha5)) playerName += "%";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha5)) || Input.GetKeyUp(KeyCode.Keypad5)) playerName += "5";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha6)) playerName += "^";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha6)) || Input.GetKeyUp(KeyCode.Keypad6)) playerName += "6";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha7)) playerName += "&";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha7)) || Input.GetKeyUp(KeyCode.Keypad7)) playerName += "7";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha8)) playerName += "*";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha8)) || Input.GetKeyUp(KeyCode.Keypad8)) playerName += "8";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Alpha9)) playerName += "(";
                else if ((!shiftStatus && Input.GetKeyUp(KeyCode.Alpha9)) || Input.GetKeyUp(KeyCode.Keypad9)) playerName += "9";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Backslash)) playerName += "|";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Backslash)) playerName += "\\";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Slash)) playerName += "?";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Slash)) playerName += "/";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Period)) playerName += ">";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Period)) playerName += ".";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Comma)) playerName += "<";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Comma)) playerName += ",";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.Minus)) playerName += "_";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.Minus)) playerName += "-";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.LeftBracket)) playerName += "{";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.LeftBracket)) playerName += "[";
                else if (shiftStatus && Input.GetKeyUp(KeyCode.RightBracket)) playerName += "}";
                else if (!shiftStatus && Input.GetKeyUp(KeyCode.RightBracket)) playerName += "]";
                else if (Input.GetKeyUp(KeyCode.Space)) playerName += " ";
                else if (Input.GetKeyUp(KeyCode.Backspace) && playerName.Length != 0)
                    playerName = playerName.Substring(0, playerName.Length - 1);
                //Input Word
                time -= Time.deltaTime;
                if (time < 0)
                {
                    time = 0.2f;
                    GameObject.Find("wordBuffer").GetComponent<WordTranslate>().inputStr = playerName;
                    GameObject.Find("wordBuffer").GetComponent<WordTranslate>().isWaitingRend = true;
                    GameObject.Find("wordRenderer").GetComponent<Transform>().position = new Vector3(1, -2, 1);
                }
            }
        }
    }
}

