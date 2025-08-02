namespace Application.Exceptions
{
    public class SlotOccupiedException : Exception
    {
        public SlotOccupiedException(string message) : base(message)
        {
        }
    }
}