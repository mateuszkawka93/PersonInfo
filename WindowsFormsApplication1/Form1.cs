using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PersonInfo
{
    public partial class Form1 : Form
    {
        private readonly Person _person = new Person();

        private readonly List<Panel> _listPanel = new List<Panel>();

        private int _index;

        public Form1()
        {
            InitializeComponent();

            textBoxName.DataBindings.Add("Text", _person, "Name",
                true, DataSourceUpdateMode.OnPropertyChanged);

            textBoxSurname.DataBindings.Add("Text", _person, "Surname",
                true, DataSourceUpdateMode.OnPropertyChanged);

            textBoxAddress.DataBindings.Add("Text", _person, "Address",
                true, DataSourceUpdateMode.OnPropertyChanged);

            maskedTextBoxPhone.DataBindings.Add("Text", _person, "Phone",
                true, DataSourceUpdateMode.OnPropertyChanged);


            textBoxName.TextChanged += textBoxes_TextChanged;
            textBoxSurname.TextChanged += textBoxes_TextChanged;
            textBoxAddress.TextChanged += textBoxes_TextChanged;
            maskedTextBoxPhone.TextChanged += maskedTextBoxPhone_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _listPanel.Add(panel1);
            _listPanel.Add(panel2);
            _listPanel.Add(panel3);
            _listPanel.Add(panel4);
            _listPanel.Add(panel5);

            _listPanel[_index].BringToFront();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_index < _listPanel.Count - 1)
                _listPanel[++_index].BringToFront();
            btnNext.Enabled = false;


            btnNext.Enabled = !string.IsNullOrWhiteSpace(textBoxSurname.Text);
            btnNext.Enabled = !string.IsNullOrWhiteSpace(textBoxAddress.Text);
            btnNext.Enabled = maskedTextBoxPhone.MaskCompleted;


            labelShowName.Text = _person.Name;
            labelShowSurname.Text = _person.Surname;
            labelShowAddress.Text = _person.Address;
            labelShowPhone.Text = _person.Phone;


            if (_index == 3)
                btnNext.Text = "Podsumuj";
            if (_index == 4)
            {
                btnNext.Visible = false;
                btnClose.Visible = true;
            }
            if (_index > 0)
                btnPrevious.Visible = true;
        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_index > 0)
            {
                _listPanel[--_index].BringToFront();
                btnNext.Enabled = true;
            }


            if (_index < 4)
            {
                btnClose.Visible = false;
                if (_index != 3)
                {
                    btnNext.Visible = true;
                    btnNext.Text = "Dalej";
                }
                else
                {
                    btnNext.Visible = true;
                    btnNext.Text = "Podsumuj";
                }
            }


            if (_index == 0)
                btnPrevious.Visible = false;
        }


        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox) sender;

            btnNext.Enabled = !string.IsNullOrWhiteSpace(textbox.Text);


            if (textbox.Name != "textBoxAddress")
            {
                textbox.MaxLength = 20;
            }
            else
                textbox.MaxLength = 40;
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char) Keys.Back);
        }

        private void textBoxSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char) Keys.Back);
        }

        private void maskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = maskedTextBoxPhone.MaskCompleted;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}