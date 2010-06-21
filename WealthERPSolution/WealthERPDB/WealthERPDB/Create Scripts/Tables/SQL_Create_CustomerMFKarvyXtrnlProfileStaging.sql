
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfileStaging]    Script Date: 06/11/2009 15:40:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfileStaging](
	[CMFKXPS_Id] [int] IDENTITY(1,1) NOT NULL,
	[ADUL_ProcessId] [int] NULL,
	[PA_AMCId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[WUS_UploadStatusCode] [varchar](5) NULL,
	[CMFKXPS_ProductCode] [varchar](15) NULL,
	[CMFKXPS_Fund] [varchar](50) NULL,
	[CMFKXPS_Folio] [varchar](20) NULL,
	[CMFKXPS_DividendOption] [varchar](50) NULL,
	[CMFKXPS_FundDescription] [varchar](150) NULL,
	[CMFKXPS_InvestorName] [varchar](75) NULL,
	[CMFKXPS_JointName1] [varchar](75) NULL,
	[CMFKXPS_JointName2] [varchar](75) NULL,
	[CMFKXPS_Address#1] [varchar](75) NULL,
	[CMFKXPS_Address#2] [varchar](75) NULL,
	[CMFKXPS_Address#3] [varchar](75) NULL,
	[CMFKXPS_City] [varchar](25) NULL,
	[CMFKXPS_Pincode] [numeric](10, 0) NULL,
	[CMFKXPS_State] [varchar](25) NULL,
	[CMFKXPS_Country] [varchar](25) NULL,
	[CMFKXPS_TPIN] [varchar](50) NULL,
	[CMFKXPS_DateofBirth] [datetime] NULL,
	[CMFKXPS_FName] [varchar](75) NULL,
	[CMFKXPS_MName] [varchar](75) NULL,
	[CMFKXPS_PhoneResidence] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneRes#1] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneRes#2] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOffice] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOff#1] [numeric](25, 0) NULL,
	[CMFKXPS_PhoneOff#2] [numeric](25, 0) NULL,
	[CMFKXPS_FaxResidence] [numeric](25, 0) NULL,
	[CMFKXPS_FaxOffice] [numeric](25, 0) NULL,
	[CMFKXPS_TaxStatus] [varchar](50) NULL,
	[CMFKXPS_OccCode] [varchar](50) NULL,
	[CMFKXPS_Email] [varchar](max) NULL,
	[CMFKXPS_BankAccno] [varchar](50) NULL,
	[CMFKXPS_BankName] [varchar](75) NULL,
	[CMFKXPS_AccountType] [varchar](25) NULL,
	[CMFKXPS_Branch] [varchar](75) NULL,
	[CMFKXPS_BankAddress#1] [varchar](75) NULL,
	[CMFKXPS_BankAddress#2] [varchar](75) NULL,
	[CMFKXPS_BankAddress#3] [varchar](75) NULL,
	[CMFKXPS_BankCity] [varchar](25) NULL,
	[CMFKXPS_BankPhone] [numeric](25, 0) NULL,
	[CMFKXPS_BankState] [varchar](25) NULL,
	[CMFKXPS_BankCountry] [varchar](25) NULL,
	[CMFKXPS_InvestorID] [varchar](50) NULL,
	[CMFKXPS_BrokerCode] [varchar](50) NULL,
	[CMFKXPS_PANNumber] [varchar](20) NULL,
	[CMFKXPS_Mobile] [varchar](50) NULL,
	[CMFKXPS_ReportDate] [datetime] NULL,
	[CMFKXPS_ReportTime] [datetime] NULL,
	[CMFKXPS_OccupationDescription] [varchar](50) NULL,
	[CMFKXPS_ModeofHolding] [varchar](50) NULL,
	[CMFKXPS_ModeofHoldingDescription] [varchar](50) NULL,
	[CMFKXPS_MapinId] [varchar](50) NULL,
	[CMFKXPS_IsRejected] [tinyint] NULL,
	[CMFKXPS_IsFolioNew] [tinyint] NULL,
	[CMFKXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFKXPS_IsCustomerNew] [tinyint] NULL,
	[WRR_RejectReasonCode] [int] NULL,
	[CMFKXPS_RejectReason] [varchar](50) NULL,
	[CMFKXPS_IsAMCNew] [tinyint] NULL,
	[CMFKXPS_CreatedBy] [int] NULL,
	[CMFKXPS_CreatedOn] [datetime] NULL,
	[CMFKXPS_ModifiedOn] [datetime] NULL,
	[CMFKXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFKXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


