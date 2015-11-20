using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static List<CarrierScript> savedGames = new List<CarrierScript>();

	// Use this for initialization
	public static void Save ()
    {
        SaveLoad.savedGames.Add(CarrierScript.current);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
