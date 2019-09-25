using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentDatabase
{
    public partial class frmStudent : Form
    {
        // Cody Dickson
        // M4 Assignment
        // CPT 231-741
        // Fall 2019

        public frmStudent()
        {
            InitializeComponent();
        }

        // Close the form
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Reset the form to initial state
        private void Reset()
        {
            txtSSN.ReadOnly = false;
            btnFind.Enabled = true;
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            txtSSN.Text = "";
            txtLastName.Text = "";
        }

        // On Form Load, call the Reset method
        private void FrmStudent_Load(object sender, EventArgs e)
        {
            Reset();
        }

        // Class SortedList variable declaration
        SortedList<int, string> studentDatabase = new SortedList<int, string>();

        // Add Button - validates the form with the IsValidData method, then adds the data to studentDatabase (SortedList)
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    studentDatabase.Add(Int32.Parse(txtSSN.Text), txtLastName.Text);
                    Reset();
                }
            }
            catch (Exception ex)
            {
                // General Exception
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n" + ex.StackTrace, "General Exception");
            }
        }

        // Verifies that the SSN is 9 digits
        private bool IsValidSSN(string SSN)
        {
            int number;
            if (SSN.Length == 9 & int.TryParse(SSN, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show("SSN must include 9 valid numeric numbers.", "Entry Error");
                return false;
            }
        }

        // Verifies that the LastName is present
        private bool IsValidLastName(string lastName)
        {
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Last Name field cannot be blank.", "Entry Error");
                return false;
            } else
            {
                return true;
            }
        }

        // Form validation method
        private bool IsValidData()
        {
            if (!IsValidSSN(txtSSN.Text)) return false;
            if (!IsValidLastName(txtLastName.Text)) return false;

            return true;
        }

        // Gets the SSN from the textbox and finds it in the SortedList
        // Tests the value of the SSN by searching studentDatabase (the Sorted List) for the key
        // If the key is present, it pulls the value that matches it and assigns it to the Last Name text box
        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidSSN(txtSSN.Text))
                {
                    int value = Int32.Parse(txtSSN.Text);
                    if (studentDatabase.ContainsKey(value))
                    {
                        txtLastName.Text = Convert.ToString(studentDatabase[value]);
                        btnModify.Enabled = true;
                        btnDelete.Enabled = true;
                        btnAdd.Enabled = false;
                        txtSSN.ReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("Student could not be found.", "Student Not Found");
                    }
                }
            }
            catch (Exception ex)
            {
                // General Exception
                MessageBox.Show(ex.Message + "\n\n" + ex.GetType().ToString() + "\n" + ex.StackTrace, "General Exception");
            }
        }

        // Takes the value in the SSN text box, finds that Key in the Sorted List, and updates the Last Name value
        private void BtnModify_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(txtSSN.Text);
            studentDatabase[value] = txtLastName.Text;
            MessageBox.Show("Student was updated in the database.", "Student Updated");
        }

        // Removes an entry from the Sorted List using the SSN Text Box as the key
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(txtSSN.Text);
            studentDatabase.Remove(value);
            MessageBox.Show("Student removed from the database.", "Student Removed");
            Reset();
        }

        // Resets the form
        private void BtnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // Cycles through the Sorted List, storing its Keys and Values in a string, and then displays that string in a Message Box
        private void BtnShow_Click(object sender, EventArgs e)
        {
            string results = "";
            foreach (KeyValuePair<int, string> student in studentDatabase)
            {
                results += student.Key + "\t" + student.Value + "\n";
            }
            MessageBox.Show(results, "Student List");
        }
    }
}
