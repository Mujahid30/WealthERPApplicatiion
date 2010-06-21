
GO

/****** Object:  Table [dbo].[CustomerMFXtrnlProfileInput]    Script Date: 06/11/2009 16:00:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFXtrnlProfileInput](
	[CMFXPI_Id] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFXPI_FirstName] [varchar](50) NULL,
	[CMFXPI_MiddleName] [varchar](50) NULL,
	[CMFXPI_LastName] [varchar](50) NULL,
	[CMFXPI_Gender] [varchar](50) NULL,
	[CMFXPI_DOB] [varchar](50) NULL,
	[CMFXPI_Type] [varchar](50) NULL,
	[CMFXPI_SubType] [varchar](50) NULL,
	[CMFXPI_Salutation] [varchar](50) NULL,
	[CMFXPI_PANNum] [varchar](50) NULL,
	[CMFXPI_Adr1Line1] [varchar](50) NULL,
	[CMFXPI_Adr1Line2] [varchar](50) NULL,
	[CMFXPI_Adr1Line3] [varchar](50) NULL,
	[CMFXPI_Adr1PinCode] [varchar](50) NULL,
	[CMFXPI_Adr1City] [varchar](50) NULL,
	[CMFXPI_Adr1State] [varchar](50) NULL,
	[CMFXPI_Adr1Country] [varchar](50) NULL,
	[CMFXPI_Adr2Line1] [varchar](50) NULL,
	[CMFXPI_Adr2Line2] [varchar](50) NULL,
	[CMFXPI_Adr2Line3] [varchar](50) NULL,
	[CMFXPI_Adr2PinCode] [varchar](50) NULL,
	[CMFXPI_Adr2City] [varchar](50) NULL,
	[CMFXPI_Adr2State] [varchar](50) NULL,
	[CMFXPI_Adr2Country] [varchar](50) NULL,
	[CMFXPI_ResISDCode] [varchar](50) NULL,
	[CMFXPI_ResSTDCode] [varchar](50) NULL,
	[CMFXPI_ResPhoneNum] [varchar](50) NULL,
	[CMFXPI_OfcISDCode] [varchar](50) NULL,
	[CMFXPI_OfcSTDCode] [varchar](50) NULL,
	[CMFXPI_OfcPhoneNum] [varchar](50) NULL,
	[CMFXPI_Email] [varchar](50) NULL,
	[CMFXPI_AltEmail] [varchar](50) NULL,
	[CMFXPI_Mobile1] [varchar](50) NULL,
	[CMFXPI_Mobile2] [varchar](50) NULL,
	[CMFXPI_ISDFax] [varchar](50) NULL,
	[CMFXPI_STDFax] [varchar](50) NULL,
	[CMFXPI_Fax] [varchar](50) NULL,
	[CMFXPI_OfcFax] [varchar](50) NULL,
	[CMFXPI_OfcFaxISD] [varchar](50) NULL,
	[CMFXPI_OfcFaxSTD] [varchar](50) NULL,
	[CMFXPI_Occupation] [varchar](50) NULL,
	[CMFXPI_Qualification] [varchar](50) NULL,
	[CMFXPI_MarriageDate] [varchar](50) NULL,
	[CMFXPI_MaritalStatus] [varchar](50) NULL,
	[CMFXPI_Nationality] [varchar](50) NULL,
	[CMFXPI_RBIRefNum] [varchar](50) NULL,
	[CMFXPI_RBIApprovalDate] [varchar](50) NULL,
	[CMFXPI_CompanyName] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine1] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine2] [varchar](50) NULL,
	[CMFXPI_OfcAdrLine3] [varchar](50) NULL,
	[CMFXPI_OfcAdrPinCode] [varchar](50) NULL,
	[CMFXPI_OfcAdrCity] [varchar](50) NULL,
	[CMFXPI_OfcAdrState] [varchar](50) NULL,
	[CMFXPI_OfcAdrCountry] [varchar](50) NULL,
	[CMFXPI_RegistrationDate] [varchar](50) NULL,
	[CMFXPI_CommencementDate] [varchar](50) NULL,
	[CMFXPI_RegistrationPlace] [varchar](50) NULL,
	[CMFXPI_RegistrationNum] [varchar](50) NULL,
	[CMFXPI_CompanyWebsite] [varchar](50) NULL,
	[CMFXPI_BankName] [varchar](50) NULL,
	[CMFXPI_AccountType] [varchar](50) NULL,
	[CMFXPI_AccountNum] [varchar](50) NULL,
	[CMFXPI_BankModeOfOperation] [varchar](50) NULL,
	[CMFXPI_BranchName] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine1] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine2] [varchar](50) NULL,
	[CMFXPI_BranchAdrLine3] [varchar](50) NULL,
	[CMFXPI_BranchAdrPinCode] [varchar](50) NULL,
	[CMFXPI_BranchAdrCity] [varchar](50) NULL,
	[CMFXPI_BranchAdrState] [varchar](50) NULL,
	[CMFXPI_BranchAdrCountry] [varchar](50) NULL,
	[CMFXPI_MICR] [varchar](50) NULL,
	[CMFXPI_IFSC] [varchar](50) NULL,
	[CMFXPI_AMCName] [varchar](100) NULL,
	[CMFXPI_FolioNum] [varchar](50) NULL,
	[CMFXPI_AccountOpeningDate] [datetime] NULL,
	[CMFXPI_FolioModeOfOperating] [varchar](5) NULL,
	[CMFXPI_CreatedOn] [datetime] NULL,
	[CMFXPI_CreatedBy] [int] NULL,
	[CMFXPI_ModifiedOn] [datetime] NULL,
	[CMFXPI_ModifiedBy] [int] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFXtrnlProfileInput] PRIMARY KEY CLUSTERED 
(
	[CMFXPI_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


