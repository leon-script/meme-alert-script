using TwitchLeonScript.UI.WinForms.Models;

namespace TwitchLeonScript.UI.WinForms.Wrappers
{
    internal sealed class MemeRedemptionsGridWrapper(DataGridView dataGridView)
    {
        private readonly DataGridView _dataGridView = dataGridView;

        public void ClearRows()
        {
            if (this._dataGridView.InvokeRequired)
            {
                this._dataGridView.Invoke(this._dataGridView.Rows.Clear);
            }
            else
            {
                this._dataGridView.Rows.Clear();
            }
        }

        public int AddNewRow(MemeRedemptionGridRow model)
        {
            if (this._dataGridView.InvokeRequired)
            {
                return this._dataGridView.Invoke(() => AddRowInternal(model));
            }
            else
            {
                return AddRowInternal(model);
            }
        }

        public void UpdateRowStatus(int rowId, string status)
        {
            if (this._dataGridView.InvokeRequired)
            {
                this._dataGridView.Invoke(() => UpdateRowStatusInternal(rowId, status));
            }
            else
            {
                UpdateRowStatusInternal(rowId, status);
            }
        }

        public void ResolveRow(int rowId)
        {
            if (this._dataGridView.InvokeRequired)
            {
                this._dataGridView.Invoke(() => ResolveRowInternal(rowId));
            }
            else
            {
                ResolveRowInternal(rowId);
            }
        }

        public void DeclineRow(int rowId)
        {
            if (this._dataGridView.InvokeRequired)
            {
                this._dataGridView.Invoke(() => DeclineRowInternal(rowId));
            }
            else
            {
                DeclineRowInternal(rowId);
            }
        }

        private int AddRowInternal(MemeRedemptionGridRow model)
        {
            this._dataGridView.Rows.Insert(0, 1);
            var row = this._dataGridView.Rows[0];

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

        private void UpdateRowStatusInternal(int rowId, string status)
        {
            this._dataGridView.Rows[rowId].Cells["MemeRedemptionStatusColumn"].Value = status;
        }
        private void ResolveRowInternal(int rowId)
        {
            this._dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void DeclineRowInternal(int rowId)
        {
            this._dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightYellow;
        }
    }
}
