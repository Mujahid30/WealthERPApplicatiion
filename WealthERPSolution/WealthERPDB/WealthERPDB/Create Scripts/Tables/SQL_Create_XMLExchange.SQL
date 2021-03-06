/****** Object:  Table [dbo].[XMLExchange]    Script Date: 06/23/2009 19:03:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExchange]') AND type in (N'U'))
DROP TABLE [dbo].[XMLExchange]
GO
/****** Object:  Table [dbo].[XMLExchange]    Script Date: 06/23/2009 19:03:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLExchange]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLExchange](
	[XE_ExchangeCode] [varchar](5) NOT NULL,
	[XE_ExchangeName] [varchar](10) NULL,
	[XE_CreatedBy] [int] NULL,
	[XE_CreatedOn] [datetime] NULL,
	[XE_ModifiedBy] [int] NULL,
	[XE_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLExchange] PRIMARY KEY CLUSTERED 
(
	[XE_ExchangeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
