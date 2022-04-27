using System;
using System.Windows.Forms;
using Application;
using Microsoft.Extensions.Logging;

namespace Client;

public partial class Form1 : Form, IUpdateUserView
{
    private readonly IUpdateUserPresenter _updateUserPresenter;

    public Form1(ILogger<Form1> logger, IUserRepository userRepository)
    {
        _updateUserPresenter = new UpdateUserPresenter(this);
        InitializeComponent();
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

    private void button1_Click(object sender, EventArgs e)
    {

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