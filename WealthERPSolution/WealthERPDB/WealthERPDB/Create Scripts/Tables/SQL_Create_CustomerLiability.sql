
GO

/****** Object:  Table [dbo].[CustomerLiability]    Script Date: 06/11/2009 13:08:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CustomerLiability](
	[CLP_LiabilityId] [int] NULL,
	[CLP_CreatedBy] [int] NOT NULL,
	[CLP_CreatedOn] [datetime] NOT NULL,
	[CLP_ModifiedBy] [int] NOT NULL,
	[CLP_ModifiedOn] [datetime] NOT NULL
) ON [PRIMARY]

GO


