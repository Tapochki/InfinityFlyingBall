using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TandC.FlyBall
{
    public class UIManager : IService, IUIManager
    {
        public List<IUIElement> Pages { get { return _uiPages; } }

        private List<IUIElement> _uiPages;
        private List<IUIPopup> _uiPopups;

        public IUIElement CurrentPage { get; private set; }
        public Stack<IUIElement> PreviousPages { get; private set; }
        public IUIPopup CurrentPopup { get; private set; }
        public Stack<IUIPopup> PreviousPopups { get; private set; }
        public CanvasScaler CanvasScaler { get; set; }
        public GameObject Canvas { get; set; }

        public Camera UICamera { get; set; }

        public void Dispose()
        {
            foreach (var page in _uiPages)
                page.Dispose();

            foreach (var popup in _uiPopups)
                popup.Dispose();
        }

        public void Init()
        {
            Canvas = GameObject.Find("Canvas");
            UICamera = GameObject.Find("Camera_UI").GetComponent<Camera>();
            CanvasScaler = Canvas.GetComponent<CanvasScaler>();

            PreviousPages = new Stack<IUIElement>();
            PreviousPopups = new Stack<IUIPopup>();

            _uiPages = new List<IUIElement>()
            {
                new MainPage(),
            };
            foreach (var page in _uiPages)
                page.Init();

            _uiPopups = new List<IUIPopup>()
            {

            };

            //GameClient.Get<ILocalizationManager>().ApplyLocalization();
            foreach (var popup in _uiPopups)
                popup.Init();
        }

        public void Update()
        {
            foreach (var page in _uiPages)
                page.Update();

            foreach (var popup in _uiPopups)
                popup.Update();
        }

        public void HideAllPages()
        {
            foreach (var _page in _uiPages)
            {
                _page.Hide();
            }
        }

        public void SetPage<T>(bool hideAll = false) where T : IUIElement
        {
            if (hideAll)
            {
                HideAllPages();
            }
            else
            {
                if (CurrentPage != null)
                    CurrentPage.Hide();
            }

            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    CurrentPage = _page;
                    break;
                }
            }
            CurrentPage.Show();
        }

        public void SaveCurrentPage()
        {
            if (CurrentPage == null) return;
            if (PreviousPages == null) return;
            if (PreviousPages.Contains(CurrentPage)) return;

            PreviousPages.Push(CurrentPage);
        }

        public void LoadPreviousPage()
        {
            if (PreviousPages == null) return;
            if (PreviousPages.Count == 0) return;

            CurrentPage.Hide();
            CurrentPage = PreviousPages.Pop();
            CurrentPage.Show();
        }

        public void SaveCurrentPopup()
        {
            if (CurrentPopup == null) return;
            if (PreviousPopups == null) return;
            if (PreviousPopups.Contains(CurrentPopup)) return;

            PreviousPopups.Push(CurrentPopup);
        }

        public void LoadPreviousPopup()
        {
            if (PreviousPopups == null) return;
            if (PreviousPopups.Count == 0) return;

            CurrentPopup.Hide();
            CurrentPopup = PreviousPopups.Pop();
            CurrentPopup.Show();
        }

        public void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup
        {
            foreach (var _popup in _uiPopups)
            {
                if (_popup is T)
                {
                    CurrentPopup = _popup;
                    break;
                }
            }

            if (setMainPriority)
                CurrentPopup.SetMainPriority();

            if (message == null)
                CurrentPopup.Show();
            else
                CurrentPopup.Show(message);
        }

        public void HidePopup<T>() where T : IUIPopup
        {
            foreach (var _popup in _uiPopups)
            {
                if (_popup is T)
                {
                    _popup.Hide();
                    break;
                }
            }
        }

        public IUIPopup GetPopup<T>() where T : IUIPopup
        {
            IUIPopup popup = null;
            foreach (var _popup in _uiPopups)
            {
                if (_popup is T)
                {
                    popup = _popup;
                    break;
                }
            }

            return popup;
        }

        public IUIElement GetPage<T>() where T : IUIElement
        {
            IUIElement page = null;
            foreach (var _page in _uiPages)
            {
                if (_page is T)
                {
                    page = _page;
                    break;
                }
            }

            return page;
        }
    }
}