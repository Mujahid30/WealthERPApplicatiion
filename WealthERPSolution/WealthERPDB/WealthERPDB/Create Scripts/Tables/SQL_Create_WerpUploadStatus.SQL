/****** Object:  Table [dbo].[WerpUploadStatus]    Script Date: 06/23/2009 19:02:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadStatus]') AND type in (N'U'))
DROP TABLE [dbo].[WerpUploadStatus]
GO
/****** Object:  Table [dbo].[WerpUploadStatus]    Script Date: 06/23/2009 19:02:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpUploadStatus](
	[WUS_UploadStatusCode] [varchar](5) NULL,
	[WUS_UploadStatus] [varchar](20) NULL,
	[WUS_CreatedBy] [int] NULL,
	[WUS_CreatedOn] [datetime] NULL,
	[WUS_ModifiedBy] [int] NULL,
	[WUS_ModifiedOn] [datetime] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
