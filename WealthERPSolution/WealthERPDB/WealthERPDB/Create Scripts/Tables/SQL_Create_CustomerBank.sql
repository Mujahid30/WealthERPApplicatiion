
GO

/****** Object:  Table [dbo].[CustomerBank]    Script Date: 06/11/2009 11:57:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerBank](
	[CB_CustBankAccId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[CB_BankName] [varchar](50) NULL,
	[XBAT_BankAccountTypeCode] [varchar](5) NULL,
	[CB_AccountNum] [varchar](50) NULL,
	[XMOH_ModeOfHoldingCode] [varchar](5) NULL,
	[CB_BranchName] [varchar](50) NULL,
	[CB_BranchAdrLine1] [varchar](75) NULL,
	[CB_BranchAdrLine2] [varchar](75) NULL,
	[CB_BranchAdrLine3] [varchar](75) NULL,
	[CB_BranchAdrPinCode] [numeric](10, 0) NULL,
	[CB_BranchAdrCity] [varchar](25) NULL,
	[CB_BranchAdrState] [varchar](25) NULL,
	[CB_BranchAdrCountry] [varchar](25) NULL,
	[CB_MICR] [numeric](9, 0) NULL,
	[CB_Balance] [numeric](18, 3) NULL,
	[CB_IFSC] [varchar](11) NULL,
	[CB_CreatedOn] [datetime] NOT NULL,
	[CB_CreatedBy] [int] NOT NULL,
	[CB_ModifiedOn] [datetime] NOT NULL,
	[CB_ModifiedBy] [int] NOT NULL,
 CONSTRAINT [PK_CustomerBankAccount] PRIMARY KEY CLUSTERED 
(
	[CB_CustBankAccId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Bank Account Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerBank'
GO

ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBank_XMLBankAccountType] FOREIGN KEY([XBAT_BankAccountTypeCode])
REFERENCES [dbo].[XMLBankAccountType] ([XBAT_BankAccountTypeCode])
GO

ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBank_XMLBankAccountType]
GO

ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBank_XMLModeOfHolding] FOREIGN KEY([XMOH_ModeOfHoldingCode])
REFERENCES [dbo].[XMLModeOfHolding] ([XMOH_ModeOfHoldingCode])
GO

ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBank_XMLModeOfHolding]
GO

ALTER TABLE [dbo].[CustomerBank]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBankAccount_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerBank] CHECK CONSTRAINT [FK_CustomerBankAccount_CustomerMaster]
GO


