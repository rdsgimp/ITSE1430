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
using Nile.Data.Memory;

namespace Nile.Windows
{
    //Test if i can committ
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            RefreshUI();
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

            //add to database
            _database.Add(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
            //get first empty array element and add product to it
            //var index = FindEmptyProductIndex();
            //if (index >= 0)
            //    _products[index] = form.Product;


            //for (var index = 0; index < _products.Length; ++index)
            //{
            //    if (_products[index] == null)
            //    {
            //        _products[index] = form.Product;
            //        break;
            //    }
            //}

        }

        //private int FindEmptyProductIndex()
        //{
        //    for (var index = 0; index < _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;
        //    }
        //    return -1;
        //}

        private void OnProductRemove( object sender, EventArgs e )
        {
            //get 1st prod
            var product = GetSelectedProduct();
            if (product == null)
                return;

            //var index = FindEmptyProductIndex() - 1;
            //if (index < 0)
            //    return;

            if (!ShowConfirmation("Are you sure?", "Remove Product"))
                return;
            //todo: remove product
            // MessageBox.Show("Not implemented");
            //Remove product
            _database.Remove(product.Id);
            RefreshUI();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;
            ////get 1st prod
            //var products = _database.GetAll();
            //var product = (products.Length > 0) ? products[0] : null;
            if (product == null)
                return;

            //var index = FindEmptyProductIndex() -1;
            //if (index < 0)
            //    return;

            //MessageBox.Show(this, "Not implemented", "Product edit box", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       // if (_products == null)
                       //         return;

            var form = new ProductDetailForm(product);
            //var form = new ProductDetailForm("Edit Product");
            //form.Text = "Edit Product";
            //form.Product = _product;

            //Show form modally
            var result = form.ShowDialog(this);
                        if (result != DialogResult.OK)
                               return;

            //update the product
            form.Product.Id = product.Id;
            _database.Edit(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
            //_products[index] = form.Product;
            RefreshUI();
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

        private Product GetSelectedProduct()
        {
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Product;
            return null;
        }

        private void RefreshUI()
        {
            //get prods
            var products = _database.GetAll();


            //bind to grid
            dataGridView1.DataSource = products;
        }
        private bool ShowConfirmation (string message, string title)
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        }

        private MemoryProductDatabase _database = new MemoryProductDatabase();
    }
}
