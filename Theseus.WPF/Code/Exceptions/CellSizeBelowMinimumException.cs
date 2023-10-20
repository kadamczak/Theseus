using System;

namespace Theseus.WPF.Code.Exceptions
{
    public class CellSizeBelowMinimumException : Exception
    {
        public CellSizeBelowMinimumException(string? message) : base(message)
        {
        }
    }
}
