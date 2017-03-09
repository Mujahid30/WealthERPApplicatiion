using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace BoCommon
{
    public class GetDifferenceInTables
    {
        public DataTable Difference(DataTable First, DataTable Second)
        {

            //Create Empty Table

            DataTable table1 = new DataTable("Difference");


            //Must use a Dataset to make use of a DataRelation object

            DataSet ds1 = new DataSet();


            //Add tables

            //ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

            ds1.Tables.Add(First.Copy());
            ds1.Tables[0].TableName = "First";
            ds1.Tables.Add(Second.Copy());
            ds1.Tables[1].TableName = "Second";
            //Get Columns for DataRelation

            DataColumn[] firstcolumns = new DataColumn[ds1.Tables[0].Columns.Count];

            for (int i = 0; i < firstcolumns.Length; i++)
            {

                firstcolumns[i] = ds1.Tables[0].Columns[i];

            }



            DataColumn[] secondcolumns = new DataColumn[ds1.Tables[1].Columns.Count];

            for (int i = 0; i < secondcolumns.Length; i++)
            {

                secondcolumns[i] = ds1.Tables[1].Columns[i];

            }

            //Create DataRelation

            DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);

            ds1.Relations.Add(r);



            //Create columns for return table

            for (int i = 0; i < First.Columns.Count; i++)
            {

                table1.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);

            }



            //If First Row not in Second, Add to return table.

            table1.BeginLoadData();

            foreach (DataRow parentrow in ds1.Tables[0].Rows)
            {

                DataRow[] childrows = parentrow.GetChildRows(r);

                if (childrows == null || childrows.Length == 0)

                    table1.LoadDataRow(parentrow.ItemArray, true);

            }

            table1.EndLoadData();





            return table1;

        }
    }
}
