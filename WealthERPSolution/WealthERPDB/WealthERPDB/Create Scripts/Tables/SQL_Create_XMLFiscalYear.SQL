/****** Object:  Table [dbo].[XMLFiscalYear]    Script Date: 06/23/2009 19:03:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLFiscalYear]') AND type in (N'U'))
DROP TABLE [dbo].[XMLFiscalYear]
GO
/****** Object:  Table [dbo].[XMLFiscalYear]    Script Date: 06/23/2009 19:03:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLFiscalYear]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLFiscalYear](
	[XFY_FiscalYearCode] [varchar](5) NOT NULL,
	[XFY_FiscalYear] [varchar](20) NULL,
	[XFY_CreatedBy] [int] NULL,
	[XFY_CreatedOn] [datetime] NULL,
	[XFY_ModifiedOn] [datetime] NULL,
	[XFY_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_WerpFiscalYear_XML] PRIMARY KEY CLUSTERED 
(
	[XFY_FiscalYearCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
