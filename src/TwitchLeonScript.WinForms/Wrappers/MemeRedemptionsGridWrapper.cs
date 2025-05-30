using TwitchLeonScript.WinForms.Models;

namespace TwitchLeonScript.WinForms.Wrappers
{
    internal sealed class MemeRedemptionsGridWrapper(DataGridView dataGridView)
    {
        public void ClearRows()
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(dataGridView.Rows.Clear);
            }
            else
            {
                dataGridView.Rows.Clear();
            }
        }

        public int AddNewRow(MemeRedemptionGridRow model)
        {
            if (dataGridView.InvokeRequired)
            {
                return dataGridView.Invoke(() => AddRowInternal(model));
            }
            else
            {
                return AddRowInternal(model);
            }
        }

        public void UpdateRowStatus(int rowId, string status)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(() => UpdateRowStatusInternal(rowId, status));
            }
            else
            {
                UpdateRowStatusInternal(rowId, status);
            }
        }

        public void ResolveRow(int rowId)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(() => ResolveRowInternal(rowId));
            }
            else
            {
                ResolveRowInternal(rowId);
            }
        }

        public void DeclineRow(int rowId)
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(() => DeclineRowInternal(rowId));
            }
            else
            {
                DeclineRowInternal(rowId);
            }
        }

        public int FindRowIndexByRedemptionId(string redemptionId)
        {
            if (dataGridView.InvokeRequired)
            {
                return (int)dataGridView.Invoke(() => FindRowIndexInternal(redemptionId));
            }
            else
            {
                return FindRowIndexInternal(redemptionId);
            }
        }

        private int AddRowInternal(MemeRedemptionGridRow model)
        {
            dataGridView.Rows.Insert(0, 1);
            var row = dataGridView.Rows[0];

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
            dataGridView.Rows[rowId].Cells["MemeRedemptionStatusColumn"].Value = status;
        }

        private void ResolveRowInternal(int rowId)
        {
            dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void DeclineRowInternal(int rowId)
        {
            dataGridView.Rows[rowId].DefaultCellStyle.BackColor = Color.LightYellow;
        }

        private int FindRowIndexInternal(string redemptionId)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (string.Equals(row.Cells["MemeRedemptionIdColumn"].Value?.ToString(), redemptionId))
                {
                    return row.Index;
                }
            }

            return -1;
        }
    }
}
