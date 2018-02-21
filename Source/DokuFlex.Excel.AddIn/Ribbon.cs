//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Excel.AddIn
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text;
    using Microsoft.Office.Tools.Ribbon;

    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.WinForms.Common;
    using DokuFlex.WinForms.Common.Resources;

    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            btnOpen.Label = StringResources.OpenText;
            btnOpen.Image = ImageResources.OpenLarge;
            btnSave.Label = StringResources.SaveText;
            btnSave.Image = ImageResources.SaveLarge;
            btnMetadata.Label = StringResources.MetadataText;
            btnMetadata.Image = ImageResources.MetadataLarge;
            sbtnFavorites.Label = StringResources.FavoritesText;
            sbtnFavorites.Image = ImageResources.FavoriteLarge;
            btnAddToFavorite.Label = StringResources.AddToFavoritesText;
            btnAddToFavorite.Image = ImageResources.FavoriteAddSmall;
            btnRecents.Label = StringResources.RecentsText;
            btnRecents.Image = ImageResources.RecentListLarge;
            btnSettings.Label = StringResources.SettingText;
            btnSettings.Image = ImageResources.SettingsLarge;
            btnFind.Label = StringResources.SearchText;
            btnFind.Image = ImageResources.SearchLarge;
            groupWorksheet.Label = StringResources.WorksheetText;
            groupData.Label = StringResources.DataText;
            groupMosUsed.Label = StringResources.MosUsedText;
            groupSearch.Label = StringResources.SearchText;
            groupTools.Label = StringResources.ToolsText;
        }

        private void btnOpen_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.OpenWorkBook();
        }

        private void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.SaveWorkbook();
        }

        private void btnRecents_Click(object sender, RibbonControlEventArgs e)
        {
            var recent = (RecentFile)null;

            //Step 1: Login to DokuFlex to get the ticket;
            var ticket = Session.GetTikect();

            using (var form = new RecentsForm(ticket, ".xls", 2))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    recent = form.SelectedValue;
                }
            }

            if (recent == null) return;

            Globals.ThisAddIn.OpenRecentWorkbook(ticket, recent);
        }

        private async void btnAddToFavorite_Click(object sender, RibbonControlEventArgs e)
        {
            await Globals.ThisAddIn.AddToFavorites();
        }

        private void btnSettings_Click(object sender, RibbonControlEventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private void sbtnFavorites_Click(object sender, RibbonControlEventArgs e)
        {
            var recent = (RecentFile)null;

            //Step 1: Login to DokuFlex to get the ticket;
            var ticket = Session.GetTikect();

            using (var form = new FavoritesForm(ticket, ".xls", 2))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    recent = form.SelectedValue;
                }
            }

            if (recent == null) return;

            Globals.ThisAddIn.OpenRecentWorkbook(ticket, recent);
        }

        private void btnMetadata_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.ShowMetadata();
        }

        private void btnChangeCredentials_Click(object sender, RibbonControlEventArgs e)
        {
            Session.ChangeCredentials();
        }

        private void btnFind_Click(object sender, RibbonControlEventArgs e)
        {
            var result = (SearchResult)null;

            //Step 1: Login to DokuFlex to get the ticket;
            var ticket = Session.GetTikect();

            using (var form = new SearchForm("xls*", 2))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    result = form.SelectedValue;
                }
            }

            if (result == null) return;

            Globals.ThisAddIn.OpenRecentWorkbook(ticket, result);
        }
    }
}
