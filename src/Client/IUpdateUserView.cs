namespace Client;

public interface IUpdateUserView
{
    bool SubmitEnabled { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}