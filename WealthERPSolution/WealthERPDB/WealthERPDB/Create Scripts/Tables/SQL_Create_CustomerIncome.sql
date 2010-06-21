
GO

/****** Object:  Table [dbo].[CustomerIncome]    Script Date: 06/11/2009 13:05:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerIncome](
	[CI_IncomeId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[CI_CreatedBy] [int] NOT NULL,
	[CI_CreatedOn] [datetime] NOT NULL,
	[CI_ModifiedBy] [int] NOT NULL,
	[CI_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerIncome] PRIMARY KEY CLUSTERED 
(
	[CI_IncomeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Income Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerIncome'
GO

ALTER TABLE [dbo].[CustomerIncome]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerIncome_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerIncome] CHECK CONSTRAINT [FK_CustomerIncome_CustomerMaster]
GO


