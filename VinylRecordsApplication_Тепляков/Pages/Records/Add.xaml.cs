using System;
using System.Collections.Generic;
using System.Linq;
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

namespace VinylRecordsApplication_Тепляков.Pages.Records
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public IEnumerable<Classes.Manufacturer> Manufacturers = Classes.Manufacturer.AllManufacturers();
        public IEnumerable<Classes.State> AllStates = Classes.State.AllState();
        private Classes.Record changeRecord;
        public Add(Classes.Record changeRecord = null)
        {
            InitializeComponent();
            foreach (var item in Manufacturers)
                tbManufacturer.Items.Add(item.Name);
            if (Manufacturers.Count() > 0)
                tbManufacturer.SelectedIndex = 0;
            foreach (var item in AllStates)
                tbState.Items.Add(item.Name);
            if (AllStates.Count() > 0)
                tbState.SelectedIndex = 0;
            if (changeRecord != null)
            {
                this.changeRecord = changeRecord;
                tbName1.Text = changeRecord.Name;
                tbYear.Text = changeRecord.Year.ToString();
                tbPrice.Text = changeRecord.Price.ToString().Replace(",", ".");
                tbDescription.Text = changeRecord.Description;
                tbFormat.SelectedIndex = changeRecord.Format;
                tbManufacturer.SelectedIndex = Manufacturers.ToList().FindIndex(x => x.Id == changeRecord.IdManufacturer);
                tbSize.SelectedIndex = changeRecord.Size;
                tbState.SelectedIndex = AllStates.ToList().FindIndex(x => x.Id == changeRecord.IdState);
                addBtn.Content = "Изменить";
            }
        }
        private void AddRecord(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbName1.Text))
                if (!String.IsNullOrEmpty(tbYear.Text))
                    if (!String.IsNullOrEmpty(tbPrice.Text))
                        if (tbName1.Text.Length <= 250)
                        {
                            if (changeRecord == null)
                            {
                                Classes.Record newRecord = new Classes.Record()
                                {
                                    Name = tbName1.Text,
                                    Year = Convert.ToInt32(tbYear.Text),
                                    Format = tbFormat.SelectedIndex,
                                    Size = tbSize.SelectedIndex,
                                    IdManufacturer = Manufacturers.Where(x => x.Name == tbManufacturer.SelectedValue.ToString()).First().Id,
                                    Price = float.Parse(tbPrice.Text.Replace(".", ",")),
                                    IdState = AllStates.Where(x => x.Name == tbState.SelectedItem.ToString()).First().Id,
                                    Description = tbDescription.Text,
                                };
                                newRecord.Save();
                                MessageBox.Show($"Пластинка {newRecord.Name} успешно добавлена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                                MainWindow.mainWindow.OpenPage(new Pages.Records.Add(newRecord));
                            }
                            else
                            {
                                changeRecord.Name = tbName1.Text;
                                changeRecord.Year = Convert.ToInt32(tbYear.Text);
                                changeRecord.Format = tbFormat.SelectedIndex;
                                changeRecord.Size = tbSize.SelectedIndex;
                                changeRecord.IdManufacturer = Manufacturers.Where(x => x.Name == tbManufacturer.SelectedValue.ToString()).First().Id;
                                changeRecord.Description = tbDescription.Text;
                                changeRecord.Save(true);
                                MessageBox.Show($"Пластинка {changeRecord.Name} успешно изменена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                            MessageBox.Show($"Наименование пластинки слишком большое.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                        MessageBox.Show($"Пожалуйста, укажите стоимость пластинки.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show($"Пожалуйста, укажите год выпуска пластинки.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show($"Пожалуйста, укажите наименование пластинки.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void tbPreviewNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void tbPreviewFloat(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0)) && e.Text != ".";
        }
    }
}
