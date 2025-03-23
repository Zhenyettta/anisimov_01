using System;

namespace Anisimov01
{
    internal class Model
    {
        public DateTime BirthDate { get; set; } = DateTime.Now.AddDays(-1);
        public string AsianSign { get; set; } = string.Empty;
        public string WesternSign { get; set; } = string.Empty;
    }
}