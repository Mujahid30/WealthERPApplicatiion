/****** Object:  Table [dbo].[XMLExternalSourceFileType]    Script Date: 06/23/2009 19:03:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLExternalSourceFileType]
GO
/****** Object:  Table [dbo].[XMLExternalSourceFileType]    Script Date: 06/23/2009 19:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLExternalSourceFileType](
	[XESFT_FileTypeId] [int] IDENTITY(1,1) NOT NULL,
	[XESFT_AssetGroup] [varchar](5) NULL,
	[XESFT_FileType] [varchar](25) NULL,
	[XESFT_FileExtension] [varchar](20) NULL,
	[XES_SourceCode] [varchar](5) NULL,
	[WUXFT_XMLFileTypeId] [int] NULL,
	[XESFT_CreatedBy] [int] NULL,
	[XESFT_CreatedOn] [datetime] NULL,
	[XESFT_ModifiedBy] [int] NULL,
	[XESFT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadProcesses] PRIMARY KEY CLUSTERED 
(
	[XESFT_FileTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_WerpUploadXMLFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_WerpUploadXMLFileType] FOREIGN KEY([WUXFT_XMLFileTypeId])
REFERENCES [dbo].[WerpUploadXMLFileType] ([WUXFT_XMLFileTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_WerpUploadXMLFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_WerpUploadXMLFileType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_XMLExternalSource]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSource] FOREIGN KEY([XES_SourceCode])
REFERENCES [dbo].[XMLExternalSource] ([XES_SourceCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_XMLExternalSource]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSource]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_XMLExternalSourceFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType]  WITH CHECK ADD  CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSourceFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_XMLExternalSourceFileType_XMLExternalSourceFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[XMLExternalSourceFileType]'))
ALTER TABLE [dbo].[XMLExternalSourceFileType] CHECK CONSTRAINT [FK_XMLExternalSourceFileType_XMLExternalSourceFileType]
GO
