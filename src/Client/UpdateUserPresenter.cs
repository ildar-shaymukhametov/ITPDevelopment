using System;
using System.Threading.Tasks;
using Application;
using Microsoft.Extensions.Logging;

namespace Client;

public interface IUpdateUserPresenter
{
    string? FirstName { get; set; }
    string? LastName { get; set; }
    Task<bool> UpdateUserAsync();
    void UpdateView();
}

public class UpdateUserPresenter : IUpdateUserPresenter
{
    private readonly IUpdateUserView _view;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UpdateUserPresenter> _logger;

    public UpdateUserPresenter(IUpdateUserView view, IUserRepository userRepository, ILogger<UpdateUserPresenter> logger)
    {
        _view = view;
        _userRepository = userRepository;
        _logger = logger;
    }

    public int Id { get; set; }

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

    public async Task<bool> UpdateUserAsync()
    {
        try
        {
            await _userRepository.UpdateAsync(new UpdateUserModel
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName
            });

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update user with id {id}", Id);
            return false;
        }
    }

    public void UpdateView()
    {
        _view.SubmitEnabled = AllInformationIsCollected();
    }

    private bool AllInformationIsCollected()
    {
        return !string.IsNullOrWhiteSpace(_firstName) && !string.IsNullOrWhiteSpace(_lastName);
    }
}