//=======================================================================================
// PulsarSoft Inc.
//=======================================================================================
// EL SOFTWARE SE ENTREGA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO, EXPRESAS O IMPLÍCITAS,
// INCLUYENDO PERO NO LIMITADAS A LAS GARANTÍAS DE COMERCIALIZACIÓN, APTITUD PARA UN
// PROPÓSITO PARTICULAR Y NO INFRACCIÓN. EN NINGÚN CASO, LOS AUTORES O TITULARES DEL
// COPYRIGHT SERÁN RESPONSABLES POR CUALQUIER RECLAMACIÓN, DAÑO U OTRA RESPONSABILIDAD,
// YA SEA EN UNA ACCIÓN DE CONTRATO, AGRAVIO O CUALQUIER OTRA FORMA, QUE SURJAN DE O EN
// CONEXION CON EL SOFTWARE O EL USO U OTROS TRATOS EN EL SOFTWARE.
//=======================================================================================
// Copyright (c) PulsarSoft Inc. Reservados todos los derechos.
// Este código es liberado bajo los términos de la licencia Apache v2.0,
// vea el archivo de texto licencia-es.txt para más información.
//=======================================================================================

namespace DokuFlex.FileSync
{
    using DokuFlex.Common.ServiceAgents;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class StopSyncingView : Form
    {
        private StopSyncingPresenter _presenter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public StopSyncingView()
        {
            InitializeComponent();

            _presenter = new StopSyncingPresenter();
            lstUserGroup.DataSource = new BindingList<SyncTableItem>(_presenter.GetSyncFolders());
        }

        private void StopSyncingView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                var item = lstUserGroup.SelectedItem as SyncTableItem;

                if (item != null)
                {
                    _presenter.StopSyncing(item.GroupId);
                }
            }
        }
    }
}
