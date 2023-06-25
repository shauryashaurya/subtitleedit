﻿using Nikse.SubtitleEdit.Core.Common;
using Nikse.SubtitleEdit.Logic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nikse.SubtitleEdit.Forms.VTT
{
    public sealed partial class WebVttStylePicker : Form
    {
        public List<WebVttStyle> ImportExportStyles { get; set; }

        public WebVttStylePicker(List<WebVttStyle> styles, Paragraph paragraph)
        {
            InitializeComponent();
            UiUtil.FixFonts(this);

            ImportExportStyles = new List<WebVttStyle>();
            listViewExportStyles.Columns[0].Width = listViewExportStyles.Width - 20;

            var paragraphStyles = WebVttHelper.GetParagraphStyles(paragraph);
            foreach (var style in styles)
            {
                listViewExportStyles.Items.Add(new ListViewItem(style.Name) { Checked = paragraphStyles.Contains(style.Name), Tag = style });
            }

            Text = LanguageSettings.Current.SubStationAlphaStyles.Export;
            labelStyles.Text = LanguageSettings.Current.SubStationAlphaStyles.Styles;
            toolStripMenuItemInverseSelection.Text = LanguageSettings.Current.Main.Menu.Edit.InverseSelection;
            toolStripMenuItemSelectAll.Text = LanguageSettings.Current.Main.Menu.ContextMenu.SelectAll;
            buttonOK.Text = LanguageSettings.Current.General.Ok;
            buttonCancel.Text = LanguageSettings.Current.General.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewExportStyles.Items)
            {
                if (item.Checked)
                {
                    ImportExportStyles.Add((WebVttStyle)item.Tag);
                }
            }

            DialogResult = ImportExportStyles.Count == 0 ? DialogResult.Cancel : DialogResult.OK;
        }

        private void WebVttImportExport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewExportStyles.Items)
            {
                item.Checked = true;
            }
        }

        private void toolStripMenuItemInverseSelection_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewExportStyles.Items)
            {
                item.Checked = !item.Checked;
            }
        }
    }
}
