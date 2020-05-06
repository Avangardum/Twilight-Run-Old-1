using UnityEngine;
using System.Xml.Serialization;
using System.IO;

namespace TwilightRun.Tests
{
	public class SaveTest : MonoBehaviour
	{
		[SerializeField] private string _path;
		[SerializeField] private string _fileName;

		private void Awake()
		{
			SaveData saveData = new SaveData();
			saveData.HighScore = 100;
			saveData.Coins = 50;
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			StreamWriter writer = new StreamWriter(_path + _fileName);
			serializer.Serialize(writer.BaseStream, saveData);
			writer.Close();

			StreamReader reader = new StreamReader(_path + _fileName);
			SaveData loadedData = (SaveData)serializer.Deserialize(reader.BaseStream);
			reader.Close();
			print(loadedData.HighScore + " " + loadedData.Coins);
		}
	} 
}
