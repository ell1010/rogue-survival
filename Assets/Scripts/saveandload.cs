﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveandload : MonoBehaviour {
    
    public Keybindings keybinds;
    public Playerinformation playerinfo;
	public OptionValues optionvalues;
    // Use this for initialization
    void Awake () {
        Load();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quittomenu()
	{
		saveInv();
		Save();
		SceneManager.LoadScene(1);
	}

	public void saveInv()
	{
		playerinfo.playerinv = PlayerInventory.instance.invItems;
	}
	public void loadinv()
	{
		PlayerInventory.instance.invItems = playerinfo.playerinv;
	}

    public void Save()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        BinaryFormatter bf = new BinaryFormatter();

        
        FileStream savefile = File.Create(Application.persistentDataPath + "/Saves/Save1.rs");
        List<string> objects = new List<string>();
        objects.Add(JsonUtility.ToJson(keybinds));
        objects.Add(JsonUtility.ToJson(playerinfo));
		objects.Add(JsonUtility.ToJson(optionvalues));
        //var json = JsonUtility.ToJson(objects);
        bf.Serialize(savefile, objects);
        savefile.Close();
        
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Saves/Save1.rs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream savefile = File.Open(Application.persistentDataPath + "/Saves/Save1.rs", FileMode.Open);
            //List<System.Object> objects = new List<object>();
            //JsonUtility.FromJsonOverwrite((string)bf.Deserialize(savefile), objects as List<System.Object>);
            object serializedobject = bf.Deserialize(savefile);
            List<string> objects = serializedobject as List<string>;
            JsonUtility.FromJsonOverwrite(objects[0], keybinds);
            JsonUtility.FromJsonOverwrite(objects[1], playerinfo);
			JsonUtility.FromJsonOverwrite(objects[2], optionvalues);
            savefile.Close();
			if (SceneManager.GetActiveScene().buildIndex == 2)
				loadinv();
			loadoptions();
		}
		else
		{
			Save();
			print("save");
		}
    }

	public void loadoptions()
	{
		Screen.SetResolution(optionvalues.resolution.x, optionvalues.resolution.y, optionvalues.fullscreen);
	}
}
