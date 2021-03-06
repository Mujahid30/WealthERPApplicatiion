/****** Object:  Table [dbo].[XMLDebtIssuer]    Script Date: 06/23/2009 19:02:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLDebtIssuer]') AND type in (N'U'))
DROP TABLE [dbo].[XMLDebtIssuer]
GO
/****** Object:  Table [dbo].[XMLDebtIssuer]    Script Date: 06/23/2009 19:02:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLDebtIssuer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLDebtIssuer](
	[XDI_DebtIssuerCode] [varchar](5) NOT NULL,
	[XDI_DebtIssuerName] [varchar](30) NULL,
	[XDI_CreatedBy] [int] NULL,
	[XDI_CreatedOn] [datetime] NULL,
	[XDI_ModifiedBy] [int] NULL,
	[XDI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpDebtIssuer_XML] PRIMARY KEY CLUSTERED 
(
	[XDI_DebtIssuerCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
