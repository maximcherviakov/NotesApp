using NotesApp.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.controllers
{
    public class NoteManager
    {
        private static DBHelper dBHelper = new DBHelper();
        public static ObservableCollection<Note> notesList { get; set; } = new ObservableCollection<Note>();

        public NoteManager()
        {
            loadListFromDB(dBHelper.GetAllTable());
        }

        public void AddNewNote()
        {
            addNewNoteToList();
        }

        private void addNewNoteToList()
        {
            int noteId = dBHelper.Insert("orange", "<FlowDocument PagePadding=\"5, 0, 5, 0\" AllowDrop=\"True\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>Write new note</Paragraph></FlowDocument>");
            DataTable table = dBHelper.GetRowById(noteId);
            Note note = new Note(new NoteData()
            {
                Id = Convert.ToInt32(table.Rows[0][0].ToString()),
                Date = table.Rows[0][1]?.ToString(),
                Color = table.Rows[0][2]?.ToString(),
                NoteText = table.Rows[0][3]?.ToString()
            });
            note.Show();
            notesList.Insert(0, note);
        }

        public void loadListFromDB(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                notesList.Add((new Note(new NoteData()
                {
                    Id = Convert.ToInt32(table.Rows[i][0].ToString()),
                    Date = table.Rows[i][1]?.ToString(),
                    Color = table.Rows[i][2]?.ToString(),
                    NoteText = table.Rows[i][3]?.ToString()
                })));
            }
        }
        public void loadListFromDBByName(DataTable table, string text)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Note note = new Note(new NoteData()
                {
                    Id = Convert.ToInt32(table.Rows[i][0].ToString()),
                    Date = table.Rows[i][1]?.ToString(),
                    Color = table.Rows[i][2]?.ToString(),
                    NoteText = table.Rows[i][3]?.ToString()
                });

                if(text.Replace(" ", "").Equals("")
                    || note.PreviewText.ToLower().Contains(text.ToLower()))
                {
                    notesList.Add(note);
                }
            }
        }
    }
}
