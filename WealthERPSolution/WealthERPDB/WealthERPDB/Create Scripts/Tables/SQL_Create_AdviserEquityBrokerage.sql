
GO

/****** Object:  Table [dbo].[AdviserEquityBrokerage]    Script Date: 06/11/2009 10:48:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdviserEquityBrokerage](
	[AEB_BrokerageId] [int] IDENTITY(1000,1) NOT NULL,
	[A_AdviserId] [int] NULL,
	[AEB_BuySell] [char](1) NULL,
	[AEB_Brokerage] [numeric](10, 5) NULL,
	[AEB_ServiceTax] [numeric](10, 5) NULL,
	[AEB_Clearing] [numeric](10, 5) NULL,
	[AEB_STT] [numeric](10, 4) NULL,
	[AEB_IsSpeculative] [tinyint] NULL,
	[AEB_Class] [char](1) NULL,
	[AEB_CalculationBasis] [char](1) NULL,
	[AEB_CreatedBy] [int] NULL,
	[AEB_CreatedOn] [datetime] NULL,
	[AEB_ModifiedOn] [datetime] NULL,
	[AEB_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_AdviserEquityBrokerage] PRIMARY KEY CLUSTERED 
(
	[AEB_BrokerageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AdviserEquityBrokerage]  WITH CHECK ADD  CONSTRAINT [FK_AdviserEquityBrokerage_Adviser] FOREIGN KEY([A_AdviserId])
REFERENCES [dbo].[Adviser] ([A_AdviserId])
GO

ALTER TABLE [dbo].[AdviserEquityBrokerage] CHECK CONSTRAINT [FK_AdviserEquityBrokerage_Adviser]
GO


