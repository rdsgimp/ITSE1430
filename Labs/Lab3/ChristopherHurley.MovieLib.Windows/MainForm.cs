/* Christopher Hurley
 * ITSE 1430
 * Lab 3
 * 1 April, 2018
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

        //Called when a cell is double clicked
        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            OnProductEdit(sender, e);
        }

        //Called when a key is pressed while in a cell
        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            //not 100% sure why Delete is handled here when it seems to work from the menu item shortcut key?
            //does this account for backspace or something similar? 
            //TODO:need to remember to ask in class
            //if (e.KeyCode == Keys.Delete)
            //{
            //    e.Handled = true;
            //    OnProductRemove(sender, e);
            //} else 

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                OnProductEdit(sender, e);
            };
        }



        private void OnProductAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            // show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"add" the movie
            _movie.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
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

            //Get selected movie
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

            //Bind to grid
            productBindingSource.DataSource = new List<Movie>(movies);
        }

        private bool ShowConfirmation( string message, string title ) => MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private IMovieDatabase _movie = new MemoryMovieDatabase();
    }
}
