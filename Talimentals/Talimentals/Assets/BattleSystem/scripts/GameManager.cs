using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public NavigationController nav; //updated to public
   // private InputController ui;
    public List<string> team;

    private void Awake() //classic singleton structure
    {
        team = new List<string>();
        team.Clear();
        team.Add("Dante");
        team.Add("Ignum");
        team.Add("Molom");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
       
        
        
        //nav = GetComponent<NavigationController>();
        //ui = GetComponent<InputController>();
        Load();
        //ui.UpdateDisplayText(nav.Unpack()); //get the room description and display
        //ui.onGameOver += ClearTeam;
    }

    private void ClearTeam()
    {
        Debug.Log("Team Reset");
    }

    public void Save()
    {
        SaveState theData = new SaveState();
        //theData.currentRoom = nav.currentRoom.name; //string version of the so (just the name)

        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log(Application.persistentDataPath);
        FileStream fileStream = File.Create(Application.persistentDataPath + "/player.save");
        bf.Serialize(fileStream, theData);
        fileStream.Close();
    }

    public void Load()
    {

        //check to see if they have saved previously
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState theData = (SaveState)bf.Deserialize(fileStream);
            fileStream.Close();

            //nav.currentRoom = nav.GetRoomByName(theData.currentRoom);
            //loads the saved inventory
            team.Add(theData.team);
        }
    }
}
