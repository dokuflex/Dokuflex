//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Outlook.AddIn
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text;
    using Microsoft.Office.Tools.Ribbon;

    using DokuFlex.WinForms.Common.Resources;
    using DokuFlex.WinForms.Common;

    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            btnEMailAdd.Label = StringResources.UploadMailText;
            btnMailAttachAdd.Label = StringResources.UploadAttachmentsText;
            btnMailAttachAdd.Image = btnEMailAdd.Image = ImageResources.MailAddLarge;
            btnTaskAdd.Label = StringResources.NewTaskFromMailText;
            btnTaskAdd.Image = ImageResources.TaskAddLarge;       
            btnSettings.Label = StringResources.SettingText;
            btnSettings.Image = ImageResources.SettingsLarge;
            groupMail.Label = StringResources.MailText;
            groupTasks.Label = StringResources.TasksText;
            groupTools.Label = StringResources.ToolsText;
        }

        private void btnTaskAdd_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count == 0 ||
                !Globals.ThisAddIn.IsMailItem)
            {
                MessageBox.Show("No hay correo seleccionado");
                return;
            }

            var ticket = Session.GetTikect();

            using (var newTaskForm = new UpdateTaskView(ticket))
            {
                newTaskForm.ShowDialog();
            }
        }

        private void btnSettings_Click(object sender, RibbonControlEventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void btnChangeCredentials_Click(object sender, RibbonControlEventArgs e)
        {
            Session.ChangeCredentials();
        }

        private void btnMailAttachAdd_Click(object sender, RibbonControlEventArgs e)
        {
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count == 0 ||
                !Globals.ThisAddIn.IsMailItem)
            {
                MessageBox.Show("No hay correo seleccionado");
                return;
            }

            using (var uploadEmail = new UploadEmailView())
            {
                uploadEmail.ShowDialog();
            }
        }

        private void btnEMailAdd_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.UploadEmailAsEmlFile();
        }
    }
}
