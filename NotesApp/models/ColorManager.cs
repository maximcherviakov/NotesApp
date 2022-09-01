using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.models
{
    static class ColorManager
    {
        public static Dictionary<string, string> colors = new Dictionary<string, string>()
        {
            {"orange", "#e6b904" },
            {"green", "#65ba5a" },
            {"pink", "#ea86c2" },
            {"purple", "#b07fe0" },
            {"blue", "#59c0e7" },
            {"gray", "#989898" },
            {"coal", "#444444" }
        };
        public static Color GetColorByName(string color)
        {
            switch (color)
            {
                case "green":
                    return Color.FromArgb(255, 101, 186, 90);
                case "pink":
                    return Color.FromArgb(255, 234, 134, 194);
                case "purple":
                    return Color.FromArgb(255, 176, 127, 224);
                case "blue":
                    return Color.FromArgb(255, 89, 192, 231);
                case "gray":
                    return Color.FromArgb(255, 152, 152, 152);
                case "coal":
                    return Color.FromArgb(255, 68, 68, 68);
                case "orange":
                default:
                    return Color.FromArgb(255, 230, 185, 4);
            }
        }
    }
}
