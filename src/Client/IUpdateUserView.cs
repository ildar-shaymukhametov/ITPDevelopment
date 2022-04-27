namespace Client;

public interface IUpdateUserView
{
    bool SubmitEnabled { set; }
    public string? FirstName { set; }
    public string? LastName { set; }
}