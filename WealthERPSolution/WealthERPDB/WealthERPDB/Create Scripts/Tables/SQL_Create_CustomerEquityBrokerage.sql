
GO

/****** Object:  Table [dbo].[CustomerEquityBrokerage]    Script Date: 06/11/2009 12:00:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerEquityBrokerage](
	[CEB_BrokerageId] [int] IDENTITY(1000,1) NOT NULL,
	[C_CustomerId] [int] NULL,
	[CEB_BuySell] [char](1) NULL,
	[CEB_Brokerage] [numeric](10, 5) NULL,
	[CEB_ServiceTax] [numeric](10, 5) NULL,
	[CEB_Clearing] [numeric](10, 5) NULL,
	[CEB_STT] [numeric](10, 4) NULL,
	[CEB_IsSpeculative] [tinyint] NULL,
	[CEB_Class] [char](1) NULL,
	[CEB_CalculationBasis] [char](1) NULL,
	[XB_BrokerCode] [varchar](5) NULL,
	[CEB_CreatedBy] [int] NULL,
	[CEB_CreatedOn] [datetime] NULL,
	[CEB_ModifiedOn] [datetime] NULL,
	[CEB_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_CustomerEquityBrokerage] PRIMARY KEY CLUSTERED 
(
	[CEB_BrokerageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomerEquityBrokerage]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerEquityBrokerage_Customer] FOREIGN KEY([C_CustomerId])
REFERENCES [dbo].[Customer] ([C_CustomerId])
GO

ALTER TABLE [dbo].[CustomerEquityBrokerage] CHECK CONSTRAINT [FK_CustomerEquityBrokerage_Customer]
GO

ALTER TABLE [dbo].[CustomerEquityBrokerage]  WITH CHECK ADD  CONSTRAINT [FK_CustomerEquityBrokerage_XMLBroker] FOREIGN KEY([XB_BrokerCode])
REFERENCES [dbo].[XMLBroker] ([XB_BrokerCode])
GO

ALTER TABLE [dbo].[CustomerEquityBrokerage] CHECK CONSTRAINT [FK_CustomerEquityBrokerage_XMLBroker]
GO


