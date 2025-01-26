namespace SaveUpApp.Interfaces
{
    public interface IAlertService
    {
        Task<bool> ShowConfirmation(string title, string message, string accept, string cancel);
        Task ShowMessage(string title, string message, string cancel);
    }
}