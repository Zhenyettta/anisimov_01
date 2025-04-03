using System;

namespace Anisimov
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException() : base("Дата народження не може бути в майбутньому.") { }
        public FutureBirthDateException(string message) : base(message) { }
        public FutureBirthDateException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TooOldBirthDateException : Exception
    {
        public TooOldBirthDateException() : base("Вік користувача має бути в межах від 0 до 135 років.") { }
        public TooOldBirthDateException(string message) : base(message) { }
        public TooOldBirthDateException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base("Невірний формат електронної пошти.") { }
        public InvalidEmailException(string message) : base(message) { }
        public InvalidEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}