/****** Object:  Table [dbo].[WerpTradeDate]    Script Date: 06/12/2009 18:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WerpTradeDate](
	[WTD_TradingDayId] [int] IDENTITY(1,1) NOT NULL,
	[WTD_Date] [datetime] NULL,
	[WTD_Month] [int] NULL,
	[WTD_Year] [int] NULL,
	[WTD_DayOfMonth] [int] NULL,
	[WTD_DayOfYear] [int] NULL,
	[WTD_CreatedBy] [int] NULL,
	[WTD_CreatedOn] [datetime] NULL,
	[WTD_ModifiedBy] [int] NULL,
	[WTD_ModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_WerpTradeDate] PRIMARY KEY CLUSTERED 
(
	[WTD_TradingDayId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 