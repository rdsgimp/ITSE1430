﻿/* Christopher Hurley
 * ITSE 1430
 * Lab 4
 * 4 May, 2018
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using ChristopherHurley.MovieLib.Data;
using ChristopherHurley.MovieLib.Data.Sql;

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

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _movie = new SqlMovieDatabase(connString.ConnectionString);
        }

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
            RefreshUI();

            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            //var message2 = "temp";  //using as a flag for while loop below

            //Show form modally
            //until message2 is null or empty we restore the window
            //it was hacked like this to preserve the form contents, 
            //i.e. dont dump the description info if they had something entered
           // while (!String.IsNullOrEmpty(message2))
            //{
                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

            try
            {
                _movie.Add(form.Movie);
            } catch (NotImplementedException)
            {
                MessageBox.Show("Not implemented yet");
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                //"add" the movie
                //_movie.Add(form.Movie, out var message);
                //message2 = message;
                //if (!String.IsNullOrEmpty(message))
                //{
                //    MessageBox.Show(message);
                //    //OnProductAdd(sender, e); this was the first or second bad idea to try and redisplay the form on error
                //}
           // }
            RefreshUI();
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            RefreshUI();

            var movie = GetSelectedMovie();
            if (movie == null)
                return;
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            try
            {
                _movie.Remove(movie.Id);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshUI();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            RefreshUI();

            //Get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            var form = new MovieDetailForm(movie);

            //var message2 = "temp";  //using as a flag for while loop below

            //Show form modally
            //until message2 is null or empty we restore the window
            //it was hacked like this to preserve the form contents, 
            //i.e. dont dump the description info if they had something entered
            //while (!String.IsNullOrEmpty(message2)) 
            //{
                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

                //"update" the movie
                //_movie = form.Movie;
                form.Movie.Id = movie.Id;
            try
            {
                _movie.Update(form.Movie);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

                //_movie.Update(form.Movie, out var message);
                //message2 = message;
                //if (!String.IsNullOrEmpty(message))
                //{
                //    MessageBox.Show(message);  
                //}
           // }

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
        protected void RefreshUI()
        {
            //Get products
            IEnumerable<Movie> movies = null;
            try
            {
                movies = _movie.GetAll();
            }catch (Exception)
            {
                MessageBox.Show("Error loading movies");
            }

            productBindingSource.DataSource = movies?.ToList();
            //var movies = _movie.GetAll();
            //_movie2 = _movie;
            //_m = _movie.GetAll();

            //Bind to grid
            //productBindingSource.DataSource = new List<Movie>(movies);
        }

        private bool ShowConfirmation( string message, string title ) => MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        private IMovieDatabase _movie;// = new MemoryMovieDatabase();
        //dead code from prev lab
        public static IMovieDatabase _movie2 = new MemoryMovieDatabase();
        public  IEnumerable<Movie> _m ;


        //public static MemoryMovieDatabase database = _movie.GetAll();

        //IMovieDatabase _movie2 = _movies.GetAll();



        //bits from trying to get a dynamic error message to check for duplicate Title names..
        //
        //public void Ref2()
        //{
        //   // _m = _movie2.GetAll();
        //    RefreshUI();
        //   // MainForm.RefreshUI();
        //    IEnumerable<Movie>  m = _movie2.GetAll();
        //   // return _movie2.GetAll();
        //}
        //public static IEnumerable<Movie> m = _movie.GetAll();
    }
}
