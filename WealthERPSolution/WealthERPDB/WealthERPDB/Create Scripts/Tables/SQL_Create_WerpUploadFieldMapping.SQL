/****** Object:  Table [dbo].[WerpUploadFieldMapping]    Script Date: 06/23/2009 19:02:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadFieldMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpUploadFieldMapping]
GO
/****** Object:  Table [dbo].[WerpUploadFieldMapping]    Script Date: 06/23/2009 19:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadFieldMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpUploadFieldMapping](
	[WUFM_WearpNameForExternalColumn] [varchar](50) NULL,
	[WUFM_WerpTable] [varchar](100) NULL,
	[WUFM_WerpTableColumn] [varchar](100) NULL,
	[WUFM_IsMandatoryForTransaction] [tinyint] NULL,
	[WUFM_IsManadatoryforProfile] [tinyint] NULL,
	[WUFM_IsMandatoryForCombination] [tinyint] NULL,
	[WUFM_CreatedBy] [int] NULL,
	[WUFM_CreatedOn] [datetime] NULL,
	[WUFM_ModifiedOn] [datetime] NULL,
	[WUFM_ModifiedBy] [int] NULL,
	[WUXFT_XMLFileTypeId] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpUploadFieldMapping_WerpUploadXMLFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpUploadFieldMapping]'))
ALTER TABLE [dbo].[WerpUploadFieldMapping]  WITH CHECK ADD  CONSTRAINT [FK_WerpUploadFieldMapping_WerpUploadXMLFileType] FOREIGN KEY([WUXFT_XMLFileTypeId])
REFERENCES [dbo].[WerpUploadXMLFileType] ([WUXFT_XMLFileTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WerpUploadFieldMapping_WerpUploadXMLFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[WerpUploadFieldMapping]'))
ALTER TABLE [dbo].[WerpUploadFieldMapping] CHECK CONSTRAINT [FK_WerpUploadFieldMapping_WerpUploadXMLFileType]
GO
