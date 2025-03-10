using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTL.Business.Utilities
{
    public abstract class ImportData
    {
        public abstract DataTable CreateDataTable();

        public abstract DataRow CreateDataRow(DataTable dataTable, string[] str);

        public abstract void ColumnMapping(SqlBulkCopy sqlBulkCopy);
        public abstract void BulkInsert(DataTable destinationTable);

    }
}
