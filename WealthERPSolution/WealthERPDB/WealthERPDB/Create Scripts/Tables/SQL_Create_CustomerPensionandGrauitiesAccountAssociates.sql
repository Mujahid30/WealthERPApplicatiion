
GO

/****** Object:  Table [dbo].[CustomerPensionandGrauitiesAccountAssociates]    Script Date: 06/11/2009 16:05:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates](
	[CPGAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CPGA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CPGAA_AssociationType] [varchar](30) NULL,
	[CPGAA_NomineeShare] [numeric](5, 2) NULL,
	[CPGAA_CreatedBy] [int] NULL,
	[CPGAA_CreatedOn] [datetime] NULL,
	[CPGAA_ModifiedOn] [datetime] NULL,
	[CPGAA_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerPensionandGrauitiesAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CPGAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates] CHECK CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerPensionandGratuitiesAccount] FOREIGN KEY([CPGA_AccountId])
REFERENCES [dbo].[CustomerPensionandGratuitiesAccount] ([CPGA_AccountId])
GO

ALTER TABLE [dbo].[CustomerPensionandGrauitiesAccountAssociates] CHECK CONSTRAINT [FK_CustomerPensionandGrauitiesAccountAssociates_CustomerPensionandGratuitiesAccount]
GO


