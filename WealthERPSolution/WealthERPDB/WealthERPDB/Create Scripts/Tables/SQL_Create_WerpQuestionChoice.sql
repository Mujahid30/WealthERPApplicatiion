
GO

/****** Object:  Table [dbo].[WerpQuestionChoice]    Script Date: 06/12/2009 18:38:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WerpQuestionChoice](
	[WQCH_ChoiceId] [int] IDENTITY(1000,1) NOT NULL,
	[WQM_QuestionId] [int] NOT NULL,
	[WQCH_Choice] [varchar](max) NULL,
	[WQCH_Score] [int] NULL,
	[WQCH_Order] [int] NULL,
	[WQCH_CreatedBy] [bigint] NOT NULL,
	[WQCH_CreatedOn] [datetime] NOT NULL,
	[WQCH_ModifiedBy] [bigint] NOT NULL,
	[WQCH_ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_QuestionChoice] PRIMARY KEY CLUSTERED 
(
	[WQCH_ChoiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Question Choice Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WerpQuestionChoice'
GO

ALTER TABLE [dbo].[WerpQuestionChoice]  WITH CHECK ADD  CONSTRAINT [FK_QuestionChoice_QuestionMaster] FOREIGN KEY([WQM_QuestionId])
REFERENCES [dbo].[WerpQuestionMaster] ([WQM_QuestionId])
GO

ALTER TABLE [dbo].[WerpQuestionChoice] CHECK CONSTRAINT [FK_QuestionChoice_QuestionMaster]
GO


