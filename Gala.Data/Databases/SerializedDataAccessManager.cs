using System;
using System.IO;

namespace Gala.Data.Databases
{
    using Galatea;
    using Galatea.AI.Abstract;
    using Galatea.AI.Characterization;
    using Galatea.Runtime;

    internal class SerializedDataAccessManager : DataAccessManager
    {
        public SerializedDataAccessManager(string connectionString) : base(connectionString)
        {
        }

        public void RestoreBackup(string path)
        {
            FileInfo fi = new FileInfo(path);
            fi.CopyTo(this.ConnectionString, true);
        }

        protected internal override void InitializeMemoryBank()
        {
            try
            {
                //throw new Exception("fkoff");

                // Initialize Template Collections
                this.Add(new ColorTemplateCollection());
                this.Add(new ShapeTemplateCollection());
                this.Add(new SymbolTemplateCollection());
                this.Add(new NamedEntityCollection());

                #region // Read from File

                string nextLine;

                // Make a backup first
                FileInfo fi = new FileInfo(this.ConnectionString);
                fi.CopyTo(Path.Combine(fi.DirectoryName, fi.Name + ".backup"), true);

                // Read File from beginning
                StreamReader reader = new StreamReader(this.ConnectionString);
                TemplateType templateType = TemplateType.Null;

                nextLine = reader.ReadLine();

                // Initialize SerializationHelper
                SerializationHelper.Library = this;

                while (nextLine.Contains(SerializationHelper.TemplateTypeDelimiter))
                {
                    string tType = nextLine.Replace(SerializationHelper.TemplateTypeDelimiter, "").Replace(",", "");
                    templateType = SerializationHelper.ToTemplateType(tType);
                    nextLine = reader.ReadLine();

                    while (!nextLine.Contains(SerializationHelper.TemplateTypeDelimiter)
                        && !nextLine.Contains(SerializationHelper.EndOfFileDelimiter))
                    {
                        string[] templateData = nextLine.Split(':');
                        int id = int.Parse(templateData[0].Trim());
                        string name = templateData[1];
                        string friendlyName = templateData[2];
                        string serialData = templateData[3];
                        ICreator creator = null;

                        if (templateData.Length > 4 && templateData[4] != null)
                        {
                            // Initialize User or AI Creator from Memory
                            string[] creatorData = templateData[4].Split(';');
                            creator = Creators.Fetch(creatorData[0], creatorData[1]);
                        }

                        this[templateType].Add(id, name, friendlyName, serialData, creator);
                        nextLine = reader.ReadLine();

                        #region Other Tables

                        if (nextLine.Contains(SerializationHelper.FeedbackDelimiter))
                        {
                            nextLine = reader.ReadLine();
                            FeedbackCounterTable feedbackCounterTable = SerializationHelper.ToFeedbackCounterTable(nextLine);
                            SetFeedbackCounterTable(feedbackCounterTable);
                            nextLine = reader.ReadLine();
                        }

                        #endregion
                    }

                    if (nextLine.Contains(SerializationHelper.EndOfFileDelimiter))
                        break;
                }

                // Close the file
                reader.Close();

                #endregion

                if (this[TemplateType.Color].Count == 0)
                    throw new TeaInitializeDataException("No Color Templates initialized.");
                if (this[TemplateType.Shape].Count == 0)
                    throw new TeaInitializeDataException("No Shape Templates initialized.");
                if (this[TemplateType.Symbol].Count == 0)
                    throw new TeaInitializeDataException("No Symbol Templates initialized.");
                if (this[TemplateType.PatternEntity].Count == 0)
                    throw new TeaInitializeDataException("No Named Entities initialized.");
                if (this.FeedbackCounterTable.Count == 0)
                    throw new TeaInitializeDataException("No Feedback Counter Table ticks initialized.");
            }
            catch (Exception e)
            {
                if (!(e is TeaInitializeDataException))
                    throw;
            }
        }

        public override void SaveAll()
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(ConnectionString);

            foreach (IBaseTemplateCollection collection in this)
            {
                string collectionData = collection.SerializeAll();
                streamWriter.Write(collectionData);
            }

            // Save Feedback Counter table
            streamWriter.WriteLine(SerializationHelper.FeedbackDelimiter);
            string feedbackData = SerializationHelper.Serialize(FeedbackCounterTable);
            streamWriter.WriteLine(feedbackData);

            // Indicate EoF
            streamWriter.Write(SerializationHelper.EndOfFileDelimiter);

            // Finalize
            streamWriter.Close();
        }

        /*
        public bool RestoreV1 { get { return _restoreV1; } }

        private bool _restoreV1;
         */
    }
}
