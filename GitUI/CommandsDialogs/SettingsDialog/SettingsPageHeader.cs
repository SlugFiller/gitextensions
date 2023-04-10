﻿namespace GitUI.CommandsDialogs.SettingsDialog
{
    public interface IGlobalSettingsPage : ISettingsPage
    {
        void SetGlobalSettings();
    }

    public interface ILocalSettingsPage : IGlobalSettingsPage
    {
        void SetLocalSettings();

        void SetEffectiveSettings();
    }

    public interface IDistributedSettingsPage : ILocalSettingsPage
    {
        void SetDistributedSettings();
    }

    public partial class SettingsPageHeader
    {
        private readonly SettingsPageWithHeader? _page;

        public SettingsPageHeader(SettingsPageWithHeader? page)
        {
            InitializeComponent();
            InitializeComplete();

            label1.Font = new System.Drawing.Font(label1.Font, System.Drawing.FontStyle.Bold);

            if (page is not null)
            {
                settingsPagePanel.Controls.Add(page);
                page.Dock = DockStyle.Fill;
                _page = page;
                ConfigureHeader();
            }
        }

        private void ConfigureHeader()
        {
            if (!(_page is ILocalSettingsPage localSettingsPage))
            {
                GlobalRB.Checked = true;

                EffectiveRB.Visible = false;
                DistributedRB.Visible = false;
                LocalRB.Visible = false;
                arrows1.Visible = false;
                arrows2.Visible = false;
                arrow3.Visible = false;
                tableLayoutPanel2.RowStyles[2].Height = 0;
            }
            else
            {
                LocalRB.CheckedChanged += (a, b) =>
                {
                    if (LocalRB.Checked)
                    {
                        localSettingsPage.SetLocalSettings();
                    }
                };

                EffectiveRB.CheckedChanged += (a, b) =>
                {
                    if (EffectiveRB.Checked)
                    {
                        arrows1.ForeColor = EffectiveRB.ForeColor;
                        localSettingsPage.SetEffectiveSettings();
                    }
                    else
                    {
                        arrows1.ForeColor = arrows1.BackColor;
                    }

                    arrows2.ForeColor = arrows1.ForeColor;
                    arrow3.ForeColor = arrows1.ForeColor;
                };

                EffectiveRB.Checked = true;

                if (!(localSettingsPage is IDistributedSettingsPage distributedSettingsPage))
                {
                    DistributedRB.Visible = false;
                    arrow3.Visible = false;
                }
                else
                {
                    DistributedRB.CheckedChanged += (a, b) =>
                    {
                        if (DistributedRB.Checked)
                        {
                            distributedSettingsPage.SetDistributedSettings();
                        }
                    };
                }
            }
        }

        private void GlobalRB_CheckedChanged(object sender, EventArgs e)
        {
            if (GlobalRB.Checked)
            {
                _page?.SetGlobalSettings();
            }
        }
    }
}
