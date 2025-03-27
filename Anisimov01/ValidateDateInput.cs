namespace Anisimov01
{
    public class ValidateDateInput(string errorMsg)
    {
        public string ErrorMsg { get; } = errorMsg;
    }

    public delegate void 
        ValidateDateInputHandler(object? sender, ValidateDateInput e);
}