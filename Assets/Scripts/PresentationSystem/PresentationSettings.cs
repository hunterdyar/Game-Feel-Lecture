using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace PresentationSystem
{
    [CreateAssetMenu(fileName = "Presentation Settings", menuName = "Presentation/Presentation Settings", order = 0)]
    public class PresentationSettings : ScriptableObject
    {
        [Header("Configuration")]
        public VisualTreeAsset SingleHeaderSlideDocument => _singleHeaderSlideDocument;
        [SerializeField] VisualTreeAsset _singleHeaderSlideDocument;
        public VisualTreeAsset OverlaySlideDocument => _overlaySlideDocument;
        [SerializeField] VisualTreeAsset _overlaySlideDocument;
        public PanelSettings PanelSettings => _panelSettings;
        [SerializeField] private PanelSettings _panelSettings;
        
        [Header("Display Settings")]
        public Color DefaultTextColor;
        public Color DefaultBackgroundColor;

    }

}