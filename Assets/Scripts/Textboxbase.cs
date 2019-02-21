using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textboxbase : MonoBehaviour {
	string tbcontent;
	public GameObject textbox;
	bool trigger = false;
	GameObject tb;
	Text tbtext;
	bool istyping;
	bool canceltyping = false;
	bool key;
	bool keyt;
	public bool finishedtyping = false;
    bool skip;

	// Use this for initialization
	void Start () {
		tbcontent = "This is a text box that has a lot of text ot give me time to test different things while the text is typing";
        tbtext = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        starttext();
    }

	
	// Update is called once per frame
	void Update () 
	{

	}
	void starttext()
	{
        StartCoroutine(typewriter());


		//if (settingsmanager.instance.DownPressed()) 
		//{
		//	if (!key && trigger) 
		//	{
		//		print ("true");
		//		key = true;
		//		keyt = true;
		//	}
		//}
		//if(!settingsmanager.instance.DownPressed())
		//{
		//	key = false;
		//}
		//if (keyt) {
		//	if (!textbox.activeInHierarchy)
		//	{
		//		textbox.SetActive (true);
		//	} else if (textbox.activeInHierarchy) 
		//	{
		//		if (!istyping && !finishedtyping) {
		//			StartCoroutine (typewriter ());
		//			istyping = true;
		//			keyt = false;
		//		} else if (istyping && !canceltyping) 
		//		{
		//			canceltyping = true;
		//			print ("stop");
		//			keyt = false;
		//		}
		//		if (finishedtyping) 
		//		{
		//			textbox.SetActive (false);
		//			StopCoroutine (typewriter ());
		//			tbtext.text = "";
		//			keyt = false;
		//			finishedtyping = false;
		//		}
		//	}
		//}
		//if (!trigger) 
		//{
		//	textbox.SetActive (false);
		//	StopCoroutine (typewriter ());
		//	tbtext.text = "";
		//}
	}
    public void skiptext()
    {
        if (istyping)
            canceltyping = true;
        else
        {
            tbtext.text = "";
            gameObject.SetActive(false);
        }

    }
	public IEnumerator typewriter()
	{
		int letter = 0;
		tbtext.text = "";
		istyping = true;
		canceltyping = false;
		while (istyping && !canceltyping && (letter < tbcontent.Length - 1)) 
		{
			tbtext.text += tbcontent [letter];
			letter += 1;
			yield return new WaitForSeconds (0.02f);
		}
		tbtext.text = tbcontent;
		finishedtyping = true;
		istyping = false;
		canceltyping = false;
		yield break;
	}
}
