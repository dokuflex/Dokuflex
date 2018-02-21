//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.WinForms.Common.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Helpers;

    public partial class MetadataControl : UserControl
    {
        private Control CreateComboControl(DokuField dokuField)
        {
            var comboCtrl = new ComboBox();
            comboCtrl.Dock = DockStyle.Fill;
            comboCtrl.DropDownWidth = 280;
            comboCtrl.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCtrl.Tag = dokuField;

            if (dokuField.mandatory == 1)
            {
                comboCtrl.BackColor = Color.LightYellow;
            }

            if (!String.IsNullOrWhiteSpace(dokuField.options))
            {
                var options = dokuField.options.Split('#');
                comboCtrl.Items.AddRange(options);
            }

            comboCtrl.SelectedItem = dokuField.value;

            return comboCtrl;
        }

        private Control CreateDateControl(DokuField dokuField)
        {

            // Create a new DateTimePicker control and initialize it.
            var dateCtrl = new DateTimePicker();
            dateCtrl.Format = DateTimePickerFormat.Short;
            dateCtrl.MinDate = new DateTime(1970, 1, 1);
            dateCtrl.Tag = dokuField;

            if (dokuField.mandatory == 1)
            {
                dateCtrl.BackColor = Color.LightYellow;
            }

            if (dokuField.value != null && !string.IsNullOrWhiteSpace(dokuField.value.ToString()))
            {
                var longDate = long.Parse(dokuField.value.ToString());
                dateCtrl.Value = DateTimeHelper.FromUnixEpoch(longDate);
            }
            else
            {
                dateCtrl.Value = DateTime.Now;
            }

            return dateCtrl;
        }

        private Control CreateTimeControl(DokuField dokuField)
        {
            // Create a new DateTimePicker control and initialize it.
            var timeCtrl = new DateTimePicker();
            timeCtrl.Tag = dokuField;
            // Set the MinDate and MaxDate.
            timeCtrl.MinDate = new DateTime(1970, 1, 1);
            // Set the CustomFormat string
            timeCtrl.Format = DateTimePickerFormat.Time;
            // Show the CheckBox and display the control as an up-down control.
            timeCtrl.ShowUpDown = true;

            if (dokuField.mandatory == 1)
            {
                timeCtrl.BackColor = Color.LightYellow;
            }

            if (dokuField.value != null && !string.IsNullOrWhiteSpace(dokuField.value.ToString()))
            {
                var unixDate = long.Parse(dokuField.value.ToString());
                timeCtrl.Value = unixDate.FromUnixEpoch();
                //timeCtrl.Value = DateTime.Parse(dokuField.value.ToString(), new CultureInfo("es-ES"));
            }
            else
            {
                timeCtrl.Value = DateTime.Now;
            }

            return timeCtrl;
        }

        public Control CreateCurrencyControl(DokuField dokuField)
        {
            //Create and initialize a NumericUpDown control.
            var currencyCtrl = new NumericUpDown();
            currencyCtrl.Dock = DockStyle.Fill;
            currencyCtrl.DecimalPlaces = 5;
            currencyCtrl.Maximum = int.MaxValue;
            currencyCtrl.Minimum = 0;
            currencyCtrl.Value = string.IsNullOrWhiteSpace(dokuField.value as string) ? 0 : decimal.Parse(dokuField.value.ToString());
            currencyCtrl.Tag = dokuField;

            if (dokuField.mandatory == 1)
            {
                currencyCtrl.BackColor = Color.LightYellow;
            }

            return currencyCtrl;
        }

        public Control CreateNumericControl(DokuField dokuField)
        {
            var textCtrl = new TextBox();
            textCtrl.AcceptsReturn = true;
            textCtrl.AcceptsTab = true;
            textCtrl.Dock = DockStyle.Fill;
            textCtrl.Multiline = false;
            textCtrl.ScrollBars = ScrollBars.None;
            textCtrl.Tag = dokuField;
            textCtrl.Text = dokuField.value == null ? string.Empty : dokuField.value.ToString();
            textCtrl.KeyPress += TextCtrl_KeyPress;

            if (dokuField.mandatory == 1)
            {
                textCtrl.BackColor = Color.LightYellow;
            }

            return textCtrl;
        }

        private void TextCtrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                var textCtrl = sender as TextBox;

                if (textCtrl.Text.Contains("."))
                    e.Handled = true;
                else
                    e.KeyChar = '.';
            }
            else
            {
                if (char.IsControl(e.KeyChar)) return;
                if (char.IsLetter(e.KeyChar)) e.Handled = true;
            }
        }

        private Control CreateTextControl(DokuField dokuField)
        {
            var textCtrl = new TextBox();
            textCtrl.AcceptsReturn = true;
            textCtrl.AcceptsTab = true;
            textCtrl.Dock = DockStyle.Fill;
            textCtrl.Multiline = false;
            textCtrl.ScrollBars = ScrollBars.None;
            textCtrl.Tag = dokuField;
            textCtrl.Text = dokuField.value == null ? String.Empty : dokuField.value.ToString();

            if (dokuField.mandatory == 1)
            {
                textCtrl.BackColor = Color.LightYellow;
            }

            return textCtrl;
        }

        private Control CreateMemoControl(DokuField dokuField)
        {
            var memoCtrl = new TextBox();
            memoCtrl.AcceptsReturn = true;
            memoCtrl.AcceptsTab = true;
            memoCtrl.Dock = DockStyle.Fill;
            memoCtrl.Multiline = true;
            memoCtrl.ScrollBars = ScrollBars.Vertical;
            memoCtrl.Tag = dokuField;
            memoCtrl.Height = 60;
            memoCtrl.Text = dokuField.value == null ? String.Empty : dokuField.value.ToString();

            if (dokuField.mandatory == 1)
            {
                memoCtrl.BackColor = Color.LightYellow;
            }

            return memoCtrl;
        }

        private Control CreateListControl(DokuField dokuField)
        {
            var listCtrl = new ListBox();
            listCtrl.MultiColumn = false;
            listCtrl.SelectionMode = SelectionMode.One;
            listCtrl.SelectedItem = dokuField.value;
            listCtrl.Tag = dokuField;

            if (dokuField.mandatory == 1)
            {
                listCtrl.BackColor = Color.LightYellow;
            }

            return listCtrl;
        }

        private Control CreateRichTextControl(DokuField dokuField)
        {
            var richTextCtrl = new RichTextBox();
            richTextCtrl.Height = 60;
            richTextCtrl.Tag = dokuField;
            richTextCtrl.Text = dokuField.value == null ? String.Empty : dokuField.value.ToString();

            if (dokuField.mandatory == 1)
            {
                richTextCtrl.BackColor = Color.LightYellow;
            }

            return richTextCtrl;
        }

        private Control CreateLabelControl(string text)
        {
            // Create an instance of a Label.
            var labelCtrl = new Label();
            labelCtrl.BorderStyle = BorderStyle.None;
            labelCtrl.ImageAlign = ContentAlignment.MiddleCenter;
            labelCtrl.AutoSize = true;
            // Specify that the text can display mnemonic characters.
            labelCtrl.UseMnemonic = true;
            // Set the text of the control and specify a mnemonic character.
            labelCtrl.Text = text;

            return labelCtrl;
        }

        public bool HasErrors()
        {
            var hasError = false;

            foreach (Control control in layoutPane.Controls)
            {
                if (control.Tag != null)
                {
                    var dokuField = control.Tag as DokuField;

                    if (dokuField.mandatory == 1 &&
                        (dokuField.value == null || string.IsNullOrWhiteSpace(dokuField.value.ToString())))
                    {
                        errorProvider.SetError(control, "Debe indicar un valor para la propiedad");
                        hasError = true;
                    }
                    else
                    {
                        errorProvider.SetError(control, string.Empty);
                    }
                }
            }

            return hasError;
        }

        public void ApplyChanges()
        {
            foreach (Control control in layoutPane.Controls)
            {
                if (control is Label) continue;

                var dokuField = control.Tag as DokuField;

                if (control is TextBox)
                {
                    dokuField.value = (control as TextBox).Text;
                }

                if (control is DateTimePicker)
                {
                    var dateTime = (control as DateTimePicker).Value;
                    var unixDate = DateTimeHelper.ToUnixEpoch(dateTime);
                    dokuField.value = unixDate;
                }

                if (control is ComboBox)
                {
                    dokuField.value = (control as ComboBox).SelectedItem;
                }

                if (control is RichTextBox)
                {
                    dokuField.value = (control as RichTextBox).Text;
                }

                if (control is NumericUpDown)
                {
                    dokuField.value = (control as NumericUpDown).Value;
                }

                if (control is MaskedTextBox)
                {
                    dokuField.value = (control as MaskedTextBox).Text;
                }
            }
        }

        public void BindMetadata(List<DokuField> metadata)
        {
            //Clear the layout pane content
            layoutPane.Controls.Clear();

            if (metadata == null) return;

            Control labelCtrl = null;
            Control imputCtrl = null;

            foreach (var dokuField in metadata)
            {
                if (dokuField.mandatory == 1)
                    labelCtrl = CreateLabelControl(string.Format("{0}*", dokuField.text));
                else
                    labelCtrl = CreateLabelControl(dokuField.text);

                switch (dokuField.type)
                {
                    case "F":
                        imputCtrl = CreateDateControl(dokuField);
                        break;

                    case "H":
                        imputCtrl = CreateTimeControl(dokuField);
                        break;

                    case "O":
                        imputCtrl = CreateComboControl(dokuField);
                        break;

                    case "M":
                        imputCtrl = CreateCurrencyControl(dokuField);
                        break;

                    case "N":
                        imputCtrl = CreateNumericControl(dokuField);
                        break;

                    case "RT":
                        imputCtrl = CreateMemoControl(dokuField);
                        break;

                    default:
                        imputCtrl = CreateTextControl(dokuField);
                        break;
                }

                layoutPane.Controls.Add(labelCtrl);
                layoutPane.Controls.Add(imputCtrl);
            }
        }

        public MetadataControl()
        {
            InitializeComponent();
        }

        private void layoutPane_ControlRemoved(object sender, ControlEventArgs e)
        {
            e.Control.Dispose();
        }
    }
}
