namespace Anisimov
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }

        public bool IsAdult { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }

        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;

            IsAdult = CalculateIsAdult();
            SunSign = CalculateSunSign();
            ChineseSign = CalculateChineseSign();
            IsBirthday = CalculateIsBirthday();
        }

        private bool CalculateIsAdult()
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear)
                age--;
            return age >= 18;
        }

        private string CalculateSunSign()
        {
            int day = DateOfBirth.Day;
            int month = DateOfBirth.Month;
            return (month, day) switch
            {
                (3, >= 21) or (4, <= 19) => "Aries",
                (4, >= 20) or (5, <= 20) => "Taurus",
                (5, >= 21) or (6, <= 20) => "Gemini",
                (6, >= 21) or (7, <= 22) => "Cancer",
                (7, >= 23) or (8, <= 22) => "Leo",
                (8, >= 23) or (9, <= 22) => "Virgo",
                (9, >= 23) or (10, <= 22) => "Libra",
                (10, >= 23) or (11, <= 21) => "Scorpio",
                (11, >= 22) or (12, <= 21) => "Sagittarius",
                (12, >= 22) or (1, <= 19) => "Capricorn",
                (1, >= 20) or (2, <= 18) => "Aquarius",
                (2, >= 19) or (3, <= 20) => "Pisces",
                _ => "Unknown"
            };
        }

        private string CalculateChineseSign()
        {
            var signs = new[]
            {
                "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox",
                "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"
            };
            return signs[DateOfBirth.Year % 12];
        }

        private bool CalculateIsBirthday()
        {
            return DateOfBirth.Day == DateTime.Today.Day && DateOfBirth.Month == DateTime.Today.Month;
        }
    }
}

