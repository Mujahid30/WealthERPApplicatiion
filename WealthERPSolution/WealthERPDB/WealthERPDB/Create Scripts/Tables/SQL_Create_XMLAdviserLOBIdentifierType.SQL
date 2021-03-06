/****** Object:  Table [dbo].[XMLAdviserLOBIdentifierType]    Script Date: 06/23/2009 19:02:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBIdentifierType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLAdviserLOBIdentifierType]
GO
/****** Object:  Table [dbo].[XMLAdviserLOBIdentifierType]    Script Date: 06/23/2009 19:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLAdviserLOBIdentifierType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLAdviserLOBIdentifierType](
	[XALIT_IdentifierTypeCode] [varchar](5) NOT NULL,
	[XALIT_IdentifierTypeName] [varchar](30) NULL,
	[XALIT_CreatedBy] [int] NULL,
	[XALIT_CreatedOn] [datetime] NULL,
	[XALIT_ModifiedOn] [datetime] NULL,
	[XALIT_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_XMLAdviserLOBIdentifierType] PRIMARY KEY CLUSTERED 
(
	[XALIT_IdentifierTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
