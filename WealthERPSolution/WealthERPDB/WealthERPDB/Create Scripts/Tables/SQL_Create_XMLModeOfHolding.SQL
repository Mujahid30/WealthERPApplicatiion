/****** Object:  Table [dbo].[XMLModeOfHolding]    Script Date: 06/23/2009 19:03:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLModeOfHolding]') AND type in (N'U'))
DROP TABLE [dbo].[XMLModeOfHolding]
GO
/****** Object:  Table [dbo].[XMLModeOfHolding]    Script Date: 06/23/2009 19:03:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLModeOfHolding]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLModeOfHolding](
	[XMOH_ModeOfHoldingCode] [varchar](5) NOT NULL,
	[XMOH_ModeOfHolding] [varchar](30) NULL,
	[XMOH_CreatedBy] [int] NULL,
	[XMOH_CreatedOn] [datetime] NULL,
	[XMOH_ModifiedBy] [int] NULL,
	[XMOH_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLModeOfHolding] PRIMARY KEY CLUSTERED 
(
	[XMOH_ModeOfHoldingCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
