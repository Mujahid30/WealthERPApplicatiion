USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[WerpExternalSourceHeaderMaster]    Script Date: 06/12/2009 18:23:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpExternalSourceHeaderMaster](
	[XESFT_FileTypeId] [int] NOT NULL,
	[WESHM_ExternalColumnName] [varchar](50) NULL,
	[WESHM_WerpNameOfExternalColumn] [varchar](50) NULL,
	[WESHM_CreatedBy] [int] NULL,
	[WESHM_CreatedOn] [datetime] NULL,
	[WESHM_ModifiedBy] [int] NULL,
	[WESHM_ModifedOn] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[WerpExternalSourceHeaderMaster]  WITH CHECK ADD  CONSTRAINT [FK_WerpExternalSourceHeaderMaster_XMLExternalFileType] FOREIGN KEY([XESFT_FileTypeId])
REFERENCES [dbo].[XMLExternalSourceFileType] ([XESFT_FileTypeId])
GO

ALTER TABLE [dbo].[WerpExternalSourceHeaderMaster] CHECK CONSTRAINT [FK_WerpExternalSourceHeaderMaster_XMLExternalFileType]
GO


