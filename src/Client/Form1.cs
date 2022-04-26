using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application;
using Domain;
using Microsoft.Extensions.Logging;

namespace Client
{
    public partial class Form1 : Form
    {
        private readonly ILogger<Form1> logger;
        private readonly IUserRepository userRepository;

        public Form1(ILogger<Form1> logger, IUserRepository userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await PopulateTreeViewAsync();
        }

        private async Task PopulateTreeViewAsync()
        {
            TreeNode rootNode;
            var users = await userRepository.GetAllAsync();

            DirectoryInfo info = new DirectoryInfo(@"../..");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(users, rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(List<User> users, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            List<User> subSubDirs;
            foreach (var subDir in users)
            {
                aNode = new TreeNode(subDir.FirstName, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.Users.ToList();
                if (subSubDirs.Any())
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }
    }
}
