/****** Object:  Table [dbo].[XMLAdviserLOBAssetGroup]    Script Date: 06/23/2009 19:02:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBAssetGroup]') AND type in (N'U'))
DROP TABLE [dbo].[XMLAdviserLOBAssetGroup]
GO
/****** Object:  Table [dbo].[XMLAdviserLOBAssetGroup]    Script Date: 06/23/2009 19:02:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBAssetGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLAdviserLOBAssetGroup](
	[XALAG_LOBAssetGroupsCode] [varchar](5) NOT NULL,
	[XALAG_LOBAssetGroup] [varchar](20) NULL,
	[XALAG_CreatedBy] [int] NULL,
	[XALAG_CreatedOn] [datetime] NULL,
	[XALAG_ModifiedBy] [int] NULL,
	[XALAG_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBAssetGroup] PRIMARY KEY CLUSTERED 
(
	[XALAG_LOBAssetGroupsCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
