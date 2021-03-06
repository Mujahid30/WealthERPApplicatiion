/****** Object:  Table [dbo].[XMLBankAccountType]    Script Date: 06/23/2009 19:02:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLBankAccountType]') AND type in (N'U'))
DROP TABLE [dbo].[XMLBankAccountType]
GO
/****** Object:  Table [dbo].[XMLBankAccountType]    Script Date: 06/23/2009 19:02:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XMLBankAccountType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[XMLBankAccountType](
	[XBAT_BankAccountTypeCode] [varchar](5) NOT NULL,
	[XBAT_BankAccountTye] [varchar](30) NULL,
	[XBAT_CreatedBy] [int] NULL,
	[XBAT_CreatedOn] [datetime] NULL,
	[XBAT_ModifiedBy] [int] NULL,
	[XBAT_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_XMLBankAccountType] PRIMARY KEY CLUSTERED 
(
	[XBAT_BankAccountTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
