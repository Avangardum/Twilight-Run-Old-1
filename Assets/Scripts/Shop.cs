using System;
using UnityEngine;

namespace TwilightRun
{
    public class Shop : MonoBehaviour
    {
        [Header("Prices")]
        [SerializeField] private int _multiColorBackgroundPrice;
        [SerializeField] private int _grassBackgroundPrice;
        [SerializeField] private int _skyBackgroundPrice;
        [SerializeField] private int _playerCatEarsPrice;
        [SerializeField] private int _spikeCatEarsPrice;
        [Header("Background Indexes")]
        [SerializeField] private int _greenBackgroundIndex;
        [SerializeField] private int _multiColorBackgroundIndex;
        [SerializeField] private int _grassBackgroundIndex;
        [SerializeField] private int _skyBackgroundIndex;
        [Header("Button Clusters")]
        [SerializeField] private ButtonCluster _multiColorBackgroundButtons;
        [SerializeField] private ButtonCluster _grassBackgroundButtons;
        [SerializeField] private ButtonCluster _skyBackgroundButtons;
        [SerializeField] private ButtonCluster _playerCatEarsButtons;
        [SerializeField] private ButtonCluster _spikeCatEarsButtons;

        [Serializable]
        private struct ButtonCluster
        {
            public GameObject Buy;
            public GameObject Enable;
            public GameObject Disable;

            public GameObject[] AllButtons => new GameObject[] { Buy, Enable, Disable };
        }

        private bool SpendCoins(int amount)
        {
            if (SaveDataManager.Instance.Coins < amount)
                return false;
            SaveDataManager.Instance.Coins -= amount;
            return true;
        }

        private void Refresh()
        {
            ButtonCluster[] clusters = new ButtonCluster[]
            { _multiColorBackgroundButtons, _grassBackgroundButtons, _skyBackgroundButtons, _playerCatEarsButtons, _spikeCatEarsButtons };
            foreach (ButtonCluster cluster in clusters)
                foreach (GameObject button in cluster.AllButtons)
                    button.SetActive(false);
            EnableNecessaryButtonForItem(SaveDataManager.Instance.MultiColorBackgroundBought,
                SaveDataManager.Instance.ActiveBackground == _multiColorBackgroundIndex, _multiColorBackgroundButtons);
            EnableNecessaryButtonForItem(SaveDataManager.Instance.GrassBackgroundBought,
                SaveDataManager.Instance.ActiveBackground == _grassBackgroundIndex, _grassBackgroundButtons);
            EnableNecessaryButtonForItem(SaveDataManager.Instance.SkyBackgroundBought,
                SaveDataManager.Instance.ActiveBackground == _skyBackgroundIndex, _skyBackgroundButtons);
            EnableNecessaryButtonForItem(SaveDataManager.Instance.PlayerCatEarsBought,
                SaveDataManager.Instance.PlayerCatEarsActive, _playerCatEarsButtons);
            EnableNecessaryButtonForItem(SaveDataManager.Instance.SpikeCatEarsBought,
                SaveDataManager.Instance.SpikeCatEarsActive, _spikeCatEarsButtons);
        }

        private void EnableNecessaryButtonForItem(bool _isBought, bool _isActive, ButtonCluster buttons)
        {
            if (_isBought)
                if (_isActive)
                    buttons.Disable.SetActive(true);
                else
                    buttons.Enable.SetActive(true);
            else
                buttons.Buy.SetActive(true);
        }

        private void BuyItem(int price, Action savePurchase)
        {
            bool sucess = SpendCoins(price);
            if (!sucess) return;
            savePurchase.Invoke();
            Refresh();
        }

        #region Для OnClick()
        public void BuyMultiColorBackground() => BuyItem(_multiColorBackgroundPrice, () => SaveDataManager.Instance.MultiColorBackgroundBought = true);
        public void BuyGrassBackground() => BuyItem(_grassBackgroundPrice, () => SaveDataManager.Instance.GrassBackgroundBought = true);
        public void BuySkyBackground() => BuyItem(_skyBackgroundPrice, () => SaveDataManager.Instance.SkyBackgroundBought = true);
        public void BuyPlayerCatEars() => BuyItem(_playerCatEarsPrice, () => SaveDataManager.Instance.PlayerCatEarsBought = true);
        public void BuySpikeCatEars() => BuyItem(_spikeCatEarsPrice, () => SaveDataManager.Instance.SpikeCatEarsBought = true);

        public void SetActiveBackground(int index)
        {
            SaveDataManager.Instance.ActiveBackground = index;
            Refresh();
        }
        public void SetPlayerCatEarsActive(bool value)
        {
            SaveDataManager.Instance.PlayerCatEarsActive = value;
            Refresh();
        }
        public void SetSpikeCatEarsActive(bool value)
        {
            SaveDataManager.Instance.SpikeCatEarsActive = value;
            Refresh();
        }
        #endregion

        private void Start()
        {
            Refresh();
        }
    } 
}
