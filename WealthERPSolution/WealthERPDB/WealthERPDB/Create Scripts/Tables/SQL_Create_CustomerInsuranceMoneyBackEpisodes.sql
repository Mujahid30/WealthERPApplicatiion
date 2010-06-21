
GO

/****** Object:  Table [dbo].[CustomerInsuranceMoneyBackEpisodes]    Script Date: 06/11/2009 13:07:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes](
	[CIMBE_EpisodeId] [int] IDENTITY(1000,1) NOT NULL,
	[CINP_InsuranceNPId] [int] NULL,
	[CIMBE_RepaymentDate] [datetime] NULL,
	[CIMBE_RepaidPer] [numeric](5, 2) NULL,
	[CIMBE_CreatedOn] [datetime] NULL,
	[CIMBE_CreatedBy] [int] NULL,
	[CIMBE_ModifiedOn] [datetime] NULL,
	[CIMBE_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerInsuranceMoneyBackEpisodes] PRIMARY KEY CLUSTERED 
(
	[CIMBE_EpisodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes]  WITH CHECK ADD  CONSTRAINT [FK_CustomerInsuranceMoneyBackEpisodes_CustomerInsuranceNetPosition] FOREIGN KEY([CINP_InsuranceNPId])
REFERENCES [dbo].[CustomerInsuranceNetPosition] ([CINP_InsuranceNPId])
GO

ALTER TABLE [dbo].[CustomerInsuranceMoneyBackEpisodes] CHECK CONSTRAINT [FK_CustomerInsuranceMoneyBackEpisodes_CustomerInsuranceNetPosition]
GO


