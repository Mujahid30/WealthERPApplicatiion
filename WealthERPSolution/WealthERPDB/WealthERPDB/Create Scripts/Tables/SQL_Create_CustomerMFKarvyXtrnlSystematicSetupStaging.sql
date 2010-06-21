
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetupStaging]    Script Date: 06/11/2009 15:41:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetupStaging](
	[CMFKXSSS_SystematicId] [int] NOT NULL,
	[CMFKXSSS_ProductCode] [varchar](30) NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFKXSSS_Agent Code] [varchar](30) NULL,
	[CMFKXSSS_ Fund] [varchar](50) NULL,
	[CMFKXSSS_FolioNumber] [varchar](20) NULL,
	[CMFKXSSS_SchemeCode] [varchar](20) NULL,
	[CMFKXSSS_FundDescription] [varchar](50) NULL,
	[CMFKXSSS_InvestorName] [varchar](30) NULL,
	[CMFKXSSS_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSSS_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSSS_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSSS_InvestorCity] [varchar](25) NULL,
	[CMFKXSSS_InvestorState] [varchar](25) NULL,
	[CMFKXSSS_PinCode] [numeric](6, 0) NULL,
	[CMFKXSSS_EmailAddress] [varchar](max) NULL,
	[CMFKXSSS_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSSS_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSSS_TransactionType] [varchar](20) NULL,
	[CMFKXSSS_Frequency] [varchar](20) NULL,
	[CMFKXSSS_StartingDate] [datetime] NULL,
	[CMFKXSSS_EndingDate] [datetime] NULL,
	[CMFKXSSS_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSSS_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSSS_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSSS_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSSS_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSSS_PaymentMethod] [varchar](20) NULL,
	[CMFKXSSS_Subroker] [varchar](30) NULL,
	[CMFKXSSS_IHNO] [varchar](30) NULL,
	[CMFKXSSS_Remarks] [varchar](50) NULL,
	[CMFKXSSS_CreatedBy] [int] NULL,
	[CMFKXSSS_CreatedOn] [datetime] NULL,
	[CMFKXSSS_ModifiedBy] [int] NULL,
	[CMFKXSSS_ModifiedOn] [datetime] NULL,
	[A_AdviserId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFKXSSS_IsRejected] [tinyint] NULL,
	[CMFKXSSS_RejectedRemark] [varchar](50) NULL,
 CONSTRAINT [PK_CustomerMFKarvyXtrnlSystematicSetupStaging] PRIMARY KEY CLUSTERED 
(
	[CMFKXSSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


