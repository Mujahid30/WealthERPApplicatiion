/****** Object:  Table [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]') AND type in (N'U'))
DROP TABLE [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]
GO
/****** Object:  Table [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]    Script Date: 06/23/2009 19:01:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WerpKarvyBankAccountTypeDataTranslatorMapping](
	[WKBATDTM_AccountType] [varchar](50) NULL,
	[XBAT_BankAccountTypeCode] [varchar](5) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
