using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;

namespace DataCompression
{
    public static class Blob
    {
        private static byte[] Compress(this byte[] buffer)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(buffer, 0, buffer.Length);
                zip.Close();
                ms.Position = 0;

                MemoryStream outStream = new MemoryStream();

                byte[] compressed = new byte[ms.Length];
                ms.Read(compressed, 0, compressed.Length);

                byte[] gzBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
                return gzBuffer;
            }
            catch
            {
                return new byte[0];
            }
        }

        private static byte[] Decompress(this byte[] gzBuffer)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
                zip.Read(buffer, 0, buffer.Length);

                return buffer;
            }
            catch
            {
                return new byte[0];
            }
        }

        public static DataSet ConvertToDataSet(byte[] byteArray)
        {
            var dataSet = new DataSet();

            var binary = byteArray.Decompress();

            if (binary.Length == 0) binary = byteArray;

            var byteIndex = 0;
            var sizeOfFileName = BitConverter.ToInt16(binary, byteIndex);
            byteIndex += sizeof(short);

            var fileName = Encoding.ASCII.GetString(binary, byteIndex, sizeOfFileName);
            byteIndex += fileName.Length;

            var fileTime = new DateTime(BitConverter.ToInt64(binary, byteIndex));
            byteIndex += sizeof(long);

            var columnNamesSize = BitConverter.ToInt16(binary, byteIndex);
            byteIndex += sizeof(short);

            var columnNameList = Encoding.ASCII.GetString(binary, byteIndex, columnNamesSize);
            byteIndex += columnNameList.Length;

            if (columnNameList.Replace("[", string.Empty).Replace("]", string.Empty).Replace("\"", string.Empty) is string columnNames && columnNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries) is string[] columnList)
            {
                var dataTable = new DataTable(fileName);

                const string Timestamp = "Timestamp";

                foreach (var column in columnList)
                {
                    var columnName = column.ToString();
                    var columnType = columnName == Timestamp ? typeof(DateTime) : typeof(float);
                    var dataColumn = dataTable.Columns.Add(columnName, columnType);
                    dataColumn.AllowDBNull = true;
                }

                do
                {
                    DataRow row = dataTable.NewRow();

                    foreach (var column in columnList)
                    {
                        var columnName = column.ToString();

                        if (columnName == Timestamp)
                        {
                            row[columnName] = fileTime.AddMilliseconds(BitConverter.ToDouble(binary, byteIndex));
                            byteIndex += sizeof(double);
                        }
                        else
                        {
                            var floatValue = BitConverter.ToSingle(binary, byteIndex);
                            if (float.IsNaN(floatValue)) row[columnName] = DBNull.Value;
                            else row[columnName] = floatValue;
                            byteIndex += sizeof(float);
                        }
                    }
                    dataTable.Rows.Add(row);

                } while (byteIndex < binary.Length);

                dataSet.Tables.Add(dataTable);
            }
            return dataSet;
        }

        public static byte[] ConvertToByteArray(DataSet dataSet)
        {
            var byteArray = new List<byte>();

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0] is DataTable dataTable)
            {
                var columnNameList = new List<string>();
                var fileName = string.Empty;
                var timestampColumnName = string.Empty;
                const string Timestamp = nameof(Timestamp);

                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if (dataColumn.DataType == typeof(string)) { fileName = dataColumn.ColumnName; }
                    else if (dataColumn.DataType == typeof(DateTime)) { columnNameList.Add(Timestamp); timestampColumnName = dataColumn.ColumnName; }
                    else { columnNameList.Add(dataColumn.ColumnName); }
                }

                const int TimestampAndAtLeastOneColumn = 2;

                if (columnNameList.Contains(Timestamp) && columnNameList.Count >= TimestampAndAtLeastOneColumn && dataTable.Rows.Count > 0 && dataTable.Rows[0] is DataRow dataRow)
                {
                    var fileTime = (DateTime)dataRow[timestampColumnName];
                    var columnNames = "[" + string.Join(",", columnNameList) + "]";

                    var header = new
                    {
                        SizeOfFileName = BitConverter.GetBytes(Convert.ToInt16(fileName.Length)),
                        FileName = Encoding.ASCII.GetBytes(fileName),
                        FileTime = BitConverter.GetBytes(fileTime.Ticks),
                        ColumnNamesSize = BitConverter.GetBytes(Convert.ToInt16(Encoding.ASCII.GetBytes(columnNames).Length)),
                        ColumnNames = Encoding.ASCII.GetBytes(columnNames)
                    };

                    byteArray.AddRange(header.SizeOfFileName);
                    byteArray.AddRange(header.FileName);
                    byteArray.AddRange(header.FileTime);
                    byteArray.AddRange(header.ColumnNamesSize);
                    byteArray.AddRange(header.ColumnNames);

                    var lastBinaryValueByColumn = new Dictionary<string, byte[]>();

                    foreach (DataRow row in dataTable.Rows) foreach (var columnName in columnNameList)
                        {
                            byte[] data;

                            if (columnName == Timestamp)
                            {
                                data = BitConverter.GetBytes(Convert.ToDouble(((DateTime)row[Timestamp] - fileTime).TotalMilliseconds));
                            }
                            else
                            {
                                data = row[columnName].GetType() == typeof(DBNull)
                                     ? lastBinaryValueByColumn.ContainsKey(columnName) ? lastBinaryValueByColumn[columnName] : BitConverter.GetBytes(float.MinValue)
                                     : BitConverter.GetBytes(Convert.ToSingle(row[columnName]));
                            }
                            byteArray.AddRange(data);
                            lastBinaryValueByColumn[columnName] = data;
                        }
                }
            }
            return byteArray.ToArray().Compress();
        }
    }
}
