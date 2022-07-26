using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.models
{
    static class ColorManager
    {
        public static Color GetColorByName(string color)
        {
            switch (color)
            {
                case "purple":
                    return Color.FromArgb(150, 136, 242);
                case "blue":
                    return Color.FromArgb(126, 188, 242);
                case "mint":
                    return Color.FromArgb(106, 217, 145);
                case "red":
                    return Color.FromArgb(242, 80, 80);
                case "orange":
                default:
                    return Color.FromArgb(255, 165, 0);
            }
        }
    }
}
