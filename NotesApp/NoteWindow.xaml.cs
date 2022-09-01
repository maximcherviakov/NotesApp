using NotesApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        public Note Note { get; set; }


        public NoteWindow(Note note)
        {
            Note = note;
            InitializeComponent();
            this.DataContext = Note;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseButton.Content = FindResource("white-close");
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseButton.Content = FindResource("black-close");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            this.DragMove();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            openPopUp();
        }

        private void BoldTextButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Focus();
            
            try
            {
                if (noteTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                {
                    noteTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
                }
                else
                {
                    noteTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                }
            }
            catch
            {
                noteTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
            }
        }

        private void ItalicTextButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Focus();

            try
            {
                if (noteTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                {
                    noteTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    noteTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                }
            }
            catch
            {
                noteTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Regular);
            }
        }

        private void UnderlineTextButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Focus();

            TextRange selectionRange = new TextRange(noteTextBox.Selection.Start, noteTextBox.Selection.End);
            object selectedProperty = selectionRange.GetPropertyValue(TextBlock.TextDecorationsProperty);

            TextDecorationCollection textDecorationCollection = selectedProperty as TextDecorationCollection;
            if (textDecorationCollection != null && textDecorationCollection.Count != 0)
            {
                foreach (TextDecoration textDecoration in textDecorationCollection)
                {
                    if (textDecoration.Location == TextDecorationLocation.Underline)
                    {
                        noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, null);
                    }
                    else
                    {
                        noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, TextDecorations.Underline);
                    }
                }
            }
            else
            {
                noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void StrikethroughTextButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Focus();

            TextRange selectionRange = new TextRange(noteTextBox.Selection.Start, noteTextBox.Selection.End);
            object selectedProperty = selectionRange.GetPropertyValue(TextBlock.TextDecorationsProperty);

            TextDecorationCollection textDecorationCollection = selectedProperty as TextDecorationCollection;
            if (textDecorationCollection != null && textDecorationCollection.Count != 0)
            {
                foreach (TextDecoration textDecoration in textDecorationCollection)
                {
                    if (textDecoration.Location == TextDecorationLocation.Strikethrough)
                    {
                        noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, null);
                    }
                    else
                    {
                        noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, TextDecorations.Strikethrough);
                    }
                }
            }
            else
            {
                noteTextBox.Selection.ApplyPropertyValue(TextBox.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
        }

        private void MainGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SettingsMenu.Height > 0)
            {
                DoubleAnimation settingMenuAnimation = new DoubleAnimation();
                settingMenuAnimation.From = SettingsMenu.ActualHeight;
                settingMenuAnimation.To = 0;
                settingMenuAnimation.Duration = TimeSpan.FromMilliseconds(100);
                SettingsMenu.BeginAnimation(Grid.HeightProperty, settingMenuAnimation);
            }
        }

        private void openPopUp()
        {
            DoubleAnimation settingMenuAnimation = new DoubleAnimation();
            settingMenuAnimation.From = SettingsMenu.ActualHeight;
            settingMenuAnimation.To = 100;
            settingMenuAnimation.Duration = TimeSpan.FromMilliseconds(100);
            SettingsMenu.BeginAnimation(Grid.HeightProperty, settingMenuAnimation);
        }

        private void closePopUp()
        {
            DoubleAnimation settingMenuAnimation = new DoubleAnimation();
            settingMenuAnimation.From = SettingsMenu.ActualHeight;
            settingMenuAnimation.To = 0;
            settingMenuAnimation.Duration = TimeSpan.FromMilliseconds(100);
            SettingsMenu.BeginAnimation(Grid.HeightProperty, settingMenuAnimation);
        }

        private void yellowColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("orange");
        }

        private void greenColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("green");
        }

        private void pinkColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("pink");
        }

        private void purpleColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("purple");
        }

        private void blueColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("blue");
        }

        private void grayColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("gray");
        }

        private void coalColor_Click(object sender, RoutedEventArgs e)
        {
            updateColor("coal");
        }

        private void updateColor(string color)
        {
            Note.changed = true;
            Note.Data.Color = color;
            Note.Color = ColorManager.colors[Note.Data.Color];
            closePopUp();
        }

        public void noteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Note.changed = true;
            Note.Data.NoteText = Note.GetStrFromDoc();
            Note.PreviewText = Note.GetTextFromFlowDocument();
            Note.Update();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Note.Delete();
        }
    }
}
