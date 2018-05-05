/* Christopher Hurley
 * ITSE 1430
 * Lab 4
 * 4 May, 2018
 */
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ChristopherHurley.MovieLib.Windows
{
    /// <summary>
    /// This is our movie detail form, used to add and edit movies.
    /// </summary>
    public partial class MovieDetailForm : Form
    {
        #region Consruction
        /// <summary>
        /// no arg constructor
        /// </summary>
        public MovieDetailForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// constructor that sets the window title when called with a string
        /// </summary>
        /// <param name="title">the title to set for the window</param>
        public MovieDetailForm( string title ) : this() // : base ()
        {
            Text = title;
        }
        /// <summary>
        /// constructor that loads an existing movie for editing
        /// </summary>
        /// <param name="movie">the movie to edit</param>
        public MovieDetailForm( Movie movie ) : this("Edit Movie")
        {
            Movie = movie;
        }

        #endregion

        /// <summary>
        /// movie property for use in our form
        /// </summary>
        public Movie Movie { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //Load movie
            if (Movie != null)
            {
                _txtName.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtPrice.Text = Movie.Length.ToString();
                _chkIsOwned.Checked = Movie.IsOwned;
            };

            ValidateChildren();
        }

        private void OnCancel( object sender, EventArgs e )
        {
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
            {
                return;
            }

            //create movie 
            var movie = new Movie {
                Title = _txtName.Text,
                Description = _txtDescription.Text,
                Length = ConvertToInt(_txtPrice),
                IsOwned = _chkIsOwned.Checked };

            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                DisplayError(errors.ElementAt(0).ErrorMessage);
                return;
            };

            //Return from form
            Movie = movie;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DisplayError( string message ) => MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private int ConvertToInt( TextBox control )
        {
            if (Int32.TryParse(control.Text, out var price))
            {
                if (price >= 0)
                    return price;
            }
            return -1;
        }

        private void _txtName_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Title is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
            
            
              // more bits of trying to get a dynamic error message to detect duplicates
                var existing = textbox.Text;
            //MainForm.Ref2();
            //((MainForm)Parent).Ref2();
            var m = MainForm._movie2.GetAll();

            foreach (var Movie in m)
              {
                
                if ((String.Compare(Movie.Title, existing, true) == 0) && Movie.Title != existing)
                {
                    _errorProvider.SetError(textbox, "A Movie with this name already exists");
                    e.Cancel = true;
                } 

              }                               
        }

        private void _txtPrice_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;
            var price = ConvertToInt(textbox);
            if (price == -1)
            {
                _errorProvider.SetError(textbox, "Testing auto change invalid to 0");

                textbox.Text = "0";
            }

            if (price < 0)
            {
                _errorProvider.SetError(textbox, "Length must be >= 0");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
