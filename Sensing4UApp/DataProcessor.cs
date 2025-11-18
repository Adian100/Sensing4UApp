// Name & Student ID: Adian Dzananovic, 30067523
// Task: AT1 - MVP that manages sensors for Sensing4U
// Date: 04/11/2025

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UApp
{
    /// <summary>
    /// Provides functionality for data loading, processing, and navigation 
    /// between the datasets. Also, it will implement a Singleton pattern
    /// for consistent access throughout the application.
    /// </summary>
    public sealed class DataProcessor
    {
        #region Singleton Implementation and Fields
        private static readonly Lazy<DataProcessor> _instance = new(() => new DataProcessor());
        /// <summary>
        /// This provides access to the global instance of the DataProcessor.
        /// </summary>
        public static DataProcessor Instance => _instance.Value;
        /// <summary>
        /// Gets the collection of datasets, where each dataset is represented
        /// as a 2D array of <see cref="SensorRecord"/> objects.
        /// </summary>
        public List<SensorRecord[,]> Datasets { get; } = [];
        /// <summary>
        /// Tracks which dataset is currently active for display or processing.
        /// </summary>
        public int CurrentDatasetIndex { get; set; } = -1;
        /// <summary>
        /// Lower threshold that's used for status evaluation.
        /// </summary>
        public double LowThreshold { get; set; } = 0.0;
        /// <summary>
        /// Upper threshold that's used for status evaluation.
        /// </summary>
        public double HighThreshold { get; set; } = 100.0;
        /// <summary>
        /// Event that is raised to provide feedback messages to the user interface.
        /// Bool parameter indicates if the message is an error.
        /// </summary>
        public event Action<string, bool>? FeedbackRaised;
        /// <summary>
        /// Map of labels to their positions in the current dataset.
        /// Built on demand and rebuilt when the dataset changes.
        /// </summary>
        private List<(string Label, int Row, int Col)>? _labelIndex;
        /// <summary>
        /// Private constructor to enforce Singleton pattern.
        /// </summary>
        private DataProcessor() { }
        #endregion

        #region Dataset Management
        /// <summary>
        /// Loads a dataset from a binary file and adds it to the internal list.
        /// </summary>
        /// <param name="path">The full file path to the binary file containing the dataset. Cannot be null or empty.</param>
        /// <returns>The number of records successfully loaded from the dataset.</returns>
        public int LoadDatasetFromBin(string path)
        {
            try
            {
                // Using FileManager to load the binary data
                SensorRecord[,] ds = FileManager.LoadBinary(path);

                if (ds == null)
                {
                    RaiseFeedback("Failed to load dataset (file empty or invalid).", true);
                    return 0;
                }

                Datasets.Add(ds);
                // Always switch to the newly loaded dataset.
                CurrentDatasetIndex = Datasets.Count - 1;

                // Evaluate status for the correct visualization
                RecalculateStatuses(ds);
                BuildLabelIndex();
                int totalRecords = ds.GetLength(0) * ds.GetLength(1);
                RaiseFeedback($"Dataset loaded successfully: {totalRecords} records.", false);
                return totalRecords;
            }
            catch (Exception ex) // Catch all exceptions to provide feedback
            {
                RaiseFeedback($"Error loading dataset: {ex.Message}", true);
                return 0;
            }
        }

        /// <summary>
        /// Saves the currently active dataset to a binary file.
        /// </summary>
        /// <param name="path">The file path where the binary data will be saved. The path must be a valid file path and cannot be null or
        /// empty.</param>
        public void SaveCurrentToBin(string path)
        {
            try
            {
                if (CurrentDatasetIndex < 0 || CurrentDatasetIndex >= Datasets.Count)
                {
                    RaiseFeedback("No dataset selected to save.", true);
                    return;
                }

                // Using FileManager to save the binary data
                var current = Datasets[CurrentDatasetIndex];
                FileManager.SaveBinary(path, current);
                RaiseFeedback("Dataset saved successfully.", false);
            }
            catch (Exception ex) // Catch all exceptions to provide feedback
            {
                RaiseFeedback($"Error saving dataset: {ex.Message}", true);
            }
        }
        #endregion

        #region Upper and Lower Bounds Management
        /// <summary>
        /// Updates the lower and upper bounds for acceptable sensor values.
        /// </summary>
        /// <param name="lo">The lower bound value. Must be less than or equal to <paramref name="hi"/>.</param>
        /// <param name="hi">The upper bound value. Must be greater than or equal to <paramref name="lo"/>.</param>
        public void SetBounds(double lo, double hi)
        {
            if (lo > hi)
            {
                RaiseFeedback("Lower bound cannot be greater than upper bound.", true);
                return;
            }

            LowThreshold = lo;
            HighThreshold = hi;

            // Re-evaluate statuses in the current dataset
            if (CurrentDatasetIndex >= 0 && CurrentDatasetIndex < Datasets.Count)
            {
                // Update statuses based on new thresholds
                RecalculateStatuses(Datasets[CurrentDatasetIndex]);
                RaiseFeedback($"Thresholds updated: Low = {lo}, High = {hi}", false);
            }
            else
            {
                RaiseFeedback("No dataset loaded. Bounds saved, load a dataset to apply them.", false);
            }
        }

        /// <summary>
        /// Evaluates a numeric value and returns its corresponding <see cref="Status"/>
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        public Status EvaluateStatus(double value)
        {
            // Compare the value against the thresholds
            if (value > HighThreshold)
                return Status.High;
            else if (value < LowThreshold)
                return Status.Low;
            else
                return Status.OK;
        }

        /// <summary>
        /// Recalculates the CurrentStatus for all records within a dataset.
        /// </summary>
        /// <param name="dataset">A 2D array of <see cref="SensorRecord"/> objects representing the dataset to process. Each
        /// record's status will be updated based on its value.</param>
        private void RecalculateStatuses(SensorRecord[,] dataset)
        {
            int rows = dataset.GetLength(0);
            int cols = dataset.GetLength(1);

            // Update each record's status based on the current thresholds
            for (int r = 0; r < rows; r++)
            {
                // Update each column in the current row
                for (int c = 0; c < cols; c++)
                {
                    // Evaluate and set the status
                    dataset[r, c].CurrentStatus = EvaluateStatus(dataset[r, c].Value);
                }
            }
        }
        #endregion

        #region Dataset Navigation and Processing
        /// <summary>
        /// Advances to the next dataset in the list.
        /// </summary>
        public void NextDataset()
        {
            if (Datasets.Count == 0) 
            {
                RaiseFeedback("No dataset loaded.", true);
                return;
            }
            // Only advance if not at the last dataset
            if (CurrentDatasetIndex < Datasets.Count - 1)
            {
                CurrentDatasetIndex++; // Move to the next dataset
                _labelIndex = null; // Invalidate the label index
                BuildLabelIndex(); // Rebuild the label index for the new dataset
                RaiseFeedback($"Switched to dataset #{CurrentDatasetIndex + 1}", false);
            }
            else
            {
                RaiseFeedback("Already at the last dataset.", false);
            }
        }

        /// <summary>
        /// Switches to the previous dataset in the list.
        /// </summary>
        public void PreviousDataset()
        {
            if (Datasets.Count == 0)
            {
                RaiseFeedback("No dataset loaded.", true);
                return;
            }
            // Only go back if not at the first dataset
            if (CurrentDatasetIndex > 0)
            {
                CurrentDatasetIndex--; // Move to the previous dataset
                _labelIndex = null; // Invalidate the label index
                BuildLabelIndex(); // Rebuild the label index for the new dataset
                RaiseFeedback($"Switched to dataset #{CurrentDatasetIndex + 1}", false);
            }
            else
            {
                RaiseFeedback("Already at the first dataset.", false);
            }
        }
        #endregion

        #region Data Retrieval and Calculation
        /// <summary>
        /// It will retrieve a specific SensorRecord from the current dataset.
        /// </summary>
        /// <param name="row">The row index of the targeted record.</param>
        /// <param name="col">The column index of the targeted record.</param>
        public SensorRecord GetRecord(int row, int col)
        {
            RequireCurrent();
            var ds = Datasets[CurrentDatasetIndex];
            return ds[row, col];
        }

        /// <summary>
        /// Returns a 2D array of only numeric values for display or calculations.
        /// </summary>
        public double[,] GetGridValues()
        {
            RequireCurrent();
            // Extract the numeric values from the current dataset
            var ds = Datasets[CurrentDatasetIndex];
            int rows = ds.GetLength(0);
            int cols = ds.GetLength(1);
            // Create a 2D array to hold the values
            double[,] values = new double[rows, cols];
            for (int r = 0; r < rows; r++) // Iterate through each row
            {
                for (int c = 0; c < cols; c++) // Iterate through each column
                {
                    values[r, c] = ds[r, c].Value; // Extract the numeric value
                }
            }
            return values; // Return the 2D array of values
        }

        /// <summary>
        /// Calculates the average Value across the current dataset.
        /// </summary>
        public double GetAverage()
        {
            RequireCurrent();
            var ds = Datasets[CurrentDatasetIndex]; // Get the current dataset
            double total = 0; // Accumulator for the sum of values
            int rows = ds.GetLength(0);
            int cols = ds.GetLength(1);

            // Sum all the values in the dataset
            for (int r = 0; r < rows; r++)
            {
                // Iterate through each column in the current row
                for (int c = 0; c < cols; c++)
                {
                    // Add the value to the total
                    total += ds[r, c].Value;
                }
            }
            // Calculate and return the average
            return rows * cols > 0 ? total / (rows * cols) : 0;
        }
        #endregion

        #region Sorting and Searching
        /// <summary>
        /// Sorts the current dataset by a given key (e.g., Label, Value, Id)
        /// </summary>
        /// <typeparam name="TKey">The type of the key to sort by.</typeparam>
        /// <param name="keySelector">A function that selects the key from a SensorRecord.</param>
        public void SortBy<TKey>(Func<SensorRecord, TKey> keySelector)
        {
            RequireCurrent();
            var ds = Datasets[CurrentDatasetIndex]; // Get the current dataset
            var flat = ds.Cast<SensorRecord>().OrderBy(keySelector).ToList(); // Flatten and sort
            int rows = ds.GetLength(0);
            int cols = ds.GetLength(1);
            int index = 0; // Index for the sorted list

            // Rebuild the 2D array from the sorted list
            for (int r = 0; r < rows; r++)
            {
                // Iterate through each column in the current row
                for (int c = 0; c < cols; c++)
                {
                    // Assign the sorted record back to the 2D array
                    ds[r, c] = flat[index++];
                }
            }

            RecalculateStatuses(ds); // Re-evaluate statuses after sorting
            BuildLabelIndex(); // Rebuild the label index
            RaiseFeedback("Dataset sorted successfully.", false);
        }

        /// <summary>
        /// Searches the current dataset by Label using custom binary search.
        /// Returns the (row, col) position of the first matching record, or (-1, -1) if it's not found.
        /// </summary>
        /// <param name="label">The label to search for.</param>
        public (int row, int col) FindByLabel(string label)
        {
            RequireCurrent();
            // Binary search requires a sorted label index
            // Rebuild if it's missing or empty
            if (_labelIndex is null || _labelIndex.Count == 0)
            {
                BuildLabelIndex();
            }

            int low = 0; // Start of the search range
            int high = _labelIndex!.Count - 1; // End of the search range
            while (low <= high)
            {
                // Calculate the mid index
                int mid = (low + high) / 2; 
                var midEntry = _labelIndex![mid];
                string midLabel = midEntry.Label;
                
                // Compare the search label with the current middle label
                int comparison = string.Compare(
                    label, midLabel, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0) 
                {
                    RaiseFeedback(
                        $"Found '{label}' at ({midEntry.Row}, {midEntry.Col}).",
                        false);
                    return (midEntry.Row, midEntry.Col);
                }
                else if (comparison < 0)
                {
                    // Search left half
                    high = mid - 1;
                }
                else
                {
                    // Search right half 
                    low = mid + 1;
                }
            }
            RaiseFeedback($"Label '{label}' not found.", true);
            return (-1, -1);
        }

        /// <summary>
        /// Builds or rebuilds the sorted label index for the current dataset.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if there is no current dataset loaded.
        /// </exception>
        private void BuildLabelIndex()
        {
            RequireCurrent();
            var ds = Datasets[CurrentDatasetIndex]; // Get the current dataset
            // Build the list of labels with their positions
            var list = new List<(string Label, int Row, int Col)>(ds.Length);
            // Loop through all rows and columns of the dataset
            for (int r = 0; r < ds.GetLength(0); r++)
            {
                for (int c = 0; c < ds.GetLength(1); c++)
                {
                    // Add the label and its position to the list
                    // Use empty string if label is null
                    list.Add((ds[r, c].Label ?? string.Empty, r, c));
                }
            }
            // Sort the list by label for binary search
            list.Sort((a, b) => string.Compare(a.Label, b.Label, StringComparison.OrdinalIgnoreCase));
            _labelIndex = list; // Save the sorted list as the active label index
        }
        #endregion

        #region Validation and Feedback
        /// <summary>
        /// This is to ensure there's a current dataset loaded before performing operations.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        private void RequireCurrent()
        {
            // Validate that a current dataset is selected
            if (CurrentDatasetIndex < 0 || CurrentDatasetIndex >= Datasets.Count)
                throw new InvalidOperationException("No dataset loaded."); // No dataset loaded
        }

        /// <summary>
        /// Triggers the feedback event safely for the UI.
        /// </summary>
        /// <param name="message">The feedback message to send.</param>
        /// <param name="isError">Indicates if the message is an error.</param>
        public void RaiseFeedback(string message, bool isError)
        {
            // Invoke the feedback event if there are subscribers
            FeedbackRaised?.Invoke(message, isError);
        }
        #endregion
    }
}