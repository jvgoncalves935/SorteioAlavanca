using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public static string[] Load() {
        string textFile = Resources.Load<TextAsset>("sorteio_fevereiro").text;
        return textFile.Split('\n');
    }
}
