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
        public bool TutorialPassed
        {
            get => _saveData.TutorialPassed;
            set
            {
                _saveData.TutorialPassed = value;
                Save();
            }
        }
        public int ActiveBackground
        {
            get => _saveData.ActiveBackground;
            set
            {
                _saveData.ActiveBackground = value;
                Save();
            }
        }
        public bool PlayerCatEarsActive
        {
            get => _saveData.PlayerCatEarsActive;
            set
            {
                _saveData.PlayerCatEarsActive = value;
                Save();
            }
        }
        public bool SpikeCatEarsActive
        {
            get => _saveData.SpikeCatEarsActive;
            set
            {
                _saveData.SpikeCatEarsActive = value;
                Save();
            }
        }
        public bool GrassBackgroundBought
        {
            get => _saveData.GrassBackgroundBought;
            set
            {
                _saveData.GrassBackgroundBought = value;
                Save();
            }
        }
        public bool MultiColorBackgroundBought
        {
            get => _saveData.MultiColorBackgroundBought;
            set
            {
                _saveData.MultiColorBackgroundBought = value;
                Save();
            }
        }
        public bool SkyBackgroundBought
        {
            get => _saveData.SkyBackgroundBought;
            set
            {
                _saveData.SkyBackgroundBought = value;
                Save();
            }
        }
        public bool PlayerCatEarsBought
        {
            get => _saveData.PlayerCatEarsBought;
            set
            {
                _saveData.PlayerCatEarsBought = value;
                Save();
            }
        }
        public bool SpikeCatEarsBought
        {
            get => _saveData.SpikeCatEarsBought;
            set
            {
                _saveData.SpikeCatEarsBought = value;
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
