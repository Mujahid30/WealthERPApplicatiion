
GO

/****** Object:  Table [dbo].[CustomerMFXtrnlProfileStaging]    Script Date: 06/11/2009 16:01:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFXtrnlProfileStaging](
	[CMFXPS_Id] [int] IDENTITY(1000,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXPS_FirstName] [varchar](25) NULL,
	[CMFXPS_MiddleName] [varchar](25) NULL,
	[CMFXPS_LastName] [varchar](50) NULL,
	[CMFXPS_Gender] [varchar](10) NULL,
	[CMFXPS_DOB] [datetime] NULL,
	[CMFXPS_Type] [varchar](25) NULL,
	[CMFXPS_SubType] [varchar](25) NULL,
	[CMFXPS_Salutation] [varchar](5) NULL,
	[CMFXPS_PANNum] [varchar](10) NULL,
	[CMFXPS_Adr1Line1] [varchar](50) NULL,
	[CMFXPS_Adr1Line2] [varchar](50) NULL,
	[CMFXPS_Adr1Line3] [varchar](50) NULL,
	[CMFXPS_Adr1PinCode] [numeric](6, 0) NULL,
	[CMFXPS_Adr1City] [varchar](25) NULL,
	[CMFXPS_Adr1State] [varchar](25) NULL,
	[CMFXPS_Adr1Country] [varchar](25) NULL,
	[CMFXPS_Adr2Line1] [varchar](30) NULL,
	[CMFXPS_Adr2Line2] [varchar](30) NULL,
	[CMFXPS_Adr2Line3] [varchar](30) NULL,
	[CMFXPS_Adr2PinCode] [numeric](6, 0) NULL,
	[CMFXPS_Adr2City] [varchar](25) NULL,
	[CMFXPS_Adr2State] [varchar](25) NULL,
	[CMFXPS_Adr2Country] [varchar](25) NULL,
	[CMFXPS_ResISDCode] [numeric](4, 0) NULL,
	[CMFXPS_ResSTDCode] [numeric](4, 0) NULL,
	[CMFXPS_ResPhoneNum] [numeric](16, 0) NULL,
	[CMFXPS_OfcISDCode] [numeric](4, 0) NULL,
	[CMFXPS_OfcSTDCode] [numeric](4, 0) NULL,
	[CMFXPS_OfcPhoneNum] [numeric](16, 0) NULL,
	[CMFXPS_Email] [varchar](50) NULL,
	[CMFXPS_AltEmail] [varchar](50) NULL,
	[CMFXPS_Mobile1] [numeric](10, 0) NULL,
	[CMFXPS_Mobile2] [numeric](10, 0) NULL,
	[CMFXPS_ISDFax] [numeric](4, 0) NULL,
	[CMFXPS_STDFax] [numeric](4, 0) NULL,
	[CMFXPS_Fax] [numeric](8, 0) NULL,
	[CMFXPS_OfcFax] [numeric](8, 0) NULL,
	[CMFXPS_OfcFaxISD] [numeric](4, 0) NULL,
	[CMFXPS_OfcFaxSTD] [numeric](4, 0) NULL,
	[CMFXPS_Occupation] [varchar](25) NULL,
	[CMFXPS_Qualification] [varchar](25) NULL,
	[CMFXPS_MarriageDate] [datetime] NULL,
	[CMFXPS_MaritalStatus] [varchar](25) NULL,
	[CMFXPS_Nationality] [varchar](25) NULL,
	[CMFXPS_RBIRefNum] [varchar](25) NULL,
	[CMFXPS_RBIApprovalDate] [datetime] NULL,
	[CMFXPS_CompanyName] [varchar](50) NULL,
	[CMFXPS_OfcAdrLine1] [varchar](25) NULL,
	[CMFXPS_OfcAdrLine2] [varchar](25) NULL,
	[CMFXPS_OfcAdrLine3] [varchar](25) NULL,
	[CMFXPS_OfcAdrPinCode] [numeric](6, 0) NULL,
	[CMFXPS_OfcAdrCity] [varchar](25) NULL,
	[CMFXPS_OfcAdrState] [varchar](25) NULL,
	[CMFXPS_OfcAdrCountry] [varchar](25) NULL,
	[CMFXPS_RegistrationDate] [datetime] NULL,
	[CMFXPS_CommencementDate] [datetime] NULL,
	[CMFXPS_RegistrationPlace] [varchar](20) NULL,
	[CMFXPS_RegistrationNum] [varchar](25) NULL,
	[CMFXPS_CompanyWebsite] [varchar](25) NULL,
	[CMFXPS_BankName] [varchar](30) NULL,
	[CMFXPS_AccountType] [varchar](30) NULL,
	[CMFXPS_AccountNum] [numeric](15, 0) NULL,
	[CMFXPS_BankModeOfOperation] [varchar](5) NULL,
	[CMFXPS_BranchName] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine1] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine2] [varchar](50) NULL,
	[CMFXPS_BranchAdrLine3] [varchar](50) NULL,
	[CMFXPS_BranchAdrPinCode] [numeric](6, 0) NULL,
	[CMFXPS_BranchAdrCity] [varchar](25) NULL,
	[CMFXPS_BranchAdrState] [varchar](25) NULL,
	[CMFXPS_BranchAdrCountry] [varchar](25) NULL,
	[CMFXPS_MICR] [numeric](9, 0) NULL,
	[CMFXPS_IFSC] [varchar](11) NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioId] [int] NULL,
	[A_AdviserId] [int] NULL,
	[PA_AMCCode] [int] NULL,
	[CMFXPS_FolioNum] [varchar](50) NULL,
	[CMFXPS_AccountOpeningDate] [datetime] NULL,
	[CMFXPS_FolioModeOfOperating] [char](5) NULL,
	[CMFXPS_RejectedRemark] [varchar](100) NULL,
	[CMFXPS_IsRejected] [tinyint] NULL,
	[CMFXPS_IsCustomerNew] [tinyint] NULL,
	[CMFXPS_IsFolioNew] [tinyint] NULL,
	[CMFXPS_IsBankAccountNew] [tinyint] NULL,
	[CMFXPS_CreatedOn] [datetime] NULL,
	[CMFXPS_CreatedBy] [int] NULL,
	[CMFXPS_ModifiedOn] [datetime] NULL,
	[CMFXPS_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlProfileStaging] PRIMARY KEY CLUSTERED 
(
	[CMFXPS_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


