using NotesApp.controllers;
using NotesApp.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NoteManager noteManager = new NoteManager();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = noteManager;
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

        private void ListItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((Note)notesList.SelectedItem).Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CreateNewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            noteManager.AddNewNote();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string textForSearch = searchTextBox.Text;
            DBHelper dBHelper = new DBHelper();

            NoteManager.notesList.Clear();
            noteManager.loadListFromDBByName(dBHelper.GetAllTable(), textForSearch);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
