// Name & Student ID: Adian Dzananovic, 30067523
// Task: AT1 - MVP that manages sensors for Sensing4U
// Date: 07/11/2025

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sensing4UApp
{
    /// <summary>
    /// Represents the main user interface window for the Sensing4U application.
    /// Handles all user interactions like loading, saving, navigating datasets,
    /// setting threshold bounds, and searching datasets.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly DataProcessor _proc = DataProcessor.Instance;

        #region Constructor and Initialization
        /// <summary>
        /// Initializes the main window and configures event handlers,
        /// form size, title, and the legend in the status strip.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Text = "Sensing4U - Main Window";
            Width = 1000; Height = 650;
            _proc.FeedbackRaised += ShowFeedback; // Subscribe to feedback events
            // Colour legend to the StatusStrip
            var legendSpacer = new ToolStripStatusLabel(" | ")
            {
                Spring = false
            };
            var legendOk = new ToolStripStatusLabel(" OK ")
            {
                BackColor = Color.LightGreen,
                BorderSides = ToolStripStatusLabelBorderSides.All
            };
            var legendHigh = new ToolStripStatusLabel(" High ")
            {
                BackColor = Color.LightCoral,
                BorderSides = ToolStripStatusLabelBorderSides.All
            };
            var legendLow = new ToolStripStatusLabel(" Low ")
            {
                BackColor = Color.LightBlue,
                BorderSides = ToolStripStatusLabelBorderSides.All
            };
            var legendLabel = new ToolStripStatusLabel("Legend:");
            statusStripMain.Items.Add(legendLabel);
            statusStripMain.Items.Add(legendSpacer);
            statusStripMain.Items.Add(legendOk);
            statusStripMain.Items.Add(legendHigh);
            statusStripMain.Items.Add(legendLow);
            legendOk.Margin = new Padding(4, 2, 4, 2);
            legendHigh.Margin = new Padding(4, 2, 4, 2);
            legendLow.Margin = new Padding(4, 2, 4, 2);
        } 
        #endregion

        #region Menu, Bounds, Search & Navigation
        /// <summary>
        /// Handles the click event for the Load menu item.
        /// Opens a file dialog to load a binary dataset file.
        /// </summary>
        private void MenuItemLoad_Click(object sender, EventArgs e) => OnLoadClicked();
        /// <summary>
        /// Handles the click event for the Save menu item.
        /// Open a dialog to save the current dataset to a binary file.
        /// </summary>
        private void MenuItemSave_Click(object sender, EventArgs e) => OnSaveClicked();

        // Top controls

        /// <summary>
        /// Handles the click event for the Set Bounds button.
        /// Reads low/high inputs and applies the new thresholds.
        /// </summary>
        private void ButtonSetBounds_Click(object sender, EventArgs e) => OnBoundsChanged();
        /// <summary>
        /// Handles the click event for the Search button.
        /// Peforms a binary search by label in the current dataset.
        /// </summary>
        private void ButtonSearch_Click(object sender, EventArgs e) => OnSearchLabel();
        /// <summary>
        /// Handles the click event for the Previous Dataset button.
        /// </summary>
        private void ButtonPreviousDataset_Click(object sender, EventArgs e) => OnPrevClicked();
        /// <summary>
        /// Handles the click event for the Next Dataset button.
        /// </summary>
        private void ButtonNextDataset_Click(object sender, EventArgs e) => OnNextClicked();
        #endregion

        #region DataGridView Cell Formatting
        /// <summary>
        /// Colours each cell in the DataGridView based on the status tag (OK/High/Low).
        /// </summary>
        private void DataGridViewSensors_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var row = dataGridViewSensors.Rows[e.RowIndex];
            if (row == null) return;
            var cell = row.Cells[e.ColumnIndex];
            if (cell == null) return;
            if (e.CellStyle == null) return;
            if (cell.Tag is Status s)
            {
                if (s == Status.OK) e.CellStyle.BackColor = Color.LightGreen;
                else if (s == Status.High) e.CellStyle.BackColor = Color.LightCoral;
                else if (s == Status.Low) e.CellStyle.BackColor = Color.LightBlue;
            }
        }
        #endregion

        #region Private Methods for User Actions (UI)
        /// <summary>
        /// Loads a dataset from a binary file and updates the grid display.
        /// </summary>
        private void OnLoadClicked(string? path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                using var dlg = new OpenFileDialog { Filter = "Binary (*.bin)|*.bin|All Files (*.*)|*.*" };
                if (dlg.ShowDialog() != DialogResult.OK) return;
                path = dlg.FileName;
            }
            try { _proc.LoadDatasetFromBin(path); UpdateGrid(); ShowAverage(); }
            catch (Exception ex) { ShowFeedback("Load failed: " + ex.Message, true); }
        }

        /// <summary>
        /// Saves the currently active dataset to a binary file.
        /// </summary>
        private void OnSaveClicked(string? path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                using var dlg = new SaveFileDialog { Filter = "Binary (*.bin)|*.bin|All Files (*.*)|*.*" };
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                path = dlg.FileName;
            }
            try { _proc.SaveCurrentToBin(path); }
            catch (Exception ex) { ShowFeedback("Save failed: " + ex.Message, true); }
        }

        /// <summary>
        /// Applies new threshold bounds entered by the user.
        /// </summary>
        private void OnBoundsChanged(double? lo = null, double? hi = null)
        {
            if (!lo.HasValue || !hi.HasValue)
            {
                if (!double.TryParse(textBoxLow.Text, out var vlo) ||
                    !double.TryParse(textBoxHigh.Text, out var vhi))
                { ShowFeedback("Enter numeric Low/High bounds.", true); return; }
                lo = vlo; hi = vhi;
            }
            _proc.SetBounds(lo.Value, hi.Value);
            UpdateGrid(); ShowAverage();
        }

        /// <summary>
        /// Moves to the previous dataset if available and updates the grid display.
        /// </summary>
        private void OnPrevClicked() { _proc.PreviousDataset(); UpdateGrid(); ShowAverage(); }
        /// <summary>
        /// Moves to the next dataset if available and updates the grid display.
        /// </summary>
        private void OnNextClicked() { _proc.NextDataset(); UpdateGrid(); ShowAverage(); }

        /// <summary>
        /// Searches the current active dataset for a record by its label.
        /// Highlights the matching cell if found.
        /// </summary>
        #endregion

        #region Binary Search by Label
        private void OnSearchLabel(string? label = null)
        {
            // Ensure a dataset is loaded before searching
            if (_proc.Datasets.Count == 0 || _proc.CurrentDatasetIndex < 0)
            {
                ShowFeedback("No dataset loaded.", true);
                return;
            }

            try
            {
                // Error handling for empty search input
                label ??= textBoxSearch.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(label))
                {
                    ShowFeedback("Search label cannot be empty.", true);
                    return;
                }
                var (row, col) = _proc.FindByLabel(label);
                if (row >= 0 && col >= 0)
                {
                    // Highlight the found cell in the DataGridView
                    dataGridViewSensors.ClearSelection();
                    dataGridViewSensors.CurrentCell = dataGridViewSensors[col, row];
                    dataGridViewSensors[col, row].Selected = true;

                    if (row >= 0 && row < dataGridViewSensors.RowCount)
                        dataGridViewSensors.FirstDisplayedScrollingRowIndex = Math.Max(0, row - 3);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowFeedback(ex.Message, true);
            }
        }
        #endregion

        #region Private Helper Methods
        /// <summary>
        /// Populates the DataGridView with values and labels from the current dataset.
        /// Each cell includes a tooltip describing its status (OK/High/Low).
        /// </summary>
        private void UpdateGrid()
        {
            dataGridViewSensors.Columns.Clear();
            dataGridViewSensors.Rows.Clear();
            if (_proc.Datasets.Count == 0 || _proc.CurrentDatasetIndex < 0) return;
            var ds = _proc.Datasets[_proc.CurrentDatasetIndex];
            int rows = ds.GetLength(0), cols = ds.GetLength(1);
            // Create columns dynamically based on dataset dimensions
            for (int c = 0; c < cols; c++)
                dataGridViewSensors.Columns.Add("C" + c, "C" + c);
            dataGridViewSensors.Rows.Add(rows);

            // Populate each cell with value, label, and status tooltip
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    var rec = ds[r, c];
                    dataGridViewSensors[c,r].Value = $"{rec.Value:0.###} ({rec.Label})";
                    dataGridViewSensors[c,r].Tag = rec.CurrentStatus;
                    // Tooltip shows the textual status
                    dataGridViewSensors[c,r].ToolTipText = rec.CurrentStatus.ToString();
                }
        }

        /// <summary>
        /// Updates the average display label for the current dataset.
        /// </summary>
        private void ShowAverage()
        {
            try { labelAverage.Text = $"Average: {_proc.GetAverage():0.###}"; }
            catch { labelAverage.Text = "Average: -"; }
        }

        /// <summary>
        /// Displays feedback messages in the StatusStrip for 5 seconds,
        /// then changes to 'Ready'. Colour indicates success or error.
        /// </summary>
        private void ShowFeedback(string msg, bool isError)
        {
            toolStripStatusLabelMessage.Text = msg;
            toolStripStatusLabelMessage.ForeColor = isError ? Color.DarkRed : Color.DarkGreen;
        }
        #endregion
    }
}