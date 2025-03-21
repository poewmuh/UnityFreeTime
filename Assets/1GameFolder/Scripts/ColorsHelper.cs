using UnityEngine;

namespace GameFolder
{
    public static class ColorsHelper
    {
        public static Color textHighlightedColor
        {
            get
            {
                ColorUtility.TryParseHtmlString("#E9E75E", out var color);
                return color;
            }
        }
        
        public static Color settingsTabTextColor
        {
            get
            {
                ColorUtility.TryParseHtmlString("#B7B7B7", out var color);
                return color;
            }
        }
    }
}