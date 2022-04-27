using System.Windows.Forms;

namespace Client;

public interface IUserTree
{
    TreeNode[] Nodes { set; }
}