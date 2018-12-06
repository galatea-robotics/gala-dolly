using System.IO;
using Galatea.AI.Abstract;
using Galatea.Core;
using Galatea.Data;

namespace Gala.Data.Mdb
{
    internal class MdbDataAccessManager : DataAccessManager
    {
        public MdbDataAccessManager(string mdbConnectionString) : base(mdbConnectionString)
        {
            mdbTemplateTableAdapter = new MdbDataSetTableAdapters.TemplateTableAdapter();
            mdbTemplateTableAdapter.Connection = new System.Data.OleDb.OleDbConnection(mdbConnectionString);
            Components.Add(mdbTemplateTableAdapter);
        }

        #region Utilities

        public override void LogResponse(string responder, string response)
        {
            throw new System.NotImplementedException();
        }
        public override void LogContext(Stream data)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        #region Library

        public override ColorTemplateCollection GetColorTemplates()
        {
            ColorTemplateCollection result = new ColorTemplateCollection();

            // Get Data
            MdbDataSet.TemplateDataTable templateTable = 
                mdbTemplateTableAdapter.GetByTemplateType((short)TemplateType.Color);

            foreach (MdbDataSet.TemplateRow row in templateTable.Rows)
            {
                result.Add(new ColorTemplate(row.ID, row.Name, row.Name, row.MetaData));
            }

            return result;
        }

        #endregion
        #region Data Access

        //private static void FillDataRow(MdbDataSet.TemplateRow row, ITemplate template)
        //{
        //    template.Id = row.ID;
        //    template.Name = row.Name;
        //    template.FriendlyName = row.FriendlyName;
        //    template.UnpackMetadata(row.MetaData);
        //    template.TemplateType = (TemplateType)row.TemplateTypeID;
        //}

        #endregion

        private Gala.Data.Mdb.MdbDataSetTableAdapters.TemplateTableAdapter mdbTemplateTableAdapter;
    }
}
