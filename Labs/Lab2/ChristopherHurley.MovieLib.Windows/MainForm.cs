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
            _movie = form.Movie;
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            // check for null condition first, or else we'll get a null pointer error when displaying the title in the message box
            if (_movie == null)
            {
                MessageBox.Show(this, "No movie to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (!ShowConfirmation("Are you sure you want to remove \n" + _movie.Title + "? \nThis cannot be undone.", "Remove Movie: " + _movie.Title))
                return;

            _movie = null;
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            // check for null condition first, if so then display an error accordingly
            if (_movie == null)
            {
                MessageBox.Show(this, "No movie to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            var form = new MovieDetailForm(_movie);

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Editing" the movie
            _movie = form.Movie;
        }

        private void OnFileExit( object sender, EventArgs e ) => Close();

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new AboutBox1();
            form.ShowDialog(this);
        }

        private bool ShowConfirmation( string message, string title ) => MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private Movie _movie;
    }
}
