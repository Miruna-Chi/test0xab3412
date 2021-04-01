using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data.SQLite;

public class ImagePicker : MonoBehaviour
{

    public static ImagePicker Instance;

    public GameObject GamePage;
    public GameObject CongratsPage;

    public GameObject W3_button;
    public GameObject Skyrim_button;
    public GameObject HZD_button;
    public GameObject TLOU2_button;

    public GameObject W3_panel;
    public GameObject Skyrim_panel;
    public GameObject HZD_panel;
    public GameObject TLOU2_panel;

    public GameObject text;
    public GameObject text_panel;

    public GameObject replay;

    private string connectionString;

    enum PageState 
    {
        None,
        Game,
        Congrats
    }

    List<int> imgOrder = new List<int>() {0, 0, 0, 0};
    bool congrats = false;

    public bool Congrats { get { return congrats; } }

    void Awake() 
    {
        Instance = this;
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                GamePage.SetActive(false);
                CongratsPage.SetActive(false);
                break;
            case PageState.Game:
                GamePage.SetActive(true);
                CongratsPage.SetActive(false);
                break;
            case PageState.Congrats:
                GamePage.SetActive(false);
                CongratsPage.SetActive(true);
                break;
        }
    }

    public void EnableButtons ()
    {
        for (int i = 0; i < imgOrder.Count; i++)
            imgOrder[i] = 0;

        W3_button.SetActive(true);
        Skyrim_button.SetActive(true);
        HZD_button.SetActive(true);
        TLOU2_button.SetActive(true);
    }

    public void PressButton (string name)
    {
        int i;
        for (i = 0; i < imgOrder.Count; i++)
            if (imgOrder[i] == 0)
                break;

        if (i == imgOrder.Count)
            return;
        
        switch (name) 
        {
            case "W3":
                imgOrder[i] = 1;
                W3_button.SetActive(false);
                break;
            case "Skyrim":
                imgOrder[i] = 2;
                Skyrim_button.SetActive(false);
                break;
            case "HZD":
                imgOrder[i] = 3;
                HZD_button.SetActive(false);
                break;
            case "TLOU2":
                imgOrder[i] = 4;
                TLOU2_button.SetActive(false);
                break;
        }
    }

    public void Replay()
    {
        replay.SetActive(false);
        SetPageState(PageState.Game);
        EnableButtons();
    }

   /* private void CreateDB()
    {

        SQLiteConnection connection = new SQLiteConnection(@"Data Source=" + Application.dataPath + "/Assets/Plugins/imgDB.db;Version=3;");
        connection.Open();
        SQLiteCommand command = connection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = "CREATE TABLE IF NOT EXISTS 'Images' ( " +
                            "   'TapOrderNo'	INTEGER NOT NULL, " +
                            "   'ID'	INTEGER, " +
                            "   'ImageName'	TEXT NOT NULL," +
                            "   PRIMARY KEY("TapOrderNo" AUTOINCREMENT)" +
                            ");";
        command.ExecuteNonQuery();
        connection.Close();
    }
    */
    
    // Start is called before the first frame update
    void Start()
    {
        connectionString = "URI=file:" + Application.dataPath + "/Assets/Plugins/imgDB.db";
        // CreateDB();
        SetPageState(PageState.Game);
    }

    // Update is called once per frame
    void Update()
    {
        if ((imgOrder[0] == 1) && (imgOrder[1] == 4) && (imgOrder[2] == 3) && (imgOrder[3] == 2))
        {
            SetPageState(PageState.Congrats);
            replay.SetActive(true);
        }
        else if ((imgOrder[0] != 0) && (imgOrder[1] != 0) && (imgOrder[2] != 0) && (imgOrder[3] != 0))
        {
            EnableButtons();
        }
    }
}
