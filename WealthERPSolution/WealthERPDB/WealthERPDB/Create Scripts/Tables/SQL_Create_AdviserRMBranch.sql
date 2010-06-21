
GO

/****** Object:  Table [dbo].[AdviserRMBranch]    Script Date: 06/11/2009 10:49:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdviserRMBranch](
	[AR_RMId] [int] NOT NULL,
	[AB_BranchId] [int] NOT NULL,
	[ARB_CreatedBy] [int] NOT NULL,
	[ARB_CreatedOn] [datetime] NOT NULL,
	[ARB_ModifiedBy] [int] NOT NULL,
	[ARB_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RM Branch Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdviserRMBranch'
GO

ALTER TABLE [dbo].[AdviserRMBranch]  WITH CHECK ADD  CONSTRAINT [FK_RMBranch_AdvisorBranch] FOREIGN KEY([AB_BranchId])
REFERENCES [dbo].[AdviserBranch] ([AB_BranchId])
GO

ALTER TABLE [dbo].[AdviserRMBranch] CHECK CONSTRAINT [FK_RMBranch_AdvisorBranch]
GO

ALTER TABLE [dbo].[AdviserRMBranch]  WITH CHECK ADD  CONSTRAINT [FK_RMBranch_RMMaster] FOREIGN KEY([AR_RMId])
REFERENCES [dbo].[AdviserRM] ([AR_RMId])
GO

ALTER TABLE [dbo].[AdviserRMBranch] CHECK CONSTRAINT [FK_RMBranch_RMMaster]
GO


