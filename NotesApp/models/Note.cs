using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Windows.Documents;
using NotesApp.controllers;
using System.Windows.Markup;

namespace NotesApp.models
{
    public class Note : INotifyPropertyChanged
    {
        private NoteWindow? noteWindow;
        string time;
        private string previewText;
        private string color;

        public bool visible = false;
        public bool changed = false;

        public NoteData Data { get; set; }

        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                if (!value.Equals(time))
                {
                    time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        public string PreviewText
        {
            get
            {
                return previewText;
            }

            set
            {
                if (!value.Equals(previewText))
                {
                    previewText = value;
                    NotifyPropertyChanged("PreviewText");
                }
            }
        }

        public string Color
        {
            get
            {
                return color;
            }

            set
            {
                if (value != color)
                {
                    color = value;
                    NotifyPropertyChanged("Color");
                }
            }
        }

        public Note(NoteData noteData)
        {
            this.Data = noteData;
            Time = Data.Date;
            Color = ColorManager.colors[Data.Color];
            PreviewText = GetTextFromFlowDocument();
        }

        public void Show()
        {
            if (!visible)
            {
                visible = true;
                noteWindow = new NoteWindow(this);
                noteWindow.noteTextBox.TextChanged -= noteWindow.noteTextBox_TextChanged;
                noteWindow.noteTextBox.Document = GetDocFromStr();
                noteWindow.noteTextBox.TextChanged += noteWindow.noteTextBox_TextChanged;
                noteWindow.Closing += Window_Closing;
                noteWindow.Show();
            }
            else
            {
                noteWindow?.Focus();
            }
        }

        public void Delete()
        {
            DBHelper dBHelper = new DBHelper();

            noteWindow?.Close();
            visible = false;
            noteWindow = null;
            dBHelper.Delete(Data.Id);
            NoteManager.notesList.Remove(this);
        }

        public string GetStrFromDoc()
        {
            StringWriter stringWriter = new StringWriter();
            XamlWriter.Save(noteWindow.noteTextBox.Document, stringWriter);
            return stringWriter.ToString();
        }

        public FlowDocument GetDocFromStr()
        {
            string text = Data.NoteText;
            FlowDocument flowDocument = XamlReader.Parse(text) as FlowDocument;
            flowDocument.FontFamily = new FontFamily("Segoe UI");
            flowDocument.FontSize = 14;
            return flowDocument;
        }

        public string GetTextFromFlowDocument()
        {
            FlowDocument flowDocument = GetDocFromStr();
            TextRange flowDocSelection = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            return flowDocSelection.Text;
        }

        public void Update()
        {
            DBHelper dBHelper = new DBHelper();

            Data.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Time = Data.Date;
            dBHelper.Update(Data.Id, Data.Date, Data.Color, Data.NoteText);
            NoteManager.notesList.Move(NoteManager.notesList.IndexOf(this), 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changed)
            {
                Data.NoteText = GetStrFromDoc();
                Update();
            }
            visible = false;
            changed = false;
            noteWindow = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
