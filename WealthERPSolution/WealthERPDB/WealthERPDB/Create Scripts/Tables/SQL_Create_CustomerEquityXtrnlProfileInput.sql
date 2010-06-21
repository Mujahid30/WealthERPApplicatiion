
GO

/****** Object:  Table [dbo].[CustomerEquityXtrnlProfileInput]    Script Date: 06/11/2009 12:07:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityXtrnlProfileInput](
	[CEXPI_Id] [int] IDENTITY(1,1) NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CEXPI_FirstName] [varchar](50) NULL,
	[CEXPI_MiddleName] [varchar](50) NULL,
	[CEXPI_LastName] [varchar](50) NULL,
	[CEXPI_Gender] [varchar](50) NULL,
	[CEXPI_DOB] [varchar](50) NULL,
	[CEXPI_Type] [varchar](50) NULL,
	[CEXPI_SubType] [varchar](50) NULL,
	[CEXPI_Salutation] [varchar](50) NULL,
	[CEXPI_PANNum] [varchar](50) NULL,
	[CEXPI_Adr1Line1] [varchar](50) NULL,
	[CEXPI_Adr1Line2] [varchar](50) NULL,
	[CEXPI_Adr1Line3] [varchar](50) NULL,
	[CEXPI_Adr1PinCode] [varchar](50) NULL,
	[CEXPI_Adr1City] [varchar](50) NULL,
	[CEXPI_Adr1State] [varchar](50) NULL,
	[CEXPI_Adr1Country] [varchar](50) NULL,
	[CEXPI_Adr2Line1] [varchar](50) NULL,
	[CEXPI_Adr2Line2] [varchar](50) NULL,
	[CEXPI_Adr2Line3] [varchar](50) NULL,
	[CEXPI_Adr2PinCode] [varchar](50) NULL,
	[CEXPI_Adr2City] [varchar](50) NULL,
	[CEXPI_Adr2State] [varchar](50) NULL,
	[CEXPI_Adr2Country] [varchar](50) NULL,
	[CEXPI_ResISDCode] [varchar](50) NULL,
	[CEXPI_ResSTDCode] [varchar](50) NULL,
	[CEXPI_ResPhoneNum] [varchar](50) NULL,
	[CEXPI_OfcISDCode] [varchar](50) NULL,
	[CEXPI_OfcSTDCode] [varchar](50) NULL,
	[CEXPI_OfcPhoneNum] [varchar](50) NULL,
	[CEXPI_Email] [varchar](50) NULL,
	[CEXPI_AltEmail] [varchar](50) NULL,
	[CEXPI_Mobile1] [varchar](50) NULL,
	[CEXPI_Mobile2] [varchar](50) NULL,
	[CEXPI_ISDFax] [varchar](50) NULL,
	[CEXPI_STDFax] [varchar](50) NULL,
	[CEXPI_Fax] [varchar](50) NULL,
	[CEXPI_OfcFax] [varchar](50) NULL,
	[CEXPI_OfcFaxISD] [varchar](50) NULL,
	[CEXPI_OfcFaxSTD] [varchar](50) NULL,
	[CEXPI_Occupation] [varchar](50) NULL,
	[CEXPI_Qualification] [varchar](50) NULL,
	[CEXPI_MarriageDate] [varchar](50) NULL,
	[CEXPI_MaritalStatus] [varchar](50) NULL,
	[CEXPI_Nationality] [varchar](50) NULL,
	[CEXPI_RBIRefNum] [varchar](50) NULL,
	[CEXPI_RBIApprovalDate] [varchar](50) NULL,
	[CEXPI_CompanyName] [varchar](50) NULL,
	[CEXPI_OfcAdrLine1] [varchar](50) NULL,
	[CEXPI_OfcAdrLine2] [varchar](50) NULL,
	[CEXPI_OfcAdrLine3] [varchar](50) NULL,
	[CEXPI_OfcAdrPinCode] [varchar](50) NULL,
	[CEXPI_OfcAdrCity] [varchar](50) NULL,
	[CEXPI_OfcAdrState] [varchar](50) NULL,
	[CEXPI_OfcAdrCountry] [varchar](50) NULL,
	[CEXPI_RegistrationDate] [varchar](50) NULL,
	[CEXPI_CommencementDate] [varchar](50) NULL,
	[CEXPI_RegistrationPlace] [varchar](50) NULL,
	[CEXPI_RegistrationNum] [varchar](50) NULL,
	[CEXPI_CompanyWebsite] [varchar](50) NULL,
	[CEXPI_BankName] [varchar](50) NULL,
	[CEXPI_AccountType] [varchar](50) NULL,
	[CEXPI_AccountNum] [varchar](50) NULL,
	[CEXPI_BankModeOfOperation] [varchar](50) NULL,
	[CEXPI_BranchName] [varchar](50) NULL,
	[CEXPI_BranchAdrLine1] [varchar](50) NULL,
	[CEXPI_BranchAdrLine2] [varchar](50) NULL,
	[CEXPI_BranchAdrLine3] [varchar](50) NULL,
	[CEXPI_BranchAdrPinCode] [varchar](50) NULL,
	[CEXPI_BranchAdrCity] [varchar](50) NULL,
	[CEXPI_BranchAdrState] [varchar](50) NULL,
	[CEXPI_BranchAdrCountry] [varchar](50) NULL,
	[CEXPI_MICR] [varchar](50) NULL,
	[CEXPI_IFSC] [varchar](50) NULL,
	[CEXPI_TradeNum] [varchar](50) NULL,
	[CEXPI_DPClientId] [varchar](50) NULL,
	[CEXPI_DPId] [varchar](50) NULL,
	[CEXPI_DPName] [varchar](50) NULL,
	[CEXPI_DPModeOfOperation] [varchar](50) NULL,
	[CEXPI_CreatedOn] [datetime] NULL,
	[CEXPI_CreatedBy] [int] NULL,
	[CEXPI_ModifiedOn] [datetime] NULL,
	[CEXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerEquityXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CEXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


