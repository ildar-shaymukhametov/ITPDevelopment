namespace Client;

public interface IUpdateUserPresenter
{
    string? FirstName { get; set; }
    string? LastName { get; set; }
}

public class UpdateUserPresenter : IUpdateUserPresenter
{
    private readonly IUpdateUserView _view;

    public UpdateUserPresenter(IUpdateUserView view)
    {
        _view = view;
    }

    private string? _firstName;
    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            UpdateView();
        }
    }

    private string? _lastName;
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            UpdateView();
        }
    }

    private void UpdateView()
    {
        _view.SubmitEnabled = AllInformationIsCollected();
    }

    private bool AllInformationIsCollected()
    {
        return !string.IsNullOrWhiteSpace(_firstName) && !string.IsNullOrWhiteSpace(_lastName);
    }
}