
GO

/****** Object:  Table [dbo].[CustomerRiskProfile]    Script Date: 06/11/2009 19:56:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerRiskProfile](
	[CRP_Id] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NOT NULL,
	[QM_QuestionId] [int] NULL,
	[QCH_ChoiceId] [int] NULL,
	[CRP_Score] [int] NULL,
	[CRP_CreatedBy] [int] NOT NULL,
	[CRP_CreatedOn] [datetime] NOT NULL,
	[CRP_ModifiedBy] [int] NOT NULL,
	[CRP_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerRiskProfile] PRIMARY KEY CLUSTERED 
(
	[CRP_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Customer Risk Profile Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerRiskProfile'
GO

ALTER TABLE [dbo].[CustomerRiskProfile]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_CustomerMaster] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_CustomerMaster]
GO

ALTER TABLE [dbo].[CustomerRiskProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_QuestionChoice] FOREIGN KEY([QCH_ChoiceId])
REFERENCES [dbo].[WerpQuestionChoice] ([WQCH_ChoiceId])
GO

ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_QuestionChoice]
GO

ALTER TABLE [dbo].[CustomerRiskProfile]  WITH CHECK ADD  CONSTRAINT [FK_CustomerRiskProfile_QuestionMaster] FOREIGN KEY([QM_QuestionId])
REFERENCES [dbo].[WerpQuestionMaster] ([WQM_QuestionId])
GO

ALTER TABLE [dbo].[CustomerRiskProfile] CHECK CONSTRAINT [FK_CustomerRiskProfile_QuestionMaster]
GO


