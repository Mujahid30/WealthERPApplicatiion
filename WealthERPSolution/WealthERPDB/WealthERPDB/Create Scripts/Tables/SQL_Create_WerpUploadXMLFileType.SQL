/****** Object:  Table [dbo].[WerpUploadXMLFileType]    Script Date: 06/23/2009 19:02:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadXMLFileType]') AND type in (N'U'))
DROP TABLE [dbo].[WerpUploadXMLFileType]
GO
/****** Object:  Table [dbo].[WerpUploadXMLFileType]    Script Date: 06/23/2009 19:02:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpUploadXMLFileType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpUploadXMLFileType](
	[WUXFT_XMLFileTypeId] [int] IDENTITY(1,1) NOT NULL,
	[WUXFT_XMLFileName] [varchar](50) NULL,
	[WUXFT_CreatedBy] [int] NULL,
	[WUXFT_CreatedOn] [datetime] NULL,
	[WUXFT_ModifiedBy] [int] NULL,
	[WUXFT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpUploadXMLFileType] PRIMARY KEY CLUSTERED 
(
	[WUXFT_XMLFileTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
