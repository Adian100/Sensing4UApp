// Name & Student ID: Adian Dzananovic, 30067523
// Task: AT1 - MVP that manages sensors for Sensing4U
// Date: 03/11/2025

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UApp
{
    /// <summary>
    /// This static class will provide methods for managing
    /// file operations, including saving data in a binary format.
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Represents a constant string value used as an identifier within the application.
        /// </summary>
        private const string Magic = "S4U1";

        /// <summary>
        /// Saves the 2D array of sensor records to a binary file at the path.
        /// </summary>
        /// <param name="path">The file path where the binary data will be saved. Cannot be null or empty.</param>
        /// <param name="ds">A 2D array of <see cref="SensorRecord"/> objects to be saved. Cannot be null.</param>
        public static void SaveBinary(string path, SensorRecord[,] ds)
        {
            // Validate the parameters
            ArgumentNullException.ThrowIfNull(ds);

            int rows = ds.GetLength(0);
            int cols = ds.GetLength(1);

            using var fs = File.Create(path); // create or overwrite the file
            using var bw = new BinaryWriter(fs, Encoding.UTF8, leaveOpen: false);

            bw.Write(Magic.ToCharArray());
            bw.Write(rows);
            bw.Write(cols);

            // Write each record in row-major order
            for (int r = 0; r < rows; r++)
            {
                // Write each column in the current row
                for (int c = 0; c < cols; c++)
                {
                    var rec = ds[r, c];
                    bw.Write(rec.Id);
                    bw.Write(rec.Timestamp.ToBinary());
                    bw.Write(rec.Label);
                    bw.Write(rec.Value);
                    bw.Write((byte)rec.CurrentStatus);
                }
            }
        }

        /// <summary>
        /// Loads binary data from the file and initializes a 2D array of <see
        /// cref="SensorRecord"/>.
        /// </summary>
        /// <param name="path">Path to the binary file which needs to be loaded. The file needs to exist and be accessible.</param>
        /// <param name="ds">When this method returns, contains the 2D array of <see cref="SensorRecord"/> populated with
        /// the data from the binary file. This parameter is passed uninitialized.</param>
        public static SensorRecord[,] LoadBinary(string path)
        {
            using var fs = File.OpenRead(path);
            using var br = new BinaryReader(fs, Encoding.UTF8, leaveOpen: false);

            var magic = new string(br.ReadChars(4));
            if (magic != Magic)
            {
                throw new InvalidDataException("The file format is invalid.");
            }

            int rows = br.ReadInt32();
            int cols = br.ReadInt32();
            if (rows <= 0 || cols <= 0)
            {
                throw new InvalidDataException("The file contains invalid dimensions.");
            }

            var data = new SensorRecord[rows, cols];

            // Read each record in row-major order
            for (int r = 0; r < rows; r++)
            {
                // Read each column in the current row
                for (int c = 0; c < cols; c++)
                {
                    int id = br.ReadInt32();
                    long ticksUtc = br.ReadInt64();

                    int labelLen = br.ReadInt32();
                    if (labelLen < 0 || labelLen > 1_000_000) // Arbitrary limit to prevent abuse
                    {
                        throw new InvalidDataException("The file contains an invalid label length.");
                    }

                    byte[] labelBytes = br.ReadBytes(labelLen);
                    string label = Encoding.UTF8.GetString(labelBytes);

                    double value = br.ReadDouble();
                    var status = (Status)br.ReadByte(); // Cast back to Status enum

                    // Create and store the SensorRecord
                    data[r, c] = new SensorRecord
                    {
                        Id = id,
                        Timestamp = DateTime.FromBinary(ticksUtc),
                        Label = label,
                        Value = value,
                        CurrentStatus = status // Cast back to Status enum
                    };
                }
            }
            return data;
        }
    }
}