﻿using System;
using System.Windows.Forms;
using Application;
using Microsoft.Extensions.Logging;

namespace Client;

public partial class Form1 : Form, IUpdateUserView, IUserTree
{
    private readonly IUpdateUserPresenter _updateUserPresenter;
    private readonly IUserTreePresenter _userTreePresenter;

    public Form1(ILogger<UpdateUserPresenter> userPresenterLogger, ILogger<UserTreePresenter> userTreePresenterLogger, IUserRepository userRepository)
    {
        InitializeComponent();
        _updateUserPresenter = new UpdateUserPresenter(this, userRepository, userPresenterLogger);
        _userTreePresenter = new UserTreePresenter(this, userRepository, userTreePresenterLogger);
    }

    private bool _submitEnabled;
    public bool SubmitEnabled
    { 
        get => _submitEnabled;
        set
        {
            _submitEnabled = value;
            button1.Enabled = _submitEnabled;
        }
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        var ok = await _updateUserPresenter.UpdateUserAsync();
        var message = ok ? "Изменения сохранены" : "Не удалось сохранить";
        MessageBox.Show(message);
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        _updateUserPresenter.FirstName = textBox1.Text;
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
        _updateUserPresenter.LastName = textBox2.Text;
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {

    }

    public TreeNode[] Nodes
    {
        set
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.AddRange(value);
        }
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        _updateUserPresenter.UpdateView();
        await _userTreePresenter.UpdateViewAsync();
    }
}