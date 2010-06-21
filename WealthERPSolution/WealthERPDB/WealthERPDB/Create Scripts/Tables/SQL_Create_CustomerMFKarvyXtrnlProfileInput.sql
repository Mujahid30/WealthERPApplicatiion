
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfileInput]    Script Date: 06/11/2009 15:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfileInput](
	[CMFKXPI_Id] [int] IDENTITY(1,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CMFKXPI_ProductCode] [varchar](150) NULL,
	[CMFKXPI_Fund] [varchar](150) NULL,
	[CMFKXPI_Folio] [varchar](150) NULL,
	[CMFKXPI_DividendOption] [varchar](150) NULL,
	[CMFKXPI_FundDescription] [varchar](150) NULL,
	[CMFKXPI_InvestorName] [varchar](150) NULL,
	[CMFKXPI_JointName1] [varchar](150) NULL,
	[CMFKXPI_JointName2] [varchar](150) NULL,
	[CMFKXPI_Address#1] [varchar](150) NULL,
	[CMFKXPI_Address#2] [varchar](150) NULL,
	[CMFKXPI_Address#3] [varchar](150) NULL,
	[CMFKXPI_City] [varchar](150) NULL,
	[CMFKXPI_Pincode] [varchar](150) NULL,
	[CMFKXPI_State] [varchar](150) NULL,
	[CMFKXPI_Country] [varchar](150) NULL,
	[CMFKXPI_TPIN] [varchar](150) NULL,
	[CMFKXPI_DateofBirth] [varchar](150) NULL,
	[CMFKXPI_FName] [varchar](150) NULL,
	[CMFKXPI_MName] [varchar](150) NULL,
	[CMFKXPI_PhoneResidence] [varchar](150) NULL,
	[CMFKXPI_PhoneRes#1] [varchar](150) NULL,
	[CMFKXPI_PhoneRes#2] [varchar](150) NULL,
	[CMFKXPI_PhoneOffice] [varchar](150) NULL,
	[CMFKXPI_PhoneOff#1] [varchar](150) NULL,
	[CMFKXPI_PhoneOff#2] [varchar](150) NULL,
	[CMFKXPI_FaxResidence] [varchar](150) NULL,
	[CMFKXPI_FaxOffice] [varchar](150) NULL,
	[CMFKXPI_TaxStatus] [varchar](150) NULL,
	[CMFKXPI_OccCode] [varchar](150) NULL,
	[CMFKXPI_Email] [varchar](150) NULL,
	[CMFKXPI_BankAccno] [varchar](150) NULL,
	[CMFKXPI_BankName] [varchar](150) NULL,
	[CMFKXPI_AccountType] [varchar](150) NULL,
	[CMFKXPI_Branch] [varchar](150) NULL,
	[CMFKXPI_BankAddress#1] [varchar](150) NULL,
	[CMFKXPI_BankAddress#2] [varchar](150) NULL,
	[CMFKXPI_BankAddress#3] [varchar](150) NULL,
	[CMFKXPI_BankCity] [varchar](150) NULL,
	[CMFKXPI_BankPhone] [varchar](150) NULL,
	[CMFKXPI_BankState] [varchar](150) NULL,
	[CMFKXPI_BankCountry] [varchar](150) NULL,
	[CMFKXPI_InvestorID] [varchar](150) NULL,
	[CMFKXPI_BrokerCode] [varchar](150) NULL,
	[CMFKXPI_PANNumber] [varchar](150) NULL,
	[CMFKXPI_Mobile] [varchar](150) NULL,
	[CMFKXPI_ReportDate] [varchar](150) NULL,
	[CMFKXPI_ReportTime] [varchar](150) NULL,
	[CMFKXPI_OccupationDescription] [varchar](150) NULL,
	[CMFKXPI_ModeofHolding] [varchar](150) NULL,
	[CMFKXPI_ModeofHoldingDescription] [varchar](150) NULL,
	[CMFKXPI_MapinId] [varchar](150) NULL,
	[CMFKXPI_CreatedBy] [int] NULL,
	[CMFKXPI_CreatedOn] [datetime] NULL,
	[CMFKXPI_ModifiedBy] [int] NULL,
	[CMFKXPI_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFKXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


