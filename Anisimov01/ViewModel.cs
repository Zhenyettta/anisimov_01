using System.ComponentModel;

namespace Anisimov01
{
    internal sealed class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ValidateDateInputHandler? ValidateDateInput;

        private readonly Model _model;

        public ViewModel(ValidateDateInputHandler? errorHandler)
        {
            _model = new Model();
            ValidateDateInput += errorHandler;
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public DateTime BirthDate
        {
            get => _model.BirthDate;
            set
            {
                if (_model.BirthDate == value)
                    return;

                _model.BirthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(AsianSignString));
                OnPropertyChanged(nameof(WesternSignString));
            }
        }

        private void OnDataInputValidate(string errorMsg) =>
            ValidateDateInput?.Invoke(this, new ValidateDateInput(errorMsg));

        private static string GetAsianSign(DateTime birthDate)
        {
            int index = ((birthDate.Year - 4) % 12 + 12) % 12;
            return index switch
            {
                0 => "Rat",
                1 => "Ox",
                2 => "Tiger",
                3 => "Rabbit",
                4 => "Dragon",
                5 => "Snake",
                6 => "Horse",
                7 => "Goat",
                8 => "Monkey",
                9 => "Rooster",
                10 => "Dog",
                11 => "Pig",
                _ => "Impossible date"
            };
        }

        private static string GetWesternSign(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;

            return (month, day) switch
            {
                (3, >= 21) or (4, <= 19) => "ARIES",
                (4, >= 20) or (5, <= 20) => "TAURUS",
                (5, >= 21) or (6, <= 20) => "GEMINI",
                (6, >= 21) or (7, <= 22) => "CANCER",
                (7, >= 23) or (8, <= 22) => "LEO",
                (8, >= 23) or (9, <= 22) => "VIRGO",
                (9, >= 23) or (10, <= 22) => "LIBRA",
                (10, >= 23) or (11, <= 21) => "SCORPIUS",
                (11, >= 22) or (12, <= 21) => "SAGITTARIUS",
                (12, >= 22) or (1, <= 19) => "CAPRICORN",
                (1, >= 20) or (2, <= 18) => "AQUARIUS",
                (2, >= 19) or (3, <= 20) => "PISCES",
                _ => "Impossible date"
            };
        }

        private int GetAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
                age--;

            if (age is < 0 or > 135)
            {
                OnDataInputValidate("Impossible age!");
                age = age < 0 ? 0 : 135;
            }

            if (birthDate.Month == today.Month && birthDate.Day == today.Day)
            {
                OnDataInputValidate("Happy birthday!");
            }

            return age;
        }

        public string WesternSignString => GetWesternSign(BirthDate);
        public string AsianSignString => GetAsianSign(BirthDate);
        public int Age => GetAge(BirthDate);
    }
}