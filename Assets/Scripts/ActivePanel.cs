using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ActivePanel : MonoBehaviour {
    public GameObject activeButton;

	// Use this for initialization
	void Start () {
        OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        if(activeButton != null)
        {
            EventSystem.current.SetSelectedGameObject(activeButton);
        }
        else
        {

        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        if(activeButton != null)
        {
            activeButton = EventSystem.current.currentSelectedGameObject;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void loadscene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
