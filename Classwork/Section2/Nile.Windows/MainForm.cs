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

            //PlayingWithProductMembers();
        }

        private void PlayingWithProductMembers()
        {
            var product = new Product();

            Decimal.TryParse("123", out decimal price);
            product.Price = price;

            var name = product.Name;
            //var name = product.GetName();
            product.Name = "Product A";
            product.Price = 50;
            product.IsDiscontinued = true;

            //product.ActualPrice = 10;
            var price2 = product.ActualPrice; 


            //product.SetName("Prod A");
            product.Description = "None";
            var error = product.Validate();

            var str = product.ToString();


            var productB = new Product();
            //productB.SetName("prod b");
            productB.Description = product.Description;
            error = productB.Validate();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new ProductDetailForm("Add Product");
            //form.Text = "Add Product";

            // show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"add" the product
            _product = form.Product;
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            if (!ShowConfirmation("Aer you sure?", "Remove Product"))
                return;
            //todo: remove product
            // MessageBox.Show("Not implemented");
            //Remove product
            _product = null;
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //MessageBox.Show(this, "Not implemented", "Product edit box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_product == null)
                                return;

            var form = new ProductDetailForm(_product);
            //var form = new ProductDetailForm("Edit Product");
            //form.Text = "Edit Product";
            //form.Product = _product;

            //Show form modally
            var result = form.ShowDialog(this);
                        if (result != DialogResult.OK)
                               return;
            
                        //"Editing" the product
            _product = form.Product;
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            //MessageBox.Show(this, "Not implemented", "File exit box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            MessageBox.Show(this, "Not implemented", "Help about box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        }

        private bool ShowConfirmation (string message, string title)
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        }

        private Product _product;
    }
}
