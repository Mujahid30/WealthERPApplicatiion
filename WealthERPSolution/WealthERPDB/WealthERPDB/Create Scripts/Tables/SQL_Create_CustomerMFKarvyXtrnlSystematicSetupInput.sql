
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetupInput]    Script Date: 06/11/2009 15:41:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetupInput](
	[CMFKXSSI_SystematicId] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXSSI_ProductCode] [varchar](30) NULL,
	[CMFKXSSI_Agent Code] [varchar](30) NULL,
	[CMFKXSSI_ Fund] [varchar](50) NULL,
	[CMFKXSSI_FolioNumber] [varchar](20) NULL,
	[CMFKXSSI_SchemeCode] [varchar](20) NULL,
	[CMFKXSSI_FundDescription] [varchar](50) NULL,
	[CMFKXSSI_InvestorName] [varchar](30) NULL,
	[CMFKXSSI_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSSI_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSSI_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSSI_InvestorCity] [varchar](25) NULL,
	[CMFKXSSI_InvestorState] [varchar](25) NULL,
	[CMFKXSSI_PinCode] [numeric](6, 0) NULL,
	[CMFKXSSI_EmailAddress] [varchar](max) NULL,
	[CMFKXSSI_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSSI_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSSI_TransactionType] [varchar](20) NULL,
	[CMFKXSSI_Frequency] [varchar](20) NULL,
	[CMFKXSSI_StartingDate] [datetime] NULL,
	[CMFKXSSI_EndingDate] [datetime] NULL,
	[CMFKXSSI_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSSI_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSSI_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSSI_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSSI_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSSI_PaymentMethod] [varchar](20) NULL,
	[CMFKXSSI_Subroker] [varchar](30) NULL,
	[CMFKXSSI_IHNO] [varchar](30) NULL,
	[CMFKXSSI_Remarks] [varchar](50) NULL,
	[CMFKXSSI_CreatedBy] [int] NULL,
	[CMFKXSSI_CreatedOn] [datetime] NULL,
	[CMFKXSSI_ModifiedBy] [int] NULL,
	[CMFKXSSI_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlSystematicSetupInput] PRIMARY KEY CLUSTERED 
(
	[CMFKXSSI_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


