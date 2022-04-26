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
    }
}
