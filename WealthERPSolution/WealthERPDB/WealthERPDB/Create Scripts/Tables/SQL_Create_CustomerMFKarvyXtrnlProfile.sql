
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlProfile]    Script Date: 06/11/2009 15:39:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlProfile](
	[CMFKXP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[C_CustomerId] [int] NULL,
	[CMFKXP_ProductCode] [varchar](10) NULL,
	[CMFKXP_Fund] [varchar](50) NULL,
	[CMFKXP_Folio] [varchar](50) NULL,
	[CMFKXP_DividendOption] [varchar](50) NULL,
	[CMFKXP_FundDescription] [varchar](100) NULL,
	[CMFKXP_InvestorName] [varchar](60) NULL,
	[CMFKXP_JointName1] [varchar](60) NULL,
	[CMFKXP_JointName2] [varchar](60) NULL,
	[CMFKXP_Address1] [varchar](75) NULL,
	[CMFKXP_Address2] [varchar](75) NULL,
	[CMFKXP_Address3] [varchar](75) NULL,
	[CMFKXP_City] [varchar](25) NULL,
	[CMFKXP_Pincode] [numeric](6, 0) NULL,
	[CMFKXP_State] [varchar](25) NULL,
	[CMFKXP_Country] [varchar](25) NULL,
	[CMFKXP_TPIN] [varchar](50) NULL,
	[CMFKXP_DateofBirth] [datetime] NULL,
	[CMFKXP_FName] [varchar](50) NULL,
	[CMFKXP_MName] [varchar](50) NULL,
	[CMFKXP_PhoneResidence] [varchar](50) NULL,
	[CMFKXP_PhoneRes1] [varchar](50) NULL,
	[CMFKXP_PhoneRes2] [varchar](50) NULL,
	[CMFKXP_PhoneOffice] [varchar](50) NULL,
	[CMFKXP_PhoneOff1] [varchar](50) NULL,
	[CMFKXP_PhoneOff2] [varchar](50) NULL,
	[CMFKXP_FaxResidence] [varchar](50) NULL,
	[CMFKXP_FaxOffice] [varchar](50) NULL,
	[CMFKXP_TaxStatus] [varchar](50) NULL,
	[CMFKXP_OccCode] [varchar](50) NULL,
	[CMFKXP_Email] [varchar](255) NULL,
	[CMFKXP_BankAccno] [numeric](15, 0) NULL,
	[CMFKXP_BankName] [varchar](75) NULL,
	[CMFKXP_AccountType] [varchar](25) NULL,
	[CMFKXP_Branch] [varchar](25) NULL,
	[CMFKXP_BankAddress1] [varchar](75) NULL,
	[CMFKXP_BankAddress2] [varchar](75) NULL,
	[CMFKXP_BankAddress3] [varchar](75) NULL,
	[CMFKXP_BankCity] [varchar](25) NULL,
	[CMFKXP_BankPhone] [varchar](50) NULL,
	[CMFKXP_BankState] [varchar](50) NULL,
	[CMFKXP_BankCountry] [varchar](50) NULL,
	[CMFKXP_InvestorID] [varchar](50) NULL,
	[CMFKXP_BrokerCode] [varchar](50) NULL,
	[CMFKXP_PANNumber] [varchar](10) NULL,
	[CMFKXP_Mobile] [varchar](50) NULL,
	[CMFKXP_ReportDate] [datetime] NULL,
	[CMFKXP_ReportTime] [varchar](50) NULL,
	[CMFKXP_OccupationDescription] [varchar](50) NULL,
	[CMFKXP_ModeofHolding] [varchar](50) NULL,
	[CMFKXP_ModeofHoldingDescription] [varchar](50) NULL,
	[CMFKXP_MapinId] [varchar](50) NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlProfile] PRIMARY KEY CLUSTERED 
(
	[CMFKXP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFKarvyXtrnlProfile_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlProfile] CHECK CONSTRAINT [FK_CustomerMFKarvyXtrnlProfile_Customer]
GO


