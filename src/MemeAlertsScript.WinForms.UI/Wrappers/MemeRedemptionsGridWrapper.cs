using MemeAlertsScript.WinForms.UI.Models;

namespace MemeAlertsScript.WinForms.Services
{
    internal class MemeRedemptionsGridWrapper
    {
        private readonly DataGridView dataGridView;

        public MemeRedemptionsGridWrapper(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        public void ClearRows()
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.dataGridView.Invoke(this.dataGridView.Rows.Clear);
            }
            else
            {
                this.dataGridView.Rows.Clear();
            }
        }

        public int AddNewRow(MemeRedemptionGridRow model)
        {
            if (this.dataGridView.InvokeRequired)
            {
                return this.dataGridView.Invoke(() => AddRowInternal(model));
            }
            else
            {
                return AddRowInternal(model);
            }
        }

        public void UpdateRowStatus(int rowId, string status)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.dataGridView.Invoke(() => UpdateRowStatusInteral(rowId, status));
            }
            else
            {
                UpdateRowStatusInteral(rowId, status);
            }
        }

        public void ResolveRow(int rowId)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.dataGridView.Invoke(() => ResolveRowInteral(rowId));
            }
            else
            {
                ResolveRowInteral(rowId);
            }
        }

        public void DeclineRow(int rowId)
        {
            if (this.dataGridView.InvokeRequired)
            {
                this.dataGridView.Invoke(() => DeclineRowInteral(rowId));
            }
            else
            {
                DeclineRowInteral(rowId);
            }
        }

        private int AddRowInternal(MemeRedemptionGridRow model)
        {
            this.dataGridView.Rows.Insert(0, 1);
            var row = this.dataGridView.Rows[0];

            row.Cells["MemeRedemptionIdColumn"].Value = model.RedemptionId;
            row.Cells["MemeRedemptionRewardIdColumn"].Value = model.RewardId;

            row.Cells["MemeRedemptionStatusColumn"].Value = model.Status;
            row.Cells["MemeRedemptionTimeColumn"].Value = model.Time;
            row.Cells["MemeRedemptionTwitchUsernameColumn"].Value = model.TwitchUsername;
            row.Cells["MemeRedemptionMemeUsernameColumn"].Value = model.MemeUsername;
            row.Cells["MemeRedemptionMemeBonusColumn"].Value = model.MemeBonus;
            row.Cells["MemeRedemptionRewardTitleColumn"].Value = model.RewardTitle;

            return 0;
        }

        private void UpdateRowStatusInteral(int rowId, string status)
        {
            this.dataGridView.Rows[rowId].Cells["MemeRedemptionStatusColumn"].Value = status;
        }
        private void ResolveRowInteral(int rowId)
        {
            this.dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void DeclineRowInteral(int rowId)
        {
            this.dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightYellow;
        }
    }
}
