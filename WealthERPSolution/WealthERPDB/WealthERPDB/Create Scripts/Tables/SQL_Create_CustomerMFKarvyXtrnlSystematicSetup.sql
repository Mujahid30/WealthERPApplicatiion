
GO

/****** Object:  Table [dbo].[CustomerMFKarvyXtrnlSystematicSetup]    Script Date: 06/11/2009 15:40:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup](
	[CMFKXSS_SystematicId] [int] NOT NULL,
	[WUPL_ProcessId] [int] NULL,
	[CMFSS_SystematicSetupId] [int] NULL,
	[CMFKXSS_ProductCode] [varchar](30) NULL,
	[CMFKXSS_Agent Code] [varchar](30) NULL,
	[CMFKXSS_ Fund] [varchar](50) NULL,
	[CMFKXSS_FolioNumber] [varchar](20) NULL,
	[CMFKXSS_SchemeCode] [varchar](20) NULL,
	[CMFKXSS_FundDescription] [varchar](50) NULL,
	[CMFKXSS_InvestorName] [varchar](30) NULL,
	[CMFKXSS_InvestorAddress1] [varchar](25) NULL,
	[CMFKXSS_InvestorAddress2] [varchar](25) NULL,
	[CMFKXSS_InvestorAddress3] [varchar](25) NULL,
	[CMFKXSS_InvestorCity] [varchar](25) NULL,
	[CMFKXSS_InvestorState] [varchar](25) NULL,
	[CMFKXSS_PinCode] [numeric](6, 0) NULL,
	[CMFKXSS_EmailAddress] [varchar](max) NULL,
	[CMFKXSS_PhoneNo1] [numeric](16, 0) NULL,
	[CMFKXSS_PhoneNo2] [numeric](16, 0) NULL,
	[CMFKXSS_TransactionType] [varchar](20) NULL,
	[CMFKXSS_Frequency] [varchar](20) NULL,
	[CMFKXSS_StartingDate] [datetime] NULL,
	[CMFKXSS_EndingDate] [datetime] NULL,
	[CMFKXSS_NoOf InstallmentsPaid] [numeric](5, 0) NULL,
	[CMFKXSS_NoOf InstalmentsPending] [numeric](5, 0) NULL,
	[CMFKXSS_TotalNoOfInstalments] [numeric](5, 0) NULL,
	[CMFKXSS_InstalmentAmount] [numeric](18, 4) NULL,
	[CMFKXSS_STPIn/OutScheme] [varchar](50) NULL,
	[CMFKXSS_PaymentMethod] [varchar](20) NULL,
	[CMFKXSS_Subroker] [varchar](30) NULL,
	[CMFKXSS_IHNO] [varchar](30) NULL,
	[CMFKXSS_Remarks] [varchar](50) NULL,
	[CMFKXSS_CreatedBy] [int] NULL,
	[CMFKXSS_CreatedOn] [datetime] NULL,
	[CMFKXSS_ModifiedBy] [int] NULL,
	[CMFKXSS_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerMFTempletonXtrnlSystematicSetup] PRIMARY KEY CLUSTERED 
(
	[CMFKXSS_SystematicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMFTempletonXtrnlSystematicSetup_CustomerMutualFundSystematicSetup] FOREIGN KEY([CMFSS_SystematicSetupId])
REFERENCES [dbo].[CustomerMutualFundSystematicSetup] ([CMFSS_SystematicSetupId])
GO

ALTER TABLE [dbo].[CustomerMFKarvyXtrnlSystematicSetup] CHECK CONSTRAINT [FK_CustomerMFTempletonXtrnlSystematicSetup_CustomerMutualFundSystematicSetup]
GO


