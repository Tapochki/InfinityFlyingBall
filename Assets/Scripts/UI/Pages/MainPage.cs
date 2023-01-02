using UnityEngine;
using UnityEngine.UI;

namespace TandC.FlyBall
{
    public class MainPage : IUIElement
    {
        private GameObject _selfObject;

        private ILoadObjectsManager _loadObjectManager;
        private IUIManager _uiManager;

        private Button _playButton;

        public void Init()
        {
            _loadObjectManager = GameClient.Get<ILoadObjectsManager>();
            _uiManager = GameClient.Get<IUIManager>();

            _selfObject = MonoBehaviour.Instantiate(_loadObjectManager.GetObjectByPath<GameObject>("Prefabs/UI/Pages/MainPage"), _uiManager.Canvas.transform);

            _playButton = _selfObject.transform.Find("Button_Play").GetComponent<Button>();

            _playButton.onClick.AddListener(PlayButtonOnClickHandler);
        }

        public void Show()
        {
            _selfObject.SetActive(true);
        }

        public void Hide()
        {
            _selfObject.SetActive(false);
        }

        public void Update()
        {
        }

        public void Dispose()
        {
            _playButton.onClick.RemoveListener(PlayButtonOnClickHandler);
        }

        #region Button Handlers
        private void PlayButtonOnClickHandler()
        {
        }
        #endregion
    }
}