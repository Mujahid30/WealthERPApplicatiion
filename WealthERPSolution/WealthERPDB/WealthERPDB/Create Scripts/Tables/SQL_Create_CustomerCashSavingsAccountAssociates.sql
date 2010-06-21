
GO

/****** Object:  Table [dbo].[CustomerCashSavingsAccountAssociates]    Script Date: 06/11/2009 11:58:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerCashSavingsAccountAssociates](
	[CCSAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CCSA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CCSAA_AssociationType] [varchar](30) NULL,
	[CCSAA_CreatedBy] [int] NULL,
	[CCSAA_CreatedOn] [datetime] NULL,
	[CCSAA_ModifiedOn] [datetime] NULL,
	[CCSAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerCashSavingsAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CCSAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates] CHECK CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerCashSavingsAccount] FOREIGN KEY([CCSA_AccountId])
REFERENCES [dbo].[CustomerCashSavingsAccount] ([CCSA_AccountId])
GO

ALTER TABLE [dbo].[CustomerCashSavingsAccountAssociates] CHECK CONSTRAINT [FK_CustomerCashSavingsAccountAssociates_CustomerCashSavingsAccount]
GO


