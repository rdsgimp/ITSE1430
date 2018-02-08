using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var product = new Product();
            var name = product.GetName();

            product.SetName("Prod A");
            product.Description = "None";
            var error = product.Validate();

            var str = product.ToString();
           

            var productB = new Product();
            //productB.SetName("prod b");
            productB.Description = product.Description;
            error = productB.Validate();


        }
    }
}
