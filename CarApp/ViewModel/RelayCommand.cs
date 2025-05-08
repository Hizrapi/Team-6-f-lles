using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApp.ViewModel
{
    public class RelayCommand : ICommand
    {
        // Action er en delegeret (= en metode uden returværdi).
        // 'execute' peger på den metode der skal køres, når brugeren trykker på knappen.
        private readonly Action execute;

        // Func<bool> er en delegeret der returnerer true/false.
        // 'canExecute' bruges til at afgøre om knappen overhovedet må kunne klikkes på.
        private readonly Func<bool> canExecute;

        // Constructor: her får vi de to metoder ind som parametre.
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;               // sætter hvilken handling der skal ske
            this.canExecute = canExecute;         // sætter betingelsen for om knappen må aktiveres
        }

        // Dette event er nødvendigt for WPF – det fortæller, at vi gerne vil have knappen genvurderet.
        // Det sker fx automatisk når tekstfelter ændres.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }   // standard WPF-mekanisme
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Bestemmer om kommandoen må køres lige nu.
        // Hvis der ikke er nogen 'canExecute', så siger vi bare 'ja'.
        public bool CanExecute(object parameter) =>
            canExecute == null || canExecute();

        // Når kommandoen køres (knappen trykkes), så udfør 'execute'.
        public void Execute(object parameter) =>
            execute();
    }
}
