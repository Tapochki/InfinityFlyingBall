using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TandC.FlyBall
{
    public interface IUIManager
    {
        GameObject Canvas { get; set; }
        CanvasScaler CanvasScaler { get; set; }

        Camera UICamera { get; set; }

        IUIElement CurrentPage { get; }
        Stack<IUIElement> PreviousPages { get; }
        IUIPopup CurrentPopup { get; }
        Stack<IUIPopup> PreviousPopups { get; }
        void SaveCurrentPage();
        void LoadPreviousPage();
        void SaveCurrentPopup();
        void LoadPreviousPopup();
        void SetPage<T>(bool hideAll = false) where T : IUIElement;
        void DrawPopup<T>(object message = null, bool setMainPriority = false) where T : IUIPopup;
        void HidePopup<T>() where T : IUIPopup;
        IUIPopup GetPopup<T>() where T : IUIPopup;
        IUIElement GetPage<T>() where T : IUIElement;

        void HideAllPages();
    }
}