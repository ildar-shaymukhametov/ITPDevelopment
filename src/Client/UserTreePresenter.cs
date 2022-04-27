using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using Domain;
using Microsoft.Extensions.Logging;

namespace Client;

public interface IUserTreePresenter
{
    Task UpdateViewAsync();
}

public class UserTreePresenter : IUserTreePresenter
{
    private readonly IUserTree _view;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserTreePresenter> _logger;

    public UserTreePresenter(IUserTree view, IUserRepository userRepository, ILogger<UserTreePresenter> logger)
    {
        _view = view;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task UpdateViewAsync()
    {
        var users = await _userRepository.GetAllAsync();
        _view.Nodes = CreateCategoryTree(users);
    }

    private static TreeNode[] CreateCategoryTree(List<User> users)
    {
        var nodes = new List<TreeNode>();

        foreach (var item in users)
        {
            if (item.ParentId == null)
            {
                nodes.Add(new TreeNode { Tag = item.Id, Text = $"{item.FirstName} {item.LastName}" });
            }
            else
            {
                CreateNode(nodes, item);
            }
        }

        return nodes.ToArray();
    }

    private static void CreateNode(IEnumerable nodes, User parent)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag.Equals(parent.ParentId))
            {
                node.Nodes.Add(new TreeNode { Tag = parent.Id, Text = $"{parent.FirstName} {parent.LastName}" });
            }
            else
            {
                CreateNode(node.Nodes, parent);
            }
        }
    }
}