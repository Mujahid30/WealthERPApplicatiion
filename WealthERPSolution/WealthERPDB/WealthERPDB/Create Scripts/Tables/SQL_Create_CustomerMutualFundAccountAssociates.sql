
GO

/****** Object:  Table [dbo].[CustomerMutualFundAccountAssociates]    Script Date: 06/11/2009 16:02:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerMutualFundAccountAssociates](
	[CMFAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CA_AssociationId] [int] NULL,
	[CMFA_AccountId] [int] NULL,
	[CMFAA_AssociationType] [varchar](30) NULL,
	[CMFAA_ModifiedBy] [int] NOT NULL,
	[CMFAA_CreatedBy] [int] NOT NULL,
	[CMFAA_ModifiedOn] [datetime] NOT NULL,
	[CMFAA_CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerAccountAssociation] PRIMARY KEY CLUSTERED 
(
	[CMFAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Account Association Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerMutualFundAccountAssociates'
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccount] FOREIGN KEY([CMFA_AccountId])
REFERENCES [dbo].[CustomerMutualFundAccount] ([CMFA_AccountId])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccount]
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccountAssociates] FOREIGN KEY([CMFAA_AccountAssociationId])
REFERENCES [dbo].[CustomerMutualFundAccountAssociates] ([CMFAA_AccountAssociationId])
GO

ALTER TABLE [dbo].[CustomerMutualFundAccountAssociates] CHECK CONSTRAINT [FK_CustomerMutualFundAccountAssociates_CustomerMutualFundAccountAssociates]
GO


