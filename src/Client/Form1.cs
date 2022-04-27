using System;
using System.Windows.Forms;
using Application;
using Microsoft.Extensions.Logging;

namespace Client;

public partial class Form1 : Form, IUpdateUserView
{
    private readonly IUpdateUserPresenter _updateUserPresenter;

    public Form1(ILogger<UpdateUserPresenter> logger, IUserRepository userRepository)
    {
        InitializeComponent();
        _updateUserPresenter = new UpdateUserPresenter(this, userRepository, logger);
        _updateUserPresenter.UpdateView();
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
}