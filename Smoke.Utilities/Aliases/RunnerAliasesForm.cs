using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smoke.Utilities.Aliases
{
    public partial class RunnerAliasesForm : Form
    {
        private Dictionary<string, List<string>> _runnerAliases;

        public RunnerAliasesForm()
        {
            InitializeComponent();
        }

        private void SetAliases()
        {
            this.selectedRunnerAliasesListBox.DataSource = _runnerAliases[existingRunnersListBox.SelectedItem.ToString()];
        }

        private void LoadData(int selectedRunnerIndex = 0, bool clearTextBoxes = true)
        {
            _runnerAliases = RunnerAliasesHelper.LoadAllRunners();
            existingRunnersListBox.DataSource = _runnerAliases.Keys.ToList();
            existingRunnersListBox.SelectedIndex = selectedRunnerIndex;
            newRunnerNameTextBox.Text = string.Empty;
            newAliasTextBox.Text = string.Empty;
            SetAliases();
        }

        private void addToExistingRunnersButton_Click(object sender, EventArgs e)
        {
            if (RunnerAliasesHelper.AddRunner(newRunnerNameTextBox.Text))
            {
                LoadData(existingRunnersListBox.Items.Count);
            }
        }

        private void addAliasButton_Click(object sender, EventArgs e)
        {
            if (RunnerAliasesHelper.AddAlias(newAliasTextBox.Text, existingRunnersListBox.SelectedItem.ToString()))
            {
                LoadData(existingRunnersListBox.SelectedIndex);
            }
        }

        private void RunnerAliasesForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void existingRunnersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetAliases();
        }
    }
}
