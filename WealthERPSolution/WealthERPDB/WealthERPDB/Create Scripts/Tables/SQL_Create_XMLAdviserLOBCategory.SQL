/****** Object:  Table [dbo].[XMLAdviserLOBCategory]    Script Date: 06/23/2009 19:02:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBCategory]') AND type in (N'U'))
DROP TABLE [dbo].[XMLAdviserLOBCategory]
GO
/****** Object:  Table [dbo].[XMLAdviserLOBCategory]    Script Date: 06/23/2009 19:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLAdviserLOBCategory](
	[XALC_LOBCategoryCode] [varchar](5) NOT NULL,
	[XALC_LOBCategory] [varchar](30) NULL,
	[XALC_CreatedBy] [int] NULL,
	[XALC_CreatedOn] [datetime] NULL,
	[XALC_ModifiedBy] [int] NULL,
	[XALC_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLAdviserLOBCategory] PRIMARY KEY CLUSTERED 
(
	[XALC_LOBCategoryCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
