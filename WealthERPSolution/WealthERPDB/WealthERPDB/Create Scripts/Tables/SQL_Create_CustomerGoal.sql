
GO

/****** Object:  Table [dbo].[CustomerGoal]    Script Date: 06/11/2009 12:58:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerGoal](
	[CG_CustomerGoalId] [int] IDENTITY(1000,1) NOT NULL,
	[CM_CustomerId] [int] NOT NULL,
	[CG_CreatedBy] [int] NOT NULL,
	[CG_CreatedOn] [datetime] NOT NULL,
	[CG_ModifiedBy] [int] NOT NULL,
	[CG_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerGoals] PRIMARY KEY CLUSTERED 
(
	[CG_CustomerGoalId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Goals Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerGoal'
GO

ALTER TABLE [dbo].[CustomerGoal]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerGoals_CustomerMaster] FOREIGN KEY([CM_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerGoal] CHECK CONSTRAINT [FK_CustomerGoals_CustomerMaster]
GO


