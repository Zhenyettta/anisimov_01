using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Anisimov
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDate;
        private string _result;
        private bool _isBusy;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(nameof(Result)); }
        }

        public bool CanProceed => !string.IsNullOrWhiteSpace(FirstName) &&
                                  !string.IsNullOrWhiteSpace(LastName) &&
                                  !string.IsNullOrWhiteSpace(Email) &&
                                  BirthDate != null &&
                                  !_isBusy;

        public ICommand ProceedCommand => new RelayCommand(async _ => await ProceedAsync(), _ => CanProceed);

        private async Task ProceedAsync()
        {
            _isBusy = true;
            OnPropertyChanged(nameof(CanProceed));
            Result = "Обробка...";

            Person person = null;

            try
            {
                person = await Task.Run(() =>
                {
                    if (!Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        throw new Exception("Невірний формат електронної пошти.");

                    if (BirthDate == null)
                        throw new Exception("Дата народження не вибрана.");

                    if (BirthDate > DateTime.Today)
                        throw new Exception("Дата народження не може бути в майбутньому.");

                    int age = DateTime.Today.Year - BirthDate.Value.Year;
                    if (DateTime.Today.DayOfYear < BirthDate.Value.DayOfYear)
                        age--;

                    if (age < 0 || age > 135)
                        throw new Exception("Вік користувача має бути в межах від 0 до 135 років.");

                    return new Person(FirstName, LastName, Email, BirthDate.Value);
                });

                if (person.IsBirthday)
                {
                    MessageBox.Show("З Днем народження! 🎉", "Привітання", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Result = $"Ім'я: {person.FirstName}\n" +
                         $"Прізвище: {person.LastName}\n" +
                         $"Email: {person.Email}\n" +
                         $"Дата народження: {person.DateOfBirth:d}\n" +
                         $"IsAdult: {person.IsAdult}\n" +
                         $"SunSign: {person.SunSign}\n" +
                         $"ChineseSign: {person.ChineseSign}\n" +
                         $"IsBirthday: {person.IsBirthday}";

                MessageBox.Show(Result, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Result = string.Empty;
            }
            finally
            {
                _isBusy = false;
                OnPropertyChanged(nameof(CanProceed));
            }
        }

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
