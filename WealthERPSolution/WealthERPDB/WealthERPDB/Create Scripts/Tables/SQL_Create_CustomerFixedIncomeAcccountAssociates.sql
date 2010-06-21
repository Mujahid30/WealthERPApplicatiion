
GO

/****** Object:  Table [dbo].[CustomerFixedIncomeAcccountAssociates]    Script Date: 06/11/2009 12:12:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerFixedIncomeAcccountAssociates](
	[CFIAA_AccountAssociateId] [int] IDENTITY(1000,1) NOT NULL,
	[CFIA_AccountId] [int] NULL,
	[CA_AssociateId] [int] NULL,
	[CFIAA_AssociationType] [varchar](30) NULL,
	[CFIAA_CreatedBy] [int] NULL,
	[CFIAA_CreatedOn] [datetime] NULL,
	[CFIAA_ModifiedBy] [int] NULL,
	[CFIAA_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerFixedIncomeAcccountAssociates] PRIMARY KEY CLUSTERED 
(
	[CFIAA_AccountAssociateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerAssociates] FOREIGN KEY([CA_AssociateId])
REFERENCES [dbo].[CustomerAssociates] ([CA_AssociationId])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates] CHECK CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerAssociates]
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates]  WITH CHECK ADD  CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerFixedIncomeAccount] FOREIGN KEY([CFIA_AccountId])
REFERENCES [dbo].[CustomerFixedIncomeAccount] ([CFIA_AccountId])
GO

ALTER TABLE [dbo].[CustomerFixedIncomeAcccountAssociates] CHECK CONSTRAINT [FK_CustomerFixedIncomeAcccountAssociates_CustomerFixedIncomeAccount]
GO


