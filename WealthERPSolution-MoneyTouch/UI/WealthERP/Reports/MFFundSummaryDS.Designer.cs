﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace WealthERP.Reports {
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("MFFundSummaryDS")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class MFFundSummaryDS : global::System.Data.DataSet {
        
        private MFFundSummaryDataTable tableMFFundSummary;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public MFFundSummaryDS() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected MFFundSummaryDS(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["MFFundSummary"] != null)) {
                    base.Tables.Add(new MFFundSummaryDataTable(ds.Tables["MFFundSummary"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public MFFundSummaryDataTable MFFundSummary {
            get {
                return this.tableMFFundSummary;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            MFFundSummaryDS cln = ((MFFundSummaryDS)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["MFFundSummary"] != null)) {
                    base.Tables.Add(new MFFundSummaryDataTable(ds.Tables["MFFundSummary"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableMFFundSummary = ((MFFundSummaryDataTable)(base.Tables["MFFundSummary"]));
            if ((initTable == true)) {
                if ((this.tableMFFundSummary != null)) {
                    this.tableMFFundSummary.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "MFFundSummaryDS";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/MFFundSummaryDS.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableMFFundSummary = new MFFundSummaryDataTable();
            base.Tables.Add(this.tableMFFundSummary);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeMFFundSummary() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            MFFundSummaryDS ds = new MFFundSummaryDS();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void MFFundSummaryRowChangeEventHandler(object sender, MFFundSummaryRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class MFFundSummaryDataTable : global::System.Data.TypedTableBase<MFFundSummaryRow> {
            
            private global::System.Data.DataColumn columnCategory;
            
            private global::System.Data.DataColumn columnPreviousValue;
            
            private global::System.Data.DataColumn columnCurrentValue;
            
            private global::System.Data.DataColumn columnCustomerName;
            
            private global::System.Data.DataColumn columnPortfolioName;
            
            private global::System.Data.DataColumn columnPortfolioId;
            
            private global::System.Data.DataColumn columnCustomerId;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryDataTable() {
                this.TableName = "MFFundSummary";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal MFFundSummaryDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected MFFundSummaryDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CategoryColumn {
                get {
                    return this.columnCategory;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn PreviousValueColumn {
                get {
                    return this.columnPreviousValue;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CurrentValueColumn {
                get {
                    return this.columnCurrentValue;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CustomerNameColumn {
                get {
                    return this.columnCustomerName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn PortfolioNameColumn {
                get {
                    return this.columnPortfolioName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn PortfolioIdColumn {
                get {
                    return this.columnPortfolioId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn CustomerIdColumn {
                get {
                    return this.columnCustomerId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryRow this[int index] {
                get {
                    return ((MFFundSummaryRow)(this.Rows[index]));
                }
            }
            
            public event MFFundSummaryRowChangeEventHandler MFFundSummaryRowChanging;
            
            public event MFFundSummaryRowChangeEventHandler MFFundSummaryRowChanged;
            
            public event MFFundSummaryRowChangeEventHandler MFFundSummaryRowDeleting;
            
            public event MFFundSummaryRowChangeEventHandler MFFundSummaryRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddMFFundSummaryRow(MFFundSummaryRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryRow AddMFFundSummaryRow(string Category, decimal PreviousValue, long CurrentValue, string CustomerName, string PortfolioName, string PortfolioId, string CustomerId) {
                MFFundSummaryRow rowMFFundSummaryRow = ((MFFundSummaryRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        Category,
                        PreviousValue,
                        CurrentValue,
                        CustomerName,
                        PortfolioName,
                        PortfolioId,
                        CustomerId};
                rowMFFundSummaryRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowMFFundSummaryRow);
                return rowMFFundSummaryRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                MFFundSummaryDataTable cln = ((MFFundSummaryDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new MFFundSummaryDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnCategory = base.Columns["Category"];
                this.columnPreviousValue = base.Columns["PreviousValue"];
                this.columnCurrentValue = base.Columns["CurrentValue"];
                this.columnCustomerName = base.Columns["CustomerName"];
                this.columnPortfolioName = base.Columns["PortfolioName"];
                this.columnPortfolioId = base.Columns["PortfolioId"];
                this.columnCustomerId = base.Columns["CustomerId"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnCategory = new global::System.Data.DataColumn("Category", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCategory);
                this.columnPreviousValue = new global::System.Data.DataColumn("PreviousValue", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnPreviousValue);
                this.columnCurrentValue = new global::System.Data.DataColumn("CurrentValue", typeof(long), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCurrentValue);
                this.columnCustomerName = new global::System.Data.DataColumn("CustomerName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCustomerName);
                this.columnPortfolioName = new global::System.Data.DataColumn("PortfolioName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnPortfolioName);
                this.columnPortfolioId = new global::System.Data.DataColumn("PortfolioId", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnPortfolioId);
                this.columnCustomerId = new global::System.Data.DataColumn("CustomerId", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCustomerId);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryRow NewMFFundSummaryRow() {
                return ((MFFundSummaryRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new MFFundSummaryRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(MFFundSummaryRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.MFFundSummaryRowChanged != null)) {
                    this.MFFundSummaryRowChanged(this, new MFFundSummaryRowChangeEvent(((MFFundSummaryRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.MFFundSummaryRowChanging != null)) {
                    this.MFFundSummaryRowChanging(this, new MFFundSummaryRowChangeEvent(((MFFundSummaryRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.MFFundSummaryRowDeleted != null)) {
                    this.MFFundSummaryRowDeleted(this, new MFFundSummaryRowChangeEvent(((MFFundSummaryRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.MFFundSummaryRowDeleting != null)) {
                    this.MFFundSummaryRowDeleting(this, new MFFundSummaryRowChangeEvent(((MFFundSummaryRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveMFFundSummaryRow(MFFundSummaryRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                MFFundSummaryDS ds = new MFFundSummaryDS();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "MFFundSummaryDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class MFFundSummaryRow : global::System.Data.DataRow {
            
            private MFFundSummaryDataTable tableMFFundSummary;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal MFFundSummaryRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableMFFundSummary = ((MFFundSummaryDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Category {
                get {
                    try {
                        return ((string)(this[this.tableMFFundSummary.CategoryColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Category\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.CategoryColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public decimal PreviousValue {
                get {
                    try {
                        return ((decimal)(this[this.tableMFFundSummary.PreviousValueColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'PreviousValue\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.PreviousValueColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public long CurrentValue {
                get {
                    try {
                        return ((long)(this[this.tableMFFundSummary.CurrentValueColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CurrentValue\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.CurrentValueColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CustomerName {
                get {
                    try {
                        return ((string)(this[this.tableMFFundSummary.CustomerNameColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CustomerName\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.CustomerNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PortfolioName {
                get {
                    try {
                        return ((string)(this[this.tableMFFundSummary.PortfolioNameColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'PortfolioName\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.PortfolioNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string PortfolioId {
                get {
                    try {
                        return ((string)(this[this.tableMFFundSummary.PortfolioIdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'PortfolioId\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.PortfolioIdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CustomerId {
                get {
                    try {
                        return ((string)(this[this.tableMFFundSummary.CustomerIdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CustomerId\' in table \'MFFundSummary\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableMFFundSummary.CustomerIdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCategoryNull() {
                return this.IsNull(this.tableMFFundSummary.CategoryColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCategoryNull() {
                this[this.tableMFFundSummary.CategoryColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPreviousValueNull() {
                return this.IsNull(this.tableMFFundSummary.PreviousValueColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPreviousValueNull() {
                this[this.tableMFFundSummary.PreviousValueColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCurrentValueNull() {
                return this.IsNull(this.tableMFFundSummary.CurrentValueColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCurrentValueNull() {
                this[this.tableMFFundSummary.CurrentValueColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCustomerNameNull() {
                return this.IsNull(this.tableMFFundSummary.CustomerNameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCustomerNameNull() {
                this[this.tableMFFundSummary.CustomerNameColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPortfolioNameNull() {
                return this.IsNull(this.tableMFFundSummary.PortfolioNameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPortfolioNameNull() {
                this[this.tableMFFundSummary.PortfolioNameColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsPortfolioIdNull() {
                return this.IsNull(this.tableMFFundSummary.PortfolioIdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetPortfolioIdNull() {
                this[this.tableMFFundSummary.PortfolioIdColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCustomerIdNull() {
                return this.IsNull(this.tableMFFundSummary.CustomerIdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCustomerIdNull() {
                this[this.tableMFFundSummary.CustomerIdColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class MFFundSummaryRowChangeEvent : global::System.EventArgs {
            
            private MFFundSummaryRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryRowChangeEvent(MFFundSummaryRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public MFFundSummaryRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591