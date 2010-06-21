USE [wealthERPQA]
GO

/****** Object:  Table [dbo].[ProductAssetGroup]    Script Date: 06/12/2009 17:04:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProductAssetGroup](
	[PAG_AssetGroupCode] [varchar](2) NOT NULL,
	[PAG_AssetGroupName] [varchar](30) NULL,
	[PAG_CreatedBy] [int] NOT NULL,
	[PAG_CreatedOn] [datetime] NOT NULL,
	[PAG_ModifiedBy] [int] NOT NULL,
	[PAG_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductAssetGroup] PRIMARY KEY CLUSTERED 
(
	[PAG_AssetGroupCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


