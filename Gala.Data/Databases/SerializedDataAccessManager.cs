﻿using System;
using System.IO;
using System.Reflection;
#if NETFX_CORE
using System.Threading.Tasks;
//using Gala.Data.Configuration;
#endif
using Gala.Data.Runtime;

namespace Gala.Data.Databases
{
    using Galatea;
    using Galatea.AI.Abstract;
    using Galatea.AI.Characterization;
    using Galatea.Runtime;

    internal class SerializedDataAccessManager : DataAccessManager
    {
/*
#if NETFX_CORE
        private static readonly Assembly serializedDataAsm = typeof(SerializedDataAccessManager).GetTypeInfo().Assembly;
#endif
 */
        public SerializedDataAccessManager(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Restores the file data specified by <paramref name="path"/> to the data file 
        /// specified by <see cref="DataAccessManager.ConnectionString"/>.
        /// </summary>
        public void RestoreBackup(string path)
        {
            FileInfo fi = new FileInfo(path);
            fi.CopyTo(this.ConnectionString, true);
        }

        public override void Initialize(IEngine engine)
        {
            base.Initialize(engine);
        }

        protected internal override void InitializeMemoryBank()
        {
            try
            {
                base.InitializeMemoryBank();

                StreamReader reader;
                string nextLine;

                #region // Get Stream from File

                // Make a backup first
                FileInfo fi = new FileInfo(this.ConnectionString);
                fi.CopyTo(Path.Combine(fi.DirectoryName, fi.Name + ".backup"), true);

                // Read File from beginning
                reader = new StreamReader(this.ConnectionString);

                #endregion

                #region // Read File Stream
                TemplateType templateType = TemplateType.Null;

                using (reader)
                {
                    nextLine = reader.ReadLine();

                    // Initialize SerializationHelper
                    SerializationHelper.Library = this;
                    FeedbackCounterTable feedbackCounterTable = null;

                    while (nextLine.Contains(SerializationHelper.TemplateTypeDelimiter))
                    {
                        string tType = nextLine.Replace(SerializationHelper.TemplateTypeDelimiter, "").Replace(",", "");
                        templateType = SerializationHelper.ToTemplateType(tType);
                        nextLine = reader.ReadLine();

                        while (!nextLine.Contains(SerializationHelper.TemplateTypeDelimiter)
                            && !nextLine.Contains(SerializationHelper.EndOfFileDelimiter))
                        {
                            string[] templateData = nextLine.Split(':');
                            int id = int.Parse(templateData[0].Trim(), System.Globalization.CultureInfo.CurrentCulture);
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

                            try
                            {
                                this[templateType].Add(id, name, friendlyName, serialData, creator);
                            }
                            catch (TeaException ex)
                            {
                                this.Engine.Debugger.HandleTeaException(ex, this);
                            }
                            catch (System.Exception ex)
                            {
                                this.Engine.Debugger.ThrowSystemException(ex, this);
                            }

                            nextLine = reader.ReadLine();

                            #region Other Tables

                            if (nextLine.Contains(SerializationHelper.FeedbackDelimiter))
                            {
                                nextLine = reader.ReadLine();
                                feedbackCounterTable = SerializationHelper.ToFeedbackCounterTable(nextLine);
                                nextLine = reader.ReadLine();
                            }

                            #endregion
                        }

                        if (nextLine.Contains(SerializationHelper.EndOfFileDelimiter))
                            break;
                    }

                    if (feedbackCounterTable == null)
                    {
                        feedbackCounterTable = new FeedbackCounterTable();
                    }

                    SetFeedbackCounterTable(feedbackCounterTable);

                    //// Close the file
                    //reader.Close();
                }
                #endregion

                if (this[TemplateType.Color].Count == 0)
                    throw new TeaInitializeDataException("No Color Templates initialized.");
                if (this[TemplateType.Shape].Count == 0)
                    throw new TeaInitializeDataException("No Shape Templates initialized.");
                if (this[TemplateType.Symbol].Count == 0)
                    throw new TeaInitializeDataException("No Symbol Templates initialized.");

                //if (this[TemplateType.PatternEntity].Count == 0)
                //    throw new TeaInitializeDataException("No Named Entities initialized.");
                //if (this.FeedbackCounterTable.Count == 0)
                //    throw new TeaInitializeDataException("No Feedback Counter Table ticks initialized.");

                if(!this.Contains(TemplateType.PatternEntity))
                    throw new TeaInitializeDataException("Named Entities table was not initialized.");
                if (this.FeedbackCounterTable == null)
                    throw new TeaInitializeDataException("Feedback Counter table was not initialized.");

                this.IsInitialized = true;
            }
            catch (TeaException ex)
            {
                this.Engine.Debugger.HandleTeaException(ex, this);
            }
            /*
            catch (System.Exception ex)
            {
                this.Engine.Debugger.ThrowSystemException(ex, this);
            }
             */
        }
        protected internal override bool IsInitialized
        {
            get => _isInitialized;
            set => _isInitialized = value;
        }

        public override void SaveAll()
        {
            using (System.IO.StreamWriter streamWriter = new StreamWriter(ConnectionString))
            {
                foreach (IBaseTemplateCollection collection in this)
                {
                    string collectionData = collection.SerializeAll();
                    streamWriter.Write(collectionData);
                }

                // Save Feedback Counter table
                if (FeedbackCounterTable != null)
                {
                    streamWriter.WriteLine(SerializationHelper.FeedbackDelimiter);
                    string feedbackData = SerializationHelper.Serialize(FeedbackCounterTable);
                    streamWriter.WriteLine(feedbackData);
                }

                // Indicate EoF
                streamWriter.Write(SerializationHelper.EndOfFileDelimiter);

                //// Finalize
                //streamWriter.Close();
            }
        }

        private bool _isInitialized;
    }
}
