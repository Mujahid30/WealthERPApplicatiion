
GO

/****** Object:  Table [dbo].[CustomerPortfolio]    Script Date: 06/11/2009 16:05:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerPortfolio](
	[CP_PortfolioId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NULL,
	[CP_PortfolioName] [varchar](50) NULL,
	[CP_IsMainPortfolio] [tinyint] NULL,
	[CP_IsPMS] [tinyint] NULL,
	[CP_PMSIdentifier] [varchar](20) NULL,
	[CP_CreatedBy] [int] NULL,
	[CP_CreatedOn] [datetime] NULL,
	[CP_ModifiedBy] [int] NULL,
	[CP_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_CustomerPortfolio] PRIMARY KEY CLUSTERED 
(
	[CP_PortfolioId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerPortfolio]  WITH CHECK ADD  CONSTRAINT [FK_CustomerPortfolio_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerPortfolio] CHECK CONSTRAINT [FK_CustomerPortfolio_Customer]
GO


