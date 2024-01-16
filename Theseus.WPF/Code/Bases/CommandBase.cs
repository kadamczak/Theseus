using System;
using System.Windows.Input;

namespace Theseus.WPF.Code.Bases
{
    /// <summary>
    /// The <c>CommandBase</c> abstract class defines basic execution logic for synchronous commands.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void Dispose() { }
    }
}
