using UnityEngine;
using System.Xml.Serialization;
using System.IO;


namespace TwilightRun
{
    public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
    {
        private const string _fileName = "save.dat";

        private XmlSerializer serializer;
        private SaveData _saveData;
        private string _path;

        public int HighScore
        {
            get => _saveData.HighScore;
            set
            {
                _saveData.HighScore = value;
                Save();
            }
        }

        public int Coins
        {
            get => _saveData.Coins;
            set
            {
                _saveData.Coins = value;
                Save();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            serializer = new XmlSerializer(typeof(SaveData));
            _path = Application.persistentDataPath + '/' + _fileName;
            Load();

        }

        private void Save()
        {
            using (StreamWriter writer = new StreamWriter(_path))
            {
                serializer.Serialize(writer.BaseStream, _saveData);
            }
        }

        private void Load()
        {
            if (File.Exists(_path))
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    _saveData = (SaveData)serializer.Deserialize(reader.BaseStream);
                }
            }
            else
                _saveData = new SaveData();
        }
    } 
}
