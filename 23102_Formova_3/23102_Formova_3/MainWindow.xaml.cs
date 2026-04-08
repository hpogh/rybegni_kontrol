using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _23102_Formova_3
{
    public partial class MainWindow : Window
    {
        private Entities1 db;

        public MainWindow()
        {
            InitializeComponent();
            db = new Entities1();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                var query = db.Discipline.AsQueryable();

                if (!string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    string search = SearchBox.Text.Trim();
                    query = query.Where(d => d.Title.Contains(search));
                }

                string sortChoice = (SortCombo.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (sortChoice == "По возрастанию названия дисциплины")
                    query = query.OrderBy(d => d.Title);
                else if (sortChoice == "По убыванию названия дисциплины")
                    query = query.OrderByDescending(d => d.Title);

                var result = query.ToList();

                if (result.Count == 0)
                {
                    MessageBox.Show("Результаты поиска отсутствуют", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData.ItemsSource = null;
                }
                else
                {
                    LoadData.ItemsSource = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
    }
}