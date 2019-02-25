using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Textboxbase : MonoBehaviour {
	string tbcontent;
	public string[] tbcsplit;
	public GameObject textbox;
	GameObject tb;
	public Text tbtext;
	public bool istyping;
	public bool canceltyping = false;
	bool key;
	bool keyt;
	public bool finishedtyping = false;
    bool skip;
	int maxcharacterlimit = 53;
	public int page = 0;

	// Use this for initialization
	public virtual void Start () {
		tbcontent = "This is a text box that has a lot of text to give me time to test different things while the text is typing";
        tbtext = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		tbcontent = SplitToLines (tbcontent, maxcharacterlimit);
		tbcsplit = tbcontent.Split (new [] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		print (tbcsplit.Length);
		StartCoroutine (typewriter (tbcsplit [page]));
		//starttext(tbcsplit[page]);
    }
	
	// Update is called once per frame
	void Update () 
	{

	}
	void starttext()
	{

	}
    public virtual void skiptext()
    {
		if (istyping)
			canceltyping = true;
		else if (!istyping && page < tbcsplit.Length - 1) 
		{
			page++;
			StartCoroutine (typewriter (tbcsplit [page]));
		}
        else
		{
			print ("hello");
            tbtext.text = "";
            gameObject.SetActive(false);
        }

    }
	public string SplitToLines(string stringToSplit, int maximumLineLength)
	{
		return Regex.Replace(stringToSplit, @"(.{1," + maximumLineLength +@"})(?:\s|$)", "$1\n");
	}
	public IEnumerator typewriter(string pagetext)
	{
		int letter = 0;
		tbtext.text = "";
		istyping = true;
		canceltyping = false;
		while (istyping && !canceltyping && (letter < pagetext.Length - 1)) {
			tbtext.text += pagetext [letter];
			letter += 1;
			yield return new WaitForSeconds (0.02f);
		}
		tbtext.text = pagetext;
		finishedtyping = true;
		istyping = false;
		canceltyping = false;
		yield break;
	}
}
