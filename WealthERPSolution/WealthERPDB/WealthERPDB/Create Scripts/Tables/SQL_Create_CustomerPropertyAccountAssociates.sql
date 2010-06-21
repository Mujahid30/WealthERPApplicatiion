
GO

/****** Object:  Table [dbo].[CustomerPropertyAccountAssociates]    Script Date: 06/11/2009 19:54:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPropertyAccountAssociates](
	[CPAA_AccountAssociationId] [int] IDENTITY(1000,1) NOT NULL,
	[CPA_AccountId] [int] NULL,
	[CA_AssociationId] [int] NULL,
	[CPAA_AssociationType] [varchar](30) NULL,
	[CPAA_NomineeShare] [numeric](3, 0) NULL,
	[CPAA_CreatedBy] [int] NULL,
	[CPAA_CreatedOn] [datetime] NULL,
	[CPAA_ModifiedBy] [int] NULL,
	[CPAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPropertyAccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CPAA_AccountAssociationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPropertyAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociationId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerPropertyAccountAssociates] CHECK CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerPropertyAccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerPropertyAccount] FOREIGN KEY([CPA_AccountId])
REFERENCES [dbo].[CustomerPropertyAccount] ([CPA_AccountId])
GO

ALTER TABLE [dbo].[CustomerPropertyAccountAssociates] CHECK CONSTRAINT [FK_CustomerPropertyAccountAssociates_CustomerPropertyAccount]
GO


