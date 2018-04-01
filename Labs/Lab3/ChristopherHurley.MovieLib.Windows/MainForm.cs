/* Christopher Hurley
 * ITSE 1430
 * Lab 2
 * 28 Feb, 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChristopherHurley.MovieLib.Data;
using ChristopherHurley.MovieLib.Data.Memory;

namespace ChristopherHurley.MovieLib.Windows
{
    /// <summary>
    /// Our main program window
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// displays the main window
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            RefreshUI();
        }

        protected override void OnLoad( EventArgs e ) => base.OnLoad(e);



        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");
            //form.Text = "Add Product";

            // show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"add" the movie
            //_movie = form.Movie;
            _movie.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();


        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            //// check for null condition first, or else we'll get a null pointer error when displaying the title in the message box
            //if (_movie == null)
            //{
            //    MessageBox.Show(this, "No movie to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}
            //if (!ShowConfirmation("Are you sure you want to remove \n" + _movie.ToString() + "? \nThis cannot be undone.", "Remove Movie: " + _movie.ToString()))
            //    return;

            //_movie = null;
            var movie = GetSelectedMovie();
            if (movie == null)
                return;
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            _movie.Remove(movie.Id);
            RefreshUI();

        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //// check for null condition first, if so then display an error accordingly
            //if (_movie == null)
            //{
            //    MessageBox.Show(this, "No movie to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}
            //var form = new MovieDetailForm(_movie);
            //Get selected product
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            var form = new MovieDetailForm(movie);

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"update" the movie
            //_movie = form.Movie;
            form.Movie.Id = movie.Id;
            _movie.Update(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();

        }

        private void OnFileExit( object sender, EventArgs e ) => Close();

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new AboutHelpBox();
            form.ShowDialog(this);
        }

        private Movie GetSelectedMovie()
        {
            //Get the first selected row in the grid, if any
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }
        private void RefreshUI()
        {
            //Get products
            var movies = _movie.GetAll();
            //products[0].Name = "Product A";

            //Bind to grid
            productBindingSource.DataSource = new List<Movie>(movies);
            //dataGridView1.DataSource = new List<Product>(products);
        }

        private bool ShowConfirmation( string message, string title ) => MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        //private Movie _movie;
        private IMovieDatabase _movie = new MemoryMovieDatabase();

    }
}
