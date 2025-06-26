using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveController : MonoBehaviour
{
    public Item item;


    [ContextMenu("Load")]
    public void FileLoad()
    {
        item = JsonUtility.FromJson<Item>(File.ReadAllText(Application.streamingAssetsPath + "/JSON.json"));
    }


    [ContextMenu("Save")]
    public void FileSave()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/JSON.json", JsonUtility.ToJson(item));
    }
    public class Item
    {
        public int SFXVolume;
        public int MusicVolume;
    }
}
