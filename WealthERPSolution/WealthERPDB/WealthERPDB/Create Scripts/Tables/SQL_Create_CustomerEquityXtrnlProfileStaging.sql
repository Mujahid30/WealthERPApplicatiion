
GO

/****** Object:  Table [dbo].[CustomerEquityXtrnlProfileStaging]    Script Date: 06/11/2009 12:08:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityXtrnlProfileStaging](
	[CEXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXPS_FirstName] [varchar](25) NULL,
	[CEXPS_MiddleName] [varchar](25) NULL,
	[CEXPS_LastName] [varchar](50) NULL,
	[CEXPS_Gender] [varchar](10) NULL,
	[CEXPS_DOB] [datetime] NULL,
	[CEXPS_Type] [varchar](25) NULL,
	[CEXPS_SubType] [varchar](25) NULL,
	[CEXPS_Salutation] [varchar](5) NULL,
	[CEXPS_PANNum] [varchar](10) NULL,
	[CEXPS_Adr1Line1] [varchar](50) NULL,
	[CEXPS_Adr1Line2] [varchar](50) NULL,
	[CEXPS_Adr1Line3] [varchar](50) NULL,
	[CEXPS_Adr1PinCode] [numeric](6, 0) NULL,
	[CEXPS_Adr1City] [varchar](25) NULL,
	[CEXPS_Adr1State] [varchar](25) NULL,
	[CEXPS_Adr1Country] [varchar](25) NULL,
	[CEXPS_Adr2Line1] [varchar](30) NULL,
	[CEXPS_Adr2Line2] [varchar](30) NULL,
	[CEXPS_Adr2Line3] [varchar](30) NULL,
	[CEXPS_Adr2PinCode] [numeric](6, 0) NULL,
	[CEXPS_Adr2City] [varchar](25) NULL,
	[CEXPS_Adr2State] [varchar](25) NULL,
	[CEXPS_Adr2Country] [varchar](25) NULL,
	[CEXPS_ResISDCode] [numeric](4, 0) NULL,
	[CEXPS_ResSTDCode] [numeric](4, 0) NULL,
	[CEXPS_ResPhoneNum] [numeric](16, 0) NULL,
	[CEXPS_OfcISDCode] [numeric](4, 0) NULL,
	[CEXPS_OfcSTDCode] [numeric](4, 0) NULL,
	[CEXPS_OfcPhoneNum] [numeric](16, 0) NULL,
	[CEXPS_Email] [varchar](50) NULL,
	[CEXPS_AltEmail] [varchar](50) NULL,
	[CEXPS_Mobile1] [numeric](10, 0) NULL,
	[CEXPS_Mobile2] [numeric](10, 0) NULL,
	[CEXPS_ISDFax] [numeric](4, 0) NULL,
	[CEXPS_STDFax] [numeric](4, 0) NULL,
	[CEXPS_Fax] [numeric](8, 0) NULL,
	[CEXPS_OfcFax] [numeric](8, 0) NULL,
	[CEXPS_OfcFaxISD] [numeric](4, 0) NULL,
	[CEXPS_OfcFaxSTD] [numeric](4, 0) NULL,
	[CEXPS_Occupation] [varchar](25) NULL,
	[CEXPS_Qualification] [varchar](25) NULL,
	[CEXPS_MarriageDate] [datetime] NULL,
	[CEXPS_MaritalStatus] [varchar](25) NULL,
	[CEXPS_Nationality] [varchar](25) NULL,
	[CEXPS_RBIRefNum] [varchar](25) NULL,
	[CEXPS_RBIApprovalDate] [datetime] NULL,
	[CEXPS_CompanyName] [varchar](50) NULL,
	[CEXPS_OfcAdrLine1] [varchar](25) NULL,
	[CEXPS_OfcAdrLine2] [varchar](25) NULL,
	[CEXPS_OfcAdrLine3] [varchar](25) NULL,
	[CEXPS_OfcAdrPinCode] [numeric](6, 0) NULL,
	[CEXPS_OfcAdrCity] [varchar](25) NULL,
	[CEXPS_OfcAdrState] [varchar](25) NULL,
	[CEXPS_OfcAdrCountry] [varchar](25) NULL,
	[CEXPS_RegistrationDate] [datetime] NULL,
	[CEXPS_CommencementDate] [datetime] NULL,
	[CEXPS_RegistrationPlace] [varchar](20) NULL,
	[CEXPS_RegistrationNum] [varchar](25) NULL,
	[CEXPS_CompanyWebsite] [varchar](25) NULL,
	[CEXPS_BankName] [varchar](30) NULL,
	[CEXPS_AccountType] [varchar](30) NULL,
	[CEXPS_AccountNum] [numeric](15, 0) NULL,
	[CEXPS_BankModeOfOperation] [varchar](5) NULL,
	[CEXPS_BranchName] [varchar](50) NULL,
	[CEXPS_BranchAdrLine1] [varchar](50) NULL,
	[CEXPS_BranchAdrLine2] [varchar](50) NULL,
	[CEXPS_BranchAdrLine3] [varchar](50) NULL,
	[CEXPS_BranchAdrPinCode] [numeric](6, 0) NULL,
	[CEXPS_BranchAdrCity] [varchar](25) NULL,
	[CEXPS_BranchAdrState] [varchar](25) NULL,
	[CEXPS_BranchAdrCountry] [varchar](25) NULL,
	[CEXPS_MICR] [numeric](9, 0) NULL,
	[CEXPS_IFSC] [varchar](11) NULL,
	[CEXPS_TradeNum] [varchar](50) NULL,
	[CEXPS_DPClientId] [varchar](20) NULL,
	[CEXPS_DPId] [varchar](20) NULL,
	[CEXPS_DPName] [varchar](50) NULL,
	[CEXPS_DPModeOfOperation] [varchar](5) NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[CEXPS_RejectedRemark] [varchar](100) NULL,
	[CEXPS_IsRejected] [tinyint] NULL,
	[CEXPS_IsCustomerNew] [tinyint] NULL,
	[CEXPS_IsTradeAccountNew] [tinyint] NULL,
	[CEXPS_IsBankAccountNew] [tinyint] NULL,
	[CEXPS_CreatedOn] [datetime] NULL,
	[CEXPS_CreatedBy] [int] NULL,
	[CEXPS_ModifiedOn] [datetime] NULL,
	[CEXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CEXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


