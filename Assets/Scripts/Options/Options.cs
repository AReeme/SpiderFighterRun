using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public DataPersistence dataPersistence;
    public GameObject returnButton;
    public GameObject deleteSaveButton;

    // Start is called before the first frame update
    void Start()
    {
        dataPersistence = DataPersistence.Instance;
        returnButton.SetActive(true);
        deleteSaveButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene("Scene2");
    }    

    public void DeleteSave()
    {
        dataPersistence.DeleteData();
        SceneManager.LoadScene("Scene 2");
    }
}
